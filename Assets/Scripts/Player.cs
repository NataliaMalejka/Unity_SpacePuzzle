using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocity;
    private Rigidbody2D rb;    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 1, 6, -1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 1, 6, -1);
        }
    } 
}