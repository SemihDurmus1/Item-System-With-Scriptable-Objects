using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCraftingRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public Item resultItem;

    public List<ItemAndQuantity> ingredients;
}

[System.Serializable]
public class ItemAndQuantity
{
    public Item item;
    public int quantity;
}