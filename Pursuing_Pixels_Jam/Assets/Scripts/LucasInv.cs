using System.Collections.Generic;
using UnityEngine;

public class LucasInv : MonoBehaviour
{
    public static LucasInv Instance { get; private set; }

    public List<RuneData> collectedRunes = new List<RuneData>(); // Lista de runas coletadas
    public RuneData[] equippedRunes = new RuneData[4]; // Array para runas equipadas (2 slots)

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
    }

    public void AddRune(RuneData rune)
    {
        if (!collectedRunes.Contains(rune))
        {
            collectedRunes.Add(rune);
            Debug.Log($"Runa {rune.runeName} coletada!");


            for (int i = 0; i < equippedRunes.Length; i++) // Verifica se há algum slot vazio para equipar a runa
            {
                if (equippedRunes[i] == null) // Se o slot estiver vazio
                {
                    EquipRune(rune, i); // Equipa a runa no primeiro slot vazio encontrado
                    return;
                }
            }
            Debug.Log("Todos os slots ocupados!");

        }
    }

    public bool EquipRune(RuneData rune, int slot) // Método para equipar uma runa em um slot específico
    {
        if (collectedRunes.Contains(rune) && slot >= 0 && slot < equippedRunes.Length) // Verifica se a runa está na lista de coletadas e se o slot é válido
        {
            equippedRunes[slot] = rune; // Equipando a runa no slot especificado
            RuneManager.Instance.UpdateAbilities(); // Atualiza as habilidades do RuneManager após equipar a runa
            return true; 
        }
        return false;
    }

    public void UnequipRune(int slot)
    {
        if (slot >= 0 && slot < equippedRunes.Length)
        {
            equippedRunes[slot] = null;
            RuneManager.Instance.UpdateAbilities();
        }
    }
}
