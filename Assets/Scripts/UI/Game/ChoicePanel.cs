using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoicePanel : MonoBehaviour {

    public GameObject choiceButtonPrefab;

    private List<GameObject> choiceButtons = new();

    public void Generate(List<string> choices, int correctIndex) {
        Clear();

        int choiceNumber = choices.Count;
        float choicePanelHeight = Mathf.Abs(gameObject.GetComponent<RectTransform>().sizeDelta.y);
        float currentY = choicePanelHeight / 2;

        for (int i = 0; i < choiceNumber; i ++) {
            GameObject choiceButton = Instantiate(choiceButtonPrefab);
            choiceButtons.Add(choiceButton);
            choiceButton.transform.SetParent(gameObject.transform);
            currentY -= choicePanelHeight / (choiceNumber + 1);
            choiceButton.transform.localScale = new Vector3(1f, 1f, 1f);
            choiceButton.transform.localPosition = new Vector3(0f, currentY, 0f);
            choiceButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(choices[i]);
            choiceButton.GetComponent<Choice>().isCorrect = (i == correctIndex);
        }
    }

    public void Clear() {
        while (choiceButtons.Count > 0) {
            GameObject choiceButton = choiceButtons[0];
            choiceButtons.RemoveAt(0);
            Destroy(choiceButton);
        }
    }
}
