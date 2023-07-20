using TMPro;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    private TextMeshPro _coordinateLabel;
    private Vector2Int _coordinates;

    private void Awake()
    {
        _coordinateLabel = GetComponent<TextMeshPro>();
        UpdateCoordinates();
        UpdateLabel();
    }

    private void Update()
    {
        if (Application.isPlaying) return;
        
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
        _coordinateLabel.text = $"{_coordinates.x}, {_coordinates.y}";
    }

    private void UpdateName()
    {
        transform.parent.name = $"Tile ({_coordinates.x}, {_coordinates.y})";
    }
}
