using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet; // Prefab da bala ou proj�til a ser disparado
    [SerializeField] private LayerMask enemiesLayer; // Defina a camada dos inimigos no Inspector ou no c�digo


    [SerializeField] private Transform attackPoint;

    [SerializeField] private PlayerAnimation playerAnim;


    //TESTE//
    //Mudar todas as refer�ncias de PlayerAbilities para verificar a partir do invent�rio.
    PlayerAbilities playerAbilities;

    private void Awake()
    {
        attackPoint = transform.Find("Aim/AttackPoint");
        Debug.Log(attackPoint.gameObject.name);
        playerAbilities = FindFirstObjectByType<PlayerAbilities>(); // Obt�m a inst�ncia de PlayerAbilities
        playerAnim = GetComponentInChildren<PlayerAnimation>(); // Obt�m o componente de anima��o do jogador
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
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, 0.4f, enemiesLayer); // Ajustar o raio conforme necess�rio

        // Verifica se o bot�o de ataque foi pressionado
        // e executa a l�gica de ataque, como tocar um som ou iniciar uma anima��o
        //Ataque a dist�ncia

        if (Input.GetButtonDown("Fire1") && playerAbilities.hasAttack) // outro bot�o para atacar, se necess�rio
        {
            // Implement attack logic here, e.g., play an animation, deal damage, etc.
            Instantiate(bullet, attackPoint.position, Quaternion.identity);

            Debug.Log("Player attacked!");
        }

        // Verifica se o bot�o de ataque foi pressionado e se h� um inimigo no alcance
        // Ataque corpo a corpo
        else if (Input.GetButtonDown("Jump")) // assumir que "Fire1" � o bot�o de ataque
        {
            if (hit != null && hit.CompareTag("Enemies")) // Verifica se h� um inimigo no alcance
            {
                // Implementar l�gica de ataque ao inimigo, como causar dano
                playerAnim.OnAttack(); // Chama a anima��o de ataque
                hit.GetComponent<Enemy>().TakeDamage(10); // Exemplo de dano, ajuste conforme necess�rio
                Debug.Log("Enemy attacked!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, 0.4f); // Visualizar o attack range no editor
    }
}
