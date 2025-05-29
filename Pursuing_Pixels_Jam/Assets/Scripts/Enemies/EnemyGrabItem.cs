using System;
using UnityEngine;

public class EnemyGrabItem : Enemy
{
    [Header("EnemyGrabItem")]
    LucasInv lucasInv; // Refer�ncia ao script de invent�rio do jogador
    [SerializeField] RuneData runeData; // Refer�ncia aos dados da runa/item que o inimigo ir� pegar
    [SerializeField] GameObject runeEnemyGameobject; // Refer�ncia ao GameObject da runa/item que o inimigo ir� pegar
    [SerializeField] private Transform mysticTree; // Refer�ncia atribu�da no Inspector

    private bool hasItem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        lucasInv = LucasInv.Instance; // Encontra o script de invent�rio do jogador na cena

        mysticTree = GameObject.Find("MistycTree")?.transform; // Tenta encontrar o GameObject da �rvore na cena e pega o transform

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
        runeData = lucasInv.equippedRunes[0]; // Obt�m a runa/item equipada no primeiro slot do invent�rio do jogador

        if (runeEnemyGameobject != null) 
        {
            var renderer = runeEnemyGameobject.GetComponent<SpriteRenderer>(); // Obt�m o componente SpriteRenderer do GameObject da runa/item
            if (renderer != null) // Verifica se o componente SpriteRenderer existe
                renderer.sprite = runeData.runeIcon; // Define o sprite da runa/item no GameObject do inimigo
        }

        // Remove do jogador
        RuneManager.Instance?.RemoveRune(runeData); // Remove a runa do RuneManager
        lucasInv.UnequipRune(0); // Desequipando a runa do primeiro slot do invent�rio do jogador

        hasItem = true;
        Debug.Log("Item roubado do jogador!");
    }

    private void MoveToTree()
    {
        agent.SetDestination(mysticTree.transform.position); // Define o destino do inimigo para a posi��o da �rvore m�stica

        if (HasReachedDestination())
        {
            DeliverItem(); // Chama o m�todo para entregar o item
        }
    }

    private void DeliverItem()
    {
        //L�gica para entregar o item � �rvore m�stica
        runeData = null; // Reseta a runa/item ap�s entregar
        hasItem = false; // Reseta o estado de ter o item

        if (runeEnemyGameobject != null) // Verifica se o GameObject da runa/item existe antes de tentar acess�-lo
        {
            var renderer = runeEnemyGameobject.GetComponent<SpriteRenderer>(); // Obt�m o componente SpriteRenderer do GameObject da runa/item
            if (renderer != null) // Verifica se o componente SpriteRenderer existe
                renderer.sprite = null; // Reseta o sprite da runa/item para null ap�s entregar
        }

        Debug.Log("Item entregue na �rvore!");
    }

    private bool HasReachedDestination()
    {
        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance;
    }

    private void TakeItem()
    {
        // M�todo para pegar o item
        if (lucasInv.equippedRunes[0] != null)
        {
            runeData = lucasInv.equippedRunes[0];

            if (runeEnemyGameobject == null)
            {
                runeEnemyGameobject = GameObject.Find("RuneEnemy"); // Encontra o GameObject da runa/item que o inimigo ir� pegar
                runeEnemyGameobject.GetComponent<SpriteRenderer>().sprite = runeData.runeIcon; // Obt�m o sprite da runa/item e o icone da runa/item

                RuneManager.Instance.RemoveRune(runeData); // Remove a runa do RuneManager
                lucasInv.UnequipRune(0); // Desequipando a runa do primeiro slot
            }

            // L�gica para pegar o item, como coletar um item do ch�o ou interagir com um objeto
            Debug.Log("Enemy grabbed the item!");
            // Aqui voc� pode adicionar mais l�gica, como destruir o item ou atualizar o invent�rio do inimigo
        }
    }
}
