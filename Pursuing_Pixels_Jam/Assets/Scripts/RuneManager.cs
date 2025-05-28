using UnityEngine;

public class RuneManager : MonoBehaviour
{
    public static RuneManager Instance { get; private set; }

    private PlayerAbilities playerAbilities;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerAbilities = FindFirstObjectByType<PlayerAbilities>();
    }

    public void UpdateAbilities()
    {
        

        //// Aplica efeitos das runas equipadas
        foreach (var rune in LucasInv.Instance.equippedRunes)
        {
            if (rune != null)
            {
                switch (rune.runeEffect)
                {
                    case RuneEffects.Attack:
                        playerAbilities.EnableAttack();
                        break;

                    case RuneEffects.Dash:
                        playerAbilities.EnableDash();
                        break;

                    default:
                        // Reseta todas as habilidades
                        playerAbilities.ResetAbilities();
                        break;
                        // Adicione outros efeitos conforme necessário
                }
            }
        }
    }
}
