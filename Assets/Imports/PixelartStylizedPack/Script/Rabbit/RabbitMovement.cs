using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour {

	public float MoveSpeed;
	private Vector2 MinWalkPoint;
	private Vector2 MaxWalkPoint;
	public bool IsWalking;
	private Rigidbody2D rbody;
	public float WalkTime;
	private float WalkCounter;
	public float WaitTime;
	private float WaitCounter;
	private int WalkDirection;
	public Collider2D WalkZone;
	private bool hasWalkZone;


	// Use this for initialization
	void Start () {

		if (WalkZone != null) {


			rbody = GetComponent<Rigidbody2D> ();
			MinWalkPoint = WalkZone.bounds.min;
			MaxWalkPoint = WalkZone.bounds.max;
			hasWalkZone = true;

		}



		WaitCounter = WaitTime;
		WalkCounter = WalkTime;

		ChooseDirection ();

	}
	void Update () {
	
		if(IsWalking)
		{
			WalkCounter -= Time.deltaTime;

			if(WalkCounter < 0)
			{
				IsWalking = false;
				WaitCounter = WaitTime;
			}
			switch(WalkDirection)
			{
			case 0:
				rbody.velocity = new Vector2 (0, MoveSpeed);
				if (hasWalkZone && transform.position.y > MaxWalkPoint.y) {
					IsWalking = false;
					WaitCounter = WaitTime;
				}
				break;

			case 1:
				rbody.velocity = new Vector2(MoveSpeed, 0);
				if (hasWalkZone && transform.position.x > MaxWalkPoint.x) {
					IsWalking = false;
					WaitCounter = WaitTime;
				}




				break;
			case 2:
				rbody.velocity = new Vector2(0, -MoveSpeed);
				if (hasWalkZone && transform.position.y < MinWalkPoint.y) {
					IsWalking = false;
					WaitCounter = WaitTime;
				}
				break;

			case 3:
				rbody.velocity = new Vector2 (-MoveSpeed, 0);
				if (hasWalkZone && transform.position.x < MinWalkPoint.x) {
					IsWalking = false;
					WaitCounter = WaitTime;
				}
				break;
			}
		}
			else
			{
				WaitCounter -= Time.deltaTime;

			rbody.velocity = Vector2.zero;

				if(WaitCounter < 0)
				{
					ChooseDirection();



			}
		}
	}

			public void ChooseDirection()
	{
		WalkDirection = Random.Range (0, 4);
		IsWalking = true;
		WalkCounter = WalkTime;


		}
	
			
void FixedUpdate ()
{



	if (rbody.velocity.x > 0)
			transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);
	else if(rbody.velocity.x < 0)
			transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);

		
	


	}
}
