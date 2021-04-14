using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Scenes.Save
{
    class SaveManager
    {

        static SaveManager()
        {
        }

        private SaveManager()
        {
        }

        public static SaveManager Instance { get; } = new SaveManager();

        private Save CurrentSave;

        public Save GetSave()
        {
            return CurrentSave;
        }

        public void SetSave(Save Save)
        {
            CurrentSave = Save;
        }

        public Save GetDefaultSave()
        {
            Save s = new Save();
            ShopData sd = new ShopData();

            ShopData.DATA_MODEL[] Balls = new ShopData.DATA_MODEL[3];
            Balls[0] = new ShopData.DATA_MODEL
            {
                id = 0,
                material = "Red",
                price = 5,
                name = "Red",
                owned = true,
            };
            Balls[1] = new ShopData.DATA_MODEL
            {
                id = 1,
                material = "Blue",
                price = 5,
                name = "Blue",
                owned = false,
            };
            Balls[2] = new ShopData.DATA_MODEL
            {
                id = 2,
                material = "Green",
                price = 5,
                name = "Green",
                owned = false,
            };
            sd.Balls = Balls;

            ShopData.DATA_MODEL[] Platforms = new ShopData.DATA_MODEL[3];
            Platforms[0] = new ShopData.DATA_MODEL
            {
                id = 1,
                material = "Red",
                price = 5,
                name = "Red",
                owned = true,
            };
            Platforms[1] = new ShopData.DATA_MODEL
            {
                id = 1,
                material = "Blue",
                price = 5,
                name = "Blue",
                owned = false,
            };
            Platforms[2] = new ShopData.DATA_MODEL
            {
                id = 2,
                material = "Green",
                price = 5,
                name = "Green",
                owned = false,
            };
            sd.Platforms = Platforms;

            ShopData.DATA_MODEL[] Holes = new ShopData.DATA_MODEL[3];
            Holes[0] = new ShopData.DATA_MODEL
            {
                id = 1,
                material = "Red",
                price = 5,
                name = "Red",
                owned = true,
            };
            Holes[1] = new ShopData.DATA_MODEL
            {
                id = 1,
                material = "Blue",
                price = 5,
                name = "Blue",
                owned = false,
            };
            Holes[2] = new ShopData.DATA_MODEL
            {
                id = 2,
                material = "Green",
                price = 5,
                name = "Green",
                owned = false,
            };
            sd.Holes = Holes;
            s.ShopData = sd;
            PlayerPrefs.SetString("HoleSkin", "Red");
            PlayerPrefs.SetString("PlatformSkin", "Red");
            PlayerPrefs.SetString("MaterialName", "Red");

            return s;
        }

        public void CreateDefaultSave()
        {
            Save save = GetDefaultSave();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
            bf.Serialize(file, save);
            file.Close();
        }

        public void LoadGame()
        {
            //CreateDefaultSave();
            if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                Save s = (Save)bf.Deserialize(file);
                Debug.Log(s.ShopData.Balls[0].owned);
                CurrentSave = s;
                file.Close();
                Debug.Log("Loaded" + GetSave());
            }
            else
            {
                Debug.Log("non exist");
                try
                {
                    CreateDefaultSave();
                } catch(Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
        }
        public void SaveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Debug.Log(CurrentSave.ShopData.Balls[0].owned);
            bf.Serialize(file, CurrentSave);
            file.Close();
        }
    }
}
