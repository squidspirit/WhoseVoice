using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SoundVisualizer : MonoBehaviour {

    private const int SAMPLE_SIZE = 2048;

    public int barCount;
    public Color barColor;
    public float barWidth;
    public float initBarHeight;
    [Range(0f, 1f)]
    public float release;

    private float parentWidth;
    private float parentHeight;
    private float spacing;
    private List<GameObject> bars = new();

    private float[] samples;
    private float[] spectrums;
    private float[] visualizedData;
    private float[] lastData;
    private float bandsIncRate;
    private float dbVal;

    public AudioSource audioSource { get; set; }

    void Awake() {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            return;

        parentWidth = gameObject.GetComponent<RectTransform>().rect.width;
        parentHeight = gameObject.GetComponent<RectTransform>().rect.height;
        spacing = (parentWidth - barCount * barWidth) / (barCount + 1);

        for (int i = 0; i < barCount; i ++) {
            GameObject bar = new GameObject(string.Format("Bar {0}", i));
            RectTransform barRectTransform = bar.AddComponent<RectTransform>();
            barRectTransform.SetParent(gameObject.transform);
            barRectTransform.sizeDelta = new Vector2(barWidth, initBarHeight);
            barRectTransform.pivot = new Vector2(0.5f, 0.5f);
            barRectTransform.anchorMin = new Vector2(0f, 0.5f);
            barRectTransform.anchorMax = new Vector2(0f, 0.5f);
            barRectTransform.anchoredPosition = new Vector2(
                (float)((i + 1) * spacing + (2 * i + 1) * barWidth / 2), 0f);
            Vector3 pos = bar.transform.position;
            bar.transform.position = new Vector3(pos.x, pos.y, 0f);
            bar.transform.localScale = new Vector3(1f, 1f, 1f);
            Image image = bar.AddComponent<Image>();
            image.color = barColor;
            image.transform.SetParent(bar.transform);
            bars.Add(bar);
        }

        samples = new float[SAMPLE_SIZE];
        spectrums = new float[SAMPLE_SIZE];
        visualizedData = new float[barCount];
        lastData = new float[barCount];
        bandsIncRate = Mathf.Pow(SAMPLE_SIZE, 0.5f / barCount);
    }

    void Update() {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            return;

        Debug.Log(bars[0].GetComponent<RectTransform>().rect.width);
        AnalyzeSound();
        Normalize();
        for (int i = 0; i < barCount; i ++) {
            float rate = Mathf.Pow(release, Time.deltaTime * 10f);
            if (visualizedData[i] < lastData[i])
                visualizedData[i] = lastData[i] * rate + visualizedData[i] * (1 - rate);
            RectTransform rectTransform = bars[i].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(
                rectTransform.sizeDelta.x,
                initBarHeight + visualizedData[i] * (parentHeight - initBarHeight) * 0.8f
            );
        }
        lastData = visualizedData.Clone() as float[];
    }

    private void AnalyzeSound() {
        audioSource.GetOutputData(samples, 0);
        audioSource.GetSpectrumData(spectrums, 0, FFTWindow.BlackmanHarris);
        float rms = 0f;
        foreach (float val in samples)
            rms += val * val;
        rms = Mathf.Sqrt(rms / samples.Length);
        dbVal = rms != 0 ? Mathf.Log10(rms) : 0;
        if (dbVal >= 0f) return;
        float avg = 0f;
        for (int i = 0, count = 0; i < visualizedData.Length; i++) {
            int sampleCount = (int)Mathf.Pow(bandsIncRate, i);
            for (int j = 0; j < sampleCount; j ++)
                avg += spectrums[count < spectrums.Length ? count ++ : count - 1];
            avg /= sampleCount;
            visualizedData[i] = avg;
        }
    }

    private void Normalize() {
        if (dbVal >= 0f) return;
        float avg = 0f, sd = 0f, max = 0f, min = 0f;
        foreach (float val in visualizedData)
            avg += val;
        avg /= visualizedData.Length;
        foreach (float val in visualizedData)
            sd += val * val;
        sd = Mathf.Sqrt(sd / visualizedData.Length - avg * avg);
        for (int i = 0; i < visualizedData.Length; i++)
            visualizedData[i] = (visualizedData[i] - avg) / sd / 2f + 0.5f;
        foreach (float val in visualizedData) {
            max = Mathf.Max(val, max);
            min = Mathf.Min(val, min);
        }
        for (int i = 0; i < visualizedData.Length; i++)
            visualizedData[i] = (visualizedData[i] - min) / (max - min) / Mathf.Abs(dbVal);
    }

    private void SetBarsAlpha(float alpha) {
        for (int i = 0; i < bars.Count; i++) {
            Image image = bars[i].GetComponent<Image>();
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    public void Hide() {
        SetBarsAlpha(0f);
    }

    public void Show() {
        SetBarsAlpha(1f);
    }
}
