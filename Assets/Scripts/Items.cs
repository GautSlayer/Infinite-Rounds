using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

	public enum Type {	HEALTH,POWER,FIRERATE,MVTSPEED,INVICIBILITY,REPAIR,WEAPON};


	[Tooltip("Durée de vie de l'item(dans la scene)")]
	[SerializeField]float lifespan = 20;
	[SerializeField]int magnitude = 5;
	[SerializeField] float timer = 10 ;
	
	[SerializeField]Type type;

	// Use this for initialization
	void Start () {
		type = ((Type)Random.Range(0, 6));

	}
	
	// Update is called once per frame
	void Update () {
		if(lifespan>0){
			lifespan-=Time.deltaTime;
		}

	}
}
