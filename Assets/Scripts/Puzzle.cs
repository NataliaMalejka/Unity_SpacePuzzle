using UnityEditor.UI;
using UnityEngine;

enum Connection {HOLE, KNOB};
enum Sides { LEFT, RIGHT, UP, DOWN };

public class Puzzle : MonoBehaviour
{
    [SerializeField, Tooltip("The type of connection on each side of the puzzle piece. (Can be either 'Hole' or 'Knob')")]
    Connection[] connections = new Connection[4];
    
    [SerializeField] public bool portable;
    [SerializeField] private bool alwasVisible;

    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 puzzlePosition;
    private GameObject image;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        image = this.transform.GetChild(0).gameObject;
        puzzlePosition = this.transform.position;
    }

    void FixedUpdate()
    {
        playerPosition = player.transform.position;

        if((playerPosition.y == puzzlePosition.y && (playerPosition.x >= puzzlePosition.x -1 && playerPosition.x <= puzzlePosition.x +1)) || alwasVisible)
            image.SetActive(true);
        else
            image.SetActive(false);
    }

    public void isMatching()
    {

    }
}
