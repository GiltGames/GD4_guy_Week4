using JetBrains.Annotations;
using TMPro;
using UnityEngine;

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


    // Scene move variables
    public bool fMove = false;
    public float vShiftTotal = 20;
    public float vShiftSp = 10;


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




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Physics.gravity * vGravity;
        PlayerSprite = transform.Find("PlayerSprite");
        aAnim = PlayerSprite.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
            pSpring();

        }

        if (other.gameObject.tag == "Rescue")

        {
            Destroy(other.gameObject);
            vScore++;
            tScore.text = vScore.ToString();
        }

    }



void pJump()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {

            if (fGrounded)
            {
                rb.AddForce(Vector3.up * vJumpForce,ForceMode.Impulse);
                fGrounded=false;
                aAnim.SetTrigger("Jump_trig");
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
        if (fGrounded)
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

        if (fRunning)
        {
            aAnim.SetFloat("Speed_f", 1f);

        }
        else
        {
            aAnim.SetFloat("Speed_f", 0f);
        }



        vMoveVector = new Vector3((vMoveH*vMoveH)+(vMoveV * vMoveV), 0, 0).normalized * vMoveSpeed  * Time.deltaTime;
       
        
        //reduce move if against wall

         if (fWall == true)
            { vMoveVector = vMoveVector / 1; }
        
        
        
        
        transform.Translate(vMoveVector);
       

       



    }

    public void pGameOver()
    {
        fGameOver = true;
        tGameOver.text = "Game Over";
        aAnim.SetInteger("DeathType_int", 1);
        
        aAnim.SetBool("Death_b", true);


    }
    void pSpring()

    {
        vMoveVector = new Vector3(.5f, 1, 0) * vSpringForce ;
        rb.AddForce(vMoveVector, ForceMode.Impulse);
       

        

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
