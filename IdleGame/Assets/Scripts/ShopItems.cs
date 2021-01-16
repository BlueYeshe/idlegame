using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{
    public GameObject[] itemPanels;
    public Image[] shopItemImages;
    public Text[] shopItemNames, shopItemPrices, levelRequirements, itemBonus;
    private bool refreshList = false;

    // Start is called before the first frame update
    void Start()
    {
        refreshList = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(refreshList)
        {
            refreshList = false;

            if(PlayerStats.instance.GetHatchetType() == "Bronze") // Bronze Hatchet
            {
                shopItemImages[0].sprite = Resources.Load<Sprite>("Sprites/ironhatchet");
                shopItemNames[0].text = "Iron Hatchet";
                shopItemPrices[0].text = "25000";
                levelRequirements[0].text = "20";
                itemBonus[0].text = "+5% Woodcutting Reduction Speed";
            }
            if(PlayerStats.instance.GetHatchetType() == "Iron") // Iron Hatchet
            {
                shopItemImages[0].sprite = Resources.Load<Sprite>("Sprites/goldhatchet");
                shopItemNames[0].text = "Gold Hatchet";
                shopItemPrices[0].text = "300000";
                levelRequirements[0].text = "40";
                itemBonus[0].text = "+5% Woodcutting Reduction Speed";
            }
            if(PlayerStats.instance.GetHatchetType() == "Gold") // Gold Hatchet
            {
                itemPanels[0].SetActive(false);
            }
            if(PlayerStats.instance.GetPickaxeType() == "Bronze") // Bronze Pickaxe
            {
                shopItemImages[1].sprite = Resources.Load<Sprite>("Sprites/ironpickaxe");
                shopItemNames[1].text = "Iron Pickaxe";
                shopItemPrices[1].text = "38000";
                levelRequirements[1].text = "20";
                itemBonus[1].text = "+5% Mining Reduction Speed";
            }
            if(PlayerStats.instance.GetPickaxeType() == "Iron") // Iron Pickaxe
            {
                shopItemImages[1].sprite = Resources.Load<Sprite>("Sprites/goldpickaxe");
                shopItemNames[1].text = "Gold Pickaxe";
                shopItemPrices[1].text = "450000";
                levelRequirements[1].text = "40";
                itemBonus[1].text = "+5% Mining Reduction Speed";
            }
            if(PlayerStats.instance.GetPickaxeType() == "Gold") // Gold Pickaxe
            {
                itemPanels[1].SetActive(false);
            }
        }
    }

    public void PurchaseItem(int itemID)
    {
        if(itemID == 0) // PurchaseButton 1
        {
            if(PlayerStats.instance.money >= int.Parse(shopItemPrices[0].text))
            {
                if(PlayerStats.instance.woodcuttingLevel >= int.Parse(levelRequirements[0].text))
                {
                    PlayerStats.instance.money -= int.Parse(shopItemPrices[0].text);
                    UIMenu.instance.coinAmount.text = "Gold Coins:\n" + PlayerStats.instance.money.ToString();

                    if(PlayerStats.instance.GetHatchetType() == "Bronze")
                    {
                        PlayerStats.instance.SetHatchetType("Iron");
                        PlayerStats.instance.woodcuttingReduction += 5;
                    }
                    else if(PlayerStats.instance.GetHatchetType() == "Iron")
                    {
                        PlayerStats.instance.SetHatchetType("Gold");
                        PlayerStats.instance.woodcuttingReduction += 5;
                    }

                    refreshList = true;
                    Skilling.instance.refreshValues = true;
                    Skilling.instance.refreshReductions = true;
                    PlayerStats.instance.SaveStats();
                }
                else
                {
                    Debug.LogError("Your woodcutting level is too low!");
                }
            }
            else
            {
                Debug.LogError("You do not have enough money!");
            }
        }
        else if(itemID == 1) // PurchaseButton 2
        {
            if(PlayerStats.instance.money >= int.Parse(shopItemPrices[1].text))
            {
                if(PlayerStats.instance.miningLevel >= int.Parse(levelRequirements[1].text))
                {
                    PlayerStats.instance.money -= int.Parse(shopItemPrices[1].text);
                    UIMenu.instance.coinAmount.text = "Gold Coins:\n" + PlayerStats.instance.money.ToString();

                    if(PlayerStats.instance.GetPickaxeType() == "Bronze")
                    {
                        PlayerStats.instance.SetPickaxeType("Iron");
                        PlayerStats.instance.miningReduction += 5;
                    }
                    else if(PlayerStats.instance.GetPickaxeType() == "Iron")
                    {
                        PlayerStats.instance.SetPickaxeType("Gold");
                        PlayerStats.instance.miningReduction += 5;
                    }

                    refreshList = true;
                    Skilling.instance.refreshValues = true;
                    Skilling.instance.refreshReductions = true;
                    PlayerStats.instance.SaveStats();
                }
                else
                {
                    Debug.LogError("Your mining level is too low!");
                }
            }
            else
            {
                Debug.LogError("You do not have enough money!");
            }
        }
    }
}
