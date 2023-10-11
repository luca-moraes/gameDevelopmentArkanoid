using UnityEngine;
using System.Collections;

public class BlocksControl : MonoBehaviour {
    private int numHits = 0;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (hitInfo.name == "Ball" && gameManager != null)
        {
            numHits++;

            if(gameObject.CompareTag("palisade") && numHits == 2){
                gameManager.Score(1);
                Destroy(gameObject);

            }else if(gameObject.CompareTag("roman") && numHits == 4){
                gameManager.Score(2);
                Destroy(gameObject);
            }
        }
    }
}