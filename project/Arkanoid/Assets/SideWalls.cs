using UnityEngine;
using System.Collections;

public class SideWalls : MonoBehaviour {
public AudioSource somPonto;

	void OnTriggerEnter2D(Collider2D hitInfo)
{

GameManager gameManager = GetComponent<GameManager>(); // Encontre a instância do GameManager

if (hitInfo.name == "Ball" && gameManager != null)
{
string wallName = transform.name;
gameManager.Score(wallName); // Chame o método Score na instância do GameManager
hitInfo.gameObject.SendMessage("RestartGame", 1.0f, SendMessageOptions.RequireReceiver);

}
}
}

