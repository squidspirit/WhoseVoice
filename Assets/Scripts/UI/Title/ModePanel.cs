using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModePanel : MonoBehaviour {

    public Button leftButton;
    public Button rightButton;
    public TextMeshProUGUI selectedText;
    public int initSelected;
    public string[] selections;

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
        PlayerPrefs.SetInt("mode", selected);
    }
}
