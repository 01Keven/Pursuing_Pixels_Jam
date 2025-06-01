using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;

    PlayerAim aimWeapon;
    [SerializeField] Vector3 mousePos;

    public static PlayerAnimation instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        aimWeapon = GetComponentInParent<PlayerAim>();
    }

    // Update is called once per frame
    void Update()
    {

        mousePos = Utils.GetMouseWorldPosition(Camera.main, Input.mousePosition) - transform.position;
    }

    private void LateUpdate()
    {
        Vector2 direction = rb.linearVelocity;

        if (direction.sqrMagnitude != 0)
        {
            anim.SetInteger("Move", 1);
            anim.SetFloat("AxisX", mousePos.x);
            anim.SetFloat("AxisY", mousePos.y);
        }
        else
        {
            anim.SetInteger("Move", 0);
            anim.SetFloat("AxisX", mousePos.x);
            anim.SetFloat("AxisY", mousePos.y);
        }

        if (mousePos.x > 0.1f)
        {
            //transform.eulerAngles = new Vector2(transform.position.x, 0);
            spriteRenderer.flipX = false;
        }
        else if (mousePos.x < -0.1f)
        {
            //transform.eulerAngles = new Vector2(transform.position.x, -180f);
            spriteRenderer.flipX = true;
        }
    }

    public void StepAudio()
    {
        //AudioManager.instance.PlayFootStepsAudio();
    }

    public void OnHit()
    {
        anim.SetTrigger("Hit");
    }

    public void OnAttack()
    {
        anim.SetTrigger("Melee");
    }
    public void OnDeath()
    {
        anim.SetTrigger("Death");
    }   
}
