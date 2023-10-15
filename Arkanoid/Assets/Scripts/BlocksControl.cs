using UnityEngine;
using System.Collections;

public class BlocksControl : MonoBehaviour {
    private int numHits = 0;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if ((hitInfo.name == "Ball" || hitInfo.name == "PowerUpBall") && gameManager != null)
        {
            numHits++;

            if(gameObject.CompareTag("palisade") && numHits == 2){
                var pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                gameManager.Score(1, pos.x, Screen.height - pos.y);
                Destroy(gameObject);

            }else if(gameObject.CompareTag("roman") && numHits == 4){
                var pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                gameManager.Score(2, pos.x, Screen.height - pos.y);
                Destroy(gameObject);
            }
        }
    }
}