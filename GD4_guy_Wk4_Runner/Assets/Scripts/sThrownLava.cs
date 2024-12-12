using UnityEngine;

public class sThrownLava : MonoBehaviour
{

    public sPlayer _sPlayer;
    public ParticleSystem vLava;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sPlayer = GameObject.Find("Player").GetComponent<sPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _sPlayer.fGameOver = true;


        }


        Destroy(gameObject);

        ParticleSystem explode = Instantiate(vLava,transform.position, Quaternion.identity);
        explode.Play();
        Destroy(explode, 5);
    }

}
