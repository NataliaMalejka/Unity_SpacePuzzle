using UnityEngine;

public class PuzzleDrag : MonoBehaviour
{
    //selected puzzel
    private GameObject selectedPiece; 
    private GameObject pieceImage;

    //position beetween mouse and puzzle
    private Vector3 offset; 
    private float zPosition = 0f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private Puzzle puzzle;
    private Grid grid;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
    }

    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        //left button drag
        if (Input.GetMouseButtonDown(0)) 
        {
            //camera view to world position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            //correct object
            if (hit.collider != null && hit.collider.CompareTag("Piece"))
            {
                selectedPiece = hit.collider.gameObject;
                offset = selectedPiece.transform.position - (Vector3)mousePosition;

                startPosition = selectedPiece.transform.position;

                // grid position
                int startX = Mathf.RoundToInt(startPosition.x / grid.cellSize);
                int startY = Mathf.RoundToInt(startPosition.y / grid.cellSize);

                puzzle = grid.GetPuzzleFromGrid(startX, startY);
                //select puzzel image
                pieceImage = selectedPiece.transform.GetChild(0).gameObject;
                //higer layer
                SetOrderInLayer(selectedPiece, 1);
                SetOrderInLayer(pieceImage, 1);
            }
        }

        //move piece
        if (selectedPiece != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, zPosition);
        }

        //drop
        if (Input.GetMouseButtonUp(0) && selectedPiece != null)
        {
            endPosition = selectedPiece.transform.position;

            // endposition to grid
            int endX = Mathf.RoundToInt(endPosition.x / grid.cellSize);
            int endY = Mathf.RoundToInt(endPosition.y / grid.cellSize);

            // check grid
            if (endX >= 0 && endX < grid.puzzles.GetLength(0) &&
                endY >= 0 && endY < grid.puzzles.GetLength(1) &&
                grid.puzzles[endX, endY] != null)
            {
                selectedPiece.transform.position = startPosition;
            }
            else
            {
                int startX = Mathf.RoundToInt(startPosition.x / grid.cellSize);
                int startY = Mathf.RoundToInt(startPosition.y / grid.cellSize);

                grid.puzzles[startX, startY] = null;
                grid.puzzles[endX, endY] = puzzle;

                selectedPiece.transform.position = new Vector3(endX * grid.cellSize, endY * grid.cellSize, zPosition);
            }

            //lower layer
            SetOrderInLayer(selectedPiece, 0); 
            SetOrderInLayer(pieceImage, 0);
            selectedPiece = null;
            pieceImage = null;
        }
    }

    private void SetOrderInLayer(GameObject piece, int order)
    {
        var spriteRenderer = piece.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = order;
        }
    }
}