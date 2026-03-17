using UnityEngine;

namespace SupKonQuest
{
    public class CampProduction : MonoBehaviour
    {
        [Header("References")]
        public Camp camp;

        [Header("Unit Prefabs")]
        public GameObject infantryPrefab;
        public GameObject rangePrefab;
        public GameObject heavyPrefab;
        public GameObject transportPrefab;
        public GameObject frigatePrefab;
        public GameObject destroyerPrefab;

        [Header("Costs")]
        public int infantryCost = 25;
        public int rangeCost = 35;
        public int heavyCost = 50;
        public int transportCost = 40;
        public int frigateCost = 60;
        public int destroyerCost = 80;

        private void Awake()
        {
            if (camp == null)
                camp = GetComponent<Camp>();
        }

        public void Produce(UnitType type)
        {
            switch (type)
            {
                case UnitType.Infantry:
                    TryProduce(infantryPrefab, infantryCost);
                    break;
                case UnitType.Range:
                    TryProduce(rangePrefab, rangeCost);
                    break;
                case UnitType.Heavy:
                    TryProduce(heavyPrefab, heavyCost);
                    break;
                case UnitType.Transport:
                    TryProduce(transportPrefab, transportCost);
                    break;
                case UnitType.Frigate:
                    TryProduce(frigatePrefab, frigateCost);
                    break;
                case UnitType.Destroyer:
                    TryProduce(destroyerPrefab, destroyerCost);
                    break;
            }
        }

        private void TryProduce(GameObject prefab, int cost)
        {
            if (camp == null || camp.owner == null || prefab == null || camp.spawnPoint == null)
                return;

            if (!camp.owner.SpendMoney(cost))
            {
                Debug.Log("Not enough money");
                return;
            }

            GameObject unitObj = Instantiate(prefab, camp.spawnPoint.position, Quaternion.identity);

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
        }
    }
}