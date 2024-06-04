using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigid;
    private Vector3 weaponOffset = new Vector3(0.7f, 0.8f, -0.49f);
    private Vector3 bulletOffset = new Vector3(1.84f, 1.11f, -0.02f);
    public ParticleSystem smoke;
    public GameObject explosion;
    private GameObject weapon;
    public GameObject bullet;
    public GameObject arduinoManager;
    public AudioClip collectDiamond;
    private AudioSource playerAudio;
    private ArduinoConnect arduinoConnection;
    public float speedVertical = 15;
    public float speedHorizontal = 7;
    public int weaponCountDownTime = 10;
    public float verticalInput;
    public float horizontalInput;
    public bool playerHasWeapon = false;
    public bool gameOver = false;
    public int playerScore = 0;
    private string path = Application.dataPath + "/test.txt";
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        arduinoConnection = arduinoManager.GetComponent<ArduinoConnect>();
        explosion = GameObject.Find("Explosion");
        explosion.SetActive(false);
        StartCoroutine(ShootingGap());
        smoke.Play();
    }

    // Update is called once per frame
    void Update()
    {   
        // if game is not over, get user's input and move player
        if (!gameOver)
        {
            //float? newY;
            if (arduinoConnection.serialPort.IsOpen)
            {
                verticalInput = arduinoConnection.ConvertYCoordinate();
                //float? y = arduinoConnection.GetCurrentCoordinate()[1];
                //newY = 0.2896f * y - 37.8188f;
            }
            else
            {
                //newY = transform.position.y;
                verticalInput = Input.GetAxis("Vertical");
            }
            horizontalInput = Input.GetAxis("Horizontal");
            //Vector3 newPosition = transform.position;
            //newPosition.y = newY.Value;
            //transform.position = newPosition;
            transform.Translate(Vector3.up * speedVertical * Time.deltaTime * verticalInput);
            transform.Translate(Vector3.forward * speedHorizontal * Time.deltaTime * horizontalInput);
        } else // if game is over, make player goes down
        {
            playerRigid.useGravity = true;
        }
        
        // if the player is out of the boundary, set player's positon below the boundart
        if (transform.position.y > 6.5)
        {
            transform.position = new Vector3(transform.position.x, 6.49f, transform.position.z);
        } else if (transform.position.y < -8 && !gameOver)
        {
            transform.position = new Vector3(transform.position.x, -7.99f, transform.position.z);
        } else if (transform.position.x < -52)
        {
            transform.position = new Vector3(-51.9f, transform.position.y, transform.position.z);
        } else if (transform.position.x > -28)
        {
            transform.position = new Vector3(-28.1f, transform.position.y, transform.position.z);
        }

        // if player has weapon, add offset to the weapon's positon
        if (playerHasWeapon)
        {
            weapon.transform.position = transform.position + weaponOffset;
        }

        // if player has weapon and press the space, shot the bullet
        
        if (playerHasWeapon && Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            weapon.GetComponent<ParticleSystem>().Play();
            Instantiate(bullet, transform.position + bulletOffset, bullet.transform.rotation);
        } else if (playerHasWeapon && Input.GetKeyUp(KeyCode.Space))
        {
            weapon.GetComponent<ParticleSystem>().Stop();
        }
    }

    // Automatic shooting when connected to arduino
    IEnumerator ShootingGap()
    {
        while (!gameOver)
        {
            if (playerHasWeapon)
            {
                weapon.GetComponent<ParticleSystem>().Play();
                Instantiate(bullet, transform.position + bulletOffset, bullet.transform.rotation);
            } 
            yield return new WaitForSeconds(0.2f);
        }
        
    }

    // check if the player hit other game object
    private void OnTriggerEnter(Collider other)
    {
        // if the other is rocket, destory rocket and active explosion
        if (other.gameObject.CompareTag("Enemy Rocket"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 10, ForceMode.Impulse);
            explosion.transform.position = other.gameObject.transform.position;
            explosion.SetActive(true);
            gameOver = true;
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Enemy Monster")) // if other is monster, game over
        {
            gameOver = true;
        } else if (other.gameObject.CompareTag("Diamond")) // if other is diamond, add score
        {
            playerAudio.PlayOneShot(collectDiamond);
            Destroy(other.gameObject);
            playerScore++;
        } else if (other.gameObject.CompareTag("Weapon")) // is other is weapon, pick up weapon
        {
            weapon = other.gameObject;
            playerHasWeapon = true;
            weapon.transform.position = gameObject.transform.position;
            StartCoroutine(WeaponCountDown());
        }
    }

    // if player pick up the weapon, start a weapon count down
    IEnumerator WeaponCountDown()
    {
        yield return new WaitForSeconds(weaponCountDownTime);
        playerHasWeapon = false;
        Destroy(weapon);
    }
}
