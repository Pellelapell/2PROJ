using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    int unitAttackDamage;
    float unitAttackSpeed;
    float unitAttackRange;
    List<GameObject> targetList;

    void Awake()
    {
        targetList = new List<GameObject>();
        UnitStats stats = GetComponent<UnitStats>();
        unitAttackDamage = stats.attackDamage;
        unitAttackSpeed = stats.attackSpeed;
        unitAttackRange = stats.attackRange;
    }

    void Update()
    {
        GetTarget();
    }


    GameObject GetTarget()
    {
        return null;
    }
}
