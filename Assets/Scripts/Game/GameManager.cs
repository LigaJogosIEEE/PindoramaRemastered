using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum GameMode {
        RUNNING, BOSS
    }

    [Tooltip("Modo de Jogo atual, Correndo ou no Boss")]
    public GameMode gameMode;

    public static GameManager Get() {
        return Instance;
    }

    private static GameManager Instance;

    private void Awake() {
        if (Instance == null)
            Instance = this;
    }

}
