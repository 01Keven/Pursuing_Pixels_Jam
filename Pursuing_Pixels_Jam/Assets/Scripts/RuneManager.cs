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
        // Reseta todas as habilidades
        playerAbilities.ResetAbilities();

        //// Aplica efeitos das runas equipadas
        //foreach (var rune in RuneInventory.Instance.equippedRunes)
        //{
        //    if (rune != null)
        //    {
        //        switch (rune.runeEffect)
        //        {
        //            case RuneEffects.Attack:
        //                playerAbilities.EnableAttack();
        //                break;

        //            case RuneEffects.Dash:
        //                playerAbilities.EnableDash();
        //                break;

        //            default:

        //                break;
        //                // Adicione outros efeitos conforme necessário
        //        }
        //    }
        //}
    }
}
