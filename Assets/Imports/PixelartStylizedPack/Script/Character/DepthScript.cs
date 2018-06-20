using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthScript : MonoBehaviour {

	public float Y;
	float layer;

	SpriteRenderer render;
	Vector3 bottom;

	void Start () {
		
		render = GetComponent<SpriteRenderer> (); 
	}
	void Update () {
		
		bottom = transform.TransformPoint(render.sprite.bounds.min);
		layer = bottom.y + Y;
		render.sortingOrder = -(int)(layer * 100);
	}
}