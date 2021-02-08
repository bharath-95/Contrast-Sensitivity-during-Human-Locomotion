using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class eyeTransform : MonoBehaviour
{

    public TextReader readIn1;
    public TextReader readIn2;

    private string textIn1;
    private string textIn2;
    private string[] lineClone;

    int rowCount = 0;
    int rowCount2 = 0;

    string[] chop1;
    string[] chop2;

    bool parseTest;

    float parseTemp;
    float[,] calibMat;

    // Start is called before the first frame update
    void Start()
    {

        //search the current directory and subdirectories for files that fulfill the search pattern

        string searchpattern = "*Alpha_tran_Tk1.csv"; 

        string[] files = Directory.GetFiles(System.Environment.CurrentDirectory, searchpattern, SearchOption.AllDirectories);

        if (files.Length == 1)
        {

            using (readIn1 = File.OpenText(files[0]))
            {

                while ((textIn1 = readIn1.ReadLine()) != null)
                {
                    if (rowCount > 6)
                    {
                        chop1 = textIn1.Split(',');
                    }

                    rowCount++;

                }

            }

            calibMat = new float[rowCount, chop1.Length];

            //Debug.Log(calibMat.GetLength(0));
            //Debug.Log(calibMat.GetLength(1));

            using (readIn2 = File.OpenText(files[0]))
            {

                while ((textIn2 = readIn2.ReadLine()) != null)
                {
                    if (rowCount2 > 6)
                    {
                        chop2 = textIn2.Split(',');

                        //Debug.Log(chop2);

                        for (int i = 0; i < chop2.Length; i++)
                        {

                            parseTest = float.TryParse(chop2[i], out parseTemp);

                            // Debug.Log(temp);

                            if (parseTest == true) // if value is present, write to 2d matrix
                            {

                                calibMat[rowCount2, i] = parseTemp;

                            }

                            if (parseTest == false) // if value is missing, use previously-stored value
                            {

                                parseTest = float.TryParse(lineClone[i], out parseTemp);

                                calibMat[rowCount2, i] = parseTemp;

                            }

                        //Debug.Log(calibMat[rowCount2, i]);

                        }


                    }

                    rowCount2++;

                    lineClone = chop2;

                }

            }







        }

    }

    // Update is called once per frame
    void Update()
    {
        





    }
}
