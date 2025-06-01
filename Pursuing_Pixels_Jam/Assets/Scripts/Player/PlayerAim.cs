using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private float maxAmmoAmount = 10f;


    //[SerializeField] private float currentAmmoAmount;

    private Transform attackPoint;


    //[SerializeField] private float shotColdown = 0.15f;
    //private float CountTime;

    //private float rechargeSpeed = 4f;
    //bool isRecharging = false;

    float angle;
    private Transform aimTransform, aimTransformRight, aimTransformLeft;
    //private Animator aimAnimator;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        //aimAnimator = aimTransform.GetComponentInChildren<Animator>();
        attackPoint = transform.Find("Aim/AttackPoint");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        RechargeWeapon();
        HandleShooting();
    }

    void RechargeWeapon()
    {

    }


    void HandleAiming()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPosition(Camera.main, Input.mousePosition);

        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        aimTransform.eulerAngles = new Vector3(0, 0, angle);


        Vector3 localScale = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;

        }
        else
        {
            localScale.y = 1f;


        }

        aimTransform.localScale = localScale;



    }

    void HandleShooting()
    {

    }

    //public void addMaxAmount(float amount)
    //{
    //    maxAmmoAmount += amount;
    //}
}
