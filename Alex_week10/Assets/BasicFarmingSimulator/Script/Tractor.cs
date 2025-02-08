using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Tractor : MonoBehaviour
{
    NavMeshAgent tractorAgent;
    public GameObject cropPrefab;
    int maxCrops = 12;
    public int currentCrops;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tractorAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                tractorAgent.SetDestination(hit.point);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Mud" && currentCrops < maxCrops)
        {
            StartCoroutine(CropSpawn());
        }
    }
    IEnumerator CropSpawn()
    {
        yield return new WaitForSeconds(3);
        if (currentCrops < maxCrops)
        {
            Vector3 randomSpawn1 = new Vector3(Random.Range(-5.3f, 0.42f), 0.33f, Random.Range(0.199f, 6.015f));
            Instantiate(cropPrefab, randomSpawn1, Quaternion.identity);
            currentCrops++;
        }
    }
}
