using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoShowedButton : MonoBehaviour {

    public Sprite enabledSprite;
    public Sprite disabledSprite;

    private GameObject eventSystem;
    private GameManager gameManager;
    private bool status;

    void Awake() {
        eventSystem = GameObject.FindGameObjectWithTag("GameController");
        gameManager = eventSystem.GetComponent<GameManager>();
        status = gameManager.difficulty < 2;
        UpdateStatus();
    }

    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick() {
        if (gameManager.difficulty != 0)
            return;
        status = !status;
        UpdateStatus();
    }

    private void UpdateStatus() {
        if (status)
            gameObject.GetComponent<Image>().sprite = enabledSprite;
        else
            gameObject.GetComponent<Image>().sprite = disabledSprite;
        gameManager.isVideoShowed = status;
    }
}
