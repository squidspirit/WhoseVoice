using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCanvas : MonoBehaviour {

    public Button backButton;
    public Button playButton;

    private GameObject eventSystem;
    private TitleManager titleManager;

    void Awake() {
        eventSystem = GameObject.FindGameObjectWithTag("GameController");
        titleManager = eventSystem.GetComponent<TitleManager>();
    }

    void Start() {
        backButton.onClick.AddListener(titleManager.Title);
        playButton.onClick.AddListener(titleManager.Play);
    }
}
