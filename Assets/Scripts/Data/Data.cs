#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplePomodoro.Data
{
    [Serializable]
    public sealed class Data
    {
        public List<DateData> DateDatas = new();

        public void UpdateData(DateTime date, int workCount)
        {
            var dateString = date.ToString();
            DateData? data = DateDatas.FirstOrDefault(d => string.Equals(d.Date, dateString));
            if (data == null)
            {
                DateDatas.Add(new DateData() { Date = dateString, WorkCount = workCount });
                return;
            }

            data.WorkCount = workCount;
        }

        public int TodayWorkCount()
        {
            var dateString = DateTime.Today.ToString();
            DateData? data = DateDatas.FirstOrDefault(d => string.Equals(d.Date, dateString));
            return data?.WorkCount ?? 0;
        }
    }
}