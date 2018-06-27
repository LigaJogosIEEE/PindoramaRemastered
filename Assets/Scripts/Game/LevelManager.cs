using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager levelManager;

    private int collectables = 0;
    private bool gameOver = false;

    public Text collectablesText;

    public GameObject gameOverText;

    private bool passed = false;
    public GameObject passedText;
    
    public int unlockStage;

    // Use this for initialization
    void Awake()
    {
        if (levelManager == null) {
            levelManager = this;
        }
        else if (levelManager != this) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        if (gameOver && Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (passed && Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("Menu");
        }
    }

    public void SetCollectables() {
        collectables++;
        collectablesText.text = collectables.ToString("00");
    }

    public int GetCollectables() {
        return collectables;
    }

    public void ResetCollectables()
    {
        collectables = 0;
        collectablesText.text = collectables.ToString("00");
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.SetActive(true);
    }

    public void Passed()
    {
        passed = true;
        LevelClear.levelClear.UnlockStage(unlockStage);
        passedText.SetActive(true);
    }
}
