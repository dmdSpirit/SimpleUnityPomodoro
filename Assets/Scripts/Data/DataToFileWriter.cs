#nullable enable

using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace SimplePomodoro.Data
{
    [UsedImplicitly]
    public sealed class DataToFileWriter
    {
        private const string FILE_NAME = @"/save.sv";

#if UNITY_EDITOR
        private readonly string _savePath = Application.persistentDataPath + @"/Editor/Saves";
#else
        private readonly string _savePath = Application.persistentDataPath + @"/Saves";
#endif

        public bool TryReadData(out string data)
        {
            try
            {
                using var sr = new StreamReader(_savePath + FILE_NAME);
                data = sr.ReadToEnd();
                return true;
            }
            catch (IOException e)
            {
                Debug.LogError($"The file {_savePath + FILE_NAME} could not be read.");
                Debug.LogError(e.Message);
            }

            data = null!;
            return false;
        }

        public bool DoesFileExist()
            => File.Exists(_savePath + FILE_NAME);

        public void WriteData(string data)
        {
            if (!Directory.Exists(_savePath))
                Directory.CreateDirectory(_savePath);

            using StreamWriter outputFile = new StreamWriter(_savePath + FILE_NAME);
            outputFile.Write(data);
        }
    }
}