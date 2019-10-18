using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
