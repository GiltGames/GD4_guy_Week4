using UnityEngine;

public class sSmoke : MonoBehaviour
{

    public float vSmokeInterval=4;
    public float vSmokeTimer;
    public sPlayer _sPlayer;
    public ParticleSystem Smoke;
    public Vector3 vSmokePos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vSmokeTimer -= Time.deltaTime;

        if (vSmokeTimer <= 0 )

        {
            vSmokeTimer = vSmokeInterval;

            vSmokePos = new Vector3(Random.Range(_sPlayer.vMoveLimitLeft,_sPlayer.vMoveLimitRight),0,Random.Range(_sPlayer.vMoveLimitBottom,_sPlayer.vMoveLimitTop));

            ParticleSystem newsmoke= Instantiate(Smoke, vSmokePos, Quaternion.identity);

            Destroy(newsmoke,5);
        }



    }
}
