using UnityEngine;

public class RunePickup : MonoBehaviour
{

    public RuneData rune; // Refer�ncia ao Rune ScriptableObject


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Adiciona a runa ao invent�rio do jogador
            //PlayerInventory.Instance.AddRune(rune);
            // Destr�i o objeto de pickup
            Destroy(gameObject);
        }
    }
}
