using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private int leftBorder;
    [SerializeField] private int rightBorder;

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(horizontalInput * velocity * Time.deltaTime, 0, 0);

        if(transform.position.x < leftBorder)
            transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);

        if (transform.position.x > rightBorder)
            transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, 9), transform.position.y, transform.position.z);
    }

}