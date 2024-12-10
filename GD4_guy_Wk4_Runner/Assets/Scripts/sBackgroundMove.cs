using UnityEngine;

public class sBackgroundMove : MonoBehaviour
{
    [SerializeField] Vector3 vBackStartPos;
    [SerializeField] float vBackSiz;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vBackStartPos = transform.position;
        vBackSiz = GetComponent<BoxCollider>().size.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < vBackStartPos.x - vBackSiz/2)
        {
            transform.position = vBackStartPos;

        }
    }
}
