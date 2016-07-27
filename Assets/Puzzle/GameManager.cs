using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private int puzzleWidth;
    private int PuzzleHeight;
    private GameObject[] puzzlePieces;
    public Text canvasText;
    int[,] locations;

    public void setupGame(int x, int y, GameObject[] pieces)
    {
        locations = new int[x, y];
        puzzleWidth = x;
        PuzzleHeight = y;
        puzzlePieces = pieces;
    }

    public void assignPiece(int x, int y, int piece)
    {
        locations[x, y] = piece;
        winnerCheck();
    }

    public void changeStatus(int oldX, int oldY, int newX, int newY, int piece)
    {
        if (oldX >= 0 && oldY >= 0)
        {
            locations[newX, newY] = piece;
            locations[oldX, oldY] = -1;
            Debug.Log("Piece: " + piece + " is now at position: (" + newX + "," + newY + ") Old Position (" + oldX + "," + oldY + ").");
        }
        else
        {
            locations[newX, newY] = piece;
        }
        winnerCheck();
    }

    public int returnPiece(int x, int y)
    {
        try
        {
            return locations[x, y];
        }
        catch
        {
            return -1;
        }
    }

    public void winnerCheck()
    {
        bool completed = true;

        for (int i = 0; i < locations.GetLength(0); i++)
        {
            for (int j = 0; j < locations.GetLength(1); j++)
            {
                int result = (i * puzzleWidth) + j;

                if (locations[i, j] != result)
                {
                    completed = false;
                    break;
                }
            }
        }
        if (completed)
        {
            canvasText.text = "Puzzle Complete";
            canvasText.color = Color.green;
        }
        else
        {
            canvasText.text = "Puzzle Incomplete";
            canvasText.color = Color.red;
        }
    }

    public bool validPosition(int x, int y)
    {
        if (x > 0 && x < puzzleWidth && y > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void completeGame()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            Vector3 position = new Vector3(puzzlePieces[i].GetComponent<PuzzlePiece>().getOriginX(), puzzlePieces[i].GetComponent<PuzzlePiece>().getOriginY(), 0);
            puzzlePieces[i].GetComponent<PuzzlePiece>().setPosition(position);
            puzzlePieces[i].transform.position = position;
            locations[(int)position.x, (int)position.y] = puzzlePieces[i].GetComponent<PuzzlePiece>().ID;
        }
        winnerCheck();
    }
}
