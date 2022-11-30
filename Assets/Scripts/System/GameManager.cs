using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Canvas playingCanvas;
    public Canvas pausingCanvas;
    public Canvas resultCanvas;

    public int difficulty { get; private set; }
    public int mode { get; private set; }
    public int score { get; set; }
    public float timer { get; set; }
    public bool isPaused { get; set; }
    public bool isVideoShowed { get; set; }
    public bool isSoundVisualize { get; set; }

    void Awake() {
        playingCanvas.gameObject.SetActive(true);
        pausingCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(false);
        difficulty = PlayerPrefs.GetInt("difficulty");
        mode = PlayerPrefs.GetInt("mode");
        isPaused = false;
    }

    void Start() {
        // difficulty = 0 時為練習模式，不扣時間
        if (difficulty == 0)
            timer = 0;
    }

    void Update() {
        isPaused = (Time.timeScale == 0);
        if (!isPaused && difficulty > 0)
            timer -= Time.deltaTime;
    }

    public void Pause() {
        Time.timeScale = 0;
        pausingCanvas.gameObject.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1;
        pausingCanvas.gameObject.SetActive(false);
    }

    public void GameOver() {
        resultCanvas.gameObject.SetActive(true);
        playingCanvas.gameObject.SetActive(false);
    }

    public void Title() {
        SceneManager.LoadScene("Title");
    }
}
