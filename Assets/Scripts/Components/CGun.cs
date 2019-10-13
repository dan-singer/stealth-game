using UnityEngine;
public class CGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform socket;
    public Camera cam;
    public float launchForceMagnitude;
    public Transform player;
    public AudioClip fireSound;

    public float pitchOffset = -75.0f;
}