using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    //------ Variaveis relativas ao salto do personagem -----
    [Range(1, 20)]
    public float jumpVelocity = 5f;
    [Range(1, 5)]
    public float fallMultiplier = 2.5f;
    [Range(1, 5)]
    public float lowJumpMultiplier = 2f;

    public float groundedSkin = 0.05f;
    public LayerMask collisionLayer;

    private bool jumpRequest = false;
    private bool grounded = true;

    private Vector2 playerSize;
    private Vector2 boxSize;

    private new Rigidbody2D rigidbody2D;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
    }
	
    public void Update () {
	    if (Input.GetButtonDown("Jump") && grounded) {
            jumpRequest = true;
        }
    }

    private void FixedUpdate() {
        if (jumpRequest) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        } else {
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            grounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, collisionLayer) != null;
        }

        JumpingOptimizations();
    }

    private void JumpingOptimizations() {
        if (rigidbody2D.velocity.y < 0) {
            rigidbody2D.gravityScale = fallMultiplier;
        }
        else if (rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump")) {
            rigidbody2D.gravityScale = lowJumpMultiplier;
        }
        else {
            rigidbody2D.gravityScale = 1;
        }
    }
}
