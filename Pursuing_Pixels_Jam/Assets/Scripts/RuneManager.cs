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
                    var itemRune = rune.GetComponentInChildren<InventoryItem>().item;
                    var slot = rune.GetComponent<inventorySlot>().slotType;
                    playerAbilities.SetAbilities(itemRune, slot);
                }
                
            }
        }
    }

    public void RemoveRune(RuneData rune, SlotType slot)
    {
        playerAbilities.UnSetAbilities(rune, slot);
    }
}
