using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class CPlayerVisibility : MonoBehaviour
{
    public PostProcessProfile profile;
    public int numEnemiesSeenBy = 0;   
    public int numEnemiesSearchingFor = 0;
    public float targetIntensity = 0.444f;
    [HideInInspector] public bool initialized = false;
}