#nullable enable
using System;

namespace SimplePomodoro.Data
{
    [Serializable]
    public class DateData
    {
        public string Date = null!;
        public int WorkCount;
    }
}