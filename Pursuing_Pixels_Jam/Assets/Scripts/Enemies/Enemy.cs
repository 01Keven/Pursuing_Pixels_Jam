using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] protected int health = 100; // Exemplo de variável de saúde do inimigo
    [SerializeField] protected int damage = 10; // Exemplo de variável de dano do inimigo
    [SerializeField] protected float speed = 5f; // Exemplo de variável de velocidade do inimigo

    [Header("Referências")]
    protected Transform target; // Referência ao alvo que o inimigo deve seguir
    protected Animator anim; // Referência ao Animator do inimigo
    protected Rigidbody2D rb; // Referência ao Rigidbody2D do inimigo

    [Header("EnemyMovement")]
    protected NavMeshAgent agent; // Referência ao NavMeshAgent para movimentação baseada em navegação
    protected PlayerHealth player; // Referência ao script de saúde do jogador


    // Start é chamado antes do primeiro frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = FindFirstObjectByType<PlayerMovement>().transform; // Encontra o jogador na cena
        player = FindFirstObjectByType<PlayerHealth>(); // Encontra o script de saúde do jogador na cena
        agent = GetComponent<NavMeshAgent>(); // Obtém o componente NavMeshAgent do inimigo
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    protected virtual void FixedUpdate()
    {
        //FollowPlayer(); // Chama o método para seguir o jogador
    }

    // Update é chamado uma vez por frame
    protected virtual void Update()
    {


        //agent.SetDestination(player.transform.position);

        //if (agent.remainingDistance <= agent.stoppingDistance) // Verifica se o inimigo está próximo o suficiente do jogador
        //{
        //    // Aqui você pode adicionar lógica para atacar o jogador, como aplicar dano
        //    if (player != null)
        //    {
        //        player.ApplyDamage(damage); // Aplica dano ao jogador
        //    }
        //}

    }


    

    // Método para aplicar dano ao inimigo
    public virtual void ApplyDamage(int amount)
    {
        TakeDamage(amount);
        // Aqui você pode adicionar lógica para animações de dano, efeitos sonoros, etc.
    }

    // Método para receber dano
    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    // Método para seguir o jogador
    protected virtual void FollowPlayer()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            //anim.SetFloat("Speed", direction.magnitude); // Atualiza a animação com base na velocidade
        }
    }

    // Método para lidar com a morte do inimigo
    protected virtual void Die()
    {
        // Aqui você pode adicionar lógica para a morte do inimigo, como animações, efeitos sonoros, etc.
        Destroy(gameObject); // Exemplo simples de destruição do inimigo
    }

}
