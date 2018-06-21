using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

	public enum Type {	HEALTH,POWER,FIRERATE,MVTSPEED,INVICIBILITY,REPAIR,WEAPON};

    [SerializeField] Sprite[] spritesItems;
    [SerializeField] Sprite[] spritesWeapon;

	[Tooltip("Durée de vie de l'item(dans la scene)")]
	[SerializeField]float lifespan = 20;
	[SerializeField]float magnitude = 5;
	[SerializeField] float timer = 10 ;
	
	[SerializeField]Type type;
    [SerializeField] RangedAttack.Weapon weapon;
	public Type Type1 { get{return type;}
    }
    public float Magnitude
    {get{return magnitude;}set{magnitude = value;}}
	public float Lifespan
    {get{return lifespan;}set{lifespan = value;}}
    public float Timer
    {get{return timer;}set{timer = value;}}

    public RangedAttack.Weapon Weapon{get{return weapon;}set{weapon = value;}}

    // Use this for initialization
    void Start () {
        SpriteRenderer spriteItem = this.GetComponentInChildren<SpriteRenderer>();
        spriteItem.sprite=spritesItems[0];

		type = ((Type)Random.Range(0, 7)); 
        spriteItem.transform.localScale=new Vector3(1.5f,1.5f,1);
		switch(Type1){
            case Items.Type.FIRERATE:       // necessite acces à RangedAttacks
            Magnitude=0.25f;
            spriteItem.sprite=spritesItems[5];
            break;
            case Items.Type.HEALTH:         // necessite acces à Health
            Magnitude=20;
            spriteItem.sprite=spritesItems[0];
            break;
            case Items.Type.INVICIBILITY:   // necessite acces à Health
            spriteItem.sprite=spritesItems[1];
            timer=5;
            break;
            case Items.Type.MVTSPEED:
            spriteItem.sprite=spritesItems[2];
            Magnitude=25;

            break;
            case Items.Type.POWER:          // necessite acces à RangedAttacks
            spriteItem.sprite=spritesItems[3];
            Magnitude=0.35f;
            
            break;
            case Items.Type.REPAIR:
            spriteItem.sprite=spritesItems[4];
            break;
            case Items.Type.WEAPON:         // necessite acces à RangedAttacks
            spriteItem.transform.localScale=new Vector3(0.5f,0.5f,1);
            weapon=((RangedAttack.Weapon)Random.Range(1,3));
            switch(weapon){
                case RangedAttack.Weapon.SHOTGUN:
                spriteItem.sprite=spritesWeapon[1];
                break;
                case RangedAttack.Weapon.MACHINEGUN:
                spriteItem.sprite=spritesWeapon[2];
                break;
            }
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
