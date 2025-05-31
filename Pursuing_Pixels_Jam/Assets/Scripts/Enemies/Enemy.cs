using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] protected int health = 100; // Exemplo de vari�vel de sa�de do inimigo
    [SerializeField] protected int damage = 10; // Exemplo de vari�vel de dano do inimigo
    [SerializeField] protected float speed = 5f; // Exemplo de vari�vel de velocidade do inimigo

    [Header("Refer�ncias")]
    protected Transform target; // Refer�ncia ao alvo que o inimigo deve seguir
    protected Animator anim; // Refer�ncia ao Animator do inimigo
    protected Rigidbody2D rb; // Refer�ncia ao Rigidbody2D do inimigo

    [Header("EnemyMovement")]
    protected NavMeshAgent agent; // Refer�ncia ao NavMeshAgent para movimenta��o baseada em navega��o
    protected PlayerHealth player; // Refer�ncia ao script de sa�de do jogador


    // Start � chamado antes do primeiro frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = FindFirstObjectByType<PlayerMovement>().transform; // Encontra o jogador na cena
        player = FindFirstObjectByType<PlayerHealth>(); // Encontra o script de sa�de do jogador na cena
        agent = GetComponent<NavMeshAgent>(); // Obt�m o componente NavMeshAgent do inimigo
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    protected virtual void FixedUpdate()
    {
        //FollowPlayer(); // Chama o m�todo para seguir o jogador
    }

    // Update � chamado uma vez por frame
    protected virtual void Update()
    {


        //agent.SetDestination(player.transform.position);

        //if (agent.remainingDistance <= agent.stoppingDistance) // Verifica se o inimigo est� pr�ximo o suficiente do jogador
        //{
        //    // Aqui voc� pode adicionar l�gica para atacar o jogador, como aplicar dano
        //    if (player != null)
        //    {
        //        player.ApplyDamage(damage); // Aplica dano ao jogador
        //    }
        //}

    }


    

    // M�todo para aplicar dano ao inimigo
    public virtual void ApplyDamage(int amount)
    {
        TakeDamage(amount);
        // Aqui voc� pode adicionar l�gica para anima��es de dano, efeitos sonoros, etc.
    }

    // M�todo para receber dano
    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    // M�todo para seguir o jogador
    protected virtual void FollowPlayer()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            //anim.SetFloat("Speed", direction.magnitude); // Atualiza a anima��o com base na velocidade
        }
    }

    // M�todo para lidar com a morte do inimigo
    protected virtual void Die()
    {
        // Aqui voc� pode adicionar l�gica para a morte do inimigo, como anima��es, efeitos sonoros, etc.
        Destroy(gameObject); // Exemplo simples de destrui��o do inimigo
    }

}
