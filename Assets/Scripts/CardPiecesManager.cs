using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPiecesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pieces;

    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // get the array of card pieces
        pieces = GameObject.FindGameObjectsWithTag("cardPiece");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
