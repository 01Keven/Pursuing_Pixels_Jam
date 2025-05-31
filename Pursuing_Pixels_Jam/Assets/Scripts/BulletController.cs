using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject impactPrefab;//Prefab de impacto, se necess�rio

    [SerializeField] private float speed = 5f; // Velocidade da bala

    Vector2 direction; // Dire��o da bala, definida no Start

    Rigidbody2D rb; // Refer�ncia ao Rigidbody2D para controlar a f�sica da bala

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obt�m o componente Rigidbody2D do objeto da bala
        Vector3 mousePosition = Utils.GetMouseWorldPosition(Camera.main, Input.mousePosition);

        Vector3 direction = (mousePosition - transform.position).normalized; // Calcula a dire��o da bala com base na posi��o do mouse em rela��o � posi��o da bala

        rb.linearVelocity = direction * speed; // Define a velocidade linear do Rigidbody2D na dire��o especificada e com a velocidade definida

        Destroy(gameObject, 5f); // Destroi a bala ap�s 5 segundos para evitar que fique indefinidamente na cena
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
