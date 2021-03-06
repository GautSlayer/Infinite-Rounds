using UnityEngine;
using System.Collections;

public class SmoothFollow2D : MonoBehaviour {
	public Transform target;
	float smoothTime = 0f;
	private Transform thisTransform;
	private Vector3 velocity;


	// Use this for initialization
	void Start () {
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		thisTransform.position = new Vector3(Mathf.SmoothDamp( thisTransform.position.x, 
		                                                      target.position.x,ref velocity.x, smoothTime),
		                                     Mathf.SmoothDamp( thisTransform.position.y, 
		                 target.position.y,ref velocity.y, smoothTime),
		                                     thisTransform.position.z);

	}
}
