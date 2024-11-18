using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab; // Dustugunde sahnede gozukecek prefab
    public bool isStackable;
}