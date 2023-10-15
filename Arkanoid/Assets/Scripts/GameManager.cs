using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// #if UNITY_EDITOR
//     using UnityEditor;
// #endif

public class GameManager : MonoBehaviour
{
	Pontuacao[] pontuacoes;
	private int faseAtual = 0;

	private bool bonusActive = false;
	private bool powerUpActive = false;

	public GUISkin layout;
	private GUIStyle guiStyleDec = new GUIStyle();
	private GUIStyle guiStyleEnd = new GUIStyle();
	private GUIStyle guiStyleInc = new GUIStyle();
	private GUIStyle guiStylePts = new GUIStyle();
	private GUIStyle guiStyleTtr = new GUIStyle();

	private Queue<PontoObject> pontos = new Queue<PontoObject>();

	GameObject theBall;
	GameObject theBonus;
	GameObject[] palisades;
	GameObject[] romanes;
	GameObject[] PowerUpBalls;
	GameObject[] PowerUpSwords;

	private void setDecStyle(){
		guiStyleDec.fontSize = 22;
		guiStyleDec.normal.textColor = Color.red;

		Texture2D debugTex = new Texture2D(1,1);
      	debugTex.SetPixel(0,0, Color.black);
      	debugTex.Apply();

		guiStyleDec.normal.background = debugTex;
	}

	private void setEndStyle(){
		guiStyleEnd.fontSize = 22;
		guiStyleEnd.normal.textColor = Color.green;

		Texture2D debugTex = new Texture2D(1,1);
      	debugTex.SetPixel(0,0,Color.black);
      	debugTex.Apply();

		guiStyleEnd.normal.background = debugTex;
	}

	private void setIncStyle(){
		guiStyleInc.fontSize = 22;
		guiStyleInc.normal.textColor = Color.blue;

		Texture2D debugTex = new Texture2D(1,1);
      	debugTex.SetPixel(0,0, Color.black);
      	debugTex.Apply();

		guiStyleInc.normal.background = debugTex;
	}

	private void setPtsStyle(){
		guiStylePts.fontSize = 22;
		guiStylePts.normal.textColor = Color.yellow;

		Texture2D debugTex = new Texture2D(1,1);
	  	debugTex.SetPixel(0,0, Color.black);
	  	debugTex.Apply();

		guiStylePts.normal.background = debugTex;
	}

	private void setTtrStyle(){
		guiStyleTtr.fontSize = 22;
		guiStyleTtr.normal.textColor = Color.blue;

		Texture2D debugTex = new Texture2D(1,1);
	  	debugTex.SetPixel(0,0, Color.green);
	  	debugTex.Apply();

		guiStyleTtr.normal.background = debugTex;
	}
	
	public void Score (int type, float x, float y) {
	    if (type == 1)
	    {
			pontuacoes[faseAtual].palisade += 1;

			PontoObject ponto = new PontoObject(x, y, "+1", true);
			pontos.Enqueue(ponto);

	    }
		else if (type == 2)
		{
	        pontuacoes[faseAtual].roman += 1;

			PontoObject ponto = new PontoObject(x, y, "+2", true);
			pontos.Enqueue(ponto);
	    }
	}

	public void dequeue(){
		pontos.Dequeue();
	}

	void showPlacar(){
		GUI.Label(new Rect(Screen.width / 2 - 60 - 12, 5, 130, 22), "PUNCTA: " + pontuacoes[faseAtual].total() , guiStylePts);
	}

	private void RestartBalls()
    {
        theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
    }

	private void decPontos(){
		pontuacoes[faseAtual].clicks += 1;
	}

	public void retBall(){
		pontuacoes[faseAtual].quedas += 1;

		PontoObject ponto = new PontoObject(Screen.width / 2 - 105, 5, "-2", false);
		pontos.Enqueue(ponto);
	}

	void OnGUI () {
	    GUI.skin = layout;
	    
		if (SceneManager.GetActiveScene().name == "Scene1")
		{
			GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 300, 500, 28), "Use as teclas A e S para mover a espada", guiStyleTtr);

