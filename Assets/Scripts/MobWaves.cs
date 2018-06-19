using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class MobWaves
    {
        private static int startMaxMobAtATime = 20;
        private static int startNumberOfEnemyInTheRound = 10;
        private static float startSpawnRate = 2f;

        public static int maxMobAtATime = startMaxMobAtATime;
        public static int numberOfEnemyInTheRound = startNumberOfEnemyInTheRound;
        public static float spawnRate = startSpawnRate;

        private static float logMaxMobAtATimeMult = 5f;
        private static float logNumberOfMubMult = 7;
        private static float logSpawnRateMult = 0.3f;

        private static int maxMaxMobAtATime = 200;
        private static int maxNumberOfEnemyInTheRound = 10000;
        private static float maxSpawnRate = 0.1f;

        //Buff waves stats
        public static void Buff()
        {
            //Version with basic log on base 2 for the increase of stats
            maxMobAtATime = (int)(Mathf.Log(GameManager.roundNumber, 2) * logMaxMobAtATimeMult + startMaxMobAtATime);
            numberOfEnemyInTheRound = (int)(Mathf.Log(GameManager.roundNumber, 2) * logNumberOfMubMult + startNumberOfEnemyInTheRound);
            spawnRate = startSpawnRate - Mathf.Log(GameManager.roundNumber, 2) * logSpawnRateMult;

            //Clamp the values
            maxMobAtATime = Mathf.Clamp(maxMobAtATime, startMaxMobAtATime, maxMaxMobAtATime);
            numberOfEnemyInTheRound = Mathf.Clamp(numberOfEnemyInTheRound, startNumberOfEnemyInTheRound, maxNumberOfEnemyInTheRound);
            spawnRate = Mathf.Clamp(spawnRate, maxSpawnRate, startSpawnRate);
        }
    }
}
