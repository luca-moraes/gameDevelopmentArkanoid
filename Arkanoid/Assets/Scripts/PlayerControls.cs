using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
  public KeyCode moveUp = KeyCode.A;
  public KeyCode moveDown = KeyCode.S;
  public float speed = 10.0f;
  public float boundX = 2.5f;
  private Rigidbody2D rb2d;

  void Start()
  {
    rb2d = GetComponent<Rigidbody2D>();   
	boundX = 2.5f; 
  }

  void Update()
  {
    var vel = rb2d.velocity;

    if (Input.GetKey(moveUp)) {
			vel.x = speed;
		}
		else if (Input.GetKey(moveDown)) {
			vel.x = -speed;
		}
		else {
			vel.x = 0;
		}

		rb2d.velocity = vel;
		var pos = transform.position;

		if (pos.x > boundX) {
			pos.x = boundX;
		}
		else if (pos.x < -boundX) {
			pos.x = -boundX;
		}

    transform.position = pos;
  }
}

