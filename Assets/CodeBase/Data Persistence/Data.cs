using CodeBase.Settings;
using System.Collections.Generic;

namespace CodeBase.DataPersistence
{
    [System.Serializable]
    public class Data
    {
        public Language Language;
        public KeyboardLayout Keyboard;
        public List<LeaderData> Leaders;
    }

    [System.Serializable]
    public class LeaderData
    {
        public string Name;
        public float Score;
        public float Speed;
        public float Mistakes;
        public string Date;

        public LeaderData(string name, float score, float speed, float mistakes, string date)
        {
            Name = name;
            Score = score;
            Speed = speed;
            Mistakes = mistakes;
            Date = date;
        }
    }
}