using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace UnityTest.Monsters
{

    public class Monster : MonoBehaviour
    {
        private int _timeToRelease = 1;
        private IObjectPool<Monster> _monsterPool;

        private float _speed = 0f;

        private float _minSpeed = 0.5f;
        private float _maxSpeed = 10.0f;

        public void SetMonsterPool(IObjectPool<Monster> monsterPool)
        {
            _monsterPool = monsterPool;
        }

        private void OnBecameInvisible()
        {
            if(gameObject.activeInHierarchy)
                StartCoroutine(ReleaseFromPool());
        }
        private void OnBecameVisible()
        {
            StopAllCoroutines();
        }

        private void OnEnable()
        {
            _speed = Mathf.Abs(Random.Range(_minSpeed, _maxSpeed));
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }

        private IEnumerator ReleaseFromPool()
        {
            yield return new WaitForSecondsRealtime(_timeToRelease);
            _monsterPool?.Release(this);
        }

    }
}
