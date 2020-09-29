using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text;
using System;
using UnityEngine.SceneManagement;

namespace Sensiks 
{
   public class SavesManager : MonoBehaviour
    {
        public static SavesManager Instance;

        private string SavesDefaultPath = $"{Application.streamingAssetsPath}/Saves/Savedata";

        public List<SerializableSavedata> savedata = new List<SerializableSavedata>();

        public SerializableSavedata selectedData;

        public void Start()
        {
            LoadSaves();         
            /*SerializableSavedata savedata = new SerializableSavedata();
            savedata.lastEdit = DateTime.Now;
            savedata.pathToData = "Data";
            savedata.pathToVideo = "Video";
            savedata.pathToThumbnail = "Thumb";
            savedata.projectName = "Test";
            SaveSavedata(savedata);*/
        }

        private void Awake()
        {
            if(Instance != null && Instance == this) 
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public void SaveSavedata(SerializableSavedata input) 
        {
            string jsonText = JsonMapper.ToJson(input);

            FileInfo fi = new FileInfo(SavesDefaultPath + "/" + input.projectName + input.lastEdit.ToString("yyyy-MM-dd-hh-mm-ss") + ".json");
            using(FileStream fs = fi.Create()) 
            {
                byte[] info = new UTF8Encoding(true).GetBytes(jsonText);

                fs.Write(info, 0, info.Length);
                fs.Close();
            }

            LoadSaves();
        }

        public void LoadSaves() 
        {
            savedata.Clear();
            DirectoryInfo dir = new DirectoryInfo(SavesDefaultPath);
            FileInfo[] info = dir.GetFiles("*.json");
            Debug.Log(info.Length);
            foreach(FileInfo file in info) 
            {
                using(StreamReader sr = file.OpenText()) 
                {
                    string jsonText = sr.ReadToEnd();
                    savedata.Add(JsonMapper.ToObject<SerializableSavedata>(new JsonReader(jsonText)));
                }
            }

            if(SceneManager.GetActiveScene().buildIndex == 0) 
            {
                GameObject.FindObjectOfType<CheckItemsLoaded>().SavesLoaded = true;
            }
        }

        public void SetSelectedData(SerializableSavedata data) 
        {
            selectedData = data;
        }
    }
}

