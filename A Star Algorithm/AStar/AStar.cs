using System;
using System.Collections.Generic;
using System.Linq;

public class AStar
{
    private char[,] map;
    public AStar(char[,] map)
    {
        this.map = map;

    }

    public static int GetH(Node current, Node goal)
    {
        var deltaY = Math.Abs(current.Col - goal.Col);
        var deltaX = Math.Abs(current.Row - goal.Row);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        var queue = new PriorityQueue<Node>();
        var parent = new Dictionary<Node, Node>();
        var cost = new Dictionary<Node, int>();

        queue.Enqueue(start);
        parent[start] = null;
        cost[start] = 0;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (IsCurrentGoal(current, goal))
            {
                break;
            }

            var nodesToCheck = new Node[]
            {
                new Node(current.Row - 1, current.Col),
                new Node(current.Row, current.Col - 1),
                new Node(current.Row + 1, current.Col),
                new Node(current.Row, current.Col + 1)
            };

            for (int i = 0; i < nodesToCheck.Length; i++)
            {
                if(!IsNeighbourWithinTheArray(nodesToCheck[i]))
                {
                    continue;
                }
                var neighbour = nodesToCheck[i];
                var newCost = cost[current] + 1;


                if (IsNeighbourForbidden(neighbour))
                {
                    continue;
                }
                if (!cost.ContainsKey(neighbour) || newCost < cost[neighbour])
                {
                    cost[neighbour] = newCost;
                    neighbour.F = newCost + GetH(neighbour, goal);
                    queue.Enqueue(neighbour);
                    parent[neighbour] = current;
                }
            }

        }
        var path = new Stack<Node>();
        if (parent.ContainsKey(goal))
        {
            var current = goal;

            while (current != start)
            {
                path.Push(current);
                current = parent[current];
            }
        }

        path.Push(start);


        return path;

    }

    private bool IsNeighbourWithinTheArray(Node node)
    {
        return node.Row >= 0 && node.Row < map.GetLength(0)
            && node.Col >= 0 && node.Col < map.GetLength(1);
    }

    private bool IsNeighbourForbidden(Node neighbour)
    {
        return this.map[neighbour.Row, neighbour.Col] == 'W';
    }

    private bool IsCurrentGoal(Node current, Node goal)
    {
        return current.Col == goal.Col && current.Row == goal.Row;
    }
}

