using UnityEngine;

public class sMoveScene : MonoBehaviour
{
    public sPlayer sPlayer;
    public GameObject Player;
    public float vShift;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.Find("Player");
        sPlayer = Player.GetComponent<sPlayer>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (sPlayer.fMove)

        {
            transform.Translate(Vector3.left * Time.deltaTime * sPlayer.vShiftSp);
           

          


        }


    }
}
