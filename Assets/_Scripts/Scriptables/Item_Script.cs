using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/Item_Script")]
public class Item_Script : ScriptableObject {
    [Header("Only gameplay")]
    public TileBase title;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;

}


public enum ItemType
{
    Tool,
    Potion
}

public enum ActionType
{
    Equip,
    Consume
}


