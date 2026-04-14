using System.Collections.Generic;
using UnityEngine;

namespace SupKonQuest
{
    public class CampProduction : MonoBehaviour
    {
        [System.Serializable]
        public class UnitProductionData
        {
            public UnitType unitType;
            public GameObject prefab;
            public int cost;
            public float buildTime;
        }

        [Header("References")]
        public Camp camp;

        [Header("Production Data")]
        public List<UnitProductionData> availableUnits = new List<UnitProductionData>();

        private readonly Queue<UnitProductionData> productionQueue = new Queue<UnitProductionData>();

        private UnitProductionData currentProduction;
        private float currentProductionTimer;
        private bool isProducing;

        private void Awake()
        {
            if (camp == null)
                camp = GetComponent<Camp>();
        }

        private void Update()
        {
            HandleProduction();
        }

        public void Produce(UnitType type)
        {
            if (camp == null || camp.owner == null)
                return;

            UnitProductionData data = GetProductionData(type);
            if (data == null)
            {
                Debug.LogWarning($"No production data found for unit type {type} on {name}");
                return;
            }

            if (!camp.owner.SpendMoney(data.cost))
            {
                Debug.Log("Not enough money");
                return;
            }

            productionQueue.Enqueue(data);
            Debug.Log($"{type} added to queue in {camp.name}. Queue size: {productionQueue.Count}");

            if (!isProducing)
            {
                StartNextProduction();
            }
        }

        private void HandleProduction()
        {
            if (!isProducing || currentProduction == null)
                return;

            currentProductionTimer -= Time.deltaTime;

            if (currentProductionTimer <= 0f)
            {
                SpawnUnit(currentProduction);
                StartNextProduction();
            }
        }

        private void StartNextProduction()
        {
            if (productionQueue.Count == 0)
            {
                isProducing = false;
                currentProduction = null;
                currentProductionTimer = 0f;
                return;
            }

            currentProduction = productionQueue.Dequeue();
            currentProductionTimer = currentProduction.buildTime;
            isProducing = true;

            Debug.Log($"Started producing {currentProduction.unitType} in {camp.name} ({currentProduction.buildTime}s)");
        }

        private void SpawnUnit(UnitProductionData data)
        {
            if (camp == null || camp.owner == null || camp.spawnPoint == null || data.prefab == null)
                return;

            GameObject unitObj = Instantiate(data.prefab, camp.spawnPoint.position, Quaternion.identity);

            UnitStats stats = unitObj.GetComponent<UnitStats>();
            if (stats != null)
            {
                stats.ownerId = camp.owner.playerId;
                stats.race = camp.owner.race;
            }

            UnitVisuals visuals = unitObj.GetComponent<UnitVisuals>();
            if (visuals != null)
            {
                visuals.ApplyRaceVisuals();
            }

            Debug.Log($"{data.unitType} spawned from {camp.name}");
        }

        private UnitProductionData GetProductionData(UnitType type)
        {
            foreach (UnitProductionData data in availableUnits)
            {
                if (data.unitType == type)
                    return data;
            }

            return null;
        }

        public int GetQueueCount()
        {
            return productionQueue.Count + (isProducing ? 1 : 0);
        }

        public bool IsProducing()
        {
            return isProducing;
        }

        public float GetCurrentProgress01()
        {
            if (!isProducing || currentProduction == null || currentProduction.buildTime <= 0f)
                return 0f;

            return 1f - (currentProductionTimer / currentProduction.buildTime);
        }

        public UnitType? GetCurrentUnitType()
        {
            if (!isProducing || currentProduction == null)
                return null;

            return currentProduction.unitType;
        }
    }
}