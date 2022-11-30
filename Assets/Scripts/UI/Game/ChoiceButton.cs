using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour {

    public Color correctColor;
    public Color wrongColor;

    private GameObject controller;

    public bool isCorrect { get; set; }

    void Awake() {
        isCorrect = false;
        controller = GameObject.Find("Playing");
    }

    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(() => {
            if (!isCorrect)
                gameObject.GetComponent<Image>().color = wrongColor;
            controller.GetComponent<PlayingCanvas>().ChoiceClicked(this);
        });
    }
}
