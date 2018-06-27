using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncreaser : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SetSpeed(8f);
        }
    }
}
