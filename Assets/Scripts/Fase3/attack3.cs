using UnityEngine;

public class attack3 : MonoBehaviour
{

    public int damage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ao colidir, salva na variável enemy que foi colidido
        enemyController3 enemy = collision.GetComponent<enemyController3>();

        //Ao colidir, salva na variável player que foi colidido
        PlayerController player = collision.GetComponent<PlayerController>();

        //Se a colisaão foi com o inimigo
        if (enemy != null)
        {
            //Inimigo recebe dano
            enemy.TakeDamage(damage);
        }

        if (player != null)
        {
            //Inimigo recebe dano
            player.takeDamage(damage);


        }

    }
}
