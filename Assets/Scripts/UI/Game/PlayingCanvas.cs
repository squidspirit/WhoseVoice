using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCanvas : MonoBehaviour {

    private const float COLOR_FADE_RATE = 0.2f;

    public Button pauseButton;
    public TextMeshProUGUI timerNumberText;
    public TextMeshProUGUI scoreNumberText;
    public GameObject videoPanel;
    public GameObject choicePanel;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public Color correctColor;
    public Color wrongColor;
    public int correctScore;
    public int wrongTime;
    public int choicesCount;
    public float initTime;
    public int initScore;

    private GameObject eventSystem;
    private GameManager gameManager;
    private JsonManager jsonManager;
    private SoundManager soundManager;

    void Awake() {
        eventSystem = GameObject.FindGameObjectWithTag("GameController");
        gameManager = eventSystem.GetComponent<GameManager>();
        jsonManager = eventSystem.GetComponent<JsonManager>();
        soundManager = eventSystem.GetComponent<SoundManager>();
        gameManager.timer = initTime;
        gameManager.score = initScore;
        if (choicesCount > 4) choicesCount = 4;
        Time.timeScale = 1;
    }

    void Start() {
        pauseButton.onClick.AddListener(gameManager.Pause);
        UpdateText();
        GenerateQuestion();
    }

    void Update() {
        UpdateText();
        videoPanel.GetComponent<VideoPanel>().isVideoShowed = gameManager.isVideoShowed;
        videoPanel.GetComponent<VideoPanel>().isSoundVisualize = gameManager.isSoundVisualize;
        if (Time.timeScale == 0) {
            gameManager.Pause();
            videoPanel.GetComponent<VideoPanel>().PauseClip();
        }
        else {
            gameManager.Resume();
            videoPanel.GetComponent<VideoPanel>().ResumeClip();
        }
        if (gameManager.timer < 0) {
            gameManager.GameOver();
        }
    }

    private Color ColorFade(Color original, Color goal, float rate) {
        return original * rate + goal * (1f - rate);
    }

    private void UpdateText() {
        float rate = Mathf.Pow(COLOR_FADE_RATE, Time.deltaTime);
        timerNumberText.SetText(((int)Mathf.Ceil(gameManager.timer)).ToString());
        timerNumberText.color = ColorFade(timerNumberText.color, Color.white, rate);
        scoreNumberText.SetText(gameManager.score.ToString());
        scoreNumberText.color = ColorFade(scoreNumberText.color, Color.white, rate);
    }

    public void GenerateQuestion() {
        ClipAttribute clipData = jsonManager.GetRandomClipData();
        List<string> choices = new List<string>();
        foreach (var cv in jsonManager.GetRandomCVList(clipData.cv, choicesCount))
            choices.Add(cv.GetName());
        int correctIndex = Random.Range(0, choicesCount);
        choices[correctIndex] = clipData.cv.GetName();
        choicePanel.GetComponent<ChoicePanel>().Generate(choices, correctIndex);
        videoPanel.GetComponent<VideoPanel>().PlayClip(string.Format("{0:0000}", clipData.clip.GetId()));
    }

    public void ChoiceClicked(ChoiceButton choice) {
        if (choice.isCorrect) {
            soundManager.PlayClip(correctSound, 0.4f);
            gameManager.score += correctScore;
            scoreNumberText.color = correctColor;
            GenerateQuestion();
        }
        else {
            soundManager.PlayClip(wrongSound, 0.4f);
            gameManager.timer -= wrongTime;
            timerNumberText.color = wrongColor;
        }
        UpdateText();
    }
}
