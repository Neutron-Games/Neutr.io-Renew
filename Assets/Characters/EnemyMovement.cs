using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{
    public int range = 15;
    public float speed = 2f;
    NavMeshAgent agent;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity = 1f;
    bool notice;
    Animator animator;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = SetRandomPoint();
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isGameStart", true);
    }

    private void Update()
    {
        agent.speed = speed;
        if (Vector3.Distance(agent.destination, transform.position) < 1f && !notice)
        {
            transform.position = agent.destination;
            FindDestination();
        }
        else if (notice && Vector3.Distance(agent.destination, transform.position) < 2)
        {
            agent.isStopped = true;
            agent.ResetPath();
            Attack();
        }
        else
        {
            agent.isStopped = false;
        }
    }

    // *************** Script-in (Using in this script) Functions ********************
    Vector3 SetRandomPoint()
    {
        Vector3 randomPos = Random.insideUnitSphere * 15 + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, 15, NavMesh.AllAreas);
        return hit.position;
    }

    void FindDestination()
    {
        agent.isStopped = true;
        Vector3 nextPoint = SetRandomPoint();
        transform.LookAt(nextPoint);
        agent.isStopped = false;
        agent.destination = nextPoint;
    }

    void Attack()
    {

    }

    // *********************************************************************************


    // ************ Script-out (Using from another script) Functions *******************

    // ----> Notice Character -------------------
    public void NoticeHumanMove(Transform target)
    {
        notice = true;
        agent.ResetPath();
        agent.destination = target.position;
    }

    public void NoticeHumanExit(Transform target)
    {
        notice = false;
        agent.ResetPath();
        FindDestination();
    }
    // -------------------------------------------


    public void GetHit()
    {
        animator.SetTrigger("hit");
    }

    public void Death()
    {
        animator.SetTrigger("isDead");
        Destroy(gameObject, 1);
    }

    // **************************************************************************

}
