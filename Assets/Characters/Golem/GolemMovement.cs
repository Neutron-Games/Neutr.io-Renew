using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GolemMovement : MonoBehaviour
{
    public int range;
    public float speed;
    NavMeshAgent agent;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity = 1f;
    bool notice;
    Animator animator;
    Transform targetAgent;
    public GameObject prize;

    Transform GroundChecker;
    [SerializeField] LayerMask ground;


    private void Start()
    {
        GroundChecker = this.transform.GetChild(2).transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = SetRandomPoint();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Physics.CheckSphere(GroundChecker.position, 0.2f, ground))
        {
            agent.speed = speed;
            if (Vector3.Distance(agent.destination, transform.position) < 1f && !notice)
            {
                transform.position = agent.destination;
                animator.SetBool("attack", false);
                FindDestination();
            }
            else if (notice && Vector3.Distance(agent.destination, transform.position) < 1.5f)
            {
                agent.isStopped = true;
                agent.ResetPath();
                animator.SetBool("attack", true);
                StartCoroutine(Attack());
            }
            else
            {
                animator.SetBool("attack", false);
                // agent.isStopped = false;
            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(80);
        // targetAgent.GetComponent<Score>().score -= 25;
    }

    void FindDestination()
    {
        agent.isStopped = true;
        Vector3 nextPoint = SetRandomPoint();
        transform.LookAt(nextPoint);
        agent.isStopped = false;
        agent.destination = nextPoint;
    }


    Vector3 SetRandomPoint()
    {
        Vector3 vector = transform.position;
        vector = transform.position + new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
        while (vector.x > 90 || vector.x < -90 || vector.y < -90 || vector.y > 90)
        {
            vector = transform.position + new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
        }
        return vector;
    }


    // Instance Methods
    public void NoticeHumanMove(Transform target)
    {
        agent.ResetPath();
        notice = true;
        targetAgent = target;
        agent.destination = target.position;
    }
    public void NoticeHumanExit(Transform target)
    {
        notice = false;
        targetAgent = null;
        agent.ResetPath();
        FindDestination();
    }
    public void Death()
    {
        Destroy(GetComponent<Rigidbody>());
        animator.SetTrigger("isDead");
        GameObject selam = Instantiate(prize, transform.position, Quaternion.identity);
        Destroy(selam, 1);
        Destroy(gameObject, 1f);
    }


}
