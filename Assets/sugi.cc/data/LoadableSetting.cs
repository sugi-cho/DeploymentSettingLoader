using System;
using System.IO;
using UnityEngine;

namespace sugi.cc.data
{
    public abstract class LoadableSetting : ScriptableObject
    {
        [SerializeField] protected string filePath;
        public abstract DataNameAndFilePath GetNameAndPath();
        public abstract void Load(string filePath);
        public abstract void Save();
        public abstract void SaveAs(string filePath);
    }

    public abstract class LoadableSetting<T> : LoadableSetting
    {
        [SerializeField] protected T data;

        public override DataNameAndFilePath GetNameAndPath()
        {
            return new DataNameAndFilePath()
            {
                dataName = typeof(T).Name,
                filePath = filePath
            };
        }
        

        private void Reset()
        {
            filePath = $"{typeof(T).Name}.json";
        }

        public override void Load(string filePath)
        {
            base.filePath = filePath;
            Load();
        }

        private void Load()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                data = JsonUtility.FromJson<T>(json);
                Debug.Log($"loaded: {filePath}");
            }
            else
            {
                Debug.LogWarning($"file: {filePath} doesn't exist!");
                Save();
            }
        }

        [ContextMenu("save data")]
        public override void Save()
        {
            var json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, json);
            Debug.Log($"saved: {filePath}");
        }

        public override void SaveAs(string filePath)
        {
            base.filePath = filePath;
            Save();
        }

    }

    [Serializable]
    public class DataNameAndFilePath
    {
        public string dataName;
        public string filePath;
    }
}