using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    SOUTH, EAST, NORTH, WEST
}

public static class DirectionExtensions
{
    public static Vector2Int ToVector2Int(this Direction direction)
    {
        switch (direction) {
            case Direction.NORTH: return Vector2Int.up;
            case Direction.EAST: return Vector2Int.right;
            case Direction.SOUTH: return Vector2Int.down;
            case Direction.WEST: return Vector2Int.left;
            default: return Vector2Int.zero;
        }
    }

    public static Direction Parse(string name)
    {
        return (Direction) Enum.Parse(typeof(Direction), name, true);
    }

    public static Vector2 ToVector2(this Direction direction)
    {
        return ToVector2Int(direction);
    }

    public static Direction Rotate(this Direction direction, int count)
    {
        int newValue = Modulo((int) direction + count, 4);
        return (Direction) newValue;
    }

    public static int Modulo(int i, int m) {
        int r = i % m;
        return r<0 ? r+m : r;
    }
}