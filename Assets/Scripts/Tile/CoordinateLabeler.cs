using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tile
{
    [ExecuteAlways]
    public class CoordinateLabeler : MonoBehaviour
    {
        [Header("Colors")] [Tooltip("The default color of tile.")] [SerializeField]
        private Color defaultColor;

        [Tooltip("The color of tile when the tile is not placeable.")] [SerializeField]
        private Color notPlaceableColor;

        [Tooltip("The color of tile when the tile is placeable and is hovered.")] [SerializeField]
        private Color hoveredColor;

        private WayPoint _wayPoint;
        private TextMeshPro _coordinateLabel;

        private Vector2Int _coordinates;
        private bool _areLabelsEnabled = true;


        private void Awake()
        {
            _wayPoint = GetComponentInParent<WayPoint>();
            _coordinateLabel = GetComponent<TextMeshPro>();
        }

        private void Update()
        {
            UpdateCoordinates();
            UpdateLabel();
            UpdateName();
        }


        private void UpdateCoordinates()
        {
            var position = transform.position;
            _coordinates.x = Mathf.RoundToInt(position.x / EditorSnapSettings.move.x);
            _coordinates.y = Mathf.RoundToInt(position.z / EditorSnapSettings.move.z);
        }

        private void UpdateLabel()
        {
            UpdateLabelText();
            UpdateLabelColor();
        }

        private void UpdateLabelColor()
        {
            if (!_wayPoint.IsPlaceable)
            {
                _coordinateLabel.color = notPlaceableColor;
            }
            else if (_wayPoint.IsHovered)
            {
                _coordinateLabel.color = hoveredColor;
            }
            else
            {
                _coordinateLabel.color = defaultColor;
            }
        }

        private void UpdateLabelText()
        {
            _coordinateLabel.text = !_areLabelsEnabled ? "" : $"{_coordinates.x}, {_coordinates.y}";
        }

        private void UpdateName()
        {
            transform.parent.name = $"Tile ({_coordinates.x}, {_coordinates.y})";
        }


        public void OnToggleLabels(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _areLabelsEnabled = !_areLabelsEnabled;
            }
        }
    }
}