using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSword : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed = 10.0f;
    public float partialSpeed = 0.05f;
    public float boundX = 2.5f;
    private bool activeMoving = false;
    private bool subindo = false;
    public AudioClip launch;
    public AudioClip hits;
    private AudioSource audioSource;

    private void GoPowerUp(){
    	activeMoving = true;
    }

    private void ResetPowerUp(){
    	rb2d.velocity = Vector2.zero;

		var pos = transform.position;

		pos.x = 2f;
		pos.y = 6f;

    	transform.position = pos;
    }

    public void positionPowerUp(){
    	rb2d.velocity = Vector2.zero;

		var pos = transform.position;

		pos.x = 0f;
		pos.y = -5f;

    	transform.position = pos;
        audioSource.PlayOneShot(launch);
    }

    public void turnOn(){
        Invoke("positionPowerUp", 1);
        Invoke("GoPowerUp", 2);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();       
        audioSource = GetComponent<AudioSource>(); 
        boundX = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeMoving){
            if(subindo)
            {
                if(transform.position.x < boundX){
                    transform.position = new Vector2(transform.position.x + partialSpeed, transform.position.y);
                } else {
                    audioSource.PlayOneShot(hits);
                    subindo = false;
                }
            } 
            else 
            {
                if(transform.position.x > -boundX){
                    transform.position = new Vector2(transform.position.x - partialSpeed, transform.position.y);
                 } else {
                    audioSource.PlayOneShot(hits);
                    subindo = true;
                }
            }                                                                   
        }
    }
}
