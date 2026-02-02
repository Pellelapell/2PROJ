using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int attackPower = 10;
    public float attackRange = 1.5f;
    public float moveSpeed = 5f;
    public int defense = 5;
    public int level = 1;
    public int experience = 0;
    public int experienceToNextLevel = 100;
}
