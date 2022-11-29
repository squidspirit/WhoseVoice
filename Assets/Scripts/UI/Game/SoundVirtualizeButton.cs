using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVirtualizeButton : MonoBehaviour {

    public Sprite enabledSprite;
    public Sprite disabledSprite;
    public bool initState;

    private GameObject eventSystem;
    private GameManager gameManager;
    private bool status;

    void Awake() {
        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            gameObject.SetActive(false);
            return;
        }
        eventSystem = GameObject.FindGameObjectWithTag("GameController");
        gameManager = eventSystem.GetComponent<GameManager>();
        status = !initState;
        onClick();
    }

    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(onClick);
    }

    private void onClick() {
        status = !status;
        if (status)
            gameObject.GetComponent<Image>().sprite = enabledSprite;
        else
            gameObject.GetComponent<Image>().sprite = disabledSprite;
        gameManager.isSoundVisualize = status;
    }
}
