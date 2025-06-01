using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Ability Status")]
    public bool hasAttack = false;
    public bool hasDash = false;
    public bool canMoveObjects = false;

    [Header("Ability Parameters")]
    public float AttackDamage = 2f;
    public float dashDistance = 5f;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float speedModifier = 1f;

    // usar finalSpeed para mover o personagem

    public static PlayerAbilities Instance { get; private set; }
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

    public void SetMovementModifier(float modifier)
    {
        speedModifier = modifier;
    }

    public void ResetAbilities()
    {
        hasAttack = false;
        hasDash = false;
    }

    public void SetAbilities(RuneData rune, SlotType slot)
    {

        switch (slot)
        {
            case SlotType.Default:
                switch (rune.actionType)
                {
                    case ActionType.Attack:
                        hasAttack = true;
                        break;
                    case ActionType.Dash:
                        hasDash = true;
                        speedModifier = 1f; // normal
                        break;
                    case ActionType.Movable:
                        canMoveObjects = true;
                        break;
                    default:
                        break;
                }
                break;
            case SlotType.Multiply:
                switch (rune.actionType)
                {
                    case ActionType.Attack:
                        hasAttack = true;
                        break;
                    case ActionType.Dash:
                        hasDash = true;
                        break;
                    case ActionType.Movable:
                        canMoveObjects = true;
                        speedModifier = 0.5f; // lento
                        Debug.Log("speed lento");
                        break;
                    default:
                        break;
                }
                break;
            case SlotType.Utilities:
                break;
            default:
                break;
        }


        switch (rune.actionType)
        {
            case ActionType.Attack:
                hasAttack = true;
                break;
            case ActionType.Dash:
                hasDash = true;
                break;
            case ActionType.Movable:
                canMoveObjects = true;
                switch (slot)
            {
                case SlotType.Default:
                    speedModifier = 1f; // normal
                    Debug.Log("speed normal");
                    break;
                case SlotType.Multiply:
                    speedModifier = 0.5f; // lento
                    Debug.Log("speed lento");
                    break;
                case SlotType.Utilities:
                    speedModifier = 1.2f; // mais rápido
                    Debug.Log("speed rapido");
                    break;
            }
            break;
        }
    }

    public void UnSetAbilities(RuneData rune)
    {
        switch (rune.actionType)
        {
            case ActionType.Attack:
                hasAttack = false;
                break;
            case ActionType.Dash:
                hasDash = false;
                break;
            case ActionType.Movable:
                canMoveObjects = false;
                break;
            default:
                break;
        }
    }


    public void EnableAttack()
    {
        hasAttack = true;
        // Aqui voc� pode adicionar l�gica visual/UI
    }

    public void EnableDash()
    {
        hasDash = true;
        // Configura��o inicial do dash
    }

    // Exemplo de uso no Update
    private void Update()
    {

    }

    private void PerformDash()
    {
        // Implementa��o do movimento do dash
        //Vector2 dashDirection = /* l�gica de dire��o */;
        //GetComponent<Rigidbody2D>().AddForce(dashDirection * dashDistance, ForceMode2D.Impulse);
    }
 

}
