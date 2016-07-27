using UnityEngine;
using System.Collections;

public class PuzzleSetup : MonoBehaviour
{
    GameObject[] puzzlePieces;
    public GameObject puzzlePiece;
    public int puzzleWidth = 0;
    public int puzzleHeight = 0;
    int[] puzzleIndex;

    void Start()
    {
        puzzlePieces = new GameObject[puzzleWidth * puzzleHeight];
        puzzleIndex = new int[puzzleWidth * puzzleHeight];

        for (int i = 0; i < puzzleIndex.Length; i++)
        {
            puzzleIndex[i] = i;
        }
        ShuffleArray(puzzleIndex);

        this.GetComponent<GameManager>().setupGame(puzzleWidth, puzzleHeight, puzzlePieces);

        for (int i = 0; i < puzzleWidth; i++)
        {
            for (int j = 0; j < puzzleHeight; j++)
            {
                int index = i * puzzleWidth + j;
                int x = (puzzleIndex[index] / puzzleWidth) * 1;
                int y = (puzzleIndex[index] - (x * puzzleWidth)) * 1;
                Vector3 position = new Vector3(i * 1.0f, j * 1.0f, 0);
                puzzlePieces[index] = (GameObject)Instantiate(puzzlePiece, position, Quaternion.identity);
                puzzlePieces[index].GetComponent<TextMesh>().text = puzzleIndex[index].ToString();
                puzzlePieces[index].GetComponent<PuzzlePiece>().setPosition(position);
                puzzlePieces[index].GetComponent<PuzzlePiece>().origin(x, y, puzzleIndex[index]);
                this.gameObject.GetComponent<GameManager>().assignPiece((int)position.x, (int)position.y, puzzleIndex[index]);
            }
        }
    }

    public static void ShuffleArray<T>(T[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }

    public int getWidth()
    {
        return puzzleWidth;
    }

    public int getHeight()
    {
        return puzzleHeight;
    }
}
