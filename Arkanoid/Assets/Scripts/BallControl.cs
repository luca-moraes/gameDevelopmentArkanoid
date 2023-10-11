using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private AudioSource audioPlayer;

    void GoBall(){
    	float rand = Random.Range(0, 2);
    	if(rand < 1){
        	rb2d.AddForce(new Vector2(20, 15));
    	} else {
        	rb2d.AddForce(new Vector2(-20, 15));
    	}
    }

    void OnCollisionEnter2D(Collision2D coll) {
    	if(coll.collider.CompareTag("Player"))
		{
        	Vector2 vel;
        	vel.x = rb2d.velocity.x*1.02f;
        	vel.y = ((rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3))*1.05f;
        	rb2d.velocity = vel;
			rb2d.AddForce(new Vector2(0.1f*vel.x, 0.1f*vel.y));
    	}

	    // audioPlayer.Play();
    }

     void ResetBall(){
    	rb2d.velocity = Vector2.zero;

		var pos = transform.position;

		pos.x = 0f;
		pos.y = -3.5f;

    	transform.position = pos;
    }

     void RestartGame(){
    	ResetBall();
    	Invoke("GoBall", 2);
    }

    void Start()
    {
	    audioPlayer = GetComponent<AudioSource>() ;
        rb2d = GetComponent<Rigidbody2D>();
    	Invoke("GoBall", 1);
    }

    void Update()
    {
    }
}
