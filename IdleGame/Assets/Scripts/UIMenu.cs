using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UIMenu : MonoBehaviour
{
    public static UIMenu instance;
    public GameObject mainMenu;
    public GameObject[] menuButtons;
    public GameObject[] windows;
    public ItemButton[] itemButtons;
    public Item activeItem;
    public string selectedItem;
    public Text itemName, itemDescription, itemValue, coinAmount;
    public Button[] itemUsageButtons;
    public Text inputHeader, messageBox;
    public GameObject inputPanel;
    public InputField inputBox;
    public Image inputItemImage;
    public Button inputNegative, inputPositive;
    public bool useInputImage;
    private bool dialogOpen;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for(int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
            menuButtons[i].GetComponentInChildren<Text>().color = Color.white;
            windows[2].SetActive(true);
            menuButtons[2].GetComponentInChildren<Text>().color = Color.red;
            mainMenu.SetActive(true);

        }
        inputPanel.SetActive(false);
        dialogOpen = false;
        coinAmount.text = "Gold Coins:\n" + PlayerStats.instance.money.ToString();
        itemUsageButtons[0].interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleWindow(int windowNumber)
    {
        if(!dialogOpen)
        {
            for(int i = 0; i < windows.Length; i++)
            {
                if(windows[i].gameObject.activeInHierarchy)
                {
                    windows[i].SetActive(false);
                    menuButtons[i].GetComponentInChildren<Text>().color = Color.white;
                }
                if(!windows[windowNumber].gameObject.activeInHierarchy)
                {
                    windows[windowNumber].SetActive(true);
                    menuButtons[windowNumber].GetComponentInChildren<Text>().color = Color.red;
                }
            }
        }
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if(GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;
        if(!activeItem.isInteractable)
        {
            itemUsageButtons[0].interactable = false;
        }
        else
        {
            itemUsageButtons[0].interactable = true;
        }
        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.itemDescription;
        itemValue.text = "Sell Value: " + activeItem.itemValue.ToString() + " Gold Coins";
    }

    public void SellItem()
    {
        if(activeItem != null)
        {
            if(activeItem.itemAmount > 1)
            {
                useInputImage = true;
                inputItemImage.sprite = activeItem.itemSprite;
                inputNegative.onClick.AddListener(CloseInputBox);
                inputPositive.onClick.AddListener(ConfirmSellQuantity);
                ShowInputBox("Confirm Item Sell Quantity",
                       "How many " + activeItem.itemName
                       + " item(s) do you wish to sell?\n" +
                       "You own " + activeItem.itemAmount + " " + activeItem.itemName + " item(s).");
                return;
            }

            PlayerStats.instance.money += activeItem.itemValue;
            GameManager.instance.RemoveItem(activeItem.itemName, 1);
            PlayerStats.instance.SaveStats();
            coinAmount.text = "Gold Coins:\n" + PlayerStats.instance.money.ToString();

            if(activeItem.itemAmount < 1)
            {
                activeItem = null;
                itemName.text = "Item Name";
                itemDescription.text = "Item Description";
                itemValue.text = "Item Value";
            }
        }
    }

    public void ShowInputBox(string title, string message)
    {
        if(useInputImage)
        {
            inputItemImage.gameObject.SetActive(true);
        }
        else
        {
            inputItemImage.gameObject.SetActive(false);
        }
        inputHeader.text = title;
        messageBox.text = message;
        inputPanel.SetActive(true);
        dialogOpen = true;
    }

    public void CloseInputBox()
    {
        inputPanel.SetActive(false);
        dialogOpen = false;
        inputBox.text = "";
    }

    public void ConfirmSellQuantity()
    {
        if(inputBox != null)
        {
            int input;
            if(int.TryParse(inputBox.text, out input))
            {
                if(input > 0 && input < 99999)
                {
                    if(input <= activeItem.itemAmount)
                    {
                        PlayerStats.instance.money += activeItem.itemValue * input;
                        GameManager.instance.RemoveItem(activeItem.itemName, input);
                        PlayerStats.instance.SaveStats();
                        coinAmount.text = "Gold Coins:\n" + PlayerStats.instance.money.ToString();
                        CloseInputBox();

                        if(activeItem.itemAmount < 1)
                        {
                            activeItem = null;
                            itemName.text = "Item Name";
                            itemDescription.text = "Item Description";
                            itemValue.text = "Item Value";
                        }
                    }
                    else
                    {
                        Debug.LogError("You don't have that many!");
                    }
                }
                else
                {
                    Debug.LogError("You entered wrong input usage. 1-99999");
                }
            }
            else
            {
                Debug.LogError("Text to int parsing failed.");
            }
        }
    }

    public void UseItem()
    {
        if(activeItem != null)
        {
            if(activeItem.itemName == "Health Potion")
            {
                GameManager.instance.RemoveItem(activeItem.itemName, 1);
                PlayerStats.instance.currentHealth = PlayerStats.instance.maxHealth;
                Debug.Log("You use a Health Potion and heal 100%.");

                Skilling.instance.playerHealth.maxValue = PlayerStats.instance.maxHealth; // Player Health
                Skilling.instance.playerHealth.value = PlayerStats.instance.currentHealth;

            }
            if(activeItem.itemAmount < 1)
            {
                activeItem = null;
                itemName.text = "Item Name";
                itemDescription.text = "Item Description";
                itemValue.text = "Item Value";
            }
            ShowItems();
            PlayerStats.instance.SaveStats();
        }
        else
        {
            Debug.LogError("No item active.");
        }
    }
}
