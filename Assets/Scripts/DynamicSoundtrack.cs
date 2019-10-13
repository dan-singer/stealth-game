using UnityEngine;
using UnityEngine.Audio;
public class DynamicSoundtrack : MonoBehaviour
{
    public AudioMixerSnapshot ambient, search, chase;
    private CPlayerVisibility playerViz;
    private void Start()
    {
        playerViz = GameObject.FindWithTag("Player").GetComponent<CPlayerVisibility>();
        playerViz.Seen += () => {
            SwitchClipsAndMaintainTime(chase);
        };
        playerViz.SearchedFor += () => {
            SwitchClipsAndMaintainTime(search);
        };
        playerViz.Hidden += () => {
            SwitchClipsAndMaintainTime(ambient);
        };
    }

    private void SwitchClipsAndMaintainTime(AudioMixerSnapshot snapshot) 
    {
        snapshot.TransitionTo(0.5f);
    }

    private void Update()
    {
    }
}