using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int health;
    public int damage;

    NavMeshAgent _agent;
    Transform player;
    Vector3 currentDestination;
    [SerializeField] float followDistance;
    Animator anim;
    Collider runBox;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        runBox = GetComponent<BoxCollider>();
        foreach (Rigidbody ragdollRB in GetComponentsInChildren<Rigidbody>())
        {
            ragdollRB.isKinematic = true;
        }
    }
    void Update()
    {
        if (health > 0)
        {
            if (Vector3.Distance(player.position, transform.position) < followDistance)
            {
                Follow();
            }
            else
            {
                Search();
            }
        }
    }
    public void ragdollTrigger()
    {
        health--;
        if (health <= 0)
        {
            foreach (Rigidbody ragdollRB in GetComponentsInChildren<Rigidbody>())
            {
                ragdollRB.isKinematic = false;
            }
            GetComponent<Collider>().enabled = false;
            StartCoroutine(EnemyKilled());
        }
    }
    void Search()
    {
        if (Vector3.Distance(currentDestination, transform.position) < 5)
        {
            currentDestination = transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }

        _agent.destination = currentDestination;
        anim.SetBool("chasingAnim", false);
        runBox.enabled = false;
    }
    void Follow()
    {
        anim.SetBool("chasingAnim", true);
        runBox.enabled = true;
        _agent.destination = player.position;
    }
    IEnumerator EnemyKilled()
    {
        runBox.enabled = false;
        _agent.enabled = false;
        anim.enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
