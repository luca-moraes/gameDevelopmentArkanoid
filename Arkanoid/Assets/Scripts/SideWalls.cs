using UnityEngine;
using System.Collections;

public class SideWalls : MonoBehaviour {

	public AudioClip wallSound;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (hitInfo.name == "Ball" && gameManager != null)
        {
            string wallName = transform.name;
            gameManager.Score(wallName);
            hitInfo.gameObject.SendMessage("RestartGame", 1.0f, SendMessageOptions.RequireReceiver);

            if (wallSound != null)
            {
                AudioSource audioPlayer = hitInfo.gameObject.GetComponent<AudioSource>();

                if (audioPlayer == null)
                {
                    audioPlayer = hitInfo.gameObject.AddComponent<AudioSource>();
                }

                audioPlayer.clip = wallSound;
                audioPlayer.Play();
            }
        }
    }
}