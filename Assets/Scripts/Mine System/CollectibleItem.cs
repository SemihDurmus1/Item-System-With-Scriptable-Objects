using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public Item item;//This item is the scriptable object of the prefab, not prefabs itself
    public int quantity = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool addedItem = InventoryManager.Instance.AddItem(item, quantity);

            if (addedItem)
            {
                InventoryUI.Instance.UpdateInventoryUI();
                Destroy(gameObject);
            }
        }
    }
}