using Composite;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    public class DataManager : Singleton<DataManager>
    {
        [SerializeField] private string _fileName;

        private FileDataHandler _fileDataHandler;
        private GameData _gameData;

        private void Awake()
        {
            _fileDataHandler = new(Application.persistentDataPath, _fileName);

            LoadData();
        }

        private void NewGame()
        {
            _gameData = new();

            FindObjectOfType<BusinessCompositeRoot>().NewGame();
        }

        private void LoadData()
        {
            _gameData = _fileDataHandler.Load();

            if (_gameData == null)
            {
                NewGame();
            }
            else
            {
                var allSaveObjects = FindAllSaveObjects();
                foreach (var saveObject in allSaveObjects)
                {
                    saveObject.LoadData(_gameData);
                }
            }
        }

        private void SaveData()
        {
            var allSaveObjects = FindAllSaveObjects();
            foreach (var saveObject in allSaveObjects)
            {
                saveObject.SaveData(_gameData);
            }

            _fileDataHandler.Save(_gameData);
        }

        private IEnumerable<IData> FindAllSaveObjects()
        {
            var allSaveObjects = FindObjectsOfType<MonoBehaviour>().OfType<IData>();
            return new List<IData>(allSaveObjects);
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        private void OnApplicationPause(bool pause)
        {
            SaveData();
        }
    }
}