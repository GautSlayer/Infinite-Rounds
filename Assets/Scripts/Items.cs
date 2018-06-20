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
    public float Timer
    {get{return timer;}set{timer = value;}}

    // Use this for initialization
    void Start () {
        SpriteRenderer sprite = this.GetComponentInChildren<SpriteRenderer>();
		type = ((Type)Random.Range(0, 5)); // mettre 6 quand les armes seront faites
		switch(Type1){
            case Items.Type.FIRERATE:       // necessite acces à RangedAttacks
            Magnitude=0.25f;
            sprite.color=Color.grey;
            break;
            case Items.Type.HEALTH:         // necessite acces à Health
            Magnitude=20;
            sprite.color=Color.green;
            break;
            case Items.Type.INVICIBILITY:   // necessite acces à Health
            sprite.color=Color.yellow;
            timer=5;
            break;
            case Items.Type.MVTSPEED:
            sprite.color=Color.white;
            Magnitude=25;

            break;
            case Items.Type.POWER:          // necessite acces à RangedAttacks
            sprite.color=new Color(153, 51, 255);
            Magnitude=2;
            break;
            case Items.Type.REPAIR:
            sprite.color=new Color(153, 102, 0);
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
        else{
            GameObject.Destroy(this.gameObject);
        }
        
	}
}
