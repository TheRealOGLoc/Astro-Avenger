using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BackgroundMoveLeft class controls the background move left
public class BackgroundMoveLeft : MonoBehaviour
{
    public float speed = 20;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the game is not over, move background left
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
