using UnityEngine;

public class CInitialForce : MonoBehaviour 
{
    public Vector3 force;
    public bool isLocal = true;
    [HideInInspector]
    public bool launched = false;
}