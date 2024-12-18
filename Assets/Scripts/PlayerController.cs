using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;
    public float currentSpeed;
    public Vector2 playerDirection;
    private bool isWalking;
    private Animator playerAnimator;



    //Player olhando para direita
    private bool playerFaceRight = true;

    //Variável Contadora
    private int punchCount;

    private float timeCross = 0.75f;

    private bool comboControl;

    private bool playerIsDead;

    //Propriedades para UI
    public int maxHealth = 10;
    public int currentHealth;
    public Sprite playerImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Obtem e inicializa as propriedades do RigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        //Obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>();

        currentHealth = maxHealth;
        FindFirstObjectByType<UIManager3>().playerHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        UpdateAnimator();


        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isWalking == false)
            {

                if (punchCount < 2)
                {
                    PlayerJab();
                    punchCount++;
                    if (!comboControl)
                    {
                        //Iniciar o temporizador
                        StartCoroutine(CrossController());
                    }
                }
                else if (punchCount >= 2)
                {
                    PlayerCross();
                    punchCount = 0;
                }
                //Parando o temporizador
                StopCoroutine(CrossController());
            }
        }
    }

    private void FixedUpdate()
    {
        //Verificar se o Player está em movimento
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);
    }

    void PlayerMove()
    {
        //Pega a entrada do jogador, e cria um Vector2 para usar no player Direction
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Se o player vai para a ESQUERDA e está olhando para a DIREITA
        if (playerDirection.x < 0 && playerFaceRight)
        {
            Flip();
        }

        else if (playerDirection.x > 0 && !playerFaceRight)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        playerAnimator.SetBool("isWalking"/*Declarado no animator*/, isWalking/*Declarado no VC*/);
    }

    void Flip()
    {
        //Vai girar o sprite player em 180° no eixo Y
        //Inverter o valor da variável
        playerFaceRight = !playerFaceRight;

        //Girar o sprite do player em 180° no eixo Y
        //               x,  y , z
        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        //Acessa animação do Jab
        //Ativa o gatilho de Ataque;
        playerAnimator.SetTrigger("isJabbing");
    }

    void PlayerCross()
    {
        //Acessa animação do Cross
        //Ativa o gatilho do Ataque;
        playerAnimator.SetTrigger("isCrossing");
    }

    IEnumerator CrossController()
    {
        comboControl = true;

        yield return new WaitForSeconds(timeCross);
        punchCount = 0;

        comboControl = false;
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void resetSpeed()
    {
        currentSpeed = playerSpeed;
    }

    public void takeDamage(int damage)
    {
        if (!playerIsDead)
        {
            currentHealth -= damage;
            playerAnimator.SetTrigger("damage");
            FindFirstObjectByType<UIManager3>().playerHealth(currentHealth);

            if (currentHealth <= 0)
            {
                playerIsDead = true;

                ZeroSpeed();

                playerAnimator.SetTrigger("death");

                SceneManager.LoadScene("Gameover");
            }
        }
    }

    public void disablePlayer()
    {
        this.gameObject.SetActive(false);
    }
}


//SceneManager.LoadScene()