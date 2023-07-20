using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color hoveredColor;
    [SerializeField] private Color notPlaceableColor;

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
        if (!_areLabelsEnabled)
        {
            _coordinateLabel.text = "";
            return;
        }
        
        _coordinateLabel.text = $"{_coordinates.x}, {_coordinates.y}";
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