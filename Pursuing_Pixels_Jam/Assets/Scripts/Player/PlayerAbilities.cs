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

    public void ResetAbilities()
    {
        hasAttack = false;
        hasDash = false;
    }

public void SetAbilities(RuneData rune)
{
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
            break;
        default:
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
