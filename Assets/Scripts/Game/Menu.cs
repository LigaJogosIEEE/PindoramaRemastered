using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void LoadStage(int stage) {
        if (stage <= LevelClear.levelClear.GetNextStage()) {
            SceneManager.LoadScene(stage);
        }
        else {
            return;
        }
    }
}
