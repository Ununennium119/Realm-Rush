using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public class ObjectPool : MonoBehaviour
    {
        [Tooltip("Insert object's prefab here.")] [SerializeField]
        private GameObject objectPrefab;

        private readonly List<GameObject> _objects = new();


        public GameObject GetObject()
        {
            var obj = _objects.FirstOrDefault(obj => !obj.activeSelf);
            if (obj != null)
            {
                obj.SetActive(true);
                return obj;
            }

            obj = Instantiate(objectPrefab);
            _objects.Add(obj);
            return obj;
        }
    }
}