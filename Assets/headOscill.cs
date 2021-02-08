using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headOscill : MonoBehaviour
{
    public GameObject head;
    public Vector3 headstartpos;
    public float oscillationFreq = 2f;
    float t = 0f;
    float theta;

    // Start is called before the first frame update
    void Start()
    {
        headstartpos=head.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        theta = Mathf.Sin(2 * Mathf.PI * oscillationFreq * t);
        //headstartpos[1] = headstartpos[1] + theta;
        transform.position = new Vector3(headstartpos[0], headstartpos[1] + theta, headstartpos[2]);
        t += Time.deltaTime;
    }
}
