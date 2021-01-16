using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int buttonValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        if(GameManager.instance.itemsHeld[buttonValue] != "")
        {
            UIMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
        }
        else
        {
            UIMenu.instance.itemName.text = "Item Name";
            UIMenu.instance.itemDescription.text = "Item Description";
            UIMenu.instance.itemValue.text = "Item Value";
            UIMenu.instance.activeItem = null;
        }
    }
}
