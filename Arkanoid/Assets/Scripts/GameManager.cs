using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public int PlayerScore1 = 0;

	public int clicks = 0;
	public int quedas = 0;

	public int pontosf1 = 0;
	public int pontosf2 = 0;
	public int pontosf3 = 0;

	private bool inicio = true;
	private bool final = false;

	public GUISkin layout;
	GameObject theBall;
	
	GameObject[] palisades;
	GameObject[] romanes;
	
	public void Score (int type) {
	    if (type == 1)
	    {
        	PlayerScore1+=1;
	    }else if (type == 2){
	        PlayerScore1+=2;
	    }
	}

	void showPlacar(){
		GUI.Label(new Rect(Screen.width / 2 - 260 - 12, 20, 160, 130), "Pontos: " + PlayerScore1);
	}

	private void RestartBalls()
    {
        theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
    }

	private void zerarPontos(){
		PlayerScore1 = 0;
	}

	private void decPontos(){
		PlayerScore1 -= 1;
		clicks++;
	}

	public void retBall(){
		PlayerScore1 -= 2;
		quedas++;
	}

	void OnGUI () {
	    GUI.skin = layout;
	    showPlacar();

	    if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "Replace ball"))
	    {
	        decPontos();
	        RestartBalls();
	    }
	    if (palisades.Length == 0 && romanes.Length == 0){
			RestartBalls();
			GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Fase terminada!");
			Invoke("RestartBalls", 1);
			Invoke("changeScene", 2);
	    }
	}

	private void changeScene(){
		if (SceneManager.GetActiveScene().name == "Scene1")
        {
			inicio = false;
            SceneManager.LoadScene("Scene2");
        }
        else if(SceneManager.GetActiveScene().name == "Scene2")
        {
			pontosf1 = PlayerScore1;
			zerarPontos();
            SceneManager.LoadScene("Scene3");
        } 
		else if(SceneManager.GetActiveScene().name == "Scene3")
        {
			pontosf2 = PlayerScore1;
			zerarPontos();
            SceneManager.LoadScene("Scene4");
        }
		else if(SceneManager.GetActiveScene().name == "Scene4")
		{
			pontosf3 = PlayerScore1;
			final = true;
			SceneManager.LoadScene("Scene5");
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
		palisades = GameObject.FindGameObjectsWithTag("palisade");
		romanes = GameObject.FindGameObjectsWithTag("roman");
    }
}
