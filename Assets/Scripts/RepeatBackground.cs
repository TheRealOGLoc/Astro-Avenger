using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 backgroundStartPosition;
    private float repeatPosition;

    // Start is called before the first frame update
    void Start()
    {
        backgroundStartPosition = transform.position;
        repeatPosition = GetComponent<BoxCollider>().size.x / 2; // get repeat x point
    }

    // Update is called once per frame
    void Update()
    {
        // repeat the background based on it's position
        if (transform.position.x < backgroundStartPosition.x - repeatPosition)
        {
            transform.position = backgroundStartPosition;
        }
    }
}
