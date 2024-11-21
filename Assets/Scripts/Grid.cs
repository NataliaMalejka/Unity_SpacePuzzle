using UnityEngine;

public class Grid : MonoBehaviour
{
    Puzzle[,] puzzles;

    [SerializeField, Tooltip("Size of the puzzle grid.")]
    int gridSizeX;

    [SerializeField, Tooltip("Size of the puzzle grid.")]
    int gridSizeY;

    [SerializeField, Tooltip("Size of individual cell.")]
    float cellSize;

    [SerializeField, Tooltip("PuzzleMap gameobject. This is very temporary solution.")]
    GameObject puzzleMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get all the pieces and assign them to array
        // This will change!!
        Puzzle[] pieces = puzzleMap.GetComponentsInChildren<Puzzle>();
        foreach(Puzzle piece in pieces)
        {
            Vector2 pos = piece.transform.position;
            puzzles[(int)pos.x, (int)pos.y] = piece;
        }

    }

    // Get the puzzle at the specified grid cell
    public Puzzle GetPuzzleFromGrid(int x, int y)
    {
        return puzzles[x, y];
    }

    // Get the puzzle at the specified world position (useful for mouse cursor)
    public Puzzle GetPuzzleFromPosition(float x, float y)
    {
        int i = (int)(x / cellSize);
        if (i < 0 || i >= gridSizeX) return null;

        int j = (int)(y / cellSize);
        if (j < 0 || j >= gridSizeX) return null;

        return puzzles[i, j];
    }
}
