using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    class EnemyStats
    {
        private float startMovementSpeed = 10f;
        private float startAttackSpeed = 1f;
        private int startDamage = 10;
        private int startMaxHealth = 40;

        public float movementSpeed;
        public float attackSpeed;
        public int damage;
        public int maxHealth;

        private float maxMovementSpeed = 30f;
        private float maxAttackSpeed = 0.2f;
        private int maxDamage = 100;
        private int maxMaxHealth = 200;

        private float logSpeedMult = 3f;
        private float logAtkSpeedMult = 0.1f;
        private float logDmgMult =10;
        private float logHealthMult =30;

        public EnemyStats()
        {
            movementSpeed = startMovementSpeed;
            attackSpeed = startAttackSpeed;
            damage = startDamage;
            maxHealth = startMaxHealth;
        }

        //Buff enemy stats
        public void Buff()
        {
            //Old / Aplha version
            /*
            movementSpeed *= 1.1f;
            attackSpeed /= 1.2f;
            damage = (int)(damage * 1.1);
            maxHealth = (int)(maxHealth * 1.3);
            */

            //New version with log on base 2 for the increase of stats
            movementSpeed = Mathf.Log(Data.mobWaves.roundNumber, 2) * logSpeedMult + startMovementSpeed;
            attackSpeed = startAttackSpeed - Mathf.Log(Data.mobWaves.roundNumber, 2) * logAtkSpeedMult;
            damage = (int)(Mathf.Log(Data.mobWaves.roundNumber, 2) * logDmgMult + startDamage);
            maxHealth = (int)(Mathf.Log(Data.mobWaves.roundNumber, 2)* logHealthMult + startMaxHealth);

            //Clamp the values
            movementSpeed = Mathf.Clamp(movementSpeed, startMovementSpeed, maxMovementSpeed);
            attackSpeed = Mathf.Clamp(attackSpeed, maxAttackSpeed, startAttackSpeed);
            damage = Mathf.Clamp(damage, startDamage, maxDamage);
            maxHealth = Mathf.Clamp(maxHealth, startMaxHealth, maxMaxHealth);

        }
    }
}
