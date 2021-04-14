using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomWalking : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    [SerializeField] private int maxDistanceMovement = 10;
    private NavMeshAgent agent;
    private Animator animator;
    private bool alerted;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        agent.speed = speed;
        StartCoroutine(Wandering());
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity != Vector3.zero)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

    private IEnumerator Wandering()
    {
        while (true)
        {
            if (!alerted)
            {
                if (agent.remainingDistance < 0.1)
                {
                    agent.destination = RandomNavSphere(transform.position, maxDistanceMovement, -1);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    private Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }
}
