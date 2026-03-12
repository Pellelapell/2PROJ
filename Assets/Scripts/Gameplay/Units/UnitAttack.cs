using UnityEngine;
using UnityEngine.AI;

namespace SupKonQuest
{
    public class UnitAttack : MonoBehaviour
    {
        [Header("Detection")]
        [SerializeField] private LayerMask unitLayerMask;

        private NavMeshAgent agent;
        private UnitStats stats;
        private float attackCooldown;

        private void Awake()
        {
            stats = GetComponent<UnitStats>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (stats == null) return;

            if (attackCooldown > 0f)
                attackCooldown -= Time.deltaTime;

            UnitStats target = FindTarget();
            if (target == null) return;

            transform.LookAt(target.transform);

            if (attackCooldown <= 0f)
            {
                Attack(target);
                attackCooldown = 1f / Mathf.Max(0.01f, stats.attackSpeed);
            }
        }

        private UnitStats FindTarget()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, stats.detectRange, unitLayerMask);

            UnitStats closest = null;
            float closestDist = float.MaxValue;

            foreach (Collider hit in hits)
            {
                if (hit.gameObject == gameObject) continue;

                UnitStats other = hit.GetComponent<UnitStats>();
                if (other == null) continue;
                if (other.ownerId == stats.ownerId) continue;
                if (other.currentHealth <= 0) continue;

                float dist = Vector3.Distance(transform.position, other.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = other;
                }
            }

            return closest;
        }

        private void Attack(UnitStats target)
        {
            if (target == null) return;
            target.TakeDamage(stats.attackDamage);
        }

        private void OnDrawGizmosSelected()
        {
            UnitStats s = GetComponent<UnitStats>();
            float range = s != null ? s.attackRange : 2f;
        }
    }
}