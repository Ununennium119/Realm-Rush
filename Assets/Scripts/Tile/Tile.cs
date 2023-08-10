using Ballista;
using Grid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tile
{
    public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Tooltip("Specifies whether a ballista can be placed on the tile or not.")] [SerializeField]
        private bool isPlaceable;

        private BallistaSpawner _ballistaSpawner;

        private GridController _gridController;


        public bool IsPlaceable => isPlaceable;
        public bool IsHovered { get; private set; }


        private void Awake()
        {
            _gridController = FindObjectOfType<GridController>();
            _ballistaSpawner = FindObjectOfType<BallistaSpawner>();

            if (!isPlaceable) _gridController.SetNodeBlocked(transform.position, true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isPlaceable) return;

            SpawnBallista();
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            IsHovered = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsHovered = false;
        }


        private void SpawnBallista()
        {
            var position = transform.position;
            _gridController.SetNodeBlocked(position, true);

            var isSpawned = _ballistaSpawner.SpawnBallista(position, Quaternion.identity);
            if (!isSpawned) return;

            isPlaceable = false;
        }
    }
}