using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    
    // private AudioSource audioPlayer;

    public void GoBonus(){
    	float rand = Random.Range(0, 2);

    	if(rand < 1){
        	rb2d.AddForce(new Vector2(20, -15));
    	} else {
        	rb2d.AddForce(new Vector2(-20, -15));
    	}
    }

    public void OnCollisionEnter2D(Collision2D coll) {
    	if(coll.collider.CompareTag("Player"))
		{
            // audioPlayer.Play();
            Destroy(gameObject);
    	}
        if(coll.collider.CompareTag("TagBall"))
        {
            GoBonus();
        }
    }

    public void ResetBonus(){
    	rb2d.velocity = Vector2.zero;

		var pos = transform.position;

		pos.x = 0f;
		pos.y = 6f;

    	transform.position = pos;
    }

    public void positionBonus(){
    	rb2d.velocity = Vector2.zero;

		var pos = transform.position;

		pos.x = 0f;
		pos.y = 5.2f;

    	transform.position = pos;
    }

    public void RestartBonus(){
    	ResetBonus();
        Invoke("GoBonus", 2);
    }

    public void dropBonus(){
        Invoke("positionBonus", 1);
        Invoke("GoBonus", 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        // audioPlayer = GetComponent<AudioSource>() ;
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}