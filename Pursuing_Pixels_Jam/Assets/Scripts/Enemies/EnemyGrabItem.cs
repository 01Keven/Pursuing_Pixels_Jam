using System;
using UnityEngine;

public class EnemyGrabItem : Enemy
{
    
    [Header("EnemyGrabItem")]
    [SerializeField] RuneData runeData; // Referência aos dados da runa/item que o inimigo irá pegar
    [SerializeField] SlotType slotType; // Tipo de slot onde a runa/item está equipada
    [SerializeField] GameObject runeEnemyGameobject; // Referência ao GameObject da runa/item que o inimigo irá pegar
    [SerializeField] private Transform mysticTree; // Referência atribuída no Inspector

    private bool hasItem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        
        mysticTree = GameObject.Find("MistycTree")?.transform; // Tenta encontrar o GameObject da árvore na cena e pega o transform

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isDeath || !agent.enabled) return; // Dupla verificação
        base.Update(); // Chama o método Update da classe base Enemy

        if (agent.velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false; // Mantém sprite do inimigo para a direita
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
            anim.SetInteger("Move", 1); // Define o estado de animação para "Chase" (1)
            anim.SetFloat("AxisX", agent.velocity.x); // Atualiza a velocidade da animação com base na velocidade do agente
            anim.SetFloat("AxisY", agent.velocity.y); // Atualiza a velocidade da animação com base na velocidade do agente
        }
        else
        {
            anim.SetInteger("Move", 0); // Define o estado de animação para "Idle" (0)
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
        if (InventoryManager.Instance == null || InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.item == null) // Verifica se o inventário existe e se o primeiro slot tem um item equipado
            return;
        // Configura o item roubado
        runeData = InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.item; // Obtém a runa/item equipada no primeiro slot do inventário do jogador
        slotType = InventoryManager.Instance.inventorySlots[2].GetComponent<inventorySlot>().slotType; // Obtém o tipo de slot onde a runa/item está equipada

        if (runeEnemyGameobject != null) 
        {
            var renderer = runeEnemyGameobject.GetComponent<SpriteRenderer>(); // Obtém o componente SpriteRenderer do GameObject da runa/item
            if (renderer != null) // Verifica se o componente SpriteRenderer existe
                renderer.sprite = runeData.runeIcon; // Define o sprite da runa/item no GameObject do inimigo
        }

        // Remove do jogador
        RuneManager.Instance?.RemoveRune(runeData, slotType); // Remove a runa do RuneManager
        GameObject item = InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.gameObject; // Obtém o GameObject do item no inventário do jogador
        Destroy(item); // Destrói o GameObject do item no inventário do jogador

        hasItem = true;
        anim.SetBool("HasItem", true); // Define o estado de animação para "HasItem" (true)
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
        anim.SetBool("HasItem", false); // Reseta o estado de animação para "HasItem" (false)

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
        if (InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.item != null)
        {
            runeData = InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.item;
            slotType = InventoryManager.Instance.inventorySlots[2].GetComponent<inventorySlot>().slotType; // Obtém o tipo de slot onde a runa/item está equipada
            if (runeEnemyGameobject == null)
            {
                runeEnemyGameobject = GameObject.Find("RuneEnemy"); // Encontra o GameObject da runa/item que o inimigo irá pegar
                runeEnemyGameobject.GetComponent<SpriteRenderer>().sprite = runeData.runeIcon; // Obtém o sprite da runa/item e o icone da runa/item

                
                RuneManager.Instance.RemoveRune(runeData, slotType); // Remove a runa do RuneManager
                //Lembrar de remover a runa do Inventory de Keven também
                GameObject item = InventoryManager.Instance.inventorySlots[2].GetComponentInChildren<InventoryItem>()?.gameObject; // Obtém o GameObject do item no inventário do jogador
                Destroy(item); // Destrói o GameObject do item no inventário do jogador
            }

            // Lógica para pegar o item, como coletar um item do chão ou interagir com um objeto
            Debug.Log("Enemy grabbed the item!");
            // Aqui você pode adicionar mais lógica, como destruir o item ou atualizar o inventário do inimigo
        }
    }
}
