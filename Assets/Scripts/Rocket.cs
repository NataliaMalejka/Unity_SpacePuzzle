using System.Net.Sockets;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private GameManager gameManager;

    private bool playerCollision = false;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerCollision)
        {
            gameManager.ShowWinPanel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
            playerCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
            playerCollision = false;
        }
    }

}
