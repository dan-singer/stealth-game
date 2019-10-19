using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages Card pieces UI and informs Guidance System about said card pieces
/// </summary>
public class CardPiecesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> pieces;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject promptText;

    [SerializeField] private Canvas uiCanvas;

    [SerializeField] private List<Image> grayedSpaces;

    private int totalCards;
    private int remainingCards;
    private Text promptMessage;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] tempGameObjectArr = GameObject.FindGameObjectsWithTag("cardPiece");
        // get the array of card pieces
        for(int i = 0; i < tempGameObjectArr.Length; i++)
        {
            // add all of the card pieces to the list
            pieces.Add(tempGameObjectArr[i]);
        }

        gameObject.GetComponentInChildren<Guidance>().piecesList = pieces;

        totalCards = pieces.Count;
        remainingCards = pieces.Count;

        promptMessage = promptText.transform.GetChild(0).GetComponent<Text>();
        promptText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cardPiece"))
        {
            promptText.SetActive(true);
            promptMessage.text = "Collect card piece (E)";
        }
        if (other.gameObject.CompareTag("Altar") /*&& pieces.Count == 0*/)
        {
            promptText.SetActive(true);
            promptMessage.text = "Submit Empress Card (E)";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(other.gameObject.tag == "cardPiece")
            {
                pieces.Remove(other.gameObject);
                Destroy(other.gameObject);
                remainingCards = pieces.Count;
                gameObject.GetComponentInChildren<Guidance>().piecesList = pieces;
                UpdateGrayedSpaces();
                promptText.SetActive(false);
            }
            if (other.gameObject.tag == "Altar" && pieces.Count == 0) 
            {
                SceneManager.LoadScene("EndScene");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("cardPiece") || other.gameObject.CompareTag("Altar"))
        {
            promptText.SetActive(false);
        }
    }

    void UpdateGrayedSpaces()
    {
        grayedSpaces[totalCards - remainingCards - 1].enabled = false;
    }
}
