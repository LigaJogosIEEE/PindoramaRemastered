using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    //------ Variaveis relativas ao salto do personagem e verificações "se está no chao" ------
    [Range(1, 20)]
    public float jumpVelocity = 5f;
    [Range(1, 5)]
    public float fallMultiplier = 2.5f;
    [Range(1, 5)]
    public float lowJumpMultiplier = 3f;

    public float groundedSkin = 0.05f;
    public LayerMask collisionLayer;

    private bool jumpRequest = false;
    private bool grounded = true;

    private Vector2 playerSize;
    private Vector2 boxSize;

    //------ Variáveis relativas ao jogador correndo ------
    [Range(1, 20)]
    public float movementSpeed = 5f;

    private new Rigidbody2D rigidbody2D;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
    }
	
    public void Update() {
        //Controles
        Controls();
        //Ações relativas aos modos de jogo disponíveis
        ProcessGameMode();
    }

    private void ProcessGameMode() {
        //Se o modo de jogo é correndo
        if (GameManager.Get().gameMode == GameManager.GameMode.RUNNING) {
            //Jogo se configura para correr sem ação do player
            Run();
        }
    }

    private void Controls() {
        //Saltar, acho que comentar isso é demais já
        if (Input.GetButtonDown("Jump") && grounded) {
            jumpRequest = true;
        }
    }

    private void FixedUpdate() {
        //Coisas que envolvem física, pertencem ao physics update
        JumpAndGrounding();
        JumpingOptimizations();
    }

    //Método que faz o player saltar e verifica se ele está no chão
    private void JumpAndGrounding() {
        //Se pediu para saltar
        if (jumpRequest) {
            //Adiciona a força no boneco
            rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            //Seta variaveis para false para ele não sair voando feito um retardado
            jumpRequest = false;
            grounded = false;
        }
        else {
            //Calcula e coloca uma caixa em baixo do personagem
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            //Se a caixa tocar em alguma coisa na camada de colisão, digo que neste frame, ele está no chão, pas
            grounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, collisionLayer) != null;
        }
    }

    private void JumpingOptimizations() {
        //Se você está caindo
        if (rigidbody2D.velocity.y < 0) {
            //Deixa a gravidade mais forte para você cair mais rápido
            rigidbody2D.gravityScale = fallMultiplier;
        }
        //Se você está subindo e não está com o botão apertado [Enquanto vc apertar o botão, sua gravidade vai continuar linda e vc sobe mais]
        else if (rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump")) {
            //Vamos aumentar sua gravidade para que seu salto seja nerfado e vc voe menos
            rigidbody2D.gravityScale = lowJumpMultiplier;
        }
        //Se você nem cai nem sobe, ta paradex, sua escala de gravidade volta ao normal.
        else {
            rigidbody2D.gravityScale = 1;
        }
    }

    private void Run() {
        //Movimenta o jogador para a direita
        transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);
    }
}