			if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height/2, 120, 53), "SATUS"))
			{
				changeScene();
			}
		}
		else
		{
			showPlacar();
		
			if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height/2 + 80, 120, 53), "PERDERE"))
			{
				DestroyAll();
			}

			if (GUI.Button(new Rect(Screen.width / 2 + 140, 5, 140, 30), "REPONERE PILA"))
			{
				PontoObject ponto = new PontoObject(Screen.width / 2 - 105, 5, "-1", false);
				pontos.Enqueue(ponto);
				decPontos();
				RestartBalls();
			}

			readVecs();

			if (palisades.Length == 0 && romanes.Length == 0)
			{
				RestartBalls();

				GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height/2, 270, 22), "VICISTI PROVOCATIONEM!", guiStyleEnd);

				// #if UNITY_EDITOR
				// 	Handles.Label(new Vector3(Screen.width/2-60, Screen.height/2, 0.0f), "Fase finalizada!", guiStyleEnd);
				// #endif

				Invoke("RestartBalls", 1);
				Invoke("changeScene", 3);
			}
			
			if(pontuacoes[faseAtual].total() > 15 && !bonusActive)
			{
				bonusActive = true;
				theBonus.SendMessage("dropBonus", 0.5f, SendMessageOptions.RequireReceiver);
			}
		}

		if(pontos.Count > 0){
			foreach(PontoObject ponto in pontos){
				if(ponto.isInc)
				{
					GUI.Label(new Rect(ponto.x, ponto.y, 28, 28), ponto.text, guiStyleInc);
				}
				else
				{
					GUI.Label(new Rect(ponto.x, ponto.y, 28, 28), ponto.text, guiStyleDec);
				}

				Invoke("dequeue", 2);
			}		
		}
	}

	private void changeScene(){
		if (SceneManager.GetActiveScene().name == "Scene1")
        {
            SceneManager.LoadScene("Scene2");
        }
        else if(SceneManager.GetActiveScene().name == "Scene2")
        {
            SceneManager.LoadScene("Scene3");
        } 
		else if(SceneManager.GetActiveScene().name == "Scene3")
        {
            SceneManager.LoadScene("Scene4");
        }
		else if(SceneManager.GetActiveScene().name == "Scene4")
		{
			SceneManager.LoadScene("Scene5");
		} 
		else if(SceneManager.GetActiveScene().name == "Scene5")
		{
			SceneManager.LoadScene("Scene6");
		}
		else if(SceneManager.GetActiveScene().name == "Scene6")
		{
			SceneManager.LoadScene("Scene7");
		}
		else if(SceneManager.GetActiveScene().name == "Scene7")
		{
			SceneManager.LoadScene("Scene8");
		}
		else if(SceneManager.GetActiveScene().name == "Scene8")
		{
			SceneManager.LoadScene("Scene9");
		}
		else if(SceneManager.GetActiveScene().name == "Scene9")
		{
			SceneManager.LoadScene("Scene10");
		}

		faseAtual += 1;
		bonusActive = false;
		readVecs();
	}

	private void DestroyAll(){
		foreach (GameObject palisade in palisades)
		{
			Destroy(palisade);
		}
		foreach (GameObject roman in romanes)
		{
			Destroy(roman);
		}
	}

	private void searchPowerUps(){
		PowerUpBalls = GameObject.FindGameObjectsWithTag("TagPowerUpBall");
		PowerUpSwords = GameObject.FindGameObjectsWithTag("TagPowerUpSword");
	}

	public void turnPowerUpsOn(){
		searchPowerUps();

		foreach (GameObject PowerUpBall in PowerUpBalls)
		{
			PowerUpBall.SendMessage("turnOn", 0.5f, SendMessageOptions.RequireReceiver);
		}
		foreach (GameObject PowerUpSword in PowerUpSwords)
		{
			PowerUpSword.SendMessage("turnOn", 0.5f, SendMessageOptions.RequireReceiver);
		}
	}

	private void readVecs(){
		palisades = GameObject.FindGameObjectsWithTag("palisade");
		romanes = GameObject.FindGameObjectsWithTag("roman");
	}

    // Start is called before the first frame update
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("TagBall");
		theBonus = GameObject.FindGameObjectWithTag("TagBonus");
		// AudioSource[] audios = GetComponents<AudioSource>();
		setDecStyle();
		setEndStyle();
		setIncStyle();
		setPtsStyle();
		setTtrStyle();

		pontuacoes = new Pontuacao[9].Select(x => new Pontuacao()).ToArray();
		// Pontuacao[] pontuacoes = InitializeArray<Pontuacao>(10);

		readVecs();
		searchPowerUps();
    }

    // Update is called once per frame
    void Update()
    {
		palisades = GameObject.FindGameObjectsWithTag("palisade");
		romanes = GameObject.FindGameObjectsWithTag("roman");

		if(bonusActive && !powerUpActive){
			GameObject checkBonus = GameObject.FindGameObjectWithTag("TagBonus");

			if(checkBonus == null){
				turnPowerUpsOn();
				powerUpActive = true;
			}
		}
    }
}