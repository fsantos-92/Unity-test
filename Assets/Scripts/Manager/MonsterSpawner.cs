using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityTest.Monsters;

namespace UnityTest.Managers
{
    public class MonsterSpawner : MonoBehaviour
    {
        private IObjectPool<Monster> _monsterPool;
        [SerializeField] private Monster _monsterPrefab;
        private Transform _spawnPosition;

        public Action OnMonsterSpawned;
        public Action OnMonsterDespawned;

        private float SpawnTime = 1f;

        private void Awake()
        {
            _monsterPool = new ObjectPool<Monster>(
                InstantiateMonster,
                OnGetMonster,
                OnReleaseMonster,
                OnDestroyMonster
            );
            _spawnPosition = transform;
        }

        private void OnDestroyMonster(Monster monster)
        {
            Destroy(monster.gameObject);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void SpawnMonsters(int quantity)
        {
            StartCoroutine(Spawn(quantity));
        }


        private Monster InstantiateMonster()
        {
            var monster = Instantiate(_monsterPrefab);
            monster.SetMonsterPool(_monsterPool);
            return monster;
        }

        private void OnGetMonster(Monster monster)
        {
            monster.transform.position = _spawnPosition.position;
            monster.gameObject.SetActive(true);
            OnMonsterSpawned?.Invoke();
        }

        private void OnReleaseMonster(Monster monster)
        {
            monster.gameObject.SetActive(false);
            OnMonsterDespawned?.Invoke();
        }

        private IEnumerator Spawn(int quantity)
        {
            _monsterPool.Get();
            quantity = quantity - 1;
            if (quantity > 0)
            {
                yield return new WaitForSeconds(SpawnTime);
                StartCoroutine(Spawn(quantity));
            }
        }
    }
}
