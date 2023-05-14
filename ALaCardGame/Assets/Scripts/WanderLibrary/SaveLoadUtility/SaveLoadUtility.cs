using System.IO;
using UnityEngine;

namespace WanderSaveLoad
{
    public static class SaveConsts
    {
        public static string fileExtention = ".json";
    }
    public static class SaveLoadUtility
    {
        
        static string OpenSaveFile(string fileName)
        {
            string path = GetFilePath(fileName);
            string context = File.ReadAllText(path);
            return context;
        }

        static string GetFilePath(string fileName)
        {
            return Path.Combine(new []{Application.persistentDataPath,fileName + SaveConsts.fileExtention});
        }

        static void CreateSaveFile(string fileName)
        {
            var stream = File.Create(GetFilePath(fileName));
            stream.Close();
        }

        public static void SetJSONSaveData<T>(string fileName, T obj)
        {
            string path = GetFilePath(fileName);
            if (!Directory.Exists(path))
            {
                CreateSaveFile(fileName);
            }

            string jsonContext = JsonUtility.ToJson(obj,true);
            File.WriteAllText(path,jsonContext);
        }
        
        public static T GetJSONSaveData<T>(string fileName)
        {
            return JsonUtility.FromJson<T>(OpenSaveFile(fileName));
        }
    }
}