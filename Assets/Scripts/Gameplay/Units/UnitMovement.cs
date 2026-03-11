using UnityEngine;
using UnityEngine.AI;

namespace SupKonQuest
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        private NavMeshAgent agent;
        private UnitStats stats;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            stats = GetComponent<UnitStats>();
        }

        private void Start()
        {
            if (stats != null)
            {
                agent.speed = stats.moveSpeed;
            }
        }

        public void MoveTo(Vector3 destination)
        {
            agent.SetDestination(destination);
        }
    }
}