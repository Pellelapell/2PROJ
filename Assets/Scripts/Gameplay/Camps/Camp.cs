using UnityEngine;

namespace SupKonQuest
{
    public class Camp : MonoBehaviour
    {
        [Header("Camp")]
        public CampType campType;
        public bool isNeutral = false;

        [Header("Owner")]
        public PlayerData owner;

        [Header("Spawn")]
        public Transform spawnPoint;

        public void SetOwner(PlayerData newOwner)
        {
            if (owner == newOwner) return;

            if (owner != null)
            {
                owner.ownedCamps.Remove(this);
            }

            owner = newOwner;
            isNeutral = (newOwner == null);

            if (owner != null && !owner.ownedCamps.Contains(this))
            {
                owner.ownedCamps.Add(this);
            }

            UpdateCampVisual();
        }

        private void OnTriggerEnter(Collider other)
        {
            UnitStats unit = other.GetComponent<UnitStats>();
            if (unit == null) return;

            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm == null) return;

            PlayerData unitOwner = gm.GetPlayerById(unit.ownerId);
            if (unitOwner == null) return;

            if (owner == null || owner.playerId != unit.ownerId)
            {
                SetOwner(unitOwner);
                Debug.Log($"{name} captured by Player {unitOwner.playerId}");
            }
        }

        private void UpdateCampVisual()
        {
            Renderer rend = GetComponent<Renderer>();
            if (rend == null) return;

            if (owner == null)
            {
                rend.material.color = Color.gray;
                return;
            }

            switch (owner.race)
            {
                case Race.Human:
                    rend.material.color = Color.blue;
                    break;
                case Race.Elf:
                    rend.material.color = Color.green;
                    break;
                case Race.Demon:
                    rend.material.color = Color.red;
                    break;
            }
        }
    }
}