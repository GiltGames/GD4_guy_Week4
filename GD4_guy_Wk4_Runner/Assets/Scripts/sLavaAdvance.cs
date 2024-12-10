using UnityEngine;

public class sLavaAdvance : MonoBehaviour
{
    public float vLavaAdvanceSp =0.5f;
    public GameObject Player;
    public sPlayer sPlayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sPlayer=Player.GetComponent<sPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(vLavaAdvanceSp*Time.deltaTime,0,0));
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag =="Player")
        {
            sPlayer.pGameOver();
        }
    }
 
    
       
    

}
