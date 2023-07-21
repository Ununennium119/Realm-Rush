using Ballista;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tile
{
    public class WayPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Tooltip("Specifies whether a ballista can be placed on the tile or not.")] [SerializeField]
        private bool isPlaceable;

        
        public bool IsPlaceable => isPlaceable;
        public bool IsHovered { get; private set; }

        private BallistaSpawner _ballistaSpawner;


        private void Awake()
        {
            _ballistaSpawner = FindObjectOfType<BallistaSpawner>();
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            IsHovered = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsHovered = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isPlaceable) return;

            isPlaceable = !_ballistaSpawner.SpawnBallista(transform.position, Quaternion.identity);
        }
    }
}