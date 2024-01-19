using TMPro;
using UnityEngine;

namespace UnityTest.Views
{

    public class MainView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _monsterCountText;
        [SerializeField] private TextMeshProUGUI _timerText;

        public string SetCountText
        {
            set { _monsterCountText.text = $"Monster Counter: {value}";}
        }

        public float SetTimerText
        {
            set 
            {
                int minutes = (int)(value / 60);
                int seconds = (int)(value % 60);
                _timerText.text = string.Format("Timer: {0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}
