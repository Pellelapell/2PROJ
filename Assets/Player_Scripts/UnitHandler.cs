using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class UnitHandler : MonoBehaviour
{
    public NavMeshAgent agent;
    private bool selected;
    public UnitStats unitStats;
    private Transform target;
    private float nextTimeToAttack = 0f;
    private float attackRange;
    private float moveSpeed;

    void Awake()
    {
        attackRange = unitStats.attackRange;
        moveSpeed = unitStats.moveSpeed;
        agent = GetComponent<NavMeshAgent>(); 
    }

    void Update()
    {
        LeftClickToSelect();
        GetDestination(selected);
        GetTarget(selected);
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

    Transform GetDestination(bool selected)
    {
        if (selected && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                target = null;
                agent.isStopped = false;
                agent.SetDestination(hit.point);
                Debug.Log(target);
            }
        }
        return target;
    }

    bool GetTarget(bool selected)
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


    GameObject StopInRangeForAttack(Transform target)
    {
        if (target != null)
        {
            float distanceTilTarget = Vector3.Distance(transform.position, target.position);
            if (distanceTilTarget <= attackRange)
            {
                agent.isStopped = true;
                float attackInterval = 1f / Mathf.Max(unitStats.attackSpeed, 0.01f);
                if (Time.time >= nextTimeToAttack)
                {
                    nextTimeToAttack = Time.time + attackInterval;
                    Attack(target.gameObject);
                }
            }
        }
        return target?.gameObject;
    }

    void Attack(GameObject target)
    {
        Debug.Log("jattaque");
        target.GetComponent<UnitStats>().TakeDamage(unitStats.attackPower);
    }
}
