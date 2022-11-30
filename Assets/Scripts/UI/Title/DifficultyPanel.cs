using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyPanel : MonoBehaviour {

    public Button leftButton;
    public Button rightButton;
    public TextMeshProUGUI selectedText;
    public int initSelected;
    public string[] selections;
    public int[] correctScore;
    public int[] wrongTime;

    private int selected;

    void Awake() {
        selected = initSelected;
    }

    void Start() {
        leftButton.onClick.AddListener(SelectLeft);
        rightButton.onClick.AddListener(SelectRight);
        UpdateText();
    }

    private void SelectLeft() {
        if (selected == 0)
            selected = selections.Length;
        selected --;
        UpdateText();
    }

    private void SelectRight() {
        selected = (selected + 1) % selections.Length;
        UpdateText();
    }

    private void UpdateText() {
        selectedText.SetText(selections[selected]);
        PlayerPrefs.SetInt("difficulty", selected);
        PlayerPrefs.SetInt("correctScore", correctScore[selected]);
        PlayerPrefs.SetInt("wrongTime", wrongTime[selected]);
    }
}
