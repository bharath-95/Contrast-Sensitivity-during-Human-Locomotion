using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class stimOnTime : MonoBehaviour

   
{
    public TextReader respReader;
    public TextReader respReader2;

    public GameObject gabor;
    private Vector3 gaborPos;

    private string text1; 
    int rows = 0;
    string[] respSplit;
    float[] respTimes;

    private string text2;
    int row2 = 0;
    string[] respSplit2;
    float[,] respTimes2;

    bool parseTest;

    float parseTemp;
    public float timerOngoing;

    // Start is called before the first frame update
    void Start()
    {

        //search the current directory and subdirectories for files that fulfill the search pattern

        string searchpattern = "*respBRSA.csv";

        string[] files = Directory.GetFiles(System.Environment.CurrentDirectory, searchpattern, SearchOption.AllDirectories);

        if (files.Length == 1)
        {

            using (respReader = File.OpenText(files[0]))
            {

                while ((text1 = respReader.ReadLine()) != null)
                {
                    if (rows < 5)
                    {
                        respSplit = text1.Split(',');
                    }

                    rows++;

                }

            }

            //Debug.Log(respSplit.Length);

            respTimes = new float[respSplit.Length];

            //Debug.Log(respTimes.Length);
        
            using (respReader2 = File.OpenText(files[0]))
            {

                while ((text2 = respReader2.ReadLine()) != null)
                {

                    if (row2 == 4)
                    {
                        respSplit2 = text2.Split(',');

                        for (int i = 0; i < respSplit2.Length; i++)
                        {

                            parseTest = float.TryParse(respSplit2[i], out parseTemp);

                            respTimes[i] = parseTemp;

                            //Debug.Log(respTimes[1, i]);
                             
                        }

                    }

                    row2 ++;

                }

            }

        }

        gaborPos = gabor.transform.position;

    }

    float t = 0f;
    float tStore;
    int q = 0;
    float duration = .025f;

    // Update is called once per frame
    void Update()
    {
        //gaborPos = gabor.transform.position;

        //Debug.Log(respTimes[q]);
        if (t >= respTimes[q])
        {
            //Debug.Log("bam");

            gabor.transform.position = new Vector3(gaborPos[0], gaborPos[1], gaborPos[2] - 1);

            //Debug.Log(gabor.transform.position[2]);

            tStore = respTimes[q]; 

            q++;
            
        }

        //Debug.Log(tStore + duration);

        if (t >= tStore + duration)
        {
            //Debug.Log("yo");

            gabor.transform.position = new Vector3(gaborPos[0], gaborPos[1], gaborPos[2] + 1);

            //Debug.Log(gabor.transform.position[2]);

            tStore = 0;
            
        }

        t += Time.deltaTime;

        //Debug.Log(t);

    }

}
