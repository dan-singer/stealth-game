using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSpawner : MonoBehaviour
{
    public GameObject toSpawn; 
    public Vector3 spawnOffset = new Vector3(0, -1.8f, 0);
    void OnCollisionEnter(Collision other) 
    {
        if (other.transform.tag == "Terrain")
        {
            Instantiate(toSpawn, transform.position + spawnOffset, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
