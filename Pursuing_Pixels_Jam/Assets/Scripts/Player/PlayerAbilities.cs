using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Ability Status")]
    public bool hasAttack = false;
    public bool hasDash = false;
    public bool hasTelecinesis = false;

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

    public void SetAbilities(RuneData rune, SlotType slot)
    {
        switch (slot)
        {

            case SlotType.Default:
                switch (rune.actionType)
                {
                    case ActionType.Attack:
                        hasAttack = true;
                        Debug.Log("Ataque Habilitado: Default" + hasAttack);
                        break;
                    case ActionType.Dash:
                        Debug.Log("Dash Habilitado: Default" + hasDash);
                        hasDash = true;
                        break;
                    case ActionType.Telekinesis:
                        hasTelecinesis = true;
                        Debug.Log("Telecinese Habilitada: Default" + hasTelecinesis);
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
                        Debug.Log("Ataque Habilitado: Multiply" + hasAttack);
                        break;
                    case ActionType.Dash:
                        hasDash = true;
                        Debug.Log("Dash Habilitado: Multiply" + hasDash);
                        break;
                    case ActionType.Telekinesis:
                        hasTelecinesis = true;
                        Debug.Log("Telecinese Habilitada: Multiply" + hasTelecinesis);
                        break;
                    default:
                        break;
                }
                break;

            case SlotType.Utilities:
                switch (rune.actionType)
                {
                    case ActionType.Attack:
                        hasAttack = true;
                        // Aqui você pode adicionar lógica visual/UI para o ataque
                        break;
                    case ActionType.Dash:
                        hasDash = true;
                        // Aqui você pode adicionar lógica visual/UI para o dash
                        break;
                    case ActionType.Telekinesis:
                        hasTelecinesis = true;
                        // Aqui você pode adicionar lógica visual/UI para a telecinese
                        break;
                    default:
                        break;
                }
                break;

            default:
                ResetAbilities();
                break;
        }
    }

    public void UnSetAbilities(RuneData rune, SlotType slot)
    {
        switch (slot)
        {

            case SlotType.Default:
                switch (rune.actionType)
                {
                    case ActionType.Attack:
                        hasAttack = false;
                        Debug.Log("Ataque Desabilitado: Default" + hasAttack);
                        break;
                    case ActionType.Dash:
                        hasDash = true;
                        Debug.Log("Dash Desabilitado: Default" + hasDash);
                        break;
                        case ActionType.Telekinesis:
                            hasTelecinesis = false;
                            Debug.Log("Telecinese Desabilitada: Default" + hasTelecinesis);
                            break;
                    default:
                        break;
                }
                break;

            case SlotType.Multiply:
                switch (rune.actionType)
                {
                    case ActionType.Attack:
                        hasAttack = false;
                        Debug.Log("Ataque Desabilitado: Multiply" + hasAttack);
                        break;
                    case ActionType.Dash:
                        hasDash = false;
                        Debug.Log("Dash Desabilitado: Multiply" + hasDash);
                        break;
                        case ActionType.Telekinesis:
                            hasTelecinesis = false;
                            Debug.Log("Telecinese Desabilitada: Multiply" + hasTelecinesis);
                            break;
                    default:
                        break;
                }
                break;

            case SlotType.Utilities:
                switch (rune.actionType)
                {
                    case ActionType.Attack:
                        hasAttack = true;
                        // Aqui você pode adicionar lógica visual/UI para o ataque
                        break;
                    case ActionType.Dash:
                        hasDash = true;
                        // Aqui você pode adicionar lógica visual/UI para o dash
                        break;
                    case ActionType.Telekinesis:
                        hasTelecinesis = false;
                        // Aqui você pode adicionar lógica visual/UI para a telecinese
                        break;
                    default:
                        break;
                }
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
