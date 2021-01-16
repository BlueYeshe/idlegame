using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skilling : MonoBehaviour
{
    public static Skilling instance;
    public Slider[] skillSliders;
    public Slider playerHealth;
    private float skillTimer;
    private float fightTimer;
    public Button[] harvestButtons;
    public int skillActive = -1;
    public Text[] expNextLevel;
    public Text[] toolTypes;
    public Text woodcuttingLevel, miningLevel, attackLevel, strengthLevel, defenceLevel;
    public Text[] woodcuttingPerSecond, miningPerSecond;
    private float evergreenSpeed, pineSpeed, birchSpeed;
    private float ironSpeed, coalSpeed, goldSpeed;
    public GameObject[] skillPanels, unlockPanels;
    public bool refreshValues = false;
    public bool refreshReductions = false;
    public bool inCombat = false;

    public GameObject newMonster;
    public GameObject monster;
    Monster monsterInfo;
    private bool monsterTurn, playerTurn;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        skillTimer = 0f;
        CheckPanels();
        skillActive = -1;

        evergreenSpeed = 3.0f;
        pineSpeed = 4.0f;
        birchSpeed = 5.0f;

        ironSpeed = 3.2f;
        coalSpeed = 4.5f;
        goldSpeed = 6.0f;

        refreshValues = true;
        refreshReductions = true;

        inCombat = false;
}

    // Update is called once per frame
    void Update()
    {
        if(refreshValues == true)
        {
            refreshValues = false;

            expNextLevel[0].text = PlayerStats.instance.woodcuttingEXP + "/" + PlayerStats.instance.expNextLevel[PlayerStats.instance.woodcuttingLevel]; // Woodcutting
            woodcuttingLevel.text = "Level " + PlayerStats.instance.woodcuttingLevel.ToString();

            expNextLevel[1].text = PlayerStats.instance.miningEXP + "/" + PlayerStats.instance.expNextLevel[PlayerStats.instance.miningLevel]; // Mining
            miningLevel.text = "Level " + PlayerStats.instance.miningLevel.ToString();

            attackLevel.text = "Level " + PlayerStats.instance.attackLevel.ToString(); // Attack
            strengthLevel.text = "Level " + PlayerStats.instance.strengthLevel.ToString(); // Strength
            defenceLevel.text = "Level " + PlayerStats.instance.defenceLevel.ToString(); // Defence

            toolTypes[0].text = PlayerStats.instance.GetHatchetType(); // Woodcutting Hatchet
            toolTypes[1].text = PlayerStats.instance.GetPickaxeType(); // Mining Pickaxe

            playerHealth.maxValue = PlayerStats.instance.maxHealth; // Player Health
            playerHealth.value = PlayerStats.instance.currentHealth;

            if (refreshReductions)
            {
                refreshReductions = false;
                LoadWoodcuttingReduction();
                LoadMiningReduction();

                woodcuttingPerSecond[0].text = evergreenSpeed.ToString("F2") + " Seconds";
                woodcuttingPerSecond[1].text = pineSpeed.ToString("F2") + " Seconds";
                woodcuttingPerSecond[2].text = birchSpeed.ToString("F2") + " Seconds";

                miningPerSecond[0].text = ironSpeed.ToString("F2") + " Seconds";
                miningPerSecond[1].text = coalSpeed.ToString("F2") + " Seconds";
                miningPerSecond[2].text = goldSpeed.ToString("F2") + " Seconds";
            }

            CheckPanels();
        }

        if(skillActive > -1)
        {
            if (skillActive == 0) // Evergreen Tree
            {
                if(skillTimer <= evergreenSpeed)
                {
                    skillTimer += Time.deltaTime;
                    skillSliders[0].maxValue = evergreenSpeed;
                    skillSliders[skillActive].value = skillTimer;
                }
                else
                {
                    skillTimer = 0f;
                    skillSliders[0].value = 0f;
                    GameManager.instance.AddItem("Evergreen Log", 1);
                    PlayerStats.instance.AddWoodcuttingExperience(10);
                    PlayerStats.instance.SaveStats();
                    refreshValues = true;
                }
            }
            if(skillActive == 1) // Pine Tree
            {
                if(skillTimer <= pineSpeed)
                {
                    skillTimer += Time.deltaTime;
                    skillSliders[1].maxValue = pineSpeed;
                    skillSliders[skillActive].value = skillTimer;
                }
                else
                {
                    skillTimer = 0f;
                    skillSliders[1].value = 0f;
                    GameManager.instance.AddItem("Pine Log", 1);
                    PlayerStats.instance.AddWoodcuttingExperience(15);
                    PlayerStats.instance.SaveStats();
                    refreshValues = true;
                }
            }
            if(skillActive == 2) // Birch Tree
            {
                if(skillTimer <= birchSpeed)
                {
                    skillTimer += Time.deltaTime;
                    skillSliders[2].maxValue = birchSpeed;
                    skillSliders[skillActive].value = skillTimer;
                }
                else
                {
                    skillTimer = 0f;
                    skillSliders[2].value = 0f;
                    GameManager.instance.AddItem("Birch Log", 1);
                    PlayerStats.instance.AddWoodcuttingExperience(22);
                    PlayerStats.instance.SaveStats();
                    refreshValues = true;
                }
            }
            if(skillActive == 3) // Iron Rock
            {
                if(skillTimer <= ironSpeed)
                {
                    skillTimer += Time.deltaTime;
                    skillSliders[3].maxValue = ironSpeed;
                    skillSliders[skillActive].value = skillTimer;
                }
                else
                {
                    skillTimer = 0f;
                    skillSliders[3].value = 0f;
                    GameManager.instance.AddItem("Iron Ore", 1);
                    PlayerStats.instance.AddMiningExperience(10);
                    PlayerStats.instance.SaveStats();
                    refreshValues = true;
                }
            }
            if(skillActive == 4) // Coal Rock
            {
                if(skillTimer <= coalSpeed)
                {
                    skillTimer += Time.deltaTime;
                    skillSliders[4].maxValue = coalSpeed;
                    skillSliders[skillActive].value = skillTimer;
                }
                else
                {
                    skillTimer = 0f;
                    skillSliders[4].value = 0f;
                    GameManager.instance.AddItem("Coal Ore", 1);
                    PlayerStats.instance.AddMiningExperience(15);
                    PlayerStats.instance.SaveStats();
                    refreshValues = true;
                }
            }
            if(skillActive == 5) // Gold Rock
            {
                if(skillTimer <= goldSpeed)
                {
                    skillTimer += Time.deltaTime;
                    skillSliders[5].maxValue = goldSpeed;
                    skillSliders[skillActive].value = skillTimer;
                }
                else
                {
                    skillTimer = 0f;
                    skillSliders[5].value = 0f;
                    GameManager.instance.AddItem("Gold Ore", 1);
                    PlayerStats.instance.AddMiningExperience(22);
                    PlayerStats.instance.SaveStats();
                    refreshValues = true;
                }
            }
            if(skillActive == 6) // Fluffy Combat
            {
                fightTimer += Time.deltaTime;

                if(inCombat)
                {
                    inCombat = false;
                    newMonster = Instantiate(monster);
                    monsterInfo = newMonster.GetComponent<Monster>();

                    monsterInfo.SetName("Fluffy");
                    monsterInfo.SetLevel(7);
                    monsterInfo.SetMaxHealth(100);
                    monsterInfo.SetHealth(100);
                    monsterInfo.SetMaxHit(16);
                    monsterInfo.SetHitChance(35);
                    PlayerStats.instance.hitChance = 35;
                }
                if(PlayerStats.instance.currentHealth < 1 || monsterInfo.GetHealth() < 1)
                {
                    skillActive = -1;
                    DestroyImmediate(newMonster);
                    monsterInfo = null;
                    harvestButtons[6].GetComponentInChildren<Text>().text = "Fight";
                    inCombat = false;
                    playerTurn = false;
                    monsterTurn = false;
                    fightTimer = 0f;
                    PlayerStats.instance.currentHealth = PlayerStats.instance.maxHealth;
                    refreshValues = true;
                    skillSliders[6].value = 100;
                    skillSliders[6].maxValue = 100;
                    return;
                }
                if(fightTimer >= 3f)
                {
                    int hit = (int)ComparePercentage(monsterInfo.GetHitChance(), PlayerStats.instance.defenceLevel);
                    monsterInfo.SetHitChance(monsterInfo.GetHitChance() - hit);

                    int playerHit = (int)ComparePercentage(PlayerStats.instance.hitChance, monsterInfo.GetLevel());
                    PlayerStats.instance.hitChance = 35 - playerHit;

                    BeginCombat();
                }
            }
        }
    }

    public void ActivateSkill(int buttonID)
    {
        if(buttonID == 0) // Evergreen Tree
        {
            if(skillActive > -1 && skillActive != 0)
            {
                Debug.LogError("You can only train one skill at a time.");
                return;
            }
            if(skillActive == -1 && skillActive != 0)
            {
                skillActive = 0;
                harvestButtons[0].GetComponentInChildren<Text>().text = "Stop";
            }
            else
            {
                skillActive = -1;
                harvestButtons[0].GetComponentInChildren<Text>().text = "Cut";
                skillTimer = 0f;
                skillSliders[0].value = 0f;
            }
        }
        if(buttonID == 1) // Pine Tree
        {
            if(skillActive > -1 && skillActive != 1)
            {
                Debug.LogError("You can only train one skill at a time.");
                return;
            }
            if(skillActive == -1 && skillActive != 1)
            {
                skillActive = 1;
                harvestButtons[1].GetComponentInChildren<Text>().text = "Stop";
            }
            else
            {
                skillActive = -1;
                harvestButtons[1].GetComponentInChildren<Text>().text = "Cut";
                skillTimer = 0f;
                skillSliders[1].value = 0f;
            }
        }
        if(buttonID == 2) // Birch Tree
        {
            if(skillActive > -1 && skillActive != 2)
            {
                Debug.LogError("You can only train one skill at a time.");
                return;
            }
            if(skillActive == -1 && skillActive != 2)
            {
                skillActive = 2;
                harvestButtons[2].GetComponentInChildren<Text>().text = "Stop";
            }
            else
            {
                skillActive = -1;
                harvestButtons[2].GetComponentInChildren<Text>().text = "Cut";
                skillTimer = 0f;
                skillSliders[2].value = 0f;
            }
        }
        if(buttonID == 3) // Iron Rock
        {
            if(skillActive > -1 && skillActive != 3)
            {
                Debug.LogError("You can only train one skill at a time.");
                return;
            }
            if(skillActive == -1 && skillActive != 3)
            {
                skillActive = 3;
                harvestButtons[3].GetComponentInChildren<Text>().text = "Stop";
            }
            else
            {
                skillActive = -1;
                harvestButtons[3].GetComponentInChildren<Text>().text = "Mine";
                skillTimer = 0f;
                skillSliders[3].value = 0f;
            }
        }
        if(buttonID == 4) // Coal Rock
        {
            if(skillActive > -1 && skillActive != 4)
            {
                Debug.LogError("You can only train one skill at a time.");
                return;
            }
            if(skillActive == -1 && skillActive != 4)
            {
                skillActive = 4;
                harvestButtons[4].GetComponentInChildren<Text>().text = "Stop";
            }
            else
            {
                skillActive = -1;
                harvestButtons[4].GetComponentInChildren<Text>().text = "Mine";
                skillTimer = 0f;
                skillSliders[4].value = 0f;
            }
        }
        if(buttonID == 5) // Gold Rock
        {
            if (skillActive > -1 && skillActive != 5)
            {
                Debug.LogError("You can only train one skill at a time.");
                return;
            }
            if (skillActive == -1 && skillActive != 5)
            {
                skillActive = 5;
                harvestButtons[5].GetComponentInChildren<Text>().text = "Stop";
            }
            else
            {
                skillActive = -1;
                harvestButtons[4].GetComponentInChildren<Text>().text = "Mine";
                skillTimer = 0f;
                skillSliders[5].value = 0f;
            }
        }
        if(buttonID == 6) // Fluffy Combat
        {
            if(skillActive > -1 && skillActive != 6)
            {
                Debug.LogError("You can only train one skill at a time.");
                return;
            }
            if(skillActive == -1 && skillActive != 6)
            {
                skillActive = 6;
                inCombat = true;
                harvestButtons[6].GetComponentInChildren<Text>().text = "Stop";
            }
            else
            {
                skillActive = -1;
                DestroyImmediate(newMonster);
                monsterInfo = null;
                harvestButtons[6].GetComponentInChildren<Text>().text = "Fight";
                inCombat = false;
                playerTurn = false;
                monsterTurn = false;
                fightTimer = 0f;
                skillSliders[6].value = 100;
                skillSliders[6].maxValue = 100;
            }
        }
    }

    public void CheckPanels()
    {
        if(PlayerStats.instance.woodcuttingLevel >= 10) // Unlock Pine Tree
        {
            skillPanels[0].SetActive(true);
            unlockPanels[0].SetActive(false);

            if(PlayerStats.instance.woodcuttingLevel >= 25) // Unlock Birch Tree
            {
                skillPanels[1].SetActive(true);
                unlockPanels[1].SetActive(false);
            }
        }
        if(PlayerStats.instance.miningLevel >= 10) // Unlock Iron Rock
        {
            skillPanels[2].SetActive(true);
            unlockPanels[2].SetActive(false);

            if (PlayerStats.instance.miningLevel >= 25) // Unlock Gold Rock
            {
                skillPanels[3].SetActive(true);
                unlockPanels[3].SetActive(false);
            }
        }
    }

    public double ComparePercentage(float number, float percentvalue)
    {
        double result = (double) number / 100 * percentvalue;
        return result;
    }

    public void LoadWoodcuttingReduction()
    {
        evergreenSpeed -= (float) ComparePercentage(evergreenSpeed, PlayerStats.instance.woodcuttingReduction);
        pineSpeed -= (float) ComparePercentage(pineSpeed, PlayerStats.instance.woodcuttingReduction);
        birchSpeed -= (float) ComparePercentage(birchSpeed, PlayerStats.instance.woodcuttingReduction);
    }

    public void LoadMiningReduction()
    {
        ironSpeed -= (float)ComparePercentage(ironSpeed, PlayerStats.instance.miningReduction);
        coalSpeed -= (float)ComparePercentage(coalSpeed, PlayerStats.instance.miningReduction);
        goldSpeed -= (float)ComparePercentage(goldSpeed, PlayerStats.instance.miningReduction);
    }

    public void BeginCombat()
    {
        if(PlayerStats.instance.attackLevel <= monsterInfo.GetLevel())
        {
            if(playerTurn == false)
            {
                monsterTurn = true;
            }
        }
        else
        {
            if(monsterTurn == false)
            {
                playerTurn = true;
            }
        }

        System.Random random = new System.Random();
        int randomNumber = random.Next(1, 100);

        if(monsterTurn)
        {
            monsterTurn = false;
            int attackHit = random.Next(1, monsterInfo.GetMaxHit());
            if(randomNumber <= monsterInfo.GetHitChance())
            {
                monsterInfo.attack(attackHit);
                Debug.Log("You were hit for " + attackHit + " damage.");
                playerTurn = true;
                refreshValues = true;
                fightTimer = 0f;
                PlayerStats.instance.SaveStats();
            }
            else
            {
                monsterInfo.attack(0);
                Debug.Log("You were hit for 0 damage.");
                playerTurn = true;
                refreshValues = true;
                fightTimer = 0f;
                PlayerStats.instance.SaveStats();
            }
            return;
        }

        if(playerTurn)
        {
            playerTurn = false;
            int attackHit = random.Next(1, PlayerStats.instance.maxHit);
            if(randomNumber <= PlayerStats.instance.hitChance)
            {
                monsterInfo.SetHealth(monsterInfo.GetHealth() - attackHit);
                Debug.Log("You hit " + monsterInfo.GetName() + " for " + attackHit + " damage.");
                skillSliders[6].maxValue = monsterInfo.GetMaxHealth(); // Fluffy Health
                skillSliders[6].value = monsterInfo.GetHealth();
                monsterTurn = true;
                fightTimer = 0f;
            }
            else
            {
                Debug.Log("You hit " + monsterInfo.GetName() + " for 0 damage.");
                monsterTurn = true;
                fightTimer = 0f;
            }
            return;
        }
    }
}
