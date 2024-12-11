using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class sMazeGen : MonoBehaviour
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


    public GameObject vRescue;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (i = 0; i < vMazeSize; i++)

        {

            for (j = 0; j < vMazeSize; j++)
            {


                vWall[i, j, 0] = 0;
                vWall[i, j, 1] = 0;



            }





        }
        vGenStartPos = new Vector3(transform.position.x, 0, transform.position.z);
        pMazeGen();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            vGenStartPos = new Vector3(transform.position.x, 0, transform.position.z);
            pMazeGen();
        }
    }

    public void pMazeGen()
    {

        //reset array


        pResetArray();

        //generate wall positions and pritn  - v1



        /*
            for (int i = 0; i < vMazeSize; i++)

        {
           // vWallTest[i] = "";

              for (int j = 0; j < vMazeSize; j++)
              {
                  vWhichWall = Random.Range(0, 2);
                  Debug.Log("Wal set up" + i + j + vWhichWall);
               
                //  vWall[i, j] = vWhichWall;
                
                if (vWhichWall == 0)
                {
                    vWallLoc = new Vector3((i + .5f) * vWallSpacing, 0, j * vWallSpacing) + vGenStartPos;

                    Instantiate(vWallVer, vWallLoc, Quaternion.identity);

                    Debug.Log("Vertical Wall");
                }

                else
                {
                    vWallLoc = new Vector3(i * vWallSpacing, 0, (j + 0.5f) * vWallSpacing) +vGenStartPos;
                   

                    Instantiate(vWallHor, vWallLoc, Quaternion.identity);
                    Debug.Log("Horizontal Wall");

                }


                //           vWallTest[i] = vWallTest[i] + vWall[i, j, 0];
            }

           // Debug.Log(vWallTest[i]);
        }
        */

        //Generte wall positions

        if (vMazeType == 0)
        {
            pMaze0();
        }
        //end of mazetype 0



        if (vMazeType == 1)

        //Aldous-Broder Algorithm
        //reset array
        {
            pMaze1();
            

        }
        //end of mazetype1




        //end of maze types








        //create generated walls
        pInstantiateMaze();

        

    }
    public void pAddLava()
    {
        for (i = 1; i < vMazeSize; i++)

        {

            for (j = 1; j < vMazeSize; j++)

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
            for (i = 0; i < vMazeSize; i++)

            {

                for (j = 0; j < vMazeSize; j++)
                {


                    vVisit[i, j] = false;
                    vWall[i, j, 0] = 1;
                    vWall[i, j, 1] = 1;
                    vSpace[i, j] = 0;
                }
            }



    }

    public void pMaze0()
 {   for (i = 0; i<vMazeSize; i++)

            {
                for (j = 0; j<vMazeSize; j++)
                    {
                        vWhichWall = Random.Range(0, 2);
                        Debug.Log("Wal set up" + i + j + vWhichWall);



                        if (vWhichWall == 0)
                        {
                            vWall[i, j, 0] = 0;

                        }

                        else
                        {
                            vWall[i, j, 1] = 0;
                        }
                
                    }
                
            
            }

        pAddLava();
    }

    public void pMaze1()
    {
        pResetArray();

        // Start of Walk



        Debug.Log("Start Maze Generation");

        i = Random.Range(1, vMazeSize );
        j = Random.Range(1, vMazeSize );

        Debug.Log("start at " + i + j);


        vVisit[i, j] = true;
        vVisitTotal = 1;


        //while not all spaces have been checked

        while (vVisitTotal < (vMazeSize) * (vMazeSize))
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
                            Debug.Log("New Cell - total now "+vVisitTotal);

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
                    for (int k = 1; k <= vMazeSize ; k++)
                            {
                                for (int l = 1; l <= vMazeSize; l++)
                                {
                                    if (vVisit[k, l])
                                        { vVisitTotal++; }


                                }
                            }
                      Debug.Log("Total visited by count =" + vVisitTotal);

                }

        //remove randdom entrance and exit walls

        for (int k = 0; k <= vMazeSize; k++)
        {
            vWall[k, 0, 1] = 1;
            vWall[0, k, 1] = 0;
            vWall[0, k, 0] = 1;
            vWall[k, 0, 0] = 0;
            vWall[k, vMazeSize-1, 1] = 1;
            vWall[vMazeSize-1, k, 0] = 1;
            vWall[k, vMazeSize-1, 0] = 0;
            vWall[vMazeSize-1, k, 1] = 0;
        }

        vWall[0, Random.Range(1, vMazeSize), 0] = 0;
        vWall[vMazeSize-1, Random.Range(1, vMazeSize), 0] = 0;


        //add Lava
        pAddLava();

        pAddRescue();

    }



    public void pInstantiateMaze()


    {
        for (int i = 0; i < vMazeSize; i++)

        {


            for (int j = 0; j < vMazeSize; j++)
            {


                if (vWall[i, j, 0] == 1)
                {
                    vWallLoc = new Vector3((i + .5f) * vWallSpacing, 0 , j * vWallSpacing) + vGenStartPos;

                    Instantiate(vWallVer, vWallLoc, Quaternion.identity);


                }

                if (vWall[i, j, 1] == 1)
                {
                    vWallLoc = new Vector3(i * vWallSpacing,0, (j + 0.5f) * vWallSpacing) + vGenStartPos;


                    Instantiate(vWallHor, vWallLoc, Quaternion.Euler(0, 90, 0));


                }

                if (vSpace[i, j] == 1)
                {

                    vWallLoc = new Vector3(i * vWallSpacing, -0.1f, (j) * vWallSpacing) + vGenStartPos;

                    Instantiate(vLava, vWallLoc, Quaternion.identity);
                }

                if (vSpace[i, j] == 2)
                {

                    vWallLoc = new Vector3(i * vWallSpacing, -.7f, (j) * vWallSpacing) + vGenStartPos;

                    Instantiate(vSpring, vWallLoc, Quaternion.identity);
                }


                if (vSpace[i, j] == 3)
                {

                    vWallLoc = new Vector3(i * vWallSpacing, .1f, (j) * vWallSpacing) + vGenStartPos;

                    Instantiate(vRescue, vWallLoc, Quaternion.Euler(0,180,0));
                }

            }


        }

    }

    public void pAddRescue()
    {
        i =Random.Range(1,vMazeSize);
        j = Random.Range(1, vMazeSize);
        vSpace[i, j] = 3;


    }

    }

