using UnityEngine;

public class RunePickup : MonoBehaviour
{

    public RuneData rune; // Referência ao Rune ScriptableObject


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Adiciona a runa ao inventário do jogador
            //PlayerInventory.Instance.AddRune(rune);
            // Destrói o objeto de pickup
            Destroy(gameObject);
        }
    }
}
