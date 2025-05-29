using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f; // saúde máxima do jogador
    float health = 100f; // saúde inicial do jogador

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
        TakeDamage(damage); // chama o método TakeDamage para aplicar o dano
        // Aqui você pode adicionar lógica adicional, como atualizar a UI de saúde do jogador

    }

    void TakeDamage(float damage)
    {
        health -= damage; // reduz a saúde do jogador pelo dano recebido
        if (health <= 0f)
        {
            Die(); // chama o método Die se a saúde chegar a zero ou menos
        }
    }

    void Die()
    {
        // Lógica para lidar com a morte do jogador, como reiniciar o nível ou exibir uma tela de game over
        Debug.Log("Player has died.");
        // Aqui você pode adicionar mais lógica, como reiniciar o jogo ou carregar uma cena de game over
        Destroy(gameObject); // destrói o objeto do jogador
    }

}
