using UnityEngine;

public class sOOBDestroy : MonoBehaviour
{
    public float OOB = 10;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < OOB || transform.position.y < -OOB)
        {
            Destroy(gameObject);

        }
    }
}
