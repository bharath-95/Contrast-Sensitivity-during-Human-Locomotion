using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Timers;


public class asciiRead : MonoBehaviour
{

    public static string path = @"C:/Users/bhara/Treadmill Room V2/Assets/BRSAsfn.asc";

    public StreamReader ascRead1 = new StreamReader(path, System.Text.Encoding.ASCII);
    public StreamReader ascRead2 = new StreamReader(path, System.Text.Encoding.ASCII);

    public GameObject eyeGazeL;
    public GameObject eyeGazeR;
    

    private Vector3 eyeScreenMovL;
    private Vector3 eyeScreenMovR;
    private Vector3 headVect;
    private Vector3 diffVect;

    //a list of lists of float
    public List<List<double>> asciiList = new List<List<double>>();

    string ascText1;
    string RegAsc;
    string ascText2;
    string RegAsc2;

    int rowCount = 0;
    int colCount = 0;
    int rowCount2 = 0;
    int colCount2 = 0;
    int yy = 0;
    int[] colAmt;

    //private decimal widthCm = 161.925M;
    //private decimal heightCm = 89.535M;
    double widthHf = 161.925 / 2;
    double heightHf = 89.535 / 2;
    double widthFactor = 161.925 / 1920;
    double heightFactor = 89.535 / 1080;

    string[] splitter;
    string[] splitter2;

    bool test1;
    bool test2;

    double temp1;
    double eyeScreenDiff;
    double[,] asciiMat;

    public float[,] headData;
    

    // Start is called before the first frame update
    void Start()
    {
        //search the current directory and subdirectories for files that fulfill search pattern
        string asciiLoc = "*BRSAsfn.asc";

        string[] files = Directory.GetFiles(System.Environment.CurrentDirectory, asciiLoc, SearchOption.AllDirectories);

        if (files.Length == 1)
        {

            using (ascRead1) // this section counts the number of rows and columns in our dataset
            {

                while ((ascText1 = ascRead1.ReadLine()) != null)
                {

                    colCount = 0;

                    RegAsc = Regex.Replace(ascText1, @"\s+", " "); // replaces each whitespace with a single whitespace

                    splitter = RegAsc.Split(' '); 

                    foreach (string q in splitter)
                    {

                        colCount++;
                        
                        //Debug.Log(splitter[q]);

                        //test1 = double.TryParse(splitter[q], out temp1);

                        //Debug.Log(test1);
                        //Debug.Log(temp1);

                    }

                    //colAmt[rowCount] = colCount; // stores each column count to 1D column array
                    //Debug.Log(colCount);

                    rowCount++;

                }

            }

            //Debug.Log(rowCount);

            //Debug.Log(colCount);


            asciiMat = new double[rowCount, colCount];
            
            // // Check matrix dims
            //Debug.Log(asciiMat.GetLength(0));
            //Debug.Log(asciiMat.GetLength(1));

            using (ascRead2)
            {

                while ((ascText2 = ascRead2.ReadLine()) != null)
                {

                    colCount2 = 0;

                    RegAsc2= Regex.Replace(ascText2, @"\s+", " ");

                    splitter2 = RegAsc2.Split(' ');

                    //Debug.Log(splitter2);

                    foreach (string w in splitter2)
                    {
                        asciiList.Add(new List<double>());

                        test1 = double.TryParse(w, out temp1);

                        if (test1 == false) //Fills missing data with zero values
                        {
                            //Debug.Log(asciiList[rowCount2][0]);

                            temp1 = asciiList[rowCount2-1][colCount2];

                            //Debug.Log(temp1);

                        }

                        asciiList[rowCount2].Add(temp1);

                        //Debug.Log(temp1);
                        //asciiMat[rowCount2,colCount2] = temp1;
                        //Debug.Log(asciiMat[rowCount2, colCount2]);
                        //Debug.Log(asciiList[rowCount2][colCount2]);

                        colCount2++;  

                    }

                    rowCount2++;
                    
                }

            }

            // This section converts our data to screen-centered coordinates
            
        for (int r = 0; r < rowCount; r++)
            {
                //Debug.Log(asciiList[r][1]);
                //Debug.Log(asciiList[r][2]);
                //Debug.Log(asciiList[r][4]);
                //Debug.Log(asciiList[r][5]);

                asciiList[r][1] = (asciiList[r][1] * widthFactor + widthHf); //LX
                asciiList[r][2] = (heightHf - (asciiList[r][2] * heightFactor)); //LY
                asciiList[r][4] = (asciiList[r][4] * widthFactor + widthHf); //RX
                asciiList[r][5] = (heightHf - (asciiList[r][5] * heightFactor)); //RY

                //Debug.Log(asciiList[r][1]);
                //Debug.Log(asciiList[r][2]);
                //Debug.Log(asciiList[r][4]);
                //Debug.Log(asciiList[r][5]);

            }
 
        }

        else
        {
            Debug.Log("check #files/ if it's there");
        }
        
        eyeScreenMovL = eyeGazeL.transform.position;

        headVect = new Vector3(headData[yy, 6] / 10, headData[yy, 7] / 10 - 14, headData[yy, 8] / 10);

        diffVect = new Vector3(((float)asciiList[yy][1] / 10) - 15 - headVect[0], eyeScreenMovL[1] + ((float)asciiList[yy][2] / 10) - headVect[1], eyeScreenMovL[2]);

        //eyeScreenMovR = eyeGazeR.transform.position;

    }

    int y = 0;

    private int frUpdate = 5;

    float t = 0f;

    int secCount = 12;

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % frUpdate != 0) return;

        headVect = new Vector3(headData[y, 6] / 10, headData[y, 7] / 10 - 16, headData[y, 8] / 10);

        diffVect = new Vector3(((float)asciiList[y][1] / 10) - 15 - headVect[0], eyeScreenMovL[1] + ((float)asciiList[y][2] / 10) - headVect[1], eyeScreenMovL[2]);

        eyeGazeL.transform.position = diffVect;

        //Debug.Log(diffVect);

        //eyeGazeR.transform.position = new Vector3(((float)asciiList[y * 3 + yy][4] / 10) - 14, (eyeScreenMovR[1] + ((float)asciiList[y * 3 + yy][5] / 10)), eyeScreenMovR[2]);

        //Debug.Log(eyeGazeL.transform.position);

        t += Time.deltaTime;

        if (t >= secCount) return;

        //Operator=(t, Time.deltaTime);
        y++;
    }
}
