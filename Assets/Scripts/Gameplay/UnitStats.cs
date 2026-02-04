using SupKonQuest;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [Header("identity")]
    public Race race;
    public UnitType unitType;

    [Header("health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("combat")]
    public int attackDamage = 10;
    public float attackSpeed = 1f;
    public float attackRange = 1.5f;

    [Header("movement")]
    public float moveSpeed = 5f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
