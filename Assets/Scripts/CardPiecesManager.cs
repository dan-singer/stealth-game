using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPiecesManager : MonoBehaviour
{
    // [SerializeField]
    public List<GameObject> pieces;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    public GameObject promptText;

    [SerializeField]
    private Canvas uiCanvas;

    [SerializeField]
    public List<Image> grayedSpaces;

    private int collectedCount = 0;
    private GameObject[] tempGameObjectArr;

    private int cardsToCollect;

    // Start is called before the first frame update
    void Start()
    {
        tempGameObjectArr = GameObject.FindGameObjectsWithTag("cardPiece");
        // get the array of card pieces
        for(int i = 0; i < tempGameObjectArr.Length; i++)
        {
            // add all of the card pieces to the list
            pieces.Add(tempGameObjectArr[i]);
        }

        gameObject.GetComponentInChildren<Guidance>().piecesList = pieces;

        cardsToCollect = pieces.Count;

        promptText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cardPiece")
        {
            promptText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // promptText.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(other.gameObject.tag == "cardPiece")
            {
                pieces.Remove(other.gameObject);
                Destroy(other.gameObject);
                cardsToCollect = pieces.Count;
                gameObject.GetComponentInChildren<Guidance>().piecesList = pieces;
                UpdateCollected();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        promptText.SetActive(false);
    }


    void UpdateCollected()
    {
        promptText.SetActive(false);
        grayedSpaces[5 - cardsToCollect].enabled = false;
    }
}
