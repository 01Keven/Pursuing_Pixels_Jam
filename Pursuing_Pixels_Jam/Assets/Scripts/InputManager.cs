using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [SerializeField] private Vector2 movementInput;
    [SerializeField] bool fireInput;
    [SerializeField] bool runInput;
    [SerializeField] bool dashInput;


    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        fireInput = false;
    }

    #region Set Inputs
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        if (value.started) //Executa quando o input é ativo
        {

        }

        if (value.performed) // Só executa quando o input está ativo
        {
            movementInput = value.ReadValue<Vector2>();
        }
        else if (value.canceled) // Quando solta o controle
        {
            movementInput = Vector2.zero;
        }
    }

    public void OnAttack(InputValue value)
    {
        fireInput = value.isPressed;
    }
    public void OnSprint(InputValue value)
    {
        runInput = value.isPressed;
    }
    #endregion

    #region Get Inputs
    public static Vector2 GetMovementInput()
    {
        return Instance.movementInput;
    }
    public static bool GetAttackInput()
    {
        return Instance.fireInput;
    }
    public static bool GetSprintInput()
    {
        return Instance.runInput;
    }
    #endregion


}
