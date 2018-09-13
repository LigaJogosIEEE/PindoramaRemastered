using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemy : MonoBehaviour {

    //------ Variaveis relacionadas ao salto do personagem e verificações "se está no chao" ------
    [Range(1, 20)]
    public float jumpVelocity = 6f;
    [Range(1, 5)]
    public float fallMultiplier = 2.5f;
    [Range(1, 5)]
    public float lowJumpMultiplier = 3f;

    public float groundedSkin = 0.05f;
    public LayerMask collisionLayer;

    public bool grounded = true;
    public bool jump = false;

    //private Vector2 playerSize;
    //private Vector2 boxSize;

    private Animator anim;

    public Transform groundCheck;


    //------ Variáveis relacionadas ao jogador correndo ------
    [Range(1, 20)]
    public float movementSpeed = 5f;

    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        //playerSize = GetComponent<BoxCollider2D>().size;
        //boxSize = new Vector2(playerSize.x, groundedSkin);
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")).transform;

        anim.SetBool("Grounded", grounded);

        if (grounded)
        {
            anim.SetBool("Jump", false);
        }
        else
            anim.SetBool("Jump", true);

        //Controles
        Controls();
        //Ações relacionadas aos modos de jogo disponíveis
        ProcessGameMode();
    }

    private void ProcessGameMode()
    {
        //Se o modo de jogo é correndo
        if (GameManager.Get().gameMode == GameManager.GameMode.RUNNING)
        {
            //Jogo se configura para correr sem ação do player
            Run();
        }
    }

    private void Controls()
    {
        //Saltar, acho que comentar isso é demais já
        if (jump)
        {
            //anim.SetBool("Jump", true);
            rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            //rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        }
    }

    public void Jump()
    {
        if (grounded)
            jump = true;
    }

    private void FixedUpdate()
    {
        /*if (jump) {
            anim.SetBool("Jump", true);
            rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            //jump = false;
        }*/
        //Coisas que envolvem física, pertencem ao physics update
        //JumpAndGrounding();
        JumpingOptimizations();

    }

    //Método que faz o player saltar e verifica se ele está no chão
    private void JumpAndGrounding()
    {
        //Se pediu para saltar
        //Calcula e coloca uma caixa em baixo do personagem
        //Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
        //Se a caixa tocar em alguma coisa na camada de colisão, digo que neste frame, ele está no chão, pas
        //grounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, collisionLayer) != null;
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")).transform;

    }

    private void JumpingOptimizations()
    {
        //Se você está caindo
        if (rigidbody2D.velocity.y < 0)
        {
            //Deixa a gravidade mais forte para você cair mais rápido
            rigidbody2D.gravityScale = fallMultiplier;
        }
        //Se você está subindo e não está com o botão apertado [Enquanto vc apertar o botão, sua gravidade vai continuar linda e vc sobe mais]
        else if (rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            //Vamos aumentar sua gravidade para que seu salto seja nerfado e vc voe menos
            rigidbody2D.gravityScale = lowJumpMultiplier;
        }
        //Se você nem cai nem sobe, ta paradex, sua escala de gravidade volta ao normal.
        else
        {
            rigidbody2D.gravityScale = 1;
        }
    }

    private void Run()
    {
        //Movimenta o jogador para a direita
        transform.Translate(Vector2.right * Time.deltaTime * movementSpeed);
    }

    public void SetSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    /*
     * Mata o personagem e encerra a fase.
     */
    /*public void Death()
    {
        //Verifica se o personagem está vivo.
        if (isAlive)
        {
            isAlive = false; //Declara a hora da morte.
            jump = false;
            anim.SetBool("Jump", false);
            anim.SetBool("Dead", true);
            movementSpeed = 0f; //Zera a velocidade, pro personagem não se mover mais pra frente.
            //"Empurra" o personagem para cima, criando uma animação legalzinha.
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            GetComponent<BoxCollider2D>().enabled = false; //Desabilita o colisor pro personagem atravessar o chão.
            Invoke("GameOver", 1f);
        }
    }

    public void GameOver()
    {
        LevelManager.levelManager.GameOver();
    }*/
}
