using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnSet;

    [SerializeField]
    private GameObject enemyModel;

    private int set1, set2, set3, set4, set5, set6, set7;

    // Start is called before the first frame update
    void Start()
    {
        set1 = Random.Range(0, 3);
        set2 = Random.Range(3, 6);
        set3 = Random.Range(6, 9);
        set4 = Random.Range(9, 12);
        set5 = Random.Range(12, 15);
        set6 = Random.Range(15, 18);
        set7 = Random.Range(18, 20);

        // do random.range(0-3), then random.range(3-6), random.range(6-9), random.range(9-12), random.range(12-15), random.range(15-18), random.range(18-21)
        // spawn enemies in these 6 random locations
        for (int i = 0; i < 7; i++)
        {
            switch (i)
            {
                case 0:
                    Instantiate(enemyModel, spawnSet[set1].transform.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(enemyModel, spawnSet[set2].transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(enemyModel, spawnSet[set3].transform.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(enemyModel, spawnSet[set4].transform.position, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(enemyModel, spawnSet[set5].transform.position, Quaternion.identity);
                    break;
                case 5:
                    Instantiate(enemyModel, spawnSet[set6].transform.position, Quaternion.identity);
                    break;
                case 6:
                    Instantiate(enemyModel, spawnSet[set7].transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
