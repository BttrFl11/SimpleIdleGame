using System.IO;
using UnityEngine;

namespace Data 
{
    public class FileDataHandler
    {
        private readonly string _dataDirPath;
        private readonly string _dataFileName;

        public FileDataHandler(string dirPath, string fileName)
        {
            _dataDirPath = dirPath;
            _dataFileName = fileName;
        }

        private string GetFullPath()
        {
            return Path.Combine(_dataDirPath, _dataFileName);
        }

        public GameData Load()
        {
            var fullPath = GetFullPath();

            if (File.Exists(fullPath))
            {
                try
                {
                    string jsonData = "";
                    using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream))
                        {
                            jsonData = streamReader.ReadToEnd();
                        }
                    }

                    var gameData = JsonUtility.FromJson<GameData>(jsonData);
                    return gameData;
                }
                catch (System.Exception e)
                {
                    throw e;
                }
            }

            return null;
        }

        public void Save(GameData gameData)
        {
            string fullPath = GetFullPath();

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                var jsonData = JsonUtility.ToJson(gameData, true);

                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.Write(jsonData);
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}