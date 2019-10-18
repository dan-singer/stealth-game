using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General camera transition component used in the main menu
/// </summary>
public class CameraTransition : MonoBehaviour
{
    public Transform[] waypoints;
    private int index = 0;

    public System.Action<int> Transitioned;
    public System.Action TransitionsComplete;
    
    public void Transition()
    {
        if (index < waypoints.Length - 1)
        {
            ++index;
            Transitioned?.Invoke(index);
        }
        else 
        {
            TransitionsComplete?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")) 
        {
            Transition();
        }

        transform.position = Vector3.Lerp(transform.position, waypoints[index].position, 0.1f);
        transform.rotation = Quaternion.Slerp(transform.rotation, waypoints[index].rotation, 0.1f);
    }
}
