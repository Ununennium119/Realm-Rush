using System.Collections;
using Common;
using UnityEngine;

namespace Ram
{
    public class RamSpawner : MonoBehaviour
    {
        [Tooltip("Insert ram object pool here.")] [SerializeField]
        private ObjectPool ramObjectPool;

        [Tooltip("The delay between spawning rams.")] [SerializeField]
        private float delay = 3f;

        [Tooltip("The number of rams to spawn")] [SerializeField]
        private int remainingCount = 5;


        private void Start()
        {
            StartCoroutine(SpawnRam());
        }


        private IEnumerator SpawnRam()
        {
            while (remainingCount > 0)
            {
                var ram = ramObjectPool.GetObject();
                ram.transform.parent = transform;

                remainingCount--;
                yield return new WaitForSeconds(delay);
            }
        }
    }
}