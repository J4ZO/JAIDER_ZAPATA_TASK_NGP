using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorItemUI : MonoBehaviour
{
    public Image imageChild;
    public int idObject;
    public TMP_Text amountChild;
    public string nameObject;
    public string descriptionObject;
    // public ItemType itemType;
    public bool HasItem => imageChild != null && imageChild.sprite != null;
    
    // public void InsertData(int id, string nameData, Sprite sprite, int amount, string description,ItemType type )
    // {
    //     idObject = id;
    //     imageChild.sprite = sprite;
    //     amountChild.text = amount.ToString();
    //     nameObject = nameData;
    //     descriptionObject = description;
    //     // itemType = type;
    // }

    public void DeleteData()
    {
        Color color = imageChild.color;
        color.a = Mathf.Clamp01(0);
        imageChild.color = color;
        imageChild.sprite = null;
        amountChild.text = "0";
        nameObject = "";
        descriptionObject = "";
        // itemType = ItemType.None;
    }

}
