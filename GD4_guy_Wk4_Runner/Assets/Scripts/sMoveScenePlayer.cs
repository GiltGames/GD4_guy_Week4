using UnityEngine;

public class sMoveScenePlayer : MonoBehaviour
{
    public sPlayer sPlayer;
   
    public float vShift;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sPlayer = sPlayer.GetComponent<sPlayer>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (sPlayer.fMove)

        {
            transform.Translate(Vector3.left * Time.deltaTime * sPlayer.vShiftSp);

            vShift += Time.deltaTime * sPlayer.vShiftSp;

            if(transform.position.x < sPlayer.vMoveLimitLeft)

            {

                vShift = 0;
                sPlayer.fMove = false;

            }          


        }


    }
}
