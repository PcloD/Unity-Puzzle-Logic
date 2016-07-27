using UnityEngine;
using System.Collections;

public class PuzzlePiece : MonoBehaviour
{
    Vector3 originalPosition;
    public int originalX;
    public int originalY;
    public int ID;

    public void origin(int x, int y, int id)
    {
        originalX = x;
        originalY = y;
        ID = id;
    }
    public void setPosition(Vector3 position)
    {
        originalPosition = position;
    }

    public Vector3 getPosition()
    {
        return originalPosition;
    }

    public int getOriginX()
    {
        return originalX;
    }

    public int getOriginY()
    {
        return originalY;
    }
}
