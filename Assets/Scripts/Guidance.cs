using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guidance : MonoBehaviour
{
    // this will get passed in from the card collection script, it will update when a card is collected and push the updated array to here
    [SerializeField]
    private GameObject[] piecesArr;

    [SerializeField]
    private GameObject compass;

    private GameObject altar;
    private GameObject tempObject;

    [SerializeField]
    private GameObject arrow;

    private float xValFocus, xValUnfocus;
    // Start is called before the first frame update
    void Start()
    {
        // set piecesArr to the array of pices that are in the world
        // when one is picked up call back to this script and update the array
        // piecesArr = gameObject.transform.Find("GameManager").

        // find the refeence to the arm holding the compass

        // xValUnfocus = compass.transform.position.x;
        // xValFocus = compass.transform.forward.x + 1;
        // arrow = gameObject.transform.Find("Arrow").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // focus the guidance
        if (Input.GetKey(KeyCode.Q)) { 
            Vector3 closest = FindClosestObjective();

            // rotate to point in the direction of the closest objective
            arrow.transform.rotation = Quaternion.LookRotation((closest - arrow.transform.position).normalized, Vector3.up);

            // still need to fix the arrow not being centered
            arrow.transform.localPosition = Vector3.zero;
        }
        else // guidance is unfocused
        {
            // rotate to point in the general direction of the closest objective
            arrow.transform.rotation = Quaternion.LookRotation(((UnfocusedDirection(FindClosestObjective())) - arrow.transform.position).normalized, Vector3.up);

            // still need to fix the arrow not being centered
            arrow.transform.localPosition = Vector3.zero;
            // arrow.transform.localPosition = new Vector3(0.5f, 0.5f, -0.8f);
        }
    }

    Vector3 UnfocusedDirection(Vector3 objective)
    {
        // get a random number to add or subtract from the actual normalized direction
        float rand = Random.Range(-45.0f, 46.0f);

        Debug.Log("Rand: " + rand);

        Vector3 unfocDir = new Vector3(objective.x + rand, objective.y + rand, objective.z + rand); // should make the arrow jump back and forth a bit

        return unfocDir;
    }

    // will find the closest piece to the player if there is one still present on the map, if not it will point towards the altar
    Vector3 FindClosestObjective()
    {
        if(piecesArr.Length >= 2) // there are at least 2 card pieces left to collect, determine which is closer
        {
            // set the first distance to check
            // float distA = Vector3.Distance(piecesArr[0].transform.position, compass.GetComponentInParent<Transform>().position);
            // tempObject = piecesArr[0];

            // will always be larger
            float distA = Mathf.Infinity;

            // loop through pieces
            for (int i = 0; i < piecesArr.Length; i++)
            {
                // check if there is a closer piece still in the world
                if(Vector3.Distance(piecesArr[i].transform.position, compass.GetComponentInParent<Transform>().position) < distA)
                {
                    distA = Vector3.Distance(piecesArr[i].transform.position, compass.GetComponentInParent<Transform>().GetComponentInParent<Transform>().position);
                    tempObject = piecesArr[i];
                    Debug.Log("Dist:" + distA);
                }
            }

            // Debug.Log(tempObject.name);
            // return the closest gameObject
            return tempObject.transform.position;
        }
        else if(piecesArr.Length == 1) // there is only 1 card piece remaining
        {
            // return the vector3 of the remaining piece
            return piecesArr[0].transform.position;
        }
        else // there are no more card pieces to collect
        {
            // return the vector3 of the altar to the arrow to rotate to look at
            return altar.transform.position;
        }
    }
}
