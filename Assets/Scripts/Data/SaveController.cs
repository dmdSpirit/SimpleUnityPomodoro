#nullable enable

using System;
using JetBrains.Annotations;
using UnityEngine;

namespace SimplePomodoro.Data
{
    [UsedImplicitly]
    public sealed class SaveController
    {
        private readonly DataToFileWriter _dataToFileWriter;

        public Data Data { get; private set; } = new();

        public SaveController(DataToFileWriter dataToFileWriter)
        {
            _dataToFileWriter = dataToFileWriter;
        }

        public void SaveGame()
        {
            string? json = JsonUtility.ToJson(Data);
            _dataToFileWriter.WriteData(json);
        }

        public bool LoadGame()
        {
            if (!_dataToFileWriter.DoesFileExist()
                || !_dataToFileWriter.TryReadData(out string data))
                return false;

            try
            {
                Data = JsonUtility.FromJson<Data>(data);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }

            return true;
        }
    }
}