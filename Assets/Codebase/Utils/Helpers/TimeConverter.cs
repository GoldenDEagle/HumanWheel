using System;
using UnityEngine;

namespace Assets.Codebase.Utils.Helpers
{
    public class TimeConverter
    {
        // returns time in 00:00
        public static string TimeInMinutes(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time - minutes * 60f);

            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            return formattedTime;
        }

        public static string TimeInHours(double totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            return time.ToString("hh':'mm':'ss");
        }
    }
}
