using System.Collections.Generic;

public class BFS : IPathFinder
{
    public IEnumerable<Node> FindPath(Node[,] map, Node start, Node target)
    {
        try
        {
            if (map == null || start == null || target == null)
                return null;

            int mapSize = map.GetLength(0);

            bool[,] visited = new bool[mapSize, mapSize];

            Queue<Node> queue = new Queue<Node>();

            Node[,] pathMatrix = new Node[mapSize, mapSize];

            visited[start.x, start.y] = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Node tmp = queue.Dequeue();

                if (tmp == target)
                {
                    List<Node> path = new List<Node>();
                    Node hlp = pathMatrix[target.x, target.y];

                    while (hlp.x != start.x || hlp.y != start.y)
                    {
                        path.Add(hlp);
                        hlp = pathMatrix[hlp.x, hlp.y];
                    }

                    path.Reverse();
                    path.Add(target);

                    return path;
                }

                if (tmp.x > 0 && !visited[tmp.x - 1, tmp.y] && !map[tmp.x - 1, tmp.y].isBlocked)
                {
                    queue.Enqueue(map[tmp.x - 1, tmp.y]);
                    visited[tmp.x - 1, tmp.y] = true;
                    pathMatrix[tmp.x - 1, tmp.y] = tmp;
                }

                if (tmp.y > 0 && !visited[tmp.x, tmp.y - 1] && !map[tmp.x, tmp.y - 1].isBlocked)
                {
                    queue.Enqueue(map[tmp.x, tmp.y - 1]);
                    visited[tmp.x, tmp.y - 1] = true;
                    pathMatrix[tmp.x, tmp.y - 1] = tmp;
                }

                if (tmp.x < mapSize - 1 && !visited[tmp.x + 1, tmp.y] && !map[tmp.x + 1, tmp.y].isBlocked)
                {
                    queue.Enqueue(map[tmp.x + 1, tmp.y]);
                    visited[tmp.x + 1, tmp.y] = true;
                    pathMatrix[tmp.x + 1, tmp.y] = tmp;
                }

                if (tmp.y < mapSize - 1 && !visited[tmp.x, tmp.y + 1] && !map[tmp.x, tmp.y + 1].isBlocked)
                {
                    queue.Enqueue(map[tmp.x, tmp.y + 1]);
                    visited[tmp.x, tmp.y + 1] = true;
                    pathMatrix[tmp.x, tmp.y + 1] = tmp;
                }
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}
