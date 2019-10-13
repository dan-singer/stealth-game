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
    public float minPitch = -45.0f;
    public float maxPitch = 45.0f;
    public float jumpForceMagnitude = 100.0f;

    public float bobSpeed = 1.5f;
    public float bobAmplitude = 3.0f;
    [HideInInspector] public Vector3 acceleration;
    [HideInInspector] public float pitch = 0;
    [HideInInspector] public float yaw = 0;
    [HideInInspector] public bool isCrouched = false;
    [HideInInspector] public float bobTime = 0.0f;

}
