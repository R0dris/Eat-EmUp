using UnityEngine;
using UnityEngine.UI;

public class UIManager3 : MonoBehaviour
{
    public Slider playerHealthBar;
    public Image playerImage;

    public GameObject enemyUI;
    public Slider enemyHealtBar;
    public Image enemyImage;


    //Objeto para armazenar os dados do Player
    private PlayerController player;

    //Timer e controles do enemy ui
    [SerializeField] private float enemyUITime = 4f;
    private float enemyTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Obtem os dados do player
        player = FindFirstObjectByType<PlayerController>();

        //Define o valor máximo da barra de vida igual ao máximo da vida do player
        playerHealthBar.maxValue = player.maxHealth;

        //Inicia a health bar cheia
        playerHealthBar.value = playerHealthBar.maxValue;

        //Definir a imagem do player
        playerImage.sprite = player.playerImage;
    }

    // Update is called once per frame
    void Update()
    {
        enemyTimer += Time.deltaTime;

        if (enemyTimer >= enemyUITime)
        {
            enemyUI.SetActive(false);
            enemyTimer = 0;
        }
    }

    public void playerHealth(int amount)
    {
        playerHealthBar.value = amount;
    }

    public void updateEnemyUI(int maxHealth, int currentHealth, Sprite image)
    {
        //Atualiza os timer 
        enemyHealtBar.maxValue = maxHealth;
        enemyHealtBar.value = currentHealth;
        enemyImage.sprite = image;

        //Zera o timer para começar a contar 4 segundos
        enemyTimer = 0;

        //Habilita a enemyUI, deixando-a visível
        enemyUI.SetActive(true);
    }
}
