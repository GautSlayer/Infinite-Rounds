using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

    public float movementSpeed = 5;
    public Transform projectileSpawn;
    public float projectileSpawnOffsetX;
    public float projectileSpawnOffsetY;

    private Rigidbody2D myRb;
    private Vector2 movement;

    // Use this for initialization
    void Start () {
        myRb = GetComponent<Rigidbody2D>();
        OrienteProjectileSpawn(270);
	}
	
    private void FixedUpdate()
    {
        if(myRb == null)
        {
            Debug.Log("Player is missing a rigidbody2D");
            return;
        }

        //Get raw input ie 1,0,-1 with keyboard
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        //Move the player with rigidbody.MovePosition()
        movement = (new Vector2(horizontal, vertical).normalized) * (movementSpeed /1000);
        Vector2 startPos = transform.position;
        myRb.MovePosition(startPos + movement);

        //Get raw input ie 1,0,-1 with keyboard
        float aimVertical = Input.GetAxisRaw("AimVertical");
        float aimHorizontal = Input.GetAxisRaw("AimHorizontal");

        //Oriente the projectile spawn according directionnal arrow
        //Should might be done in update and ot fixedUpdate
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
}
