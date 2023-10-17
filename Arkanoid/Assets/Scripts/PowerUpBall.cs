using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBall : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public AudioClip launch;
    public AudioClip hits;
    private AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D coll) {
        audioSource.PlayOneShot(hits);

    	if(coll.collider.CompareTag("Player"))
		{
            Vector2 vel;
			vel.y = rb2d.velocity.y*-1.0f;
    	}
        if(coll.collider.CompareTag("TagBall"))
        {
            GoPowerUp();
        }
        // if(coll.collider.CompareTag("TagWall"))
        // {
        //     GoPowerUp();
        // }
    }

    private void GoPowerUp(){
    	float rand = Random.Range(0, 2);

    	if(rand < 1){
        	rb2d.AddForce(new Vector2(20, 15));
    	} else {
        	rb2d.AddForce(new Vector2(-20, 15));
    	}
    }

    public void positionPowerUp(){
    	rb2d.velocity = Vector2.zero;

		var pos = transform.position;

		pos.x = 2f;
		pos.y = -3.5f;

    	transform.position = pos;
        audioSource.PlayOneShot(launch);
    }

    private void ResetPowerUp(){
    	rb2d.velocity = Vector2.zero;

		var pos = transform.position;

		pos.x = 2f;
		pos.y = 6f;

    	transform.position = pos;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
