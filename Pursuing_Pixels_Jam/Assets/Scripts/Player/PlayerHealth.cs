using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f; // sa�de m�xima do jogador
    float health = 100f; // sa�de inicial do jogador

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        TakeDamage(damage); // chama o m�todo TakeDamage para aplicar o dano
        // Aqui voc� pode adicionar l�gica adicional, como atualizar a UI de sa�de do jogador

    }

    void TakeDamage(float damage)
    {
        health -= damage; // reduz a sa�de do jogador pelo dano recebido
        if (health <= 0f)
        {
            Die(); // chama o m�todo Die se a sa�de chegar a zero ou menos
        }
    }

    void Die()
    {
        // L�gica para lidar com a morte do jogador, como reiniciar o n�vel ou exibir uma tela de game over
        Debug.Log("Player has died.");
        // Aqui voc� pode adicionar mais l�gica, como reiniciar o jogo ou carregar uma cena de game over
        Destroy(gameObject); // destr�i o objeto do jogador
    }

}
