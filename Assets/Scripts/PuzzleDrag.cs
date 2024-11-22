using UnityEngine;

public class PuzzleDrag : MonoBehaviour
{
    private GameObject selectedPiece; // Aktualnie wybrany puzzel
    private GameObject pieceImage;
    private Vector3 offset; // Ró¿nica miêdzy pozycj¹ myszy a puzzlem
    private float zPosition = 0f; // Zawsze 0, aby pozostaæ w jednej p³aszczyŸnie

    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        // Obs³uga rozpoczêcia przeci¹gania
        if (Input.GetMouseButtonDown(0)) // Lewy przycisk myszy
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Piece"))
            {
                selectedPiece = hit.collider.gameObject;
                offset = selectedPiece.transform.position - (Vector3)mousePosition;
                pieceImage = selectedPiece.transform.GetChild(0).gameObject;
                SetOrderInLayer(selectedPiece, 1); // Wy¿szy order in layer, by wyœwietlaæ puzzel na wierzchu
                SetOrderInLayer(pieceImage, 1);
            }
        }

        // Obs³uga przeci¹gania
        if (selectedPiece != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, zPosition);
        }

        // Obs³uga zakoñczenia przeci¹gania
        if (Input.GetMouseButtonUp(0) && selectedPiece != null)
        {
            SetOrderInLayer(selectedPiece, 0); // Przywróæ pierwotny order in layer
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