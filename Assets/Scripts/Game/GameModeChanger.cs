using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeChanger : MonoBehaviour {
    public GameManager.GameMode gameMode = GameManager.GameMode.BOSS;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            GameManager.Get().gameMode = gameMode;
        }
    }
}
