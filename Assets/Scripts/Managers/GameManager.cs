using UnityEngine;

namespace SupKonQuest
{
    public class GameManager : MonoBehaviour
    {
        public PlayerData[] players;
        public Camp[] camps;

        private void Start()
        {
            foreach (Camp camp in camps)
            {
                if (camp.owner != null && !camp.owner.ownedCamps.Contains(camp))
                {
                    camp.owner.ownedCamps.Add(camp);
                }
            }
        }

        public PlayerData GetPlayerById(int id)
        {
            foreach (PlayerData player in players)
            {
                if (player.playerId == id)
                    return player;
            }
            return null;
        }
    }
}