using System;
using UnityEngine;

namespace Grid
{
    public class Node : IEquatable<Node>
    {
        public Node(Vector2Int coordinates)
        {
            Coordinates = coordinates;
        }

        public Vector2Int Coordinates { get; }

        public bool IsBlocked { get; set; }
        public bool IsExplored { get; set; }

        public Node Parent { get; set; }


        public bool Equals(Node other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Coordinates.Equals(other.Coordinates);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Node)obj);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }

        public static bool operator ==(Node left, Node right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Node left, Node right)
        {
            return !Equals(left, right);
        }
    }
}