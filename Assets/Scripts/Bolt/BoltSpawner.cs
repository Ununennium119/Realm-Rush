using Common;
using UnityEngine;

namespace Bolt
{
    public class BoltSpawner: MonoBehaviour
    {
        [Tooltip("Insert bolt object pool here.")] [SerializeField]
        private ObjectPool boltObjectPool;


        public void SpawnBolt(Vector3 position, Quaternion rotation)
        {
            var bolt = boltObjectPool.GetObject();
            bolt.transform.parent = transform;
            bolt.transform.position = position;
            bolt.transform.rotation = rotation;
        }
    }
}