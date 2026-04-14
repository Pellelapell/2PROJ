using UnityEngine;

namespace SupKonQuest
{
    public class UnitStats : MonoBehaviour
    {
        [Header("Identity")]
        public int ownerId;
        public Race race;
        public UnitType unitType;

        public DamageType damagetype;

        [Header("Stats")]
        public int maxHealth = 100;
        public int currentHealth = 100;
        public int attackDamage = 10;
        public float attackSpeed = 1f;
        public float attackRange = 2f;
        public float moveSpeed = 4f;
        
        public float price;
        public float detectRange;
        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
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
}