using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    // [SerializedField]
    // public Dictionary<GameObject[], bool> spawnLocs;
    // private bool spawnIsActive = false;


    // Start is called before the first frame update
    void Start()
    {
        // loop through predetermined spawn points
        /*for(int i = 0; i < (spawnLocs.Count / 2); i++)
        {
            // select a random one  *This will need to be revised, potential for crash to occurr
            // int rand = Mathf.Random() * 6;
            // set the value of dictionary to true, enabling ocation as a valid spawn point
            spawnLocs[rand] = true;
        }
        */
        // spawn the enemies

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
