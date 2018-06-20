using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimator : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D rbody;



	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
		rbody = GetComponent <Rigidbody2D> ();

		
	}
	
	// Update is called once per frame
	void Update () {

		if (rbody.velocity.x > 0.2) {
			anim.SetBool ("WalkRight", true);
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("IdleRight", false);
			anim.SetBool ("IdleLeft", false);
		}

		if (rbody.velocity.x < -0.2) {
			anim.SetBool ("WalkLeft", true);
			anim.SetBool ("IdleLeft", true);
			anim.SetBool ("RunRight", false);
			anim.SetBool ("IdleRight", false);
		}

		if (rbody.velocity.x > -0.2 && rbody.velocity.x < 0.2) {
			anim.SetBool ("RunRight", false);
			anim.SetBool ("RunLeft", false);
		}
			
	}
}
