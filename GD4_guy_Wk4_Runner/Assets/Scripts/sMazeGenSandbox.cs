using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class sMazeGenSandbox : MonoBehaviour
{

    public int vMazeSize = 4;
    public int[,,] vWall = new int[10, 10, 2];
    public int[,] vSpace = new int[10, 10];

    [SerializeField] float vLavaChance = .1f;
    [SerializeField] float vSpringChance = 0.05f;
    //  public string[] vWallTest = new string[5];
    // public bool[,] vSpace = new bool[5, 5];
    public float vWallSpacing = 5;
    public GameObject vWallHor;
    public GameObject vWallVer;
    public GameObject vLava;
    public GameObject vSpring;
    public int vMazeType = 0;
    bool[,] vVisit = new bool[10, 10];

    public Vector3 vWallLoc;
    public Vector3 vGenStartPos;
    public int vWhichWall;
    [SerializeField] int i;
    [SerializeField] int j;
    [SerializeField] int vVisitTotal;
    
    
    //debug
    public TextMeshProUGUI Textout;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pResetArray();

        // Start of Walk



        Debug.Log("Start Maze Generation");



        i = Random.Range(1, vMazeSize);
        j = Random.Range(1, vMazeSize);



        Debug.Log("start at " + i + j);


        vVisit[i, j] = true;
        vVisitTotal = 1;
    }

    // Update is called once per frame
    void Update()
    {


        Textout.text = vVisitTotal.ToString();

        if (Input.GetKeyDown(KeyCode.M))
        {



            pMaze1();
            pInstantiateMaze();
        }


    }


    public void pMaze1()
    {



        //while not all spaces have been checked

        if (vVisitTotal < (vMazeSize) * (vMazeSize))
        {
            int vRandomWall = Random.Range(0, 4);
            // Up 0, Right 1 Down 2  Left 3
            int newi = i;
            int newj = j;

            // check for edges and change walk directio
            if (i == 1 && vRandomWall == 2)
            {
                vRandomWall = 0;
            }

            if (j == 1 && vRandomWall == 3)
            {
                vRandomWall = 1;
            }

            if (i == vMazeSize && vRandomWall == 0)
            {
                vRandomWall = 2;
            }

            if (j == vMazeSize && vRandomWall == 1)
            {
                vRandomWall = 3;
            }

            Debug.Log("Start cell " + i + j);
            Debug.Log("Random Wall " + vRandomWall);

            //Walk into next space

            switch (vRandomWall)
            {

                case 0:
                    newi = i + 1;
                    newj = j;

                    break;
                case 1:
                    newj = j + 1;
                    newi = i;
                    break;
                case 2:

                    newi = i - 1;
                    newj = j;
                    break;
                case 3:
                    newj = j - 1;
                    newi = i;
                    break;

            }

            Debug.Log("Spaces vistied " + vVisitTotal);
            Debug.Log("new cell " + newi + newj);
            if (vVisit[newi, newj] == false)
            {
                vVisitTotal = vVisitTotal + 1;
                Debug.Log("New Cell - total now " + vVisitTotal);

                vVisit[newi, newj] = true;

                switch (vRandomWall)

                {
                    case 0:

                        vWall[i, j, 0] = 0;

                        break;

                    case 1:
                        vWall[i, j, 1] = 0;
                        break;
                    case 2:
                        vWall[newi, j, 0] = 0;
                        break;
                    case 3:
                        vWall[i, newj, 1] = 0;
                        break;



                }


            }

            i = newi;
            j = newj;
            //Count spaces visited
            vVisitTotal = 0;
            for (int k = 0; k <= vMazeSize; k++)
            {
                for (int l = 0; l <= vMazeSize; l++)
                {
                    if (vVisit[k, l])
                    { vVisitTotal++; }


                }
            }
            Debug.Log("Total visited by count =" + vVisitTotal);


        }
        else
        {

            // Set outside walls

            for (int k = 0; k <= vMazeSize; k++)
            {
                vWall[k, 0, 1] = 1;
                vWall[0, k, 1] = 0;
                vWall[0, k, 0] = 1;
                vWall[k, 0, 0] = 0;
                vWall[k, vMazeSize, 1] = 1;
                vWall[vMazeSize, k, 0] = 1;
                vWall[k, vMazeSize, 0] = 0;
                vWall[vMazeSize, k, 1] = 0;
            }


            //remove randdom entrance and exit walls




            vWall[0, Random.Range(0, vMazeSize), 0] = 0;
            vWall[vMazeSize, Random.Range(0, vMazeSize), 0] = 0;

        }
        



    }



    public void pInstantiateMaze()


    {


        vWallLoc = new Vector3(i * vWallSpacing, 0, (j) * vWallSpacing) + vGenStartPos;

        Instantiate(vSpring, vWallLoc, Quaternion.identity);

        for (int i = 0; i <= vMazeSize; i++)

        {


            for (int j = 0; j <= vMazeSize; j++)
            {


                if (vWall[i, j, 0] == 1)
                {
                    vWallLoc = new Vector3((i + .5f) * vWallSpacing, 0, j * vWallSpacing) + vGenStartPos;

                    Instantiate(vWallVer, vWallLoc, Quaternion.identity);


                }

                if (vWall[i, j, 1] == 1)
                {
                    vWallLoc = new Vector3(i * vWallSpacing, 0, (j + 0.5f) * vWallSpacing) + vGenStartPos;


                    Instantiate(vWallHor, vWallLoc, Quaternion.Euler(0, 90, 0));


                }

                if (vSpace[i, j] == 1)
                {

                    vWallLoc = new Vector3(i * vWallSpacing, 0, (j) * vWallSpacing) + vGenStartPos;

                    Instantiate(vLava, vWallLoc, Quaternion.identity);
                }

                if (vSpace[i, j] == 2)
                {

                    vWallLoc = new Vector3(i * vWallSpacing, -.3f, (j) * vWallSpacing) + vGenStartPos;

                    Instantiate(vSpring, vWallLoc, Quaternion.identity);
                }

                if (vVisit[i,j])
                {

                    vWallLoc = new Vector3(i * vWallSpacing, 0, (j) * vWallSpacing) + vGenStartPos;

                    Instantiate(vLava, vWallLoc, Quaternion.identity);

                }


            }


        }

    }

    public void pAddLava()
    {
        for (i = 0; i < vMazeSize; i++)

        {

            for (j = 0; j < vMazeSize; j++)

            {

                float tvRand = Random.Range(0f, 1f);
                if (tvRand < vLavaChance)
                {
                    vSpace[i, j] = 1;

                }

                if (tvRand > (1 - vSpringChance))
                {
                    vSpace[i, j] = 2;
                }
            }




        }
    }
    public void pResetArray()
    {
        for (i = 0; i <= vMazeSize; i++)

        {

            for (j = 0; j <= vMazeSize; j++)
            {


                vVisit[i, j] = false;
                vWall[i, j, 0] = 1;
                vWall[i, j, 1] = 1;
            }
        }



    }

}
