using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCanvas : MonoBehaviour {

    public Button playButton;
    public Button quitButton;

    private GameObject eventSystem;
    private TitleManager titleManager;

    void Awake() {
        eventSystem = GameObject.FindGameObjectWithTag("GameController");
        titleManager = eventSystem.GetComponent<TitleManager>();
    }

    void Start() {
        playButton.onClick.AddListener(titleManager.Option);
        quitButton.onClick.AddListener(titleManager.Quit);
    }
}
