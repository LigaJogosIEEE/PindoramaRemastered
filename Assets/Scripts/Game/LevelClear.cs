using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClear : MonoBehaviour {

    public static LevelClear levelClear;

    private int nextStage = 1;

    // Use this for initialization
    void Awake()
    {
        if (levelClear == null) {
            levelClear = this;
        }
        else if (levelClear != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UnlockStage(int stage)
    {
        if (stage > nextStage)
        {
            nextStage = stage;
        }
    }

    public int GetNextStage()
    {
        return nextStage;
    }
}
