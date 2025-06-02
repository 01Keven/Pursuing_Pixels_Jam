using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] protected int health = 100; // Exemplo de vari�vel de sa�de do inimigo
    [SerializeField] protected int damage = 10; // Exemplo de vari�vel de dano do inimigo
    [SerializeField] protected float speed = 5f; // Exemplo de vari�vel de velocidade do inimigo
    [SerializeField] protected bool isDeath = false; // Dist�ncia de ataque do inimigo

    [Header("Refer�ncias")]
    protected Transform target; // Refer�ncia ao alvo que o inimigo deve seguir
    [SerializeField] protected Animator anim; // Refer�ncia ao Animator do inimigo
    protected Rigidbody2D rb; // Refer�ncia ao Rigidbody2D do inimigo

    [Header("EnemyMovement")]
    protected NavMeshAgent agent; // Refer�ncia ao NavMeshAgent para movimenta��o baseada em navega��o
    protected PlayerHealth player; // Refer�ncia ao script de sa�de do jogador

    [Header("AttackSystem")]
    [SerializeField] protected float attackColdownTime = 1f; // Tempo de recarga entre ataques
    [SerializeField] protected float attackTimeCount;
    Transform attackPoint; // Ponto de ataque do inimigo, onde o ataque ser� aplicado

    [Header("knockbackSystem")]
    private bool isKnockedBack;
    private Vector2 knockbackForce;
    private float knockbackDuration;
    private float stunDuration;





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
        if(isDeath)
            return; // Se o inimigo estiver morto, n�o executa mais nada


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


    protected virtual void AttackPlayer()
    {   

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
        if (isDeath) return; // Evita execu��o m�ltipla

        agent.isStopped = true;
        agent.enabled = false;

        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        // Aqui voc� pode adicionar l�gica para a morte do inimigo, como anima��es, efeitos sonoros, etc.
        anim.SetTrigger("Die"); // Aciona a anima��o de morte
        isDeath = true; // Marca o inimigo como morto
        Destroy(gameObject, 1.5f); // Destroi o inimigo ap�s 1.5 segundos para permitir a anima��o de morte
    }

}
