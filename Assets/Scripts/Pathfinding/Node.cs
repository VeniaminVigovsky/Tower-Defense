using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Vector3 _worldPosition;
    private bool _isWalkable;
    private int _gridX, _gridY;



    public Node(Vector3 worldPosition, bool isWalkable, int gridX, int gridY)
    {
        _worldPosition = worldPosition;
        _isWalkable = isWalkable;
        _gridX = gridX;
        _gridY = gridY;
    }

    public bool IsWalkable
    {
        get => _isWalkable;
    }

    public Vector3 WorldPosition
    {
        get => _worldPosition;
    }

    public int X
    {
        get => _gridX;
    }

    public int Y
    {
        get => _gridY;
    }
}
