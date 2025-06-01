using UnityEngine;

public class EnemyGolem : Enemy
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        // Aqui voc� pode inicializar vari�veis espec�ficas do Golem, se necess�rio

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isDeath || !agent.enabled) return; // Dupla verifica��o
        // Se o inimigo estiver morto ou o agente n�o estiver habilitado, n�o executa mais nada
        base.Update(); // Chama o m�todo Update da classe base Enemy

        if (agent.velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true; // Mant�m sprite do inimigo para a esquerda
        }
        else if (agent.velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false; // Inverte o sprite do inimigo para a direita
        }

        // Chama o m�todo para seguir o jogador
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("Target (Player) n�o encontrado!"); // Aviso se o jogador n�o for encontrado
        }
        if (agent.remainingDistance <= agent.stoppingDistance) // Verifica se o inimigo est� pr�ximo o suficiente do jogador
        {
            // Aqui voc� pode adicionar l�gica para atacar o jogador, como aplicar dano
            if (player != null)
            {
                AttackPlayer(); // Aplica dano ao jogador
            }
        }
    }

    
    protected override void AttackPlayer()
    {
        //base.AttackPlayer(); // Chama o m�todo AttackPlayer da classe base Enemy
                             // Aqui voc� pode adicionar l�gica espec�fica de ataque do Golem, como aplicar dano ou efeitos especiais


        if (player != null) 
        {
            // Verifica se o contador de tempo de ataque atingiu o tempo de recarga e se a anima��o de ataque n�o est� sendo reproduzida
            if (attackTimeCount >= attackColdownTime && !anim.GetCurrentAnimatorStateInfo(0).IsName("FrontAttack"))
            {
                player.ApplyDamage(damage); // Aplica dano ao jogador
                anim.SetTrigger("Attack"); // Dispara o gatilho de anima��o de ataque
            }
            else
            {
                attackTimeCount += Time.deltaTime; // Reseta o contador de tempo de ataque
            }

        }

    }

    public void OnAttackFinished()
    {
        attackTimeCount = 0f; // Reseta o contador de tempo de ataque ap�s o ataque ser conclu�do
    }
}

