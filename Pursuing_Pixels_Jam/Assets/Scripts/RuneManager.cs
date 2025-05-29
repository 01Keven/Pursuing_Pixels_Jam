using UnityEngine;
using static Unity.Collections.Unicode;

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
                playerAbilities.SetAbilities(rune);
            }
        }
    }

    public void RemoveRune(RuneData rune)
    {
        playerAbilities.UnSetAbilities(rune);
    }
}
