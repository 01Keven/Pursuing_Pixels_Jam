using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Item/Create")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public int id;
    public string itemName;
    public Sprite icon;
    public ActionType type;


}

public enum ActionType {
    RuneAttack,
    RuneDash
}
