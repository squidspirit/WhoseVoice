using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    public TextMeshProUGUI scoreNumberText;

    private GameObject eventSystem;
    private GameManager gameManager;

    void Awake() {
        eventSystem = GameObject.FindGameObjectWithTag("GameController");
        gameManager = eventSystem.GetComponent<GameManager>();
    }

    void Update() {
        scoreNumberText.SetText(gameManager.score.ToString());
    }
}
