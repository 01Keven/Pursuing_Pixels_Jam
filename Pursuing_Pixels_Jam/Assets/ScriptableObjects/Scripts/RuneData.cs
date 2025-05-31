using UnityEngine;

public enum ActionType
{
    Attack,
    Dash,
    Movable
}


[CreateAssetMenu(fileName = "NewRune", menuName = "Scriptable Objects/Runes")]
public class RuneData : ScriptableObject
{
    [Header("Rune Properties")]
    public int runeID;
    public string runeName;
    public string runeDescription;
    public Sprite runeIcon;
    [Header("Rune Information")]
    public ActionType actionType;
}