using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerXPos = 0;
    float playerYPos = 0;
    public float speed = 0;
    public float jumpForce = 0;

    public Rigidbody2D rb;

    public string coinPickupSoundName = "Money";

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("Audio manager found in scene");
        }
    }

    void Update()
    {
        playerXPos = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //playerYPos = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        
        transform.Translate(playerXPos, playerYPos, 0);

        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(Vector3.up * jumpForce);
            //jumpForce = (Time.deltaTime * 200);
            Debug.Log("Jump pressed, force = " + jumpForce);
        }

        //if (Input.GetKeyUp("space"))
        //{
            
        //    jumpForce = 0f;
        //    Debug.Log("Jump released");
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            audioManager.PlaySound(coinPickupSoundName);
            Destroy(collision.gameObject);
        }
    }
}