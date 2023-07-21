using CodeBase.DataPersistence;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Menu.Leaderboard
{
    public class LeaderboardDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _speedText;
        [SerializeField] private TMP_Text _mistakesText;
        [SerializeField] private TMP_Text _dateText;

        public void Init(LeaderData data)
        {
            _nameText.text = data.Name;
            _scoreText.text = data.Score.ToString();
            _speedText.text = data.Speed.ToString();
            _mistakesText.text = data.Mistakes.ToString();
            _dateText.text = data.Date;
        }
    }
}