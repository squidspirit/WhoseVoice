using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPanel : MonoBehaviour {

    public string rootPath;

    private RawImage rawImage;
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    private SoundVisualizer soundVisualizer;

    public bool isVideoShowed { get; set; }
    public bool isSoundVisualize { get; set; }

    void Awake() {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        soundVisualizer = gameObject.GetComponentInChildren<SoundVisualizer>();
        soundVisualizer.audioSource = audioSource;
        videoPlayer = gameObject.GetComponentInChildren<VideoPlayer>();
        videoPlayer.source = VideoSource.Url;
        videoPlayer.targetTexture = CreateRenderTexture();
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.isLooping = true;
        videoPlayer.playOnAwake = false;
        rawImage = GetComponentInChildren<RawImage>();
        rawImage.texture = videoPlayer.targetTexture;
    }

    void Update() {
        if (!rootPath.EndsWith("/"))
            rootPath += "/";

        if (isSoundVisualize)
            soundVisualizer.Show();
        else
            soundVisualizer.Hide();

        if (isVideoShowed)
            rawImage.color = new Color(1f, 1f, 1f, 1f);
        else
            rawImage.color = new Color(1f, 1f, 1f, 0f);
    }

    private RenderTexture CreateRenderTexture() {
        RenderTexture renderTexture = new RenderTexture(1280, 720, 32, RenderTextureFormat.ARGB32);
        renderTexture.Create();
        return renderTexture;
    }

    public void PlayClip(string clipName) {
        videoPlayer.url = Application.streamingAssetsPath + "/" + rootPath + clipName + ".mov";
        videoPlayer.Prepare();
        if (!videoPlayer.isPlaying) {
            videoPlayer.Play();
        }
    }

    public void StopClip() {
        videoPlayer.Stop();
    }

    public void PauseClip() {
        if (videoPlayer.isPlaying) {
            videoPlayer.Pause();
        }
    }

    public void ResumeClip() {
        if (videoPlayer.isPaused) {
            videoPlayer.Play();
        }
    }
}
