using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {

    public Canvas titleCanvas;
    public Canvas optionCanvas;

    void Awake() {
        titleCanvas.gameObject.SetActive(true);
        optionCanvas.gameObject.SetActive(false);
    }

    public void Option() {
        titleCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }

    public void Title() {
        titleCanvas.gameObject.SetActive(true);
        optionCanvas.gameObject.SetActive(false);
    }

    public void Play() {
        SceneManager.LoadScene("Game");
    }

    public void Quit() {
        Application.Quit();
    }
}
