using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMoveLeft : MonoBehaviour
{
    private float rocketSpeed = 13;
    public GameObject player;
    public PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the game is not over, move rocket left
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.up * Time.deltaTime * rocketSpeed);
        }
    }
}
