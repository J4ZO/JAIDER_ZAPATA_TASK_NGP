using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPrefab;
    [SerializeField] private RectTransform inventorySlotContainer;
    
    public List<InventorItemUI> listInventorySlotUI = new List<InventorItemUI>();

    public void InitialializeSlotUI(int invertorySize)
    {
        
        if (listInventorySlotUI != null)
        {
            foreach (InventorItemUI itemUI in listInventorySlotUI)
            {
                Destroy(itemUI.gameObject);
            }
        }
        for (int i = 0; i < invertorySize; i++)
        {
            InventorItemUI itemUI = Instantiate(inventoryPrefab, Vector3.zero, quaternion.identity)
                .GetComponent<InventorItemUI>();
            itemUI.gameObject.transform.SetParent(inventorySlotContainer);
            listInventorySlotUI.Add(itemUI);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
