using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;   

/// <summary>
/// Player Movement Settings
/// </summary>
public class CPlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    [HideInInspector]
    public float pitch = 0;
    [HideInInspector]
    public float yaw = 0;

    public float minPitch = -45.0f;
    public float maxPitch = 45.0f;
}
