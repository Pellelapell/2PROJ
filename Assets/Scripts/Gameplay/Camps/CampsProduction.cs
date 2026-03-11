using UnityEngine;

namespace SupKonQuest
{
    public class CampProduction : MonoBehaviour
    {
        [Header("References")]
        public Camp camp;
        public GameObject infantryPrefab;

        [Header("Production")]
        public int infantryCost = 25;

        private void Awake()
        {
            if (camp == null)
                camp = GetComponent<Camp>();
        }

        public void ProduceInfantry()
        {
            if (camp == null || camp.owner == null || infantryPrefab == null || camp.spawnPoint == null)
                return;

            if (!camp.owner.SpendMoney(infantryCost))
            {
                Debug.Log("Not enough money");
                return;
            }

            GameObject unitObj = Instantiate(infantryPrefab, camp.spawnPoint.position, Quaternion.identity);

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