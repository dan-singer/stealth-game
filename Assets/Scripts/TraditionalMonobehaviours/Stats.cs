using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour
{

    public CPlayerVisibility playerVisibility;
    public CGun playerGun;
    [SerializeField] private Text livesText;
    [SerializeField] private Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = $"Lives: {playerVisibility.lives}";
        ammoText.text = $"Ammo: {playerGun.CurAmmo} / {playerGun.startAmmo}";
    }
}
