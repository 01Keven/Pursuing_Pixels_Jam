using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;


    [Header("DashConfig")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] float dashSpeed = 24f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 1f;
    TrailRenderer tr;

    private float currentSpeed;
    Rigidbody2D rb;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        currentSpeed = speed;
    }




    public void ResetSpeed()
    {
        currentSpeed = speed;
    }


    private void FixedUpdate()
    {
        if (isDashing)
            return; // Se estiver dashing, n�o processa mais nada

        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
            return; // Se estiver dashing, n�o processa mais nada

        GetInputs();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && direction != Vector2.zero && !isDashing  && PlayerAbilities.Instance.hasDash)
        {
            StartCoroutine(Dash());
        }
    }

    //Metodo que faz o player se mover a partir de uma velocidade * a direcao conforme o comando do player
    void Move()
    {

        float finalSpeed = speed * PlayerAbilities.Instance.speedModifier;
        rb.linearVelocity = direction * finalSpeed;

        

    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0; // Desativa a gravidade durante o dash
        Vector2 dashDirection = direction.normalized * dashSpeed;
        rb.linearVelocity = dashDirection; // Aplica a velocidade do dash
        tr.emitting = true; // Ativa o TrailRenderer durante o dash
        yield return new WaitForSeconds(dashDuration); // Espera o tempo do dash
        tr.emitting = false; // Desativa o TrailRenderer ap�s o dash
        rb.gravityScale = originalGravityScale; // Restaura a gravidade 
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown); // Espera o tempo do dash
        canDash = true; // Permite outro dash ap�s o cooldown
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
