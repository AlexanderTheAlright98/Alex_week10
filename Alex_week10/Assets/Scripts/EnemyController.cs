using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Rigidbody ragdollRB in GetComponentsInChildren<Rigidbody>())
        {
            ragdollRB.isKinematic = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

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
        }
    }
}
