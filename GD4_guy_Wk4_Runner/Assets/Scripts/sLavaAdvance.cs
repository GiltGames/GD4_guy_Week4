using UnityEngine;

public class sLavaAdvance : MonoBehaviour
{
    public float vLavaAdvanceSp =0.5f;
    public GameObject Player;
    public sPlayer sPlayer;
    [SerializeField] float vLavaLimit=15;
    [SerializeField] float vThrowInterval=3;
    [SerializeField] float vThrowForce=1000;
    [SerializeField] float vThrownTimer=3;
    [SerializeField] float vThrownPos=8;
    public Vector3 vRockGenPos;
    public GameObject ThrownRock;
    public GameObject vThrownRock;
    public Rigidbody rb;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sPlayer=Player.GetComponent<sPlayer>();

    }

    // Update is called once per frame
    void Update()
    {

        //move lava
        transform.Translate(new Vector3(vLavaAdvanceSp*Time.deltaTime,0,0),Space.World);
        
        //reset if it falls too far behind
        if (transform.position.x < vLavaLimit)
        {
            transform.position = new Vector3(vLavaLimit,transform.position.y, transform.position.z);

        }


        //Throw rock

        if (vThrownTimer < 0)
        {
            pThrowRock();
            vThrownTimer = vThrowInterval;
        }
        vThrownTimer -= Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag =="Player")
        {
            sPlayer.pGameOver();
        }
    }
 
    void pThrowRock()
    {
        vRockGenPos = new Vector3(transform.position.x, vThrownPos, Random.Range(sPlayer.vMoveLimitBottom, sPlayer.vMoveLimitTop));
       vThrownRock = Instantiate(ThrownRock,vRockGenPos, Quaternion.identity);
        //ThrownRock.transform.position = new Vector3(ThrownRock.transform.position.x,vThrownPos, Random.Range(sPlayer.vMoveLimitBottom, sPlayer.vMoveLimitTop));
        rb = vThrownRock.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(1, 1, 0)*vThrowForce, ForceMode.Impulse);
    }
       
    

}
