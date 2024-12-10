using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class sMazeGen : MonoBehaviour
{

    public int vMazeSize = 4   ;
  public int[,,] vWall = new int[10,10,2];
  //  public string[] vWallTest = new string[5];
   // public bool[,] vSpace = new bool[5, 5];
    public float vWallSpacing = 5   ;
    public GameObject vWallHor;
    public GameObject vWallVer;
  
    public Vector3 vWallLoc;
    public Vector3 vGenStartPos;
    public int vWhichWall;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < vMazeSize; i++)

        {
            
            for (int j = 0; j < vMazeSize; j++)
            {


                vWall[i, j,0] = 0;
                 vWall[i, j, 1] = 0;

                     

            }

            



        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            vGenStartPos = new Vector3(transform.position.x,1  ,transform.position.z);
            pMazeGen();
        }
    }

    public void pMazeGen()
    {

        //reset array


        for (int i = 0; i < vMazeSize; i++)

        {

            for (int j = 0; j < vMazeSize; j++)
            {


                vWall[i, j, 0] = 0;
                vWall[i, j, 1] = 0;



            }
        }

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


        for (int i = 0; i < vMazeSize; i++)

        {
            // vWallTest[i] = "";

            for (int j = 0; j < vMazeSize; j++)
            {
                vWhichWall = Random.Range(0, 2);
                Debug.Log("Wal set up" + i + j + vWhichWall);

               

                if (vWhichWall == 0)
                {
                    vWall[i, j, 0] = 1;

                }

                else
                {
                    vWall[i, j, 1] = 1;
                }


            }

        }



        //create generated walls


        for (int i = 0; i < vMazeSize; i++)

        {
          

            for (int j = 0; j < vMazeSize; j++)
            {
                

                if (vWall[i,j,0] == 1)
                {
                    vWallLoc = new Vector3((i + .5f) * vWallSpacing, 0, j * vWallSpacing) + vGenStartPos;

                    Instantiate(vWallVer, vWallLoc, Quaternion.identity);

                    
                }

                if (vWall[i,j,1] == 1)
                {
                    vWallLoc = new Vector3(i * vWallSpacing, 0, (j + 0.5f) * vWallSpacing) + vGenStartPos;


                    Instantiate(vWallHor, vWallLoc, Quaternion.Euler(0,90,0));
                    

                }


            }


        }
    }


}
