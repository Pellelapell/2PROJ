using System.Collections.Generic;
using UnityEngine;

namespace SupKonQuest
{
    public class PlayerData : MonoBehaviour
    {
        public int playerId;
        public Race race;
        public int money = 100;

        [HideInInspector] public List<Camp> ownedCamps = new List<Camp>();

        public void AddMoney(int amount)
        {
            money += amount;
        }

        public bool SpendMoney(int amount)
        {
            if (money < amount) return false;
            money -= amount;
            return true;
        }
    }
}