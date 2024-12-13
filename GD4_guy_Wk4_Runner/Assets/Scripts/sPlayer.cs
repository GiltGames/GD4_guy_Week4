using JetBrains.Annotations;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sPlayer : MonoBehaviour
{
    //Variables about the player and movement
    public float vJumpForce = 4;
    public float vMoveSpeed = 4;
    Rigidbody rb;
    public bool fGrounded = true;
    public float vMoveLimitTop = 0;
    public float vMoveLimitBottom = -15 ;
    public float vMoveLimitLeft = 50;
    public float vMoveLimitRight = 75;
    public float vMoveTriggerfromRight = -5;
    public Vector3 vMoveVector = Vector3.zero;
    public Vector3 vNewPos = Vector3.zero;
    public float vGravity = 3;
    public GameObject JetPac;
    public bool fJetPac;



    // Scene move variables
    public bool fMove = false;
    public float vShiftTotal = 20;
    public float vShiftSp = 10;
    public float vSpeedBoost = 1.5f;

    //Walls

    public bool fWall;

    // Spring
    public float vSpringForce=1000;



    //game over and scores
    public TextMeshProUGUI tGameOver;
    public TextMeshProUGUI tScore;
    public bool fGameOver = false;
    public int vScore;



    //animations
    public Animator aAnim;
    public Transform PlayerSprite;
    public bool fRunning = false;
    public ParticleSystem Dirt;


    //sound
    public AudioSource aPlayer;
    public AudioClip vJumpSound;
    public AudioClip vDieSound;
    public AudioClip vThanks;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Physics.gravity * vGravity;
        PlayerSprite = transform.Find("PlayerSprite");
        aAnim = PlayerSprite.gameObject.GetComponent<Animator>();

        aPlayer = GetComponent<AudioSource>();
            
            }

    // Update is called once per frame
    void Update()
    {
       
        //reload
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.Scene scene = GetComponent<UnityEngine.SceneManagement.Scene>();
            Physics.gravity = Physics.gravity / vGravity;
            SceneManager.LoadScene(scene.buildIndex);


        }


        
        // If scene is shifting, isable input
        if (!fMove)
        {

            if (!fGameOver)
            {
                pJump();
                pMove();

             }
        }
       

        pLimits();

      
        //trigger scene move
        if (transform.position.x > (vMoveLimitRight+vMoveTriggerfromRight) || Input.GetKeyDown(KeyCode.Q))
            {
            fMove = true;

        }
       
        //reset child sprite to zero
        PlayerSprite.transform.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision);

        fGrounded = true;
        rb.linearVelocity = Vector3.zero;

        if (collision.gameObject.tag =="Wall")
        {
            
            fWall = true;
        }
        else
        {
            fWall=false;

        }


        if(collision.gameObject.tag =="Lava")
        {

            pGameOver();
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spring")
        {
           Destroy(other.gameObject);
            pSpring();

        }

        if (other.gameObject.tag == "Rescue")

        {
            Destroy(other.gameObject);
            vScore++;
            tScore.text = vScore.ToString();

            aPlayer.clip = vThanks;

            aPlayer.Play();

        }

    }



void pJump()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {

            if(!fGrounded && fJetPac)

            {
                fJetPac = false;
                fGrounded=true;
                JetPac.SetActive(false);
            }


            if (fGrounded)
            {
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(Vector3.up * vJumpForce,ForceMode.Impulse);
                fGrounded=false;
                aAnim.SetTrigger("Jump_trig");
                aPlayer.clip = vJumpSound;

                aPlayer.Play();

            }



        }


    }

    void pMove()
    {
        float vMoveV = Input.GetAxis("Vertical");
        float vMoveH = Input.GetAxis("Horizontal");


        vNewPos = transform.position;

        if(transform.position.y <.1f)
        {
            fGrounded = true;

        }
        if (fGrounded || fJetPac)
        {
            //facing and animiation

            fRunning = false;

            if (vMoveH >= 0)
            {
                transform.rotation = Quaternion.identity;
                  
                if (vMoveH >0)
                {
                    fRunning = true;

                }

            }

            if (vMoveH < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                fRunning = true;
            }


            if (vMoveV > 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                fRunning = true;
            }
            if (vMoveV < 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                fRunning = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (fGrounded)
            {
                Dirt.Play();
            }
        }

        if (fRunning)
        {
            aAnim.SetFloat("Speed_f", 1f);
            
            



        }
        else
        {
            aAnim.SetFloat("Speed_f", 0f);
            Dirt.Stop();    
        }



        vMoveVector = new Vector3((vMoveH*vMoveH)+(vMoveV * vMoveV), 0, 0).normalized * vMoveSpeed  * Time.deltaTime;

        //increase speed if shift is pressed

        if (Input.GetKey(KeyCode.LeftShift))
            {
            vMoveVector = vMoveVector* vSpeedBoost;
            }
        
        //reduce move if against wall

         if (fWall == true)
            { vMoveVector = vMoveVector / 1; }
        
        
        
        
        transform.Translate(vMoveVector);
       

       



    }

    public void pGameOver()
    {
        fGameOver = true;
        JetPac.SetActive(false);
        tGameOver.text = "Game Over";
        aAnim.SetInteger("DeathType_int", 1);
        
        aAnim.SetBool("Death_b", true);

        aPlayer.clip = vDieSound;

        aPlayer.Play();

    }
    void pSpring()
        //this is now the jetpac
    {
        
        JetPac.SetActive(true);
        fJetPac = true;
        

        

    }


    void pLimits()

    {
        vNewPos = transform.position;

                

        if (vNewPos.x > vMoveLimitRight)
        {
            vNewPos.x = vMoveLimitRight;
        }

        if (vNewPos.x < vMoveLimitLeft)
        {
            vNewPos.x = vMoveLimitLeft;
        }

        if (vNewPos.z > vMoveLimitTop)
        {
            vNewPos.z = vMoveLimitTop;
        }
        if (vNewPos.z < vMoveLimitBottom)
        {
            vNewPos.z = vMoveLimitBottom;
        }

        if (vNewPos.y > 50)
        {
            vNewPos.y = 50;
        }
        if (vNewPos.y < 0)
        {
            vNewPos.y = 0;
        }
        transform.position = vNewPos;
    }

}
