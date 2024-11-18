using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;//Singleton

    public List<InventorySlot> inventorySlots = new();
    public int maxSlots = 20;

    [SerializeField] GameObject slotPrefab;

    private void Awake()//Singleton
    {
        if (Instance == null)
        {
            Instance = this;
            InventorySlot.slotPrefab = slotPrefab;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddItem(Item item, int quantity)
    {
        if (item.isStackable)//Item stackable ise ekle ve return et
        {
            //Bu kod bir LINQ isaretidir, InventorySlottaki ayni turde olan slotu(s) bulur ve ona ekleme yapar
            InventorySlot slot = inventorySlots.Find(s => s.item == item);
            if (slot != null)//Turun slotu envanterde varsa
            {
                slot.quantity += quantity;
                return true;
            }
        }

        if (inventorySlots.Count < maxSlots)//Inventorydeki slotlar, siniri asmamis ise yeni slot ekle
        {
            inventorySlots.Add(new InventorySlot(item, quantity));
            return true;
        }

        //hicbir durumda basarili olamadiysa
        return false;
    }

    public Dictionary<Item, int> GetInventoryDictionary()
    {
        Dictionary<Item, int> inventoryDict = new();
        foreach (var slot in inventorySlots) 
        {
            if (inventoryDict.ContainsKey(slot.item))
            {
                inventoryDict[slot.item] += slot.quantity;
            }
            else
            {
                inventoryDict[slot.item] = slot.quantity;
            }
        }

        return inventoryDict;
    }

    public bool HasItem(Item item, int quantity)//Bir itemin envanterde varligini kontrol eder
    {
        //s ifadesi inventorySlotstaki her bir ogeyi temsil eder
        //s.item ise, belirli ogenin, bizim item ile ayni mi diye kontrol eder
        InventorySlot slot = inventorySlots.Find(s => s.item == item);
        return slot != null && slot.quantity >= quantity;
    }

    public void RemoveItem(Item item, int quantity)
    {
        InventorySlot slot = inventorySlots.Find(s => s.item == item);
        if (slot != null)
        {
            slot.quantity -= quantity;
            if (slot.quantity <= 0)
            {
                inventorySlots.Remove(slot);
            }
        }
    }


}

[System.Serializable]
public class InventorySlot
{
    public static GameObject slotPrefab;
    public Item item;
    public int quantity;

    public InventorySlot(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}