#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplePomodoro.Data
{
    [Serializable]
    public sealed class Data
    {
        public List<DateData> DateDatas=new();

        public void UpdateData(DateTime date, int workCount)
        {
            DateData? data = DateDatas.FirstOrDefault(d => d.Date == date);
            if (data == null)
            {
                DateDatas.Add(new DateData(){Date = date, WorkCount = workCount});
                return;
            }

            data.WorkCount = workCount;
        }
    }
}