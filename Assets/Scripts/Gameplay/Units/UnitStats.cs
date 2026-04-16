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

        [Header("Other")]
        public float price;
        public float detectRange;

        [Header("UI")]
        public GameObject healthBarPrefab;

        private UnitHealthBarUI spawnedHealthBar;

        private void Start()
        {
            currentHealth = maxHealth;
            CreateHealthBar();
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Max(currentHealth, 0);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void CreateHealthBar()
        {
            if (healthBarPrefab == null)
            {
                Debug.LogWarning("HealthBar prefab is missing on " + gameObject.name);
                return;
            }

            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                Debug.LogWarning("No Canvas found in scene for health bars");
                return;
            }

            GameObject barObj = Instantiate(healthBarPrefab, canvas.transform);

            UnitHealthBarUI bar = barObj.GetComponent<UnitHealthBarUI>();
            if (bar == null)
            {
                Debug.LogWarning("UnitHealthBarUI component missing on health bar prefab");
                return;
            }

            spawnedHealthBar = bar;
            spawnedHealthBar.Initialize(this);
        }

        private void Die()
        {
            if (spawnedHealthBar != null)
            {
                Destroy(spawnedHealthBar.gameObject);
            }

            Destroy(gameObject);
        }
    }
}