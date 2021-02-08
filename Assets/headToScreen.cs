using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headToScreen : MonoBehaviour
{

    public Transform headGaze;
    public Transform headMove;
    public Transform screen;

    public float[,] headData;

    public Vector3 headPos;

    public float screenZ;

    // Start is called before the first frame update
    void Start()
    {

        screenZ = screen.position[2];

        headGaze.position = new Vector3(headMove.position[0], headMove.position[1], screenZ);


    }

    // Update is called once per frame
    void Update()
    {
        screenZ = screen.position[2];

        headGaze.position = new Vector3(headMove.position[0], headMove.position[1], screenZ);



    }
}
