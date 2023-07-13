using System.Linq;
using UnityEngine;

namespace CodeBase.DataPersistence
{
    public class DataService : MonoBehaviour
    {
        [SerializeField] private string _fileName;
        [SerializeField] private bool _useEncryption;

        private FileDataHandler _fileDataHandler;

        [field: SerializeField] public Data Data { get; private set; }

        public void AddLeader(LeaderData leader)
        {
            Data.Leaders.Add(leader);
            Data.Leaders = Data.Leaders.OrderByDescending(l => l.Score).ToList();

            _fileDataHandler.Save(Data);
        }

        public void ChangeLeader(LeaderData leader)
        {
            Data.Leaders.RemoveAt(Data.Leaders.Count - 1);
            AddLeader(leader);
        }

        private void Start()
        {
            _fileDataHandler = new(Application.persistentDataPath, $"{_fileName}.json", _useEncryption);
            Load();
        }

        private void OnDisable()
        {
            _fileDataHandler.Save(Data);
        }

        private void Load()
        {
            Data = _fileDataHandler.Load<Data>();

            if (Data == null)
            {
                Data = new()
                {
                    Leaders = new()
                };

                Debug.LogWarning("No data was found.");
            }
        }
    }
}