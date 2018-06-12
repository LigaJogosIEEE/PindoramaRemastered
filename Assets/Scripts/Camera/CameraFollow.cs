using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float xDelta = 2.0f;
    public float yDelta = 0.5f;

    private void LateUpdate() {
        if (target) {
            Vector3 tPosition = target.position + new Vector3(xDelta, yDelta, -10f);
            tPosition.y = 3;
            transform.position = tPosition;
        }
    }
}
