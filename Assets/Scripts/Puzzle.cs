using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

enum Connection {EMPTY, HOLE, KNOB};


public class Puzzle : MonoBehaviour
{
    const int LEFT = 0;
    const int RIGHT = 1;
    const int UP = 2;
    const int DOWN = 3;

    [SerializeField, Tooltip("The type of connection on each side of the puzzle piece. (Can be either 'Empty', 'Hole' or 'Knob')")]
    Connection[] connections;
    
    [SerializeField] public bool portable;
    [SerializeField] private bool alwasVisible;

    private Grid grid;
    private GameObject player;
    private Vector3 playerPosition;
    private Vector3 puzzlePosition;
    private GameObject image;

    void Start()
    {
        grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
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

    public bool FitsAt(int x, int y)
    {
        //left
        Puzzle neighbour = grid.GetPuzzleFromGrid(x-1, y);
        if(neighbour != null)
        {
            if (!canConnect(connections[LEFT], neighbour.connections[RIGHT]))
                return false;
        }

        //right
        neighbour = grid.GetPuzzleFromGrid(x + 1, y);
        if (neighbour != null)
        {
            if (!canConnect(connections[RIGHT], neighbour.connections[LEFT]))
                return false;
        }

        //up
        neighbour = grid.GetPuzzleFromGrid(x, y + 1);
        if (neighbour != null)
        {
            if (!canConnect(connections[UP], neighbour.connections[DOWN]))
                return false;
        }

        //down
        neighbour = grid.GetPuzzleFromGrid(x, y - 1);
        if (neighbour != null)
        {
            if (!canConnect(connections[DOWN], neighbour.connections[UP]))
                return false;     
        }

        return true;
    }

    static bool canConnect(Connection con1, Connection con2)
    {
        if (con1 == Connection.KNOB && con2 == Connection.HOLE)
            return true;
        if (con1 == Connection.HOLE && con2 == Connection.KNOB)
            return true;

        return false;
    }
}