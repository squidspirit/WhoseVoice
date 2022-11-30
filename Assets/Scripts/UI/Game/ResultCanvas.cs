using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultCanvas : MonoBehaviour
{
    public TextMeshProUGUI scoreNumberText;
    public Button quitButton;

    private GameObject eventSystem;
    private GameManager gameManager;

    void Awake() {
        eventSystem = GameObject.FindGameObjectWithTag("GameController");
        gameManager = eventSystem.GetComponent<GameManager>();
    }

    void Start() {
        quitButton.onClick.AddListener(gameManager.Title);
        scoreNumberText.SetText(gameManager.score.ToString());
    }
}
