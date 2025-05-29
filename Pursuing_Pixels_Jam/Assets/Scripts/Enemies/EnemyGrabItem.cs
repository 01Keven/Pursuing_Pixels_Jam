using System;
using UnityEngine;

public class EnemyGrabItem : Enemy
{
    [Header("EnemyGrabItem")]
    LucasInv lucasInv; // Referência ao script de inventário do jogador
    [SerializeField] RuneData runeData; // Referência aos dados da runa/item que o inimigo irá pegar
    [SerializeField] GameObject runeEnemyGameobject; // Referência ao GameObject da runa/item que o inimigo irá pegar
    [SerializeField] private Transform mysticTree; // Referência atribuída no Inspector

    private bool hasItem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        lucasInv = LucasInv.Instance; // Encontra o script de inventário do jogador na cena

        mysticTree = GameObject.Find("MistycTree")?.transform; // Tenta encontrar o GameObject da árvore na cena e pega o transform

    }

    // Update is called once per frame
    protected override void Update()
    {

        if (hasItem)
        {
            MoveToTree();
        }
        else 
        {
            ChasePlayer();
        }

    }

    private void ChasePlayer()
    {
        if (player == null) return;

        agent.SetDestination(player.transform.position);

        if (HasReachedDestination())
        {
            TryStealItem();
        }
    }

    private void TryStealItem()
    {
        if (lucasInv == null || lucasInv.equippedRunes[0] == null)
            return;
        // Configura o item roubado
        runeData = lucasInv.equippedRunes[0]; // Obtém a runa/item equipada no primeiro slot do inventário do jogador

        if (runeEnemyGameobject != null) 
        {
            var renderer = runeEnemyGameobject.GetComponent<SpriteRenderer>(); // Obtém o componente SpriteRenderer do GameObject da runa/item
            if (renderer != null) // Verifica se o componente SpriteRenderer existe
                renderer.sprite = runeData.runeIcon; // Define o sprite da runa/item no GameObject do inimigo
        }

        // Remove do jogador
        RuneManager.Instance?.RemoveRune(runeData); // Remove a runa do RuneManager
        lucasInv.UnequipRune(0); // Desequipando a runa do primeiro slot do inventário do jogador

        hasItem = true;
        Debug.Log("Item roubado do jogador!");
    }

    private void MoveToTree()
    {
        agent.SetDestination(mysticTree.transform.position); // Define o destino do inimigo para a posição da árvore mística

        if (HasReachedDestination())
        {
            DeliverItem(); // Chama o método para entregar o item
        }
    }

    private void DeliverItem()
    {
        //Lógica para entregar o item à árvore mística
        runeData = null; // Reseta a runa/item após entregar
        hasItem = false; // Reseta o estado de ter o item

        if (runeEnemyGameobject != null) // Verifica se o GameObject da runa/item existe antes de tentar acessá-lo
        {
            var renderer = runeEnemyGameobject.GetComponent<SpriteRenderer>(); // Obtém o componente SpriteRenderer do GameObject da runa/item
            if (renderer != null) // Verifica se o componente SpriteRenderer existe
                renderer.sprite = null; // Reseta o sprite da runa/item para null após entregar
        }

        Debug.Log("Item entregue na árvore!");
    }

    private bool HasReachedDestination()
    {
        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance;
    }

    private void TakeItem()
    {
        // Método para pegar o item
        if (lucasInv.equippedRunes[0] != null)
        {
            runeData = lucasInv.equippedRunes[0];

            if (runeEnemyGameobject == null)
            {
                runeEnemyGameobject = GameObject.Find("RuneEnemy"); // Encontra o GameObject da runa/item que o inimigo irá pegar
                runeEnemyGameobject.GetComponent<SpriteRenderer>().sprite = runeData.runeIcon; // Obtém o sprite da runa/item e o icone da runa/item

                RuneManager.Instance.RemoveRune(runeData); // Remove a runa do RuneManager
                lucasInv.UnequipRune(0); // Desequipando a runa do primeiro slot
            }

            // Lógica para pegar o item, como coletar um item do chão ou interagir com um objeto
            Debug.Log("Enemy grabbed the item!");
            // Aqui você pode adicionar mais lógica, como destruir o item ou atualizar o inventário do inimigo
        }
    }
}
