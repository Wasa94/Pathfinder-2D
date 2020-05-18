using UnityEngine;

public class Node : MonoBehaviour
{

    public int x;

    public int y;

    public bool isBlocked;

    public override string ToString()
    {
        return "Node[" + x + ", " + y + "]";
    }
}
