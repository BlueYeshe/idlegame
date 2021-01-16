using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int money = 0;
    public int attackLevel = 1;
    public int strengthLevel = 1;
    public int defenceLevel = 1;
    public int woodcuttingLevel = 1;
    public int miningLevel = 1;
    public int currentHealth = 100;
    public int maxHealth = 100;
    public int maxHit = 18;
    public int hitChance = 20;
    private int maxLevel = 99;
    public int attackEXP = 0;
    public int strengthEXP = 0;
    public int defenceEXP = 0;
    public int woodcuttingEXP = 0;
    public int miningEXP = 0;
    public int[] expNextLevel;
    public int woodcuttingReduction = 0;
    public int miningReduction = 0;
    private int baseEXP = 83;
    private string hatchetType = "Bronze";
    private string pickaxeType = "Bronze";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        expNextLevel = new int[maxLevel];
        expNextLevel[1] = baseEXP;
        int increment = 2;

        for(int i = 2; i < expNextLevel.Length; i++)
        {
            expNextLevel[i] = (int) Percentage(expNextLevel[i - 1]) + expNextLevel[i - 1] + increment;
            increment++;
        }
        if(PlayerPrefs.HasKey("ExistingPlayer"))
        {
            LoadStats();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public double Percentage(int number)
    {
        double result = (double) number / 100 * 10.4;
        return result;
    }

    public void AddWoodcuttingExperience(int expToAdd)
    {
        woodcuttingEXP += expToAdd;

        if(woodcuttingEXP >= expNextLevel[woodcuttingLevel] && woodcuttingLevel < maxLevel)
        {
            woodcuttingEXP -= expNextLevel[woodcuttingLevel];
            woodcuttingLevel++;
        }
        if(woodcuttingLevel > maxLevel)
        {
            woodcuttingEXP = 0;
        }
    }

    public void AddMiningExperience(int expToAdd)
    {
        miningEXP += expToAdd;

        if(miningEXP >= expNextLevel[miningLevel] && miningLevel < maxLevel)
        {
            miningEXP -= expNextLevel[miningLevel];
            miningLevel++;
        }
        if(miningLevel > maxLevel)
        {
            miningEXP = 0;
        }
    }

    public void AddAttackExperience(int expToAdd)
    {
        attackEXP += expToAdd;

        if(attackEXP >= expNextLevel[attackLevel] && attackLevel < maxLevel)
        {
            attackEXP -= expNextLevel[attackLevel];
            attackLevel++;
        }
        if(attackLevel > maxLevel)
        {
            attackEXP = 0;
        }
    }

    public void AddStrengthExperience(int expToAdd)
    {
        strengthEXP += expToAdd;

        if(strengthEXP >= expNextLevel[strengthLevel] && strengthLevel < maxLevel)
        {
            strengthEXP -= expNextLevel[strengthLevel];
            strengthLevel++;
        }
        if(strengthLevel > maxLevel)
        {
            strengthEXP = 0;
        }
    }

    public void AddDefenceExperience(int expToAdd)
    {
        defenceEXP += expToAdd;

        if(defenceEXP >= expNextLevel[defenceLevel] && defenceLevel < maxLevel)
        {
            defenceEXP -= expNextLevel[defenceLevel];
            defenceLevel++;
        }
        if(defenceLevel > maxLevel)
        {
            defenceEXP = 0;
        }
    }

    public string GetHatchetType()
    {
        return hatchetType;
    }

    public string GetPickaxeType()
    {
        return pickaxeType;
    }

    public void SetHatchetType(string type)
    {
        if(type == "Bronze")
        {
            hatchetType = "Bronze";
        }
        else if(type == "Iron")
        {
            hatchetType = "Iron";
        }
        else if(type == "Gold")
        {
            hatchetType = "Gold";
        }
        else
        {
            Debug.LogError("You entered an invalid hatchet type.");
        }
    }

    public void SetPickaxeType(string type)
    {
        if(type == "Bronze")
        {
            pickaxeType = "Bronze";
        }
        else if(type == "Iron")
        {
            pickaxeType = "Iron";
        }
        else if(type == "Gold")
        {
            pickaxeType = "Gold";
        }
        else
        {
            Debug.LogError("You entered an invalid pickaxe type.");
        }
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("WoodcuttingLevel", woodcuttingLevel);
        PlayerPrefs.SetInt("WoodcuttingEXP", woodcuttingEXP);
        PlayerPrefs.SetInt("WoodcuttingReduction", woodcuttingReduction);
        PlayerPrefs.SetInt("MiningReduction", miningReduction);
        PlayerPrefs.SetInt("MiningLevel", miningLevel);
        PlayerPrefs.SetInt("MiningEXP", miningEXP);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetString("HatchetType", hatchetType);
        PlayerPrefs.SetString("PickaxeType", pickaxeType);
        PlayerPrefs.SetString("ExistingPlayer", "true");
        PlayerPrefs.SetInt("AttackLevel", attackLevel);
        PlayerPrefs.SetInt("AttackEXP", attackEXP);
        PlayerPrefs.SetInt("StrengthLevel", strengthLevel);
        PlayerPrefs.SetInt("StrengthEXP", strengthEXP);
        PlayerPrefs.SetInt("DefenceLevel", defenceLevel);
        PlayerPrefs.SetInt("DefenceEXP", defenceEXP);
        PlayerPrefs.SetInt("CurrentHealth", currentHealth);
        PlayerPrefs.SetInt("MaxHealth", maxHealth);
        PlayerPrefs.SetInt("MaxHit", maxHit);
        PlayerPrefs.SetInt("HitChance", hitChance);
    }

    public void LoadStats()
    {
        attackLevel = PlayerPrefs.GetInt("AttackLevel");
        attackEXP = PlayerPrefs.GetInt("AttackEXP");
        strengthLevel = PlayerPrefs.GetInt("StrengthLevel");
        strengthEXP = PlayerPrefs.GetInt("StrengthEXP");
        defenceLevel = PlayerPrefs.GetInt("DefenceLevel");
        defenceEXP = PlayerPrefs.GetInt("DefenceEXP");
        currentHealth = PlayerPrefs.GetInt("CurrentHealth");
        maxHealth = PlayerPrefs.GetInt("MaxHealth");
        maxHit = PlayerPrefs.GetInt("MaxHit");
        hitChance = PlayerPrefs.GetInt("HitChance");
        woodcuttingLevel = PlayerPrefs.GetInt("WoodcuttingLevel");
        woodcuttingEXP = PlayerPrefs.GetInt("WoodcuttingEXP");
        woodcuttingReduction = PlayerPrefs.GetInt("WoodcuttingReduction");
        miningReduction = PlayerPrefs.GetInt("MiningReduction");
        miningLevel = PlayerPrefs.GetInt("MiningLevel");
        miningEXP = PlayerPrefs.GetInt("MiningEXP");
        money = PlayerPrefs.GetInt("Money");
        hatchetType = PlayerPrefs.GetString("HatchetType");
        pickaxeType = PlayerPrefs.GetString("PickaxeType");
    }
}
