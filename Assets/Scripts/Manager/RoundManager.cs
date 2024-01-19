using System.Collections;
using UnityEngine;
using UnityTest.Views;

namespace UnityTest.Managers
{

    public class RoundManager : MonoBehaviour
    {
        private static int _currentRound = 1;
        private int _totalMonsterCounter = 0;
        private float _roundTimer = 0;

        private float RoundInterval = 1f;

        private int _roundMonsterCount = 0;

        [SerializeField] private MonsterSpawner _monsterSpawner;
        [SerializeField] private MainView _mainView;

        private void OnEnable()
        {
            _monsterSpawner.OnMonsterSpawned += MonsterSpawned;
            _monsterSpawner.OnMonsterDespawned += MonsterDespawned;
        }
        private void OnDisable()
        {
            _monsterSpawner.OnMonsterSpawned -= MonsterSpawned;
            _monsterSpawner.OnMonsterDespawned -= MonsterDespawned;
        }
        private void Start()
        {
            StartRound();
        }

        private void StartRound()
        {
            _roundMonsterCount = GetFibonacciItem(_currentRound);
            _monsterSpawner?.SpawnMonsters(_roundMonsterCount);
        }

        private void MonsterDespawned()
        {
            _roundMonsterCount--;
            if(_roundMonsterCount == 0)
            {
                EndRound();
            }
        }

        private void EndRound()
        {
            _currentRound++;
            _roundTimer = 0f;
            StartCoroutine(NextRound());
        }

        private void MonsterSpawned()
        {
            _totalMonsterCounter++;
            _mainView.SetCountText = _totalMonsterCounter.ToString();
        }

        private int GetFibonacciItem(int index)
        {
            if (index < 2)
                return index;
            else
                return GetFibonacciItem(index - 1) + GetFibonacciItem(index - 2);
        }

        private void Update()
        {
            _roundTimer += Time.deltaTime;
            _mainView.SetTimerText = _roundTimer;
            
        }

        private IEnumerator NextRound()
        {
            yield return new WaitForSecondsRealtime(RoundInterval);
            StartRound();
        }
    }
}
