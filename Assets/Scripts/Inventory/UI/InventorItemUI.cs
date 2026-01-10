using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorItemUI : MonoBehaviour
{
    public Image imageChild;
    public TMP_Text amountChild;
    public string nameObject;
    public string descriptionObject;
    public void InsertData(int id, string name, Sprite sprite, int amount, string description )
    {
        imageChild.sprite = sprite;
        amountChild.text = amount.ToString();
        nameObject = name;
        descriptionObject = description;
    }

    public void DeleteData()
    {
        Color color = imageChild.color;
        color.a = Mathf.Clamp01(0);
        imageChild.color = color;
        imageChild.sprite = null;
        amountChild.text = "";
        nameObject = "";
        descriptionObject = "";
    }
}
