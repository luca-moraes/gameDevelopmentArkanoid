using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public int PlayerScore1 = 0;
	public GUISkin layout;
	GameObject theBall;
	
	public void Score (string wallID) {
	    if (wallID == "wallR")
	    {
        	PlayerScore1++;
	    }
	}

	void showPlacar(){
		GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerScore1);
	}

	private void RestartBalls()
    {
        theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
    }

	private void zerarPontos(){
		PlayerScore1 = 0;
	}

	void OnGUI () {
	    GUI.skin = layout;
	    showPlacar();

	    if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
	    {
	        zerarPontos();
	        RestartBalls();
			changeScene();
	    }
	    if (PlayerScore1 == 5){
			zerarPontos();
			RestartBalls();
			GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER ONE WINS");
	    }
	}

	private void changeScene(){
		if (SceneManager.GetActiveScene().name == "Scene1")
        {
            SceneManager.LoadScene("Scene2");
        }
        else if(SceneManager.GetActiveScene().name == "Scene2")
        {
            SceneManager.LoadScene("Scene1");
        } 
	}

    // Start is called before the first frame update
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("TagBall");
		// AudioSource[] audios = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
