using System.Collections;
using Bolt;
using UnityEngine;

namespace Ballista
{
    public class BallistaController : MonoBehaviour
    {
        [Tooltip("Insert weapon here.")] [SerializeField]
        private Transform weapon;

        [Tooltip("The delay between shots in seconds.")] [SerializeField]
        private float fireDelay = 1f;

        [Tooltip("The range in which ballista can shot.")] [SerializeField]
        private float range = 15f;

        [Tooltip("The time between building different parts of ballista.")] [SerializeField]
        private float buildDelay = 1f;

        private BoltSpawner _boltSpawner;
        private bool _fire;
        private bool _isBuilding;
        private Transform _target;
        private bool _targetPresent;


        private void Awake()
        {
            _boltSpawner = FindObjectOfType<BoltSpawner>();
        }

        private void Start()
        {
            StartCoroutine(Fire());
        }

        private void Update()
        {
            if (!IsTargetValid()) SelectTarget();

            if (IsTargetValid()) LookAtTarget();
        }


        public IEnumerator Build()
        {
            var waitForSeconds = new WaitForSeconds(buildDelay);
            _isBuilding = true;
            foreach (Transform child in transform) child.gameObject.SetActive(false);

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                yield return waitForSeconds;
            }

            _isBuilding = false;
        }


        private void SelectTarget()
        {
            var rams = GameObject.FindGameObjectsWithTag("Ram");
            var minDistance = float.PositiveInfinity;
            foreach (var ram in rams)
            {
                if (!ram.gameObject.activeSelf) continue;

                var distance = Vector3.Distance(transform.position, ram.transform.position);
                if (!(distance < minDistance)) continue;

                minDistance = distance;
                _target = ram.transform;
            }
        }

        private bool IsTargetInRange(Transform target)
        {
            return Vector3.Distance(transform.position, target.position) <= range;
        }

        private bool IsTargetValid()
        {
            return _target != null && _target.gameObject.activeSelf && IsTargetInRange(_target);
        }

        private void LookAtTarget()
        {
            weapon.LookAt(_target.position + Vector3.up * weapon.position.y);
        }

        private IEnumerator Fire()
        {
            var waitForSeconds = new WaitForSeconds(fireDelay);
            yield return new WaitUntil(() => !_isBuilding);
            while (true)
            {
                if (!IsTargetValid())
                {
                    yield return waitForSeconds;
                    continue;
                }

                _boltSpawner.SpawnBolt(weapon.position, weapon.rotation);
                yield return waitForSeconds;
            }
        }
    }
}