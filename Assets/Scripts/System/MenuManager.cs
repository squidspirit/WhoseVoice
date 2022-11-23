using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadScence(string scenceName) {
        SceneManager.LoadScene(scenceName);
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Resume() {
        Time.timeScale = 1;
    }
}
