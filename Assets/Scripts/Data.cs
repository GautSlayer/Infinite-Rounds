using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace Assets.Scripts
{
    class Data
    {
        public static MobWaves mobWaves = null;
        public static EnemyStats enemyStats = null;

        public static void SaveData()
        {
            BinaryFormatter bf = new BinaryFormatter();
            //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
            FileStream file = File.Create(Application.persistentDataPath + "Mobwaves.ds"); 
            bf.Serialize(file, mobWaves);
            file.Close();
            //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
            FileStream file2 = File.Create(Application.persistentDataPath + "EnemyStats.ds");
            bf.Serialize(file2, enemyStats);
            file2.Close();
        }

        public static void LoadData()
        {
            if (File.Exists(Application.persistentDataPath + "Mobwaves.ds"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "Mobwaves.ds", FileMode.Open);
                mobWaves = (MobWaves)bf.Deserialize(file);
                file.Close();
            }
            if (File.Exists(Application.persistentDataPath + "EnemyStats.ds"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "EnemyStats.ds", FileMode.Open);
                enemyStats = (EnemyStats)bf.Deserialize(file);
                file.Close();
            }
        }

        public static void NewGame()
        {
            mobWaves = new MobWaves();
            enemyStats = new EnemyStats();
        }
    }
}
