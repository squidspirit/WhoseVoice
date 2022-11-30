using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausingCanvas : MonoBehaviour
{
    public Button resumeButton;
    public Button quitButton;

    private GameObject eventSystem;
    private GameManager gameManager;

    void Awake() {
        eventSystem = GameObject.FindGameObjectWithTag("GameController");
        gameManager = eventSystem.GetComponent<GameManager>();
    }

    void Start() {
        resumeButton.onClick.AddListener(gameManager.Resume);
        quitButton.onClick.AddListener(gameManager.Title);
    }
}
