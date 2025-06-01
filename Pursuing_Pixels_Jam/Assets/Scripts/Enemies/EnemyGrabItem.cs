using System;
using UnityEngine;

public class EnemyGrabItem : Enemy
{
    
    [Header("EnemyGrabItem")]
    [SerializeField] RuneData runeData; // Refer�ncia aos dados da runa/item que o inimigo ir� pegar
    [SerializeField] SlotType slotType; // Tipo de slot onde a runa/item est� equipada
    [SerializeField] GameObject runeEnemyGameobject; // Refer�ncia ao GameObject da runa/item que o inimigo ir� pegar
    [SerializeField] private Transform mysticTree; // Refer�ncia atribu�da no Inspector

    private bool hasItem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        
        mysticTree = GameObject.Find("MistycTree")?.transform; // Tenta encontrar o GameObject da �rvore na cena e pega o transform

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isDeath || !agent.enabled) return; // Dupla verifica��o
        base.Update(); // Chama o m�todo Update da classe base Enemy

        if (agent.velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false; // Mant�m sprite do inimigo para a direita
        }
        else if (agent.velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true; // Inverte o sprite do inimigo para a esquerda
        }

        if (hasItem)
        {
            MoveToTree();
        }
        else 
        {
            ChasePlayer();
        }

        if (agent.velocity.sqrMagnitude != 0)
        {
            anim.SetInteger("Move", 1); // Define o estado de anima��o para "Chase" (1)
            anim.SetFloat("AxisX", agent.velocity.x); // Atualiza a velocidade da anima��o com base na velocidade do agente
            anim.SetFloat("AxisY", agent.velocity.y); // Atualiza a velocidade da anima��o com base na velocidade do agente
        }
        else
        {
            anim.SetInteger("Move", 0); // Define o estado de anima��o para "Idle" (0)
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
        if (InventoryManager.Instance == null || InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.item == null) // Verifica se o invent�rio existe e se o primeiro slot tem um item equipado
            return;
        // Configura o item roubado
        runeData = InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.item; // Obt�m a runa/item equipada no primeiro slot do invent�rio do jogador
        slotType = InventoryManager.Instance.inventorySlots[2].GetComponent<inventorySlot>().slotType; // Obt�m o tipo de slot onde a runa/item est� equipada

        if (runeEnemyGameobject != null) 
        {
            var renderer = runeEnemyGameobject.GetComponent<SpriteRenderer>(); // Obt�m o componente SpriteRenderer do GameObject da runa/item
            if (renderer != null) // Verifica se o componente SpriteRenderer existe
                renderer.sprite = runeData.runeIcon; // Define o sprite da runa/item no GameObject do inimigo
        }

        // Remove do jogador
        RuneManager.Instance?.RemoveRune(runeData, slotType); // Remove a runa do RuneManager
        GameObject item = InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.gameObject; // Obt�m o GameObject do item no invent�rio do jogador
        Destroy(item); // Destr�i o GameObject do item no invent�rio do jogador

        hasItem = true;
        anim.SetBool("HasItem", true); // Define o estado de anima��o para "HasItem" (true)
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
        anim.SetBool("HasItem", false); // Reseta o estado de anima��o para "HasItem" (false)

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
        if (InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.item != null)
        {
            runeData = InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.item;
            slotType = InventoryManager.Instance.inventorySlots[2].GetComponent<inventorySlot>().slotType; // Obt�m o tipo de slot onde a runa/item est� equipada
            if (runeEnemyGameobject == null)
            {
                runeEnemyGameobject = GameObject.Find("RuneEnemy"); // Encontra o GameObject da runa/item que o inimigo ir� pegar
                runeEnemyGameobject.GetComponent<SpriteRenderer>().sprite = runeData.runeIcon; // Obt�m o sprite da runa/item e o icone da runa/item

                
                RuneManager.Instance.RemoveRune(runeData, slotType); // Remove a runa do RuneManager
                //Lembrar de remover a runa do Inventory de Keven tamb�m
                GameObject item = InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.gameObject; // Obt�m o GameObject do item no invent�rio do jogador
                Destroy(item); // Destr�i o GameObject do item no invent�rio do jogador
            }

            // L�gica para pegar o item, como coletar um item do ch�o ou interagir com um objeto
            Debug.Log("Enemy grabbed the item!");
            // Aqui voc� pode adicionar mais l�gica, como destruir o item ou atualizar o invent�rio do inimigo
        }
    }
}
