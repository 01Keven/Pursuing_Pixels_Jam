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
        foreach (var rune in InventoryManager.Instance.inventorySlots)
        {
            if (rune != null)
            {
                if (rune.GetComponentInChildren<InventoryItem>()?.item)
                {
                    playerAbilities.SetAbilities(rune.GetComponentInChildren<InventoryItem>().item, rune.GetComponent<inventorySlot>().slotType);
                }
                
            }
        }
    }

    public void RemoveRune(RuneData rune, SlotType slot)
    {
        playerAbilities.UnSetAbilities(rune, slot);
    }
}
