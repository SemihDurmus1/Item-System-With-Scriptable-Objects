using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Animator) )]
public class MineableObject : MonoBehaviour, IMineable
{
    //Object Properties
    [SerializeField] public string objectName;
    [SerializeField] public int durability;//Dayaniklilik degeri
    private int currentDurability;
    public bool IsDepleted { get; set; }//Deplete = tukenmek

    private Animator animator;

    //Itemdan ıtema degistirilebilen ozellikler
    [SerializeField] private List<Item> listDropItems;
    [SerializeField] private int minDrop = 3;
    [SerializeField] private int maxDrop = 6;

    [SerializeField] private float objectSpreadAmount = 1f;

    private void Start()
    {
        currentDurability = durability;
        IsDepleted = false;
        animator = GetComponent<Animator>();
    }

    public void Mine()
    {
        if (IsDepleted) { return; }

        animator.SetTrigger("GotHit");

        currentDurability--;

        if (currentDurability <= 0)
        {
            IsDepleted = true;

            DropItems();

            //Buraya obje patladiktan sonrasinda itemlar dusmesi için metot yazilacaktir


            gameObject.SetActive(false); // Veya tükenmiş durum için başka bir mantık
        }
    }

    void DropItems()
    {
        foreach (Item collectableItem in listDropItems)
        {
            int dropAmount = Random.Range(minDrop, maxDrop + 1);//buraya +1 eklemenin sebebi, max Random degeri 6 ise 5 olarak aliyor

            for (int i = 0; i < dropAmount; i++)
            {
                //Amount bilgisinden sonra, her obje icin random bir pozisyon belirleyen kod blogu
                Vector3 dropPosition = transform.position +
                new Vector3(Random.Range(-objectSpreadAmount, objectSpreadAmount),//X
                            Random.Range(-objectSpreadAmount, objectSpreadAmount) );//Y
                //Random pozisyon ile beraber ilgili itemi Instantiate eder
                Instantiate(collectableItem.itemPrefab, dropPosition, Quaternion.identity);
            }
        }
    }

    //Bu method silinebilir
    public void Respawn()
    {
        IsDepleted = false;
        currentDurability = durability;
        gameObject.SetActive(true); // Veya yeniden doğma için başka bir mantık
        Debug.Log(objectName + " has respawned.");
    }
}