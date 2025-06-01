using UnityEngine;

public class EnemyGolem : Enemy
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        // Aqui você pode inicializar variáveis específicas do Golem, se necessário

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isDeath || !agent.enabled) return; // Dupla verificação
        // Se o inimigo estiver morto ou o agente não estiver habilitado, não executa mais nada
        base.Update(); // Chama o método Update da classe base Enemy

        if (agent.velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true; // Mantém sprite do inimigo para a esquerda
        }
        else if (agent.velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false; // Inverte o sprite do inimigo para a direita
        }

        // Chama o método para seguir o jogador
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("Target (Player) não encontrado!"); // Aviso se o jogador não for encontrado
        }
        if (agent.remainingDistance <= agent.stoppingDistance) // Verifica se o inimigo está próximo o suficiente do jogador
        {
            // Aqui você pode adicionar lógica para atacar o jogador, como aplicar dano
            if (player != null)
            {
                AttackPlayer(); // Aplica dano ao jogador
            }
        }
    }

    
    protected override void AttackPlayer()
    {
        //base.AttackPlayer(); // Chama o método AttackPlayer da classe base Enemy
                             // Aqui você pode adicionar lógica específica de ataque do Golem, como aplicar dano ou efeitos especiais


        if (player != null) 
        {
            // Verifica se o contador de tempo de ataque atingiu o tempo de recarga e se a animação de ataque não está sendo reproduzida
            if (attackTimeCount >= attackColdownTime && !anim.GetCurrentAnimatorStateInfo(0).IsName("FrontAttack"))
            {
                player.ApplyDamage(damage); // Aplica dano ao jogador
                anim.SetTrigger("Attack"); // Dispara o gatilho de animação de ataque
            }
            else
            {
                attackTimeCount += Time.deltaTime; // Reseta o contador de tempo de ataque
            }

        }

    }

    public void OnAttackFinished()
    {
        attackTimeCount = 0f; // Reseta o contador de tempo de ataque após o ataque ser concluído
    }
}

