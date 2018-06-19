using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class EnemyStats
    {
        private static float startMovementSpeed = 10f;
        private static float startAttackSpeed = 1f;
        private static int startDamage = 10;
        private static int startMaxHealth = 40;

        public static float movementSpeed = startMovementSpeed;
        public static float attackSpeed = startAttackSpeed;
        public static int damage = startDamage;
        public static int maxHealth = startMaxHealth;

        private static float maxMovementSpeed = 30f;
        private static float maxAttackSpeed = 0.2f;
        private static int maxDamage = 100;
        private static int maxMaxHealth = 200;

        private static float logSpeedMult = 3f;
        private static float logAtkSpeedMult = 0.1f;
        private static float logDmgMult =10;
        private static float logHealthMult =30;

        //Buff enemy stats
        public static void Buff()
        {
            //Old / Aplha version
            /*
            movementSpeed *= 1.1f;
            attackSpeed /= 1.2f;
            damage = (int)(damage * 1.1);
            maxHealth = (int)(maxHealth * 1.3);
            */

            //New version with log on base 2 for the increase of stats
            movementSpeed = Mathf.Log(GameManager.roundNumber, 2) * logSpeedMult + startMovementSpeed;
            attackSpeed = startAttackSpeed - Mathf.Log(GameManager.roundNumber, 2) * logAtkSpeedMult;
            damage = (int)(Mathf.Log(GameManager.roundNumber, 2) * logDmgMult + startDamage);
            maxHealth = (int)(Mathf.Log(GameManager.roundNumber, 2)* logHealthMult + startMaxHealth);

            //Clamp the values
            movementSpeed = Mathf.Clamp(movementSpeed, startMovementSpeed, maxMovementSpeed);
            attackSpeed = Mathf.Clamp(attackSpeed, maxAttackSpeed, startAttackSpeed);
            damage = Mathf.Clamp(damage, startDamage, maxDamage);
            maxHealth = Mathf.Clamp(maxHealth, startMaxHealth, maxMaxHealth);

        }
    }
}
