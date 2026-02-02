using UnityEngine;
using UnityEngine.AI;

public class UnitHandler : MonoBehaviour
{
    public NavMeshAgent agent;
    private bool selected;
    private float moveSpeed;
    private float attackRange;
    private Transform target;

    void Awake()
    {
        attackRange = GetComponent<UnitStats>().attackRange;
        moveSpeed = GetComponent<UnitStats>().moveSpeed;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    void Update()
    {
        LeftClickToSelect();
        DefineDestination(selected);
        DefineTarget(selected);
        StopInRangeForAttack(target);
    }

    bool LeftClickToSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    GetComponent<Renderer>().material.color = Color.green;
                    selected = true;
                    Debug.Log("Objet sélectionné");
                }
                else
                {
                    GetComponent<Renderer>().material.color = Color.white;
                    selected = false;
                }
            }
        }
        return selected;
    }

    void DefineDestination(bool selected)
    {
        if (selected && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    bool DefineTarget(bool selected)
    {
        if (selected && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Cible ennemie sélectionnée : " + hit.transform.name);
                    target = hit.transform;
                }
            }
        }
        return target;    
    }

    void StopInRangeForAttack(Transform target)
    {
        if (target != null)
        {
            float distanceTilTarget = Vector3.Distance(transform.position, target.position);
            if (distanceTilTarget <= attackRange)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }
}
