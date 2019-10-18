using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guidance : MonoBehaviour
{
    // this will get passed in from the card collection script, it will update when a card is collected and push the updated array to here
    // [SerializeField]
    public List<GameObject> piecesList;

    [SerializeField] private GameObject compass;

    private GameObject altar;
    private GameObject tempObject;

    [SerializeField] private GameObject arrow;

    [SerializeField] private GameObject focusedPos;

    [SerializeField] private GameObject unfocusedPos;

    private bool startCheck = false;
    private float compassTransitionStartTime = 0;
    private float speed = 1.0f;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        altar = GameObject.Find("Altar");
    }

    void TransitionCompass(Vector3 localStart, Vector3 localEnd, float startTime)
    {
        journeyLength = Vector3.Distance(localStart, localEnd);
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        compass.transform.localPosition = Vector3.Lerp(localStart, localEnd, fractionOfJourney);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 closest = FindClosestObjective();

        // rotate to point in the direction of the closest objective
        arrow.transform.rotation = Quaternion.LookRotation((closest - arrow.transform.position).normalized, Vector3.up);


        if (Input.GetButtonDown("Focus") || Input.GetButtonUp("Focus"))
        {
            compassTransitionStartTime = Time.time;
        }

        // lerp to a more focused position
        if (Input.GetButton("Focus"))
        {
            TransitionCompass(unfocusedPos.transform.localPosition, focusedPos.transform.localPosition, compassTransitionStartTime);    
        }
        else
        { 
            TransitionCompass(focusedPos.transform.localPosition, unfocusedPos.transform.localPosition, compassTransitionStartTime);
        }
    }


    // will find the closest piece to the player if there is one still present on the map, if not it will point towards the altar
    Vector3 FindClosestObjective()
    {
        if(piecesList.Count >= 2) // there are at least 2 card pieces left to collect, determine which is closer
        {
            // will always be larger
            float distA = Mathf.Infinity;

            // loop through pieces
            for (int i = 0; i < piecesList.Count; i++)
            {
                // check if there is a closer piece still in the world
                if(Vector3.Distance(piecesList[i].transform.position, compass.GetComponentInParent<Transform>().position) < distA)
                {
                    distA = Vector3.Distance(piecesList[i].transform.position, compass.GetComponentInParent<Transform>().GetComponentInParent<Transform>().position);
                    tempObject = piecesList[i];
                }
            }

            // return the closest gameObject
            return tempObject.transform.position;
        }
        else if(piecesList.Count == 1) // there is only 1 card piece remaining
        {
            // return the vector3 of the remaining piece
            return piecesList[0].transform.position;
        }
        else // there are no more card pieces to collect
        {
            // return the vector3 of the altar to the arrow to rotate to look at
            return altar.transform.position;
        }
    }
}
