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
    playerAbilities.ResetAbilities();
    playerAbilities.speedModifier = 1f;

    for (int i = 0; i < InventoryManager.Instance.inventorySlots.Length; i++)
    {
        GameObject slot = InventoryManager.Instance.inventorySlots[i];
        if (slot != null)
        {
            Debug.Log($"[UpdateAbilities] Verificando Slot {i} com nome '{slot.name}'. Filhos: {slot.transform.childCount}");

            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem == null)
            {
                Debug.LogWarning($"[UpdateAbilities] Slot {i} '{slot.name}' tem {slot.transform.childCount} filhos, mas nenhum com componente InventoryItem.");

                // Vamos tentar listar todos os filhos para ver seus nomes e componentes
                foreach (Transform child in slot.transform)
                {
                    Debug.Log($"[UpdateAbilities] Filho de slot {i}: '{child.name}' - Components:");

                    var comps = child.GetComponents<Component>();
                    foreach(var c in comps)
                    {
                        Debug.Log($"    - {c.GetType().Name}");
                    }
                }
                continue;
            }

            RuneData item = inventoryItem.item;
            if (item != null)
            {
                var slotComponent = slot.GetComponent<inventorySlot>();
                SlotType slotType = slotComponent ? slotComponent.slotType : SlotType.Slot1;

                Debug.Log($"[UpdateAbilities] Slot {i} com item '{item.name}' no slotType {slotType}");

                playerAbilities.SetAbilities(item, slotType);
                ApplySlotEffect(item, slotType);
            }
            else
            {
                Debug.LogWarning($"[UpdateAbilities] Item nulo encontrado no slot {i}");
            }
        }
    }
}




    private void ApplySlotEffect(RuneData rune, SlotType slotType)
    {
        Debug.Log($"[ApplySlotEffect] Aplicando efeito da rune '{rune.name}' com ActionType '{rune.actionType}' no slot {slotType}");

        switch (rune.actionType)
        {
            case ActionType.Movable:
                if (slotType == SlotType.Slot2)
                {
                    playerAbilities.SetMovementModifier(0.5f); // por exemplo, 50% da velocidade
                    Debug.Log("[ApplySlotEffect] Movable: Slot2 - velocidade reduzida para 50%");
                }
                else if (slotType == SlotType.Slot3)
                {
                    playerAbilities.SetMovementModifier(1.2f); // 20% mais rápido
                    Debug.Log("[ApplySlotEffect] Movable: Slot3 - velocidade aumentada para 120%");
                }
                break;

            case ActionType.Dash:
                if (slotType == SlotType.Slot2)
                {
                    playerAbilities.dashDistance = 3f; // Dash menor
                    Debug.Log("[ApplySlotEffect] Dash: Slot2 - dash distance reduzido para 3f");
                }
                else if (slotType == SlotType.Slot3)
                {
                    playerAbilities.dashDistance = 7f; // Dash maior
                    Debug.Log("[ApplySlotEffect] Dash: Slot3 - dash distance aumentado para 7f");
                }
                break;

            case ActionType.Attack:
                if (slotType == SlotType.Slot3)
                {
                    playerAbilities.AttackDamage += 1f; // Dano bônus
                    Debug.Log("[ApplySlotEffect] Attack: Slot3 - dano aumentado em 1");
                }
                break;

            default:
                Debug.Log("[ApplySlotEffect] Nenhum efeito aplicado para este tipo de ação");
                break;
        }
    }

    public void RemoveRune(RuneData rune)
    {
        playerAbilities.UnSetAbilities(rune);
        Debug.Log($"[RemoveRune] Removeu efeitos da rune '{rune.name}'");
    }
}
