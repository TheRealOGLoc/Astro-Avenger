using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BulletMoveRight controlls the bullet move left
public class BulletMoveRight : MonoBehaviour
{
    public float bulletSpeed = 15.0f;
    private GameObject player;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
    }

    // If the other's tag is monster tag, destroy other's gameObject
    // and add score
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy Monster"))
        {
            playerControllerScript.playerScore += 2;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
