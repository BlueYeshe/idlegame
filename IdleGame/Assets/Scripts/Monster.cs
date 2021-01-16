using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    private string monsterName;
    private int level;
    private int currentHealth;
    private int maxHealth;
    private int maxHit;
    private int hitChance;

    public Monster(string monsterName, int level, int currentHealth, int maxHealth, int maxHit, int hitChance)
    {
        this.monsterName = monsterName;
        this.level = level;
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        this.maxHit = maxHit;
        this.hitChance = hitChance;
    }

    public string GetName()
    {
        return monsterName;
    }

    public void SetName(string monsterName)
    {
        this.monsterName = monsterName;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void SetHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public int GetMaxHit()
    {
        return maxHit;
    }

    public void SetMaxHit(int maxHit)
    {
        this.maxHit = maxHit;
    }

    public int GetHitChance()
    {
        return hitChance;
    }

    public void SetHitChance(int hitChance)
    {
        this.hitChance = hitChance;
    }

    public void attack(int damage)
    {
        PlayerStats.instance.currentHealth -= damage;
    }

    public void Start()
    {

    }

    public void Update()
    {

    }
}
