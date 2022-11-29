using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour {

    public Canvas titleCanvas;
    public Canvas optionCanvas;

    public Button optionButton;
    public Button quitButton;
    public Button titleButton;

    void Start() {
        optionButton.onClick.AddListener(() => {
            optionCanvas.gameObject.SetActive(true);
            titleCanvas.gameObject.SetActive(false);
        });

        titleButton.onClick.AddListener(() => {
            titleCanvas.gameObject.SetActive(true);
            optionCanvas.gameObject.SetActive(false);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
