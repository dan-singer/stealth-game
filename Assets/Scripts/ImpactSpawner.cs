using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSpawner : MonoBehaviour
{
    public GameObject toSpawn; 
    void OnCollisionEnter(Collision other) 
    {
        if (other.transform.tag == "Terrain")
        {
            Instantiate(toSpawn, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
