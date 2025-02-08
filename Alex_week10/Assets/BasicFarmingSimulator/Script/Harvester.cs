using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Harvester : MonoBehaviour
{
    NavMeshAgent harvesterAgent;

    public int score;
    public TMP_Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        harvesterAgent = GetComponent<NavMeshAgent>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "CROPS CROPPED: " + score.ToString();

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                harvesterAgent.SetDestination(hit.point);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crop2")
        {
            Destroy(other.gameObject);
            score++;
            GameObject.FindFirstObjectByType<Tractor>().currentCrops--;
        }
    }
}
