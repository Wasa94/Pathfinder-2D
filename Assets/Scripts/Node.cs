using UnityEngine;

public class Node : MonoBehaviour, IHeapItem<Node>
{

    public int x;

    public int y;

    public bool isBlocked;

    public float gCost;
    public float hCost;
    int heapIndex;

    public float fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }
    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }

    public override string ToString()
    {
        return "Node[" + x + ", " + y + "]";
    }
}
