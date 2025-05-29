using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject impactPrefab;//Prefab de impacto, se necessário

    [SerializeField] private float speed = 5f; // Velocidade da bala

    Vector2 direction; // Direção da bala, definida no Start

    Rigidbody2D rb; // Referência ao Rigidbody2D para controlar a física da bala

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D do objeto da bala
        Vector3 mousePosition = Utils.GetMouseWorldPosition(Camera.main, Input.mousePosition);

        Vector3 direction = (mousePosition - transform.position).normalized; // Calcula a direção da bala com base na posição do mouse em relação à posição da bala

        rb.linearVelocity = direction * speed; // Define a velocidade linear do Rigidbody2D na direção especificada e com a velocidade definida

        Destroy(gameObject, 5f); // Destroi a bala após 5 segundos para evitar que fique indefinidamente na cena
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies")) 
        {
            //GameObject impact = Instantiate(impatcPrefab, transform.position, Quaternion.identity);

            var enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.ApplyDamage(25);
            //enemy.ApplyKnockback(rb.velocity);

            //GameManager.Instance.StartCoroutine(GameManager.Instance.DestroyObject(impact, 0.51f));


            Destroy(gameObject);

        }



    }
}
