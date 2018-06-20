﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour {

    public float movementSpeed = 5;
    public Transform projectileSpawn;
    public float projectileSpawnOffsetX;
    public float projectileSpawnOffsetY;
    public Text interdictionInfo;

    // Gestion des dysfonctionnement : 0- aucune ; 1- tir ; 2-mvt_horizontal ; 3-mvt_vertical 
    public int interdictions = 0;
    public float recov_time=5;
    private float recov_interdiction=0;

    // Gestion Boost , plusieurs boost sont possibles à la fois , mais pas deux fois le même(ça ne fait que refresh le timer)
    Dictionary<Items.Type, float> tempBoost;
    Dictionary<Items.Type, float> magnitudeBoost;
    [SerializeField]List<Items.Type> memoryBoost; // principalement pour l'inspector
    
    // Communication avec les autres scripts
    RangedAttack RangedAttack;
    Health health;
    // gestion RigidB
    private Rigidbody2D myRb;
    private Animator animator;
    private Vector2 movement;
    private float lastPressedAxisVert;
    private float lastPressedAxisHori;
    
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start () {
        tempBoost =new Dictionary<Items.Type, float>();
        memoryBoost =new List<Items.Type>();
        magnitudeBoost= new Dictionary<Items.Type, float>();
        RangedAttack=gameObject.GetComponent<RangedAttack>();
        health=gameObject.GetComponentInChildren<Health>();
        myRb = GetComponent<Rigidbody2D>();
        OrienteProjectileSpawn(270);
        EndPerturbation();
        lastPressedAxisVert = 0;
        lastPressedAxisHori = 0;
    }
	
    private void FixedUpdate()
    {
        if(tempBoost.Count!=0){
            List<Items.Type> keys = new List<Items.Type> (tempBoost.Keys);
            
            foreach(Items.Type t in keys){
                if(tempBoost[t]>0){
                    tempBoost[t]-=Time.deltaTime;   // Didn't find better way
                    
                }
                else{
                    RemoveBoost(t);
                }
            }
        }

        if(interdictions!=0 && Time.time>recov_interdiction){
            EndPerturbation();
        }

        if(myRb == null)
        {
            Debug.Log("Player is missing a rigidbody2D");
            return;
        }

        /*
        //Move only in 4 directions
        float vertical = 0;
        float horizontal = 0;
        //Get the lastTime vertical axe was pressed
        if (Input.GetButtonDown("Vertical"))
        {
            lastPressedAxisVert = Time.time;
        }
        //when release return to 0 so the other axis is back to first priority
        if (Input.GetButtonUp("Vertical"))
        {
            lastPressedAxisVert = 0;
        }

        //Get the lastTime horizontal axe was pressed
        if (Input.GetButtonDown("Horizontal"))
        {
            lastPressedAxisHori = Time.time;
        }
        //when release return to 0 so the other axis is back to first priority
        if (Input.GetButtonUp("Horizontal"))
        {
            lastPressedAxisHori = 0;
        }

        //Make the last input (vertical or horizontal prioritary)
        if (lastPressedAxisVert > lastPressedAxisHori)
        {
            horizontal = 0;
            //Get raw input ie 1,0,-1 with keyboard
            vertical = Input.GetAxisRaw("Vertical");
        }
        else
        {
            //Get raw input ie 1,0,-1 with keyboard
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = 0;
        }
        */

        //Get raw input ie 1,0,-1 with keyboard
        float vertical = Input.GetAxisRaw("Vertical");
        if(interdictions==4){
            if(vertical==-1){
                vertical=0;
            }
        }
        else if (interdictions==5){
            
            if(vertical==1){
                vertical=0;
            }
        }
        //Get raw input ie 1,0,-1 with keyboard
        float horizontal = Input.GetAxisRaw("Horizontal");
        if(interdictions==2){
            if(horizontal==-1){
                horizontal=0;
            }
        }
        else if (interdictions==3){
            
            if(horizontal==1){
                horizontal=0;
            }
        }

        //Move the player with rigidbody.MovePosition()
       if(interdictions==6){
            movement = (new Vector2(vertical, horizontal).normalized) * (movementSpeed /1000);
        }
       
        else
            movement = (new Vector2(horizontal, vertical).normalized) * (movementSpeed /1000);

        Vector2 startPos = transform.position;
        myRb.MovePosition(startPos + movement);

        //Get raw input ie 1,0,-1 with keyboard
        float aimVertical = Input.GetAxisRaw("AimVertical");
        float aimHorizontal = Input.GetAxisRaw("AimHorizontal");
        bool shoot = true;

        //Oriente the projectile spawn according directionnal arrow
        //Should might be done in update and ot fixedUpdate
        if (aimVertical == 0 && aimHorizontal == 0)
        {
            shoot = false;
        }
        else
        {
            horizontal = aimHorizontal;
            vertical = aimVertical;

            if (aimVertical == 0 && aimHorizontal > 0) //right
            {
                OrienteProjectileSpawn(0);
            }
            if (aimVertical == 0 && aimHorizontal < 0) //left
            {
                OrienteProjectileSpawn(180);
            }
            if (aimVertical > 0 && aimHorizontal == 0) //up
            {
                OrienteProjectileSpawn(90);
            }
            if (aimVertical < 0 && aimHorizontal == 0) //down
            {
                OrienteProjectileSpawn(270);
            }
            /*
            if (aimVertical > 0 && aimHorizontal > 0) //right + up
            {
                OrienteProjectileSpawn(45);
            }
            if (aimVertical > 0 && aimHorizontal < 0) //left + up
            {
                OrienteProjectileSpawn(135);
            }
            if (aimVertical < 0 && aimHorizontal > 0) //right + down
            {
                OrienteProjectileSpawn(315);
            }
            if (aimVertical < 0 && aimHorizontal < 0) //left + down
            {
                OrienteProjectileSpawn(225);
            }
            */
        }

        // Handle animations
        HandleAnimations(horizontal, vertical, shoot);
    }

    private void HandleAnimations(float horizontal, float vertical, bool shoot)
    {
        animator.SetBool("Shoot", shoot);

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("Run", true);
            animator.SetFloat("FaceX", horizontal);
            animator.SetFloat("FaceY", vertical);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    //methode to orient (position + rotation) the projectile spawn with an angle in deg
    void OrienteProjectileSpawn(int angle)
    {
        Vector3 newPos = new Vector2(projectileSpawnOffsetX, 0);
        float zRot = 0f;
        switch (angle)
        {
            case 0: //right
                {
                    newPos = new Vector2(projectileSpawnOffsetX, 0);
                    zRot = 0f;
                    break;
                }
            case 45: //rigth + up
                {
                    newPos = new Vector2(projectileSpawnOffsetX, projectileSpawnOffsetY);
                    zRot = 45f;
                    break;
                }
            case 90: //up
                {
                    newPos = new Vector2(0, projectileSpawnOffsetY);
                    zRot = 90f;
                    break;
                }
            case 135: //left + up
                {
                    newPos = new Vector2(-projectileSpawnOffsetX, projectileSpawnOffsetY);
                    zRot = 135f;
                    break;
                }
            case 180: //left
                {
                    newPos = new Vector2(- projectileSpawnOffsetX, 0);
                    zRot = 180f;
                    break;
                }
            case 225: //left + down
                {
                    newPos = new Vector2(-projectileSpawnOffsetX, -projectileSpawnOffsetY);
                    zRot = 225f;
                    break;
                }
            case 270: //down
                {
                    newPos = new Vector2(0, -projectileSpawnOffsetY);
                    zRot = 270f;
                    break;
                }
            case 315: //right + down
                {
                    newPos = new Vector2(projectileSpawnOffsetX, -projectileSpawnOffsetY);
                    zRot = 315f;
                    break;
                }
            default: //by default right
                {
                    newPos = new Vector2(projectileSpawnOffsetX, 0);
                    zRot = 0f;
                    break;
                }
        }

        projectileSpawn.position = this.transform.position + newPos;
        projectileSpawn.rotation = Quaternion.Euler(0, 0, zRot);
    }

    public void StartPerturbation(int interdiction){
        Debug.Log("Handicap : "+interdiction );
        if(interdictions==0){
            if(interdictionInfo != null)
            {
                if(interdiction==1)
                    interdictionInfo.text= "Tir impossible";
                else if(interdiction==2)
                    interdictionInfo.text="Mvt gauche impossible";
                else if(interdiction==3)
                    interdictionInfo.text="Mvt droit impossible";
                else if(interdiction==4)
                    interdictionInfo.text="Mvt bas impossible";
                else if(interdiction==5)
                    interdictionInfo.text="Mvt haut impossible";
                else if(interdiction==6)
                interdictionInfo.text="Mvt inversés";
                
            }
            else
            {
                Debug.Log("GameObject Tagged Player miss the StatusText");
            }


            if(interdiction==1){
                gameObject.GetComponent<RangedAttack>().handicap=true;
                Debug.Log("fin des tirs");
            }
            interdictions=interdiction;
            recov_interdiction=Time.time+recov_time;
            
        }
    }

    void EndPerturbation(){

            if(interdictionInfo != null)
            {
                interdictionInfo.text= "";
            }
            else
            {
                Debug.Log("GameObject Tagged Player miss the StatusText");
            }
        
        Debug.Log("Fin de perturbation : "+interdictions);
        if(interdictions==1){
            gameObject.GetComponent<RangedAttack>().handicap=false;
        }
        interdictions=0;
    }

    private void OnDestroy()
    {
        GameManager.instance.PlayerDied();
    }

    private void GetBoost(Items.Type item,float magn,float tps){
        //// Cas du Soin
        if(item==Items.Type.HEALTH){         // necessite acces à Health
            health.Heal((int)magn);

       }

        //// ATTENTION : Modèle à adapter au systeme de weapon
        else{
            // on verifie si on a deja eu ce type de boost
            if(memoryBoost.Contains(item)){     // Si on l'a déjà eu on refresh le timer
                tempBoost[item]=tps;    
                
            }
            else{                               // Sinon on l'ajoute à notre base de boost
            
                memoryBoost.Add(item);
                tempBoost.Add(item,tps);
                magnitudeBoost.Add(item,magn);
                
            
            
                switch(item){
                    case Items.Type.FIRERATE:       // necessite acces à RangedAttacks
                    RangedAttack.BoostFireRate(magn);
                    break;
                    
                    case Items.Type.INVICIBILITY:   // necessite acces à Health
                    health.SwitchInviciblity();
                    break;
                    case Items.Type.MVTSPEED:
                        movementSpeed+=magn;
                    break;
                    case Items.Type.POWER:          // necessite acces à RangedAttacks
                    RangedAttack.BoostDamage((int)magn);
                    break;
                    case Items.Type.REPAIR:
                    
                    break;
                    case Items.Type.WEAPON:         // necessite acces à RangedAttacks
                    
                    break;
                    default:
                    
                    break;
                }
            }
        }
    
    }

    private void RemoveBoost(Items.Type stat){
        switch(stat){
            case Items.Type.FIRERATE:       // necessite acces à RangedAttacks
            RangedAttack.UnboostFireRate(magnitudeBoost[stat]);
            break;
            case Items.Type.HEALTH:         // necessite acces à Health
                
            break;
            case Items.Type.INVICIBILITY:   // necessite acces à Health
            health.SwitchInviciblity();
            break;
            case Items.Type.MVTSPEED:
                movementSpeed-=magnitudeBoost[stat];

            break;
            case Items.Type.POWER:          // necessite acces à RangedAttacks
            RangedAttack.UnboostDamage((int)magnitudeBoost[stat]);
            break;
            case Items.Type.REPAIR:
            
            break;
            case Items.Type.WEAPON:         // necessite acces à RangedAttacks
            
            break;
            default:
            
            break;
        }
        
        magnitudeBoost.Remove(stat);
        tempBoost.Remove(stat);
        memoryBoost.Remove(stat);
        
    }

void OnTriggerEnter2D(Collider2D other) {
        if (other.tag=="Item"){
            Items it =other.GetComponent<Items>();
            Items.Type t = it.Type1;
            GetBoost(t,it.Magnitude,it.Timer);
            GameObject.Destroy(other.gameObject);
        }
    }
}
