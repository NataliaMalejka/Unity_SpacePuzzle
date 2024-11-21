using UnityEditor.UI;
using UnityEngine;

enum Connection {HOLE, KNOB};
enum Sides { LEFT, RIGHT, UP, DOWN };

public class Puzzle : MonoBehaviour
{
    [SerializeField, Tooltip("The type of connection on each side of the puzzle piece. (Can be either 'Hole' or 'Knob')")]
    Connection[] connections = new Connection[4];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
