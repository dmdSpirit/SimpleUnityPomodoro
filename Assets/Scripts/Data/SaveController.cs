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
        private readonly SimpleTimer _timer;

        public Data Data { get; private set; } = new();

        public SaveController(DataToFileWriter dataToFileWriter, SimpleTimer timer)
        {
            _timer = timer;
            _dataToFileWriter = dataToFileWriter;
        }

        public void Save()
        {
            Data.UpdateData(DateTime.Today, _timer.WorkCounter);
            string? json = JsonUtility.ToJson(Data);
            _dataToFileWriter.WriteData(json);
        }

        public bool Load()
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

            _timer.LoadWorkCount(Data.TodayWorkCount());
            return true;
        }
    }
}