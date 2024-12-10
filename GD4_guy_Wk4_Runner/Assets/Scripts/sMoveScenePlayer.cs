using UnityEngine;

public class sMoveScenePlayer : MonoBehaviour
{
    public sPlayer sPlayer;
   
    public float vShift;
    [SerializeField] Transform Spawner;

    //Call MazeGen
    public sMazeGen sMazeGen;
    [SerializeField] Vector3 vGenStartPosfromMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sPlayer = sPlayer.GetComponent<sPlayer>();
        sMazeGen = sMazeGen.GetComponent<sMazeGen>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (sPlayer.fMove)

        {
          // Time.timeScale = 0;
            transform.Translate(Vector3.left * Time.deltaTime * sPlayer.vShiftSp);

            vShift += Time.deltaTime * sPlayer.vShiftSp;

            if(transform.position.x < sPlayer.vMoveLimitLeft)

            {

                vShift = 0;
                sPlayer.fMove = false;
                // Time.timeScale = 1;

                vGenStartPosfromMove = Spawner.transform.position;
                sMazeGen.vGenStartPos = vGenStartPosfromMove;

                sMazeGen.pMazeGen();

            }          


        }


    }
}
