using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas playingCanvas;
    public Canvas pausingCanvas;
    public Canvas resultCanvas;

    public int score { get; set; }
    public float timer { get; set; }
    public bool isPaused { get; set; }
    public bool isVideoShowed { get; set; }
    public bool isSoundVisualize { get; set; }

    void Awake() {
        playingCanvas.gameObject.SetActive(true);
        pausingCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(false);
        isPaused = false;
    }

    void Update() {
        isPaused = (Time.timeScale == 0);
        if (!isPaused)
            timer -= Time.deltaTime;
    }

    public void GamePause() {
        Time.timeScale = 0;
        pausingCanvas.gameObject.SetActive(true);
    }

    public void GameResume() {
        Time.timeScale = 1;
        pausingCanvas.gameObject.SetActive(false);
    }

    public void GameOver() {
        resultCanvas.gameObject.SetActive(true);
        playingCanvas.gameObject.SetActive(false);
    }
}
