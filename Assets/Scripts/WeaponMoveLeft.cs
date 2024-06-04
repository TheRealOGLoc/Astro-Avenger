using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMoveLeft : MonoBehaviour
{
    public float speed = 8.0f;
    public PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the game is not over, move weapon left
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        
    }
}
