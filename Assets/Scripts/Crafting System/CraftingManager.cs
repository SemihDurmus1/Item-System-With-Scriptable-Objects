using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;//Singleton

    public List<CraftingRecipe> craftingRecipes = new();

    public GameObject recipeSlotPrefab;

    private void Awake()//Singleton
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    public void CraftItem(CraftingRecipe recipe)//InventoryUI CraftButton Event
    {
        if (CanCraft(recipe, InventoryManager.Instance.GetInventoryDictionary()))
        {
            foreach (ItemAndQuantity ingredient in recipe.ingredients)
            {
                InventoryManager.Instance.RemoveItem(ingredient.item, ingredient.quantity);
            }

            InventoryManager.Instance.AddItem(recipe.resultItem, 1);//Belki ileride buraya sayi girmek yerine metoddan parametre isteriz

            InventoryUI.Instance.UpdateInventoryUI();
            InventoryUI.Instance.UpdateCraftingUI();//maybe i delete this line 
        }
        else
        {
            Debug.Log("Yeterli malzeme yok fakir git çalýþ"); //burasi gelistirilecek
        }
    }



    //                     craft recipe        /       inventory
    public bool CanCraft(CraftingRecipe recipe, Dictionary<Item, int> inventory)
    {
        //recipe.ingredients daki her bir ingredient icin kontrol et
        foreach (ItemAndQuantity ingredient in recipe.ingredients)//her bir item icin sorgulayacak
        {
            //     istenen item envanterde yoksa               istenen itemdan yeterli sayida yoksa
            if (!inventory.ContainsKey(ingredient.item) || inventory[ingredient.item] < ingredient.quantity)
            {
                return false; //buraya sonradan eksik itemlari gosterecek bir sistem yapilabilir
            }
        }
        return true;//tum sorgulamalardan tamam sekilde ciktiysa demek ki envanterde yeri var
    }
   
}