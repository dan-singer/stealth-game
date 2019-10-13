using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public CameraTransition cameraTransition;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransition.Transitioned += (int index) => {
            transform.GetChild(index - 1).gameObject.SetActive(false);
            transform.GetChild(index).gameObject.SetActive(true);
        };

        cameraTransition.TransitionsComplete += () => {
            print("Would load main level now");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
