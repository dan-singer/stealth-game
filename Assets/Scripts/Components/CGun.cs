using UnityEngine;

/// <summary>
/// Gun Component
/// </summary>
public class CGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform socket;
    public Camera cam;
    public float launchForceMagnitude;
    public Transform player;
    public AudioClip fireSound;

    public float pitchOffset = -75.0f;

    public int startAmmo = 4;

    private int curAmmo;
    public int CurAmmo
    {
        get
        {
            return curAmmo;
        }
        set
        {
            if (value < 0)              curAmmo = 0;
            else if (value > startAmmo) curAmmo = startAmmo;
            else                        curAmmo = value;
        }
    }
    [HideInInspector] public bool initialized = false;
}