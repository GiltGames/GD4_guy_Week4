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

    [SerializeField] float vLavaChance = .15f;
 //   [SerializeField] float vSpringChance = 0.1f;
    //  public string[] vWallTest = new string[5];
    // public bool[,] vSpace = new bool[5, 5];
    public float vWallSpacing = 5;
    public float vLavaspacingAdj = 0.45f;

    public GameObject vWallHor;
    public GameObject vWallVer;
    public GameObject vLavaWall;
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
    public GameObject vMarker;


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

        vWallSpacing = vWallHor.GetComponent<BoxCollider>().size.z;


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
        vWallSpacing = vWallHor.GetComponent<BoxCollider>().size.z;

        pResetArray();

      

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

               /* if (tvRand > (1 - vSpringChance))
                {
                    vSpace[i, j] = 2;
                }
               */
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

        //  i = Random.Range(1, vMazeSize );
        //  j = Random.Range(1, vMazeSize );

        // Start at 1,1 and see o/

        i = 1;
        j = 1;


       // Debug.Log("start at " + i + j);


        vVisit[i, j] = true;
        vVisitTotal = 1;


        //while not all spaces have been checked

        while (vVisitTotal < (vMazeSize-1) * (vMazeSize-1))
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

                    if (i == vMazeSize-1 && vRandomWall == 0)
                    {
                        vRandomWall = 2;
                    }

                    if (j == vMazeSize-1 && vRandomWall == 1)
                    {
                        vRandomWall = 3;
                    }

               //     Debug.Log("Start cell " + i + j);
               //     Debug.Log("Random Wall " + vRandomWall);

            //Walk into next space
            int vLavaorSpace;

            if (Random.Range(0, 1f) < vLavaChance)
            {
                vLavaorSpace = 2;


            }

            else
            {
                vLavaorSpace = 0;
            }



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

                  //  Debug.Log("Spaces vistied " + vVisitTotal);
                  //  Debug.Log("new cell " + newi + newj);
                    if (vVisit[newi, newj] == false)
                        {
                            vVisitTotal = vVisitTotal + 1;
                            Debug.Log("New Cell - "+newi+" : "+newj);
                Debug.Log("RandomWall was " + vRandomWall);


                            vVisit[newi, newj] = true;

                                 //Is wall lava instead of empty?

                         
                                 ///destroy wall
                            switch (vRandomWall)

                            {
                                case 0:

                                    vWall[i, j, 0] = vLavaorSpace;
                        Debug.Log("Wall down" + i+":" + j +" 0");
                                    break;

                                case 1:
                                    vWall[i, j, 1] = vLavaorSpace;
                        Debug.Log("Wall down" + i+":" + j+" 1");

                        break;
                                case 2:
                                    vWall[newi, j, 0] = vLavaorSpace;
                        Debug.Log("Wall down" + newi + ":" + j + " 0");
                        break;
                                case 3:
                                    vWall[i, newj, 1] = vLavaorSpace;
                        Debug.Log("Wall down" + i + ":" + newj + " 1");
                        break;



                            }


                         }

                    i = newi;
                    j = newj;
                    //Count spaces visited
                    vVisitTotal = 0;
                    for (int k = 1; k <= vMazeSize-1 ; k++)
                            {
                                for (int l = 1; l <= vMazeSize-1; l++)
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
            vWall[k, vMazeSize, 1] = 1;
            vWall[vMazeSize, k, 0] = 1;
            vWall[k, vMazeSize, 0] = 0;
            vWall[vMazeSize, k, 1] = 0;
        }

        vWall[0, Random.Range(1, vMazeSize-1), 0] = 0;
        vWall[vMazeSize-1, Random.Range(1, vMazeSize-1), 0] = 0;


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


                //normal walls

                if (vWall[i, j, 0] == 1)
                {
                    vWallLoc = new Vector3((i + .5f) * vWallSpacing, 0 , j * vWallSpacing) + vGenStartPos;

                    Instantiate(vWallHor, vWallLoc, Quaternion.identity);


                }

                if (vWall[i, j, 1] == 1)
                {
                    vWallLoc = new Vector3(i * vWallSpacing,0, (j + 0.5f) * vWallSpacing) + vGenStartPos;


                    Instantiate(vWallHor, vWallLoc, Quaternion.Euler(0, 90, 0));


                }

                //lava walls
                //

                if (vWall[i, j, 0] == 2)
                {
                    vWallLoc = new Vector3((i + vLavaspacingAdj) * vWallSpacing, 0, (j+ vLavaspacingAdj) * vWallSpacing) + vGenStartPos;

                    Instantiate(vLavaWall, vWallLoc, Quaternion.identity);


                }

                if (vWall[i, j, 1] == 2)
                {
                    vWallLoc = new Vector3((i+ vLavaspacingAdj)* vWallSpacing, 0, (j + vLavaspacingAdj) * vWallSpacing) + vGenStartPos;


                    Instantiate(vLavaWall, vWallLoc, Quaternion.Euler(0, 90, 0));


                }

                /*
                if (vVisit[i,j])
                {
                    vWallLoc = new Vector3((i ) * vWallSpacing, 0, (j) * vWallSpacing) + vGenStartPos;


                    Instantiate(vMarker, vWallLoc, Quaternion.Euler(0, 90, 0));

                }


                if (vSpace[i, j] == 1)
                {

                    vWallLoc = new Vector3(i * vWallSpacing, -0.1f, (j) * vWallSpacing) + vGenStartPos;

                    Instantiate(vLava, vWallLoc, Quaternion.identity);
                }
*/
                //change to jetpac

                if (vSpace[i, j] ==2)
                {

                    vWallLoc = new Vector3(i * vWallSpacing,1, (j) * vWallSpacing) + vGenStartPos;

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
        i =Random.Range(1,vMazeSize-1);
        j = Random.Range(1, vMazeSize-1);
        vSpace[i, j] = 3;


        // add jetpac not if rescue space
        i = Random.Range(1, vMazeSize-1);
        j = Random.Range(1, vMazeSize-1);

       
        
            vSpace[i, j] = 2;
        



    }

    }

