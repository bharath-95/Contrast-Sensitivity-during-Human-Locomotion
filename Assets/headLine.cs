using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headLine : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public LineRenderer LineGo;
    public Vector3 outputs;

    // Start is called before the first frame update
    void Start()
    {
        LineGo = GetComponent<LineRenderer>();
        float newx = start.position[0];
        float newy = start.position[1];
        float newz = start.position[2];
        Vector3 updated = new Vector3(newx, newy, newz);
        //Vector3.Lerp((start.position[0], start.position[1], start.position[2]));
        LineGo.SetPosition(0, updated);
        // extents head vector to the distance from the screen (175 cm)
        //float endz = newz + 17.5f;
        Vector3 endUpdated = new Vector3(end.position[0], end.position[1], end.position[2]);
        LineGo.SetPosition(1, endUpdated);
        LineGo.SetWidth(.05f, .05f);
    }

    // Update is called once per frame
    void Update()
    {
        float newx = start.position[0];
        float newy = start.position[1];
        float newz = start.position[2];
        Vector3 updated = new Vector3(newx, newy, newz);
        LineGo.SetPosition(0, updated);
        // extents head vector to the distance from the screen (175 cm)
        //float endz = newz + 17.5f;
        Vector3 endUpdated = new Vector3(end.position[0], end.position[1], end.position[2]);
        LineGo.SetPosition(1, endUpdated);
        LineGo.SetWidth(.05f, .05f);
        outputs = start.position;
    }
}
