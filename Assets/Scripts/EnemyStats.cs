using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class EnemyStats
    {
        public static float movementSpeed = 10f;
        public static float attackSpeed = 1f;
        public static int damage = 10;
        public static int maxHealth = 40;

        //Buff enemy stats
        public static void Buff()
        {
            movementSpeed *= 1.1f;
            attackSpeed /= 1.2f;
            damage = (int)(damage * 1.1);
            maxHealth = (int)(maxHealth * 1.3);
        }
    }
}
