using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{
    public Camera Cam1, Cam2;
    public KeyCode camButton;

    // Start is called before the first frame update
    void Start()
    {
        Cam1.enabled = true;
        Cam2.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(camButton))
        {
            Cam1.enabled = !Cam1.enabled;
            Cam2.enabled = !Cam2.enabled;

        }
    }
}
