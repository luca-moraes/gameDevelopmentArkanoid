using UnityEngine;
using System.Collections;

public class SideWalls : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (hitInfo.name == "Ball" && gameManager != null)
        {
            string wallName = transform.name;
            gameManager.retBall();
            hitInfo.gameObject.SendMessage("RestartGame", 1.0f, SendMessageOptions.RequireReceiver);

        }
    }
}