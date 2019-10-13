using UnityEngine;

/// <summary>
/// Generic initial force component
/// </summary>
public class CInitialForce : MonoBehaviour 
{
    public Vector3 force;
    [HideInInspector] public bool launched = false;
}