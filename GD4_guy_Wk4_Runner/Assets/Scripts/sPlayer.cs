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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= vGravity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fMove)
        { 
        pJump();
        pMove();
    }
       
        
        if (transform.position.x > (vMoveLimitRight+vMoveTriggerfromRight) || Input.GetKeyDown(KeyCode.Q))
            {
            fMove = true;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        fGrounded = true;

        if (collision.gameObject.tag =="Wall")
        {

            fWall = true;
        }
        else
        {
            fWall=false;

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
            }



        }


    }

    void pMove()
    {
        float vMoveV = Input.GetAxis("Vertical");
        float vMoveH = Input.GetAxis("Horizontal");

        vNewPos = transform.position;


        if (fGrounded)
        {

            //facing
            //


            if (vMoveH >= 0)
            {
                transform.rotation = Quaternion.identity;


            }

            if (vMoveH < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);

            }


            if (vMoveV > 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);

            }
            if (vMoveV < 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);

            }






            // rb.linearVelocity = (transform.right * vMoveSpeed * Time.deltaTime * (vMoveV + vMoveH) * (vMoveV + vMoveH));







        }

        if (fWall = true)
        { vMoveVector = vMoveVector / 5; }

        vNewPos = vNewPos + vMoveVector;

        vMoveVector = new Vector3(vMoveH, 0, vMoveV) * vMoveSpeed * Time.deltaTime;
        //Limits

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


        transform.position = vNewPos;

    }



}
