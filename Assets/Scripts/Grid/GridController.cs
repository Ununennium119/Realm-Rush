using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grid
{
    public class GridController : MonoBehaviour
    {
        [Tooltip("Size of the grid.")] [SerializeField]
        private Vector2Int gridSize;

        [FormerlySerializedAs("GridSnap")] [Tooltip("Should match editor's grid snapping size")] [SerializeField]
        public Vector2Int gridSnap;

        private readonly Vector2Int[] _directions =
        {
            Vector2Int.left, Vector2Int.down, Vector2Int.right, Vector2Int.up
        };

        private Node[,] _nodes;


        private void Awake()
        {
            _nodes = new Node[gridSize.x, gridSize.y];
            for (var i = 0; i < gridSize.x; i++)
            for (var j = 0; j < gridSize.y; j++)
                _nodes[i, j] = new Node(new Vector2Int(i, j));
        }


        public void SetNodeBlocked(Vector3 position, bool isBlocked)
        {
            var coordinates = GetCoordinates(position);
            SetNodeBlocked(coordinates, isBlocked);
        }

        public List<Vector3> FindPath(Vector3 source, Vector3 destination)
        {
            ResetNodes();
            var sourceCoordinates = GetCoordinates(source);
            var destinationCoordinates = GetCoordinates(destination);
            var sourceNode = _nodes[sourceCoordinates.x, sourceCoordinates.y];
            var destinationNode = _nodes[destinationCoordinates.x, destinationCoordinates.y];

            if (destinationNode.IsBlocked) return null;

            List<Vector3> path = null;
            var currentNodes = new Queue<Node>();
            destinationNode.IsExplored = true;
            currentNodes.Enqueue(destinationNode);
            while (currentNodes.Count > 0)
            {
                var currentNode = currentNodes.Dequeue();
                if (currentNode == sourceNode)
                {
                    path = new List<Vector3>();
                    while (currentNode != null)
                    {
                        var position = GetPosition(currentNode.Coordinates);
                        path.Add(position);
                        currentNode = currentNode.Parent;
                    }

                    break;
                }

                foreach (var direction in _directions)
                {
                    var newCoordinates = currentNode.Coordinates + direction;
                    if (!IsCoordinatesValid(newCoordinates)) continue;

                    var newNode = _nodes[newCoordinates.x, newCoordinates.y];
                    if (newNode.IsExplored || newNode.IsBlocked) continue;

                    newNode.IsExplored = true;
                    newNode.Parent = currentNode;
                    currentNodes.Enqueue(newNode);
                }
            }

            return path;
        }


        private Vector2Int GetCoordinates(Vector3 position)
        {
            return new Vector2Int
            (
                Mathf.RoundToInt(position.x / gridSnap.x),
                Mathf.RoundToInt(position.z / gridSnap.y)
            );
        }

        private Vector3 GetPosition(Vector2Int coordinates)
        {
            return new Vector3
            (
                coordinates.x * gridSnap.x,
                0,
                coordinates.y * gridSnap.y
            );
        }


        private bool IsCoordinatesValid(Vector2Int coordinates)
        {
            return 0 <= coordinates.x && coordinates.x < gridSize.x && 0 <= coordinates.y && coordinates.y < gridSize.y;
        }


        private void SetNodeBlocked(Vector2Int coordinates, bool isBlocked)
        {
            _nodes[coordinates.x, coordinates.y].IsBlocked = isBlocked;
        }


        private void ResetNodes()
        {
            for (var i = 0; i < gridSize.x; i++)
            for (var j = 0; j < gridSize.y; j++)
            {
                _nodes[i, j].IsExplored = false;
                _nodes[i, j].Parent = null;
            }
        }
    }
}