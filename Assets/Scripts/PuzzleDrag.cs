using UnityEngine;

public class PuzzleDrag : MonoBehaviour
{
    private GameObject selectedPiece;
    private GameObject pieceImage;

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
        //Left Button
        if (Input.GetMouseButtonDown(0))
        {
            //Mouse Position To World Position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Collision Check
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Piece"))
            {
                selectedPiece = hit.collider.gameObject;
                offset = selectedPiece.transform.position - hit.point;

                startPosition = selectedPiece.transform.position;

                //Grid Position
                int startX = Mathf.RoundToInt(startPosition.x / grid.cellSize);
                int startY = Mathf.RoundToInt(startPosition.y / grid.cellSize);

                puzzle = grid.GetPuzzleFromGrid(startX, startY);

                //Select Piece Image
                pieceImage = selectedPiece.transform.GetChild(0).gameObject;

                //Higer layer
                SetOrderInLayer(selectedPiece, 1);
                SetOrderInLayer(pieceImage, 1);
            }
        }

        //Move Piece
        if (selectedPiece != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPosition)); 
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                Vector3 mousePosition = ray.GetPoint(distance);
                selectedPiece.transform.position = mousePosition + offset;
            }
        }

        //Put Piece
        if (Input.GetMouseButtonUp(0) && selectedPiece != null)
        {
            endPosition = selectedPiece.transform.position;

            //Grid Position
            int endX = Mathf.RoundToInt(endPosition.x / grid.cellSize);
            int endY = Mathf.RoundToInt(endPosition.y / grid.cellSize);

            //Check Position
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

            //Lower layer
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