using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Ability Status")]
    public bool hasAttack = false;
    public bool hasDash = false;

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
            default:
                ResetAbilities();
                break;
        }
    }

    public void UnSetAbilities(RuneData rune)
    {
        switch (rune.actionType)
        {
            case ActionType.Attack:
                hasAttack = false;
                // Aqui você pode adicionar lógica visual/UI para desabilitar o ataque
                break;

            case ActionType.Dash:
                hasDash = false;
                // Aqui você pode adicionar lógica visual/UI para desabilitar o dash
                break;

            default:
                break;
        }
    }

    public void EnableAttack()
    {
        hasAttack = true;
        // Aqui você pode adicionar lógica visual/UI
    }

    public void EnableDash()
    {
        hasDash = true;
        // Configuração inicial do dash
    }

    // Exemplo de uso no Update
    private void Update()
    {
        
    }

    private void PerformDash()
    {
        // Implementação do movimento do dash
        //Vector2 dashDirection = /* lógica de direção */;
        //GetComponent<Rigidbody2D>().AddForce(dashDirection * dashDistance, ForceMode2D.Impulse);
    }
}
