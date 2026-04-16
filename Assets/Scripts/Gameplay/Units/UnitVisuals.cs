using UnityEngine;
using SupKonQuest;

public class UnitVisuals : MonoBehaviour
{
    [Header("Visual Components")]
    private Renderer unitRenderer;
    private UnitStats stats;

    [Header("Health Bar")]
    [SerializeField] private GameObject healthBarPrefab;
    private HealthBar healthBar;

    private void Awake()
    {
        stats = GetComponent<UnitStats>();
        unitRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        if (healthBarPrefab != null)
        {
            GameObject hbObj = Instantiate(healthBarPrefab);
            healthBar = hbObj.GetComponent<HealthBar>();
            healthBar.Initialize(stats);
            stats.OnHealthChanged += healthBar.UpdateHealth;
        }
    }

    private void OnDestroy()
    {
        if (stats != null && healthBar != null)
            stats.OnHealthChanged -= healthBar.UpdateHealth;
    }

    public void ApplyRaceVisuals()
    {
        if (stats == null || unitRenderer == null) return;
        switch (stats.race)
        {
            case Race.Human:
                unitRenderer.material.color = Color.blue;
                break;
            case Race.Elf:
                unitRenderer.material.color = Color.green;
                break;
            case Race.Demon:
                unitRenderer.material.color = Color.red;
                break;
        }
    }
}