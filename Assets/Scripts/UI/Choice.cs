using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour {

    public Color correctColor;
    public Color wrongColor;

    private GameObject controller;

    public bool isCorrect { get; set; }

    void Awake() {
        isCorrect = false;
        controller = GameObject.Find("Playing");
    }

    public void Clicked() {
        if (!isCorrect)
            gameObject.GetComponent<Image>().color = wrongColor;
        controller.GetComponent<Playing>().ChoiceClicked(this);
    }
}
