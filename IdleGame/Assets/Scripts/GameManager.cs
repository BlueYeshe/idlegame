using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Item[] referenceItems;
    public string[] itemsHeld;
    public int[] numberOfItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        LoadItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetItemDetails(string itemName)
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemName)
            {
                return referenceItems[i];
            }
        }
        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while(itemAfterSpace)
        {
            itemAfterSpace = false;

            for(int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if(itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if(itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void LoadItems()
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemAmount > 0)
            {
                itemsHeld[i] = referenceItems[i].itemName;
                numberOfItems[i] = referenceItems[i].itemAmount;
            }
        }
    }

    public void AddItem(string itemToAdd, int itemAmount)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        if(foundSpace)
        {
            bool itemExists = false;
            for(int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;
                    referenceItems[i].itemAmount += itemAmount;
                    i = referenceItems.Length;
                }
            }

            if(itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition] += itemAmount;
            }
            else
            {
                Debug.LogError("Item name does not exist in the database!");
            }
        }

        UIMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove, int itemAmount)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;
                i = itemsHeld.Length;
            }
        }

        if(foundItem)
        {
            numberOfItems[itemPosition] -= itemAmount;

            for(int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToRemove)
                {
                    referenceItems[i].itemAmount -= itemAmount;
                }
            }

            if(numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }

            UIMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError("Item name does not exist in inventory!");
        }
    }
}
