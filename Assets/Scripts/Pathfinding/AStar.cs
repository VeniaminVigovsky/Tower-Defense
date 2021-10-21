using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private List<Node> openSet;
    private HashSet<Node> closedSet;

    private Dictionary<Node, int> gCost;
    private Dictionary<Node, int> fCost;
    private Dictionary<Node, int> hCost;

    private Dictionary<Node, Node> nodeParents;

    private Node startingNode;
    private Node goalNode;
    private Node currentNode;

    private NodeGrid grid;

    
    public AStar(NodeGrid grid)
    {
        this.grid = grid;
    }


    public List<Node> GetNodePath(Vector3 startingPosition, Vector3 goalPosition)
    {        
        Node goal = GetGoalNode(startingPosition, goalPosition);       


        if (nodeParents.Count == 0) return null;

        List<Node> path = new List<Node>();

        Node child = goal;

        while (child != startingNode)
        {
            path.Add(child);

            child = nodeParents[child];
        }

        path.Add(child);
        path.Reverse();

        return path;
    }


    private Node GetGoalNode(Vector3 startingPosition, Vector3 goalPosition)
    {
        openSet = new List<Node>();
        closedSet = new HashSet<Node>();

        gCost = new Dictionary<Node, int>();
        fCost = new Dictionary<Node, int>();
        hCost = new Dictionary<Node, int>();


        nodeParents = new Dictionary<Node, Node>();
        

        startingNode = grid.GetNode(startingPosition);
        goalNode = grid.GetNode(goalPosition);

        openSet.Add(startingNode);

        gCost[startingNode] = 0;
        fCost[startingNode] = 0;
        hCost[startingNode] = GetDistance(startingNode, goalNode);
        

        while (openSet.Count > 0)
        {

            currentNode = openSet[0];

            for (int i = 0; i < openSet.Count; i++)
            {
                if (fCost[openSet[i]] < fCost[currentNode] || fCost[openSet[i]] == fCost[currentNode] && hCost[openSet[i]] < hCost[currentNode])
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == goalNode)
            {
                return currentNode;
            }

            Node[] neighbours = grid.GetNodeNeighbours(currentNode);

            foreach (var n in neighbours)
            {
                if (!n.IsWalkable || closedSet.Contains(n))
                    continue;

                int newDistanceToNeighbour = gCost[currentNode] + GetDistance(currentNode, n);

                if (!openSet.Contains(n) || newDistanceToNeighbour < gCost[n])
                {
                    gCost[n] = newDistanceToNeighbour;
                    hCost[n] = GetDistance(n, goalNode);
                    fCost[n] = gCost[n] + hCost[n];

                    if (!openSet.Contains(n))
                        openSet.Add(n);
                    nodeParents[n] = currentNode;
                }


                
            }

            
        }

        
        return startingNode;
    }


    private int GetDistance(Node nodeA, Node nodeB)
    {


        int distX = Mathf.Abs(nodeA.X - nodeB.X);
        int distY = Mathf.Abs(nodeA.Y - nodeB.Y);
        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distX + 10 * (distY - distX);
        }
    }
}
