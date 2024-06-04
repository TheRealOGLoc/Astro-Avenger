using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBoundary : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the gameObject is out of boundary, destory
        if (transform.position.x < -100 || transform.position.x > 0)
        {
            Destroy(gameObject);
        }
    }
}
