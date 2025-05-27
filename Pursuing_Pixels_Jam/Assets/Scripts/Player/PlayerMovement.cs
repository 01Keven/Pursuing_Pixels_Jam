using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    //Metodo que faz o player se mover a partir de uma velocidade * a direcao conforme o comando do player
    void Move()
    {

        rb.linearVelocity = direction * speed;

    }

    public void addSpeed(float amount)
    {
        speed += amount;
    }

    //Pegando os comandos de movimento do player
    void GetInputs()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction.Normalize();
    }
}
