using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    /// <summary>
    /// Returns true if visible, false otherwise. 
    /// </summary>
    /// <value></value>
    public bool IsVisible
    {
        get
        {
            return numVisBlockersInContactWith == 0 || !playerMovement.isCrouched;
        }
    }
    private int numVisBlockersInContactWith = 0;

    CPlayerMovement playerMovement;

    private void Start() 
    {
        playerMovement = GetComponent<CPlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VisibilityBlocker"))
        {
            ++numVisBlockersInContactWith;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("VisibilityBlocker"))
        {
            --numVisBlockersInContactWith;
        }
    }
}