using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondRotate : MonoBehaviour
{
    private float speedForward = 10;
    private Vector3 diamondDirection = new Vector3(-1.0f, 0.0f, 0.0f);
    public PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the game is not over, move diamond to left
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(diamondDirection * Time.deltaTime * speedForward);
        }
    }
}
