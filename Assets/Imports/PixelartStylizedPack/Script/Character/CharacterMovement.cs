using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	public float maxSpeed;
	private Animator anim;
	Rigidbody2D rbody;



	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		}

	void Update(){


		}
		
	void FixedUpdate ()
	{
		
		Vector2 movement_vector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (movement_vector != Vector2.zero) {
			anim.SetBool ("RunRight", true);
	
		} else {
			anim.SetBool ("RunUp", false);

		}

		rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime);

		float move = Input.GetAxis ("Horizontal");
	
		rbody.velocity = new Vector2 (move * maxSpeed, rbody.velocity.y);

		if (rbody.velocity.x > 0)
			transform.localScale = new Vector3 (1f, 1f, 1f);
		else if (rbody.velocity.x < 0)
			transform.localScale = new Vector3 (-1f, 1f, 1f);

		}
	}
