using UnityEngine;

/// <summary>
/// Helper MonoBehaviour to assist with Player Visibility settings not suited for ECS
/// </summary>
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
            return numVisBlockersInContactWith == 0;
        }
    }
    private int numVisBlockersInContactWith = 0;

    CPlayerMovement playerMovement;

    private void Start() 
    {
        playerMovement = GetComponent<CPlayerMovement>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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