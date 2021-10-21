using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{

    private float nodeSize;

    private int gridSizeX, gridSizeY;
    private float planeX, planeY;

    private Node[,] grid;

    [SerializeField]
    private LayerMask unwalkableLayers; 

    private float xOffset, yOffset;

    private void Awake()
    {
        nodeSize = 1f;
        Transform plane = transform.parent;
        xOffset = transform.position.x;
        yOffset = transform.position.z;
        planeX = plane.localScale.x * 10;
        planeY = plane.localScale.z * 10;
        gridSizeX = Mathf.FloorToInt(planeX / nodeSize);
        gridSizeY = Mathf.FloorToInt(planeY / nodeSize);
        CreateGrid();

    }


    private void CreateGrid()
    {
       Vector3 worldBottomLeft = transform.position + (Vector3.right * -gridSizeX / 2 * nodeSize) + (Vector3.forward * -gridSizeY / 2 * nodeSize) + (Vector3.right * nodeSize /2) + (Vector3.forward * nodeSize /2);

        grid = new Node[gridSizeX, gridSizeY];
        

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPosition = worldBottomLeft + (Vector3.right * nodeSize * x) + (Vector3.forward * nodeSize * y);
                bool isWalkable = !Physics.CheckSphere(worldPosition, nodeSize / 3, unwalkableLayers);
                
                grid[x, y] = new Node(worldPosition, isWalkable, x, y);
                
            }
        }
    }

    public Node GetNode(Vector3 worldPosition)
    {
        
        float xPercent = (planeX / 2 + worldPosition.x - xOffset) / planeX;
        float yPercent = (planeY / 2 + worldPosition.z - yOffset) / planeY;

        xPercent = Mathf.Clamp01(xPercent);
        yPercent = Mathf.Clamp01(yPercent);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPercent);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPercent);

        return grid[x, y];
    }

    public Node GetNodeByIndeces(int x, int y)
    {
        if (x < 0 || y < 0 || x >= gridSizeX || y >= gridSizeY)
        {
            return null;
        }
        else
        {
            return grid[x, y];
        }


    }

    public Node[] GetNodeNeighbours(Node node)
    {   

        int length = 0;

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (node.X + x < 0 || node.X + x >= gridSizeX ||
                    node.Y + y < 0 || node.Y + y >= gridSizeY ||
                    (x == 0 && y == 0) 
                    )
                {
                    continue;
                }
                else
                {
                    length++;
                }


            }
        }

        Node[] neighbours = new Node[length];

        int i = 0;

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (node.X + x < 0 || node.X + x >= gridSizeX ||
                    node.Y + y < 0 || node.Y + y >= gridSizeY ||
                    (x == 0 && y == 0)
                    )
                {
                    continue;
                }
                else
                {                    
                    neighbours[i] = grid[node.X + x, node.Y + y];
                    i++;
                }


            }
        }

        return neighbours;

    }


    public int GridSizeX
    {
        get => gridSizeX;
    }

    public int GridSizeY
    {
        get => gridSizeY;
    }

    //private void OnDrawGizmos()
    //{
    //    if (grid == null) return;

    //    for (int x = 0; x < gridSizeX; x++)
    //    {
    //        for (int y = 0; y < gridSizeY; y++)
    //        {
    //            Node node = grid[x, y];
    //            Gizmos.color = node.IsWalkable ? Color.white : Color.red;


    //            Gizmos.DrawCube(node.WorldPosition, Vector3.one * nodeSize * 0.9f);

    //        }
    //    }
    //}
}
