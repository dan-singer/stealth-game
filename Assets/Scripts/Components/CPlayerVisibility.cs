using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Adjusts various post processing settings and invokes events regarding visibility
/// </summary>
public class CPlayerVisibility : MonoBehaviour
{
    public PostProcessProfile profile;
    public int numEnemiesSeenBy = 0;   
    public int numEnemiesSearchingFor = 0;
    public float targetIntensity = 0.444f;
    public Transform[] spawnTransforms;
    public float deathDuration = 2.0f;
    public int lives = 3;
    public GameObject deathMessage;
    [HideInInspector] public float deathTimer = 0;
    [HideInInspector] public bool initialized = false;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public Vector3 nearestSpawnLocation;
    public System.Action Seen;
    public System.Action SearchedFor;
    public System.Action Hidden;


}