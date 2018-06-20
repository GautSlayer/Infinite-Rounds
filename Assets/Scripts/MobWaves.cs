using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    class MobWaves
    {
        private int startMaxMobAtATime = 20;
        private int startNumberOfEnemyInTheRound = 10;
        private float startSpawnRate = 2f;

        public int maxMobAtATime;
        public int numberOfEnemyInTheRound;
        public float spawnRate;
        public int roundNumber;

        private float logMaxMobAtATimeMult = 5f;
        private float logNumberOfMubMult = 7;
        private float logSpawnRateMult = 0.3f;

        private int maxMaxMobAtATime = 200;
        private int maxNumberOfEnemyInTheRound = 10000;
        private float maxSpawnRate = 0.1f;


        public MobWaves()
        {
            maxMobAtATime = startMaxMobAtATime;
            numberOfEnemyInTheRound = startNumberOfEnemyInTheRound;
            spawnRate = startSpawnRate;
            roundNumber = 1;
        }

            //Buff waves stats
            public void Buff()
        {
            roundNumber++;
            //Version with basic log on base 2 for the increase of stats
            maxMobAtATime = (int)(Mathf.Log(roundNumber, 2) * logMaxMobAtATimeMult + startMaxMobAtATime);
            numberOfEnemyInTheRound = (int)(Mathf.Log(roundNumber, 2) * logNumberOfMubMult + startNumberOfEnemyInTheRound);
            spawnRate = startSpawnRate - Mathf.Log(roundNumber, 2) * logSpawnRateMult;

            //Clamp the values
            maxMobAtATime = Mathf.Clamp(maxMobAtATime, startMaxMobAtATime, maxMaxMobAtATime);
            numberOfEnemyInTheRound = Mathf.Clamp(numberOfEnemyInTheRound, startNumberOfEnemyInTheRound, maxNumberOfEnemyInTheRound);
            spawnRate = Mathf.Clamp(spawnRate, maxSpawnRate, startSpawnRate);
        }

    }
}
