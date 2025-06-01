using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet; // Prefab da bala ou projétil a ser disparado
    [SerializeField] private LayerMask enemiesLayer; // Defina a camada dos inimigos no Inspector ou no código


    [SerializeField] private Transform attackPoint;

    [SerializeField] private PlayerAnimation playerAnim;


    //TESTE//
    //Mudar todas as referências de PlayerAbilities para verificar a partir do inventário.
    PlayerAbilities playerAbilities;

    private void Awake()
    {
        attackPoint = transform.Find("Aim/AttackPoint");
        Debug.Log(attackPoint.gameObject.name);
        playerAbilities = FindFirstObjectByType<PlayerAbilities>(); // Obtém a instância de PlayerAbilities
        playerAnim = GetComponentInChildren<PlayerAnimation>(); // Obtém o componente de animação do jogador
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, 0.4f, enemiesLayer); // Ajustar o raio conforme necessário

        // Verifica se o botão de ataque foi pressionado
        // e executa a lógica de ataque, como tocar um som ou iniciar uma animação
        //Ataque a distância

        if (Input.GetButtonDown("Fire1") && playerAbilities.hasAttack) // outro botão para atacar, se necessário
        {
            // Implement attack logic here, e.g., play an animation, deal damage, etc.
            Instantiate(bullet, attackPoint.position, Quaternion.identity);

            Debug.Log("Player attacked!");
        }

        // Verifica se o botão de ataque foi pressionado e se há um inimigo no alcance
        // Ataque corpo a corpo
        else if (Input.GetButtonDown("Jump")) // assumir que "Fire1" é o botão de ataque
        {
            if (hit != null && hit.CompareTag("Enemies")) // Verifica se há um inimigo no alcance
            {
                // Implementar lógica de ataque ao inimigo, como causar dano
                playerAnim.OnAttack(); // Chama a animação de ataque
                hit.GetComponent<Enemy>().TakeDamage(10); // Exemplo de dano, ajuste conforme necessário
                Debug.Log("Enemy attacked!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, 0.4f); // Visualizar o attack range no editor
    }
}
