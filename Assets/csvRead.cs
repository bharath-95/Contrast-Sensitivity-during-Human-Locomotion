using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class csvRead : MonoBehaviour
{
    public GameObject head;
    public GameObject ankle;

    public GameObject storage;
    public GameObject storage2;

    headToScreen headToScreen;
    asciiRead asciiRead;
    //stimOnTime stimOnTime;

    public TextReader reader1;
    public TextReader reader2;
    public string path = @"D:/TreadmillRoom/Assets/Alpha_dcs_Tk1_002.csv";

    private string text1;
    private string text2;
    private Vector3 headpos;
    private Vector3 anklepos;
    private Quaternion headrot;
    private Quaternion anklerot;

    int rows = 0;
    string[] split1;
    string[] split2;
    private string[] strClone;

    bool test;

    float temp;
    float[,] datamat;
    float headYpos;
    float headCm;
    float flip = 180;

    // Start is called before the first frame update
    void Start()
    {

        //search the current directory and subdirectories for files that fulfill the search pattern

        string searchpattern = "*Alpha_DCS_Tk1_001.csv";

        string[] files = Directory.GetFiles(System.Environment.CurrentDirectory, searchpattern, SearchOption.AllDirectories);

        if (files.Length == 1)
        {

            using (reader1 = File.OpenText(files[0]))
            {

                while ((text1 = reader1.ReadLine()) != null)
                {
                    if (rows > 6)
                    {
                        split1 = text1.Split(',');
                    }

                    rows++;

                }

            }

            datamat = new float[rows, split1.Length];

            int row = 0;

            using (reader2 = File.OpenText(files[0]))
            { 

                while ((text2 = reader2.ReadLine()) != null)
                {                   

                    if (row > 6)
                    {
                        split2 = text2.Split(',');

                        for (int i = 0; i < split2.Length; i++)
                        {

                            test = float.TryParse(split2[i], out temp);

                            // Debug.Log(temp);

                            if (test == true) // if value is present, write to 2d matrix
                            {

                                datamat[row, i] = temp;
                                
                            }

                            if (test == false) // if value is missing, use previously-stored value
                            {

                                test = float.TryParse(strClone[i], out temp);

                                datamat[row, i] = temp;

                            }

                        }

                    }

                    row++;

                    strClone = split2;
                }
                
            }

        }

        else
        {
            Debug.Log("check file directory");
        }

        
        headToScreen = storage.GetComponent<headToScreen>();

        headToScreen.headData = datamat;

        asciiRead = storage2.GetComponent<asciiRead>();

        asciiRead.headData = datamat;

        headpos = head.transform.position;

        headrot = head.transform.rotation;

        anklepos = ankle.transform.position;

        anklerot = ankle.transform.rotation;

    }

    int y = 0;

    private int frUpdate = 5;

    float t = 0f;

    int secCount = 12;

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % frUpdate != 0) return;

        /*headCm = datamat[y, 6];

        headYpos = headCm / 10; // converts cm data to m data*/

        head.transform.position = new Vector3(datamat[y, 6]/10, datamat[y, 7]/10 - 7, datamat[y, 8]/10);

        ankle.transform.position = new Vector3(datamat[y, 20] / 10, datamat[y, 21] / 10 - 7, datamat[y, 22] / 10);

        head.transform.rotation = new Quaternion(datamat[y, 2], datamat[y, 3], datamat[y, 4], datamat[y, 5]);

        ankle.transform.rotation = new Quaternion(datamat[y, 16], datamat[y, 17], datamat[y, 18], datamat[y, 19]);

        Vector3 angleStore = ankle.transform.rotation.eulerAngles;

        ankle.transform.rotation = Quaternion.Euler(angleStore.x, angleStore.y + flip, angleStore.z);


        // Debug.Log(head.transform.position);

        t += Time.deltaTime;



        if (t >= secCount) return;

        y++;

    }

    
}
