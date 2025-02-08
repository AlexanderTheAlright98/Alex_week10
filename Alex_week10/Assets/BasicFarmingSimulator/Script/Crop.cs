using System.Collections;
using UnityEngine;

public class Crop : MonoBehaviour
{
    MeshFilter cropMesh;
    public Mesh ripeMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cropMesh = GetComponent<MeshFilter>();
        StartCoroutine(RipenCrop());
    }


    IEnumerator RipenCrop()
    {
        yield return new WaitForSeconds(3);
        cropMesh.mesh = ripeMesh;
        transform.gameObject.tag = "Crop2";
    }

}
