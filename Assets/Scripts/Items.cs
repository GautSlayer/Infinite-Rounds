using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

	public enum Type {	HEALTH,POWER,FIRERATE,MVTSPEED,INVICIBILITY,REPAIR,WEAPON};


	[Tooltip("Durée de vie de l'item(dans la scene)")]
	[SerializeField]float lifespan = 20;
	[SerializeField]float magnitude = 5;
	[SerializeField] float timer = 10 ;
	
	[SerializeField]Type type;
	    public Type Type1 { get{return type;}
    }

    public float Magnitude
    {get{return magnitude;}set{magnitude = value;}}
	public float Lifespan
    {get{return lifespan;}set{lifespan = value;}}

    // Use this for initialization
    void Start () {
		type = ((Type)Random.Range(0, 6));
		switch(Type1){
            case Items.Type.FIRERATE:       // necessite acces à RangedAttacks
            Magnitude=0.25f;
            break;
            case Items.Type.HEALTH:         // necessite acces à Health
            Magnitude=20;
            break;
            case Items.Type.INVICIBILITY:   // necessite acces à Health
            timer=5;
            break;
            case Items.Type.MVTSPEED:
            Magnitude=25;

            break;
            case Items.Type.POWER:          // necessite acces à RangedAttacks
            Magnitude=2;
            break;
            case Items.Type.REPAIR:
            
            break;
            case Items.Type.WEAPON:         // necessite acces à RangedAttacks
            
            break;
            default:
            
            break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(lifespan>0){
			lifespan-=Time.deltaTime;
		}

	}
}
