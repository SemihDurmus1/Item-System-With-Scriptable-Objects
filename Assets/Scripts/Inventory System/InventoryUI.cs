using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;//Singleton

    public GameObject inventoryPanel;
    public GameObject craftingPanel;

    private void OnEnable()
    {
        UpdateInventoryUI();
    }

    private void Awake()//Singleton
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //activeSelf propertiesi, bir nesnenin aktiflik durumunu cevirir, ! ile de tersini alip setleriz
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);

            // Panel aktif olduðunda envanteri güncelliyoruz
            if (inventoryPanel.activeSelf) { UpdateInventoryUI(); }
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            craftingPanel.SetActive(!craftingPanel.activeSelf);

            if (craftingPanel.activeSelf) { UpdateCraftingUI(); }
        }
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var slot in InventoryManager.Instance.inventorySlots)//Envantere erisme
        {
            GameObject newSlot = Instantiate(InventorySlot.slotPrefab, inventoryPanel.transform);
            Image icon = newSlot.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI quantityText = newSlot.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();//Must convert TMPro

            icon.sprite = slot.item.icon;
            quantityText.text = slot.quantity.ToString();
        }
    }
    public void UpdateCraftingUI()//Her acilis ve kapanista tum objeleri silip yeniden Instantiate ediyor. Optimize edilmeli.
    {
        foreach (Transform child in craftingPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var recipe in CraftingManager.Instance.craftingRecipes)
        {
            GameObject craftSlot = Instantiate(CraftingManager.Instance.recipeSlotPrefab, craftingPanel.transform);

            //Prefabin iconu
            Image icon = craftSlot.transform.Find("Icon").GetComponent<Image>();

            //prefabin texti
            TextMeshProUGUI resultText = craftSlot.transform.Find("ResultText").GetComponent<TextMeshProUGUI>();

            //prefabin buttonu
            Button craftButton = craftSlot.transform.Find("CraftButton").GetComponent<Button>();

            //Ingredientslari gosterecek script
            TooltipTrigger tooltipTrigger = craftSlot.GetComponent<TooltipTrigger>();

            tooltipTrigger.header = recipe.resultItem.itemName;
            foreach (ItemAndQuantity ingredient in recipe.ingredients)//RecipeInfo'daki ingredientreferencesa item ekler
            {
                tooltipTrigger.ingredientReferences.Add(ingredient);
            }
            for (int i = 0; i < tooltipTrigger.ingredientReferences.Count; i++)
            {
                tooltipTrigger.content = tooltipTrigger.content + 
                                         tooltipTrigger.ingredientReferences[i].quantity + 
                                         " piece " + 
                                         tooltipTrigger.ingredientReferences[i].item + "\n";
            }
            


            icon.sprite = recipe.resultItem.icon;
            resultText.text = recipe.resultItem.itemName;

            craftButton.onClick.AddListener(() =>
            {
                CraftingManager.Instance.CraftItem(recipe);
            });
        }
    }
}