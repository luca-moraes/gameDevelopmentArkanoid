using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// #if UNITY_EDITOR
//     using UnityEditor;
// #endif

public class GameManager : MonoBehaviour
{
	// FinalScore finalScore;
	// private int faseAtual = 0;
	public AudioClip ponto1;
	public AudioClip ponto2;
	public AudioClip clickButton;
	public AudioClip faseComplete;
	public AudioClip faseFail;
	public AudioClip perde1;
	public AudioClip perde2;

	private AudioSource audioSource;

	private bool desistencia = false;
	private bool bonusActive = false;
	private bool powerUpActive = false;

	public GUISkin layout;
	private GUIStyle guiStyleDec = new GUIStyle();
	private GUIStyle guiStyleEnd = new GUIStyle();
	private GUIStyle guiStyleDst = new GUIStyle();
	private GUIStyle guiStyleInc = new GUIStyle();
	private GUIStyle guiStylePts = new GUIStyle();
	private GUIStyle guiStyleTtr = new GUIStyle();
	private GUIStyle guiStyleSma = new GUIStyle();

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

	private void setDstStyle(){
		guiStyleDst.fontSize = 22;
		guiStyleDst.normal.textColor = Color.red;

		Texture2D debugTex = new Texture2D(1,1);
	  	debugTex.SetPixel(0,0, Color.black);
	  	debugTex.Apply();

		guiStyleDst.normal.background = debugTex;
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

	private void setSmaStyle(){
		guiStyleSma.fontSize = 12;
		guiStyleSma.normal.textColor = Color.red;		

		Texture2D debugTex = new Texture2D(1,1);
	  	debugTex.SetPixel(0,0, new Color(0.0f, 0.0f, 0.0f, 0.3f));
	  	debugTex.Apply();

		guiStyleSma.normal.background = debugTex;
	}
	
	public void Score (int type, float x, float y) {
	    if (type == 1)
	    {
			FinalScore.pontuacoes[faseAtual()].palisade += 1;

			PontoObject ponto = new PontoObject(x, y, "+1", true);
			pontos.Enqueue(ponto);
			audioSource.PlayOneShot(ponto1);
	    }
		else if (type == 2)
		{
	        FinalScore.pontuacoes[faseAtual()].roman += 1;

			PontoObject ponto = new PontoObject(x, y, "+2", true);
			pontos.Enqueue(ponto);
			audioSource.PlayOneShot(ponto2);
	    }
	}

	public void dequeue(){
		pontos.Dequeue();
	}

	void showPlacar(){
		GUI.Label(new Rect(Screen.width / 2 - 60 - 12, 5, 130, 22), "PUNCTA: " + FinalScore.pontuacoes[faseAtual()].total() , guiStylePts);
	}

	private void RestartBalls()
    {
		//perde 1 ponto
		audioSource.PlayOneShot(perde1);
        theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
    }

	private void decPontos(){
		FinalScore.pontuacoes[faseAtual()].clicks += 1;
	}

	public void retBall(){
		FinalScore.pontuacoes[faseAtual()].quedas += 1;

		PontoObject ponto = new PontoObject(Screen.width / 2 - 105, 5, "-2", false);
		pontos.Enqueue(ponto);
		audioSource.PlayOneShot(perde2);
		//perde 2 pontos
	}

	void OnGUI () {
	    GUI.skin = layout;
	    
		if (SceneManager.GetActiveScene().name == "Scene1")
		{
			GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 300, 500, 28), "Use as teclas A e S para mover a espada", guiStyleTtr);

			if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height/2, 120, 53), "SATUS"))
			{
				audioSource.PlayOneShot(clickButton);
				Invoke("changeScene", 1);
				// changeScene();
			}
		}
		else if(SceneManager.GetActiveScene().name == "Scene11")
		{
			GUI.Label(new Rect(Screen.width / 2 - 250, 30, 120, 28), "FINIS LUDI", guiStyleEnd);

			int basePosY = 100;
			int numFase = 1;
			int somaFinal = 0;

			// Pontuacao[] pontuacaoFinal = FinalScore.getPontuacoes();

			foreach(Pontuacao pontuacao in FinalScore.pontuacoes){
				GUI.Label(new Rect(Screen.width / 2 - 250, basePosY, 140, 26), "TEMPUS " + numFase + " SUMMA: " + pontuacao.total(), guiStyleSma);

				GUI.Label(new Rect(Screen.width / 2 - 230, basePosY + 30, 450, 26),
				 "TESSELLIS -> " + pontuacao.palisade + " | " + "VALOREM -> " + pontuacao.palisadePts() 
				 + " | <---> | CAUDICES -> " + pontuacao.roman + " | " + "VALOREM -> " + pontuacao.romanPts(),
				 guiStyleSma
				);

				GUI.Label(new Rect(Screen.width / 2 - 230, basePosY + 60, 450, 26),
				 "CADIT -> " + pontuacao.quedas + " | " + "VALOREM -> " + pontuacao.quedasPts()
				 + " | <---> | REPONERES -> " + pontuacao.clicks + " | " + "VALOREM -> " + pontuacao.clicksPts(),
				 guiStyleSma
				);
				
				basePosY += 90;
				numFase += 1;
				somaFinal += pontuacao.total();
			}

			GUI.Label(new Rect(Screen.width / 2 - 60, basePosY, 140, 26), "SUMMA FINALIS: " + somaFinal, guiStyleSma);


			if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 90, 120, 53), "SILEO"))
			{
				// RestartGameManager();
				audioSource.PlayOneShot(clickButton);
				Invoke("RestartGameManager", 1);
			}
		}
		else
		{
			showPlacar();
		
			if (GUI.Button(new Rect(Screen.width / 2 + 190, Screen.height - 40, 90, 30), "PERDERE"))
			{
				desistencia = true;
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
				
				if(desistencia){
					audioSource.PlayOneShot(faseFail);
					GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height/2, 310, 22), "DESISTISTI PROVOCATIONEM!", guiStyleDst);
				}else{
					audioSource.PlayOneShot(faseComplete);
					GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height/2, 270, 22), "VICISTI PROVOCATIONEM!", guiStyleEnd);
				}

				// #if UNITY_EDITOR
				// 	Handles.Label(new Vector3(Screen.width/2-60, Screen.height/2, 0.0f), "Fase finalizada!", guiStyleEnd);
				// #endif

				Invoke("RestartBalls", 1);
				Invoke("changeScene", 3);
			}
			
			if(FinalScore.pontuacoes[faseAtual()].total() > 15 && !bonusActive)
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

	// private void savePontuacao(int faseNum){
	// 	FinalScore.addPontuacao(pontuacaoFaseAtual, faseNum);
	// }

	private int faseAtual(){
		if(SceneManager.GetActiveScene().name == "Scene2")
        {
			return 0;
        }
		else if(SceneManager.GetActiveScene().name == "Scene3")
        {
			return 1;
        }
		else if(SceneManager.GetActiveScene().name == "Scene4")
		{
			return 2;
		}
		else if(SceneManager.GetActiveScene().name == "Scene5")
		{
			return 3;
		}
		else if(SceneManager.GetActiveScene().name == "Scene6")
		{
			return 4;
		}
		else if(SceneManager.GetActiveScene().name == "Scene7")
		{
			return 5;
		}
		else if(SceneManager.GetActiveScene().name == "Scene8")
		{
			return 6;
		}
		else if(SceneManager.GetActiveScene().name == "Scene9")
		{
			return 7;
		}
		else if(SceneManager.GetActiveScene().name == "Scene10")
		{
			return 8;
		}
		else
		{
			return -1;
		}
	}

	private void changeScene(){
		// if(faseAtual < 8){
		// 	faseAtual += 1;
		// 	FinalScore.addPontuacao(pontuacaoFaseAtual, faseAtual);
		// }

		if (SceneManager.GetActiveScene().name == "Scene1")
        {
			// faseAtual = 0;
			// FinalScore.initFinalScore();
            SceneManager.LoadScene("Scene2");
        }
        else if(SceneManager.GetActiveScene().name == "Scene2")
        {
			// faseAtual = 1;
			// savePontuacao(0);
            SceneManager.LoadScene("Scene3");
        } 
		else if(SceneManager.GetActiveScene().name == "Scene3")
        {
			// savePontuacao(1);
            SceneManager.LoadScene("Scene4");
        }
		else if(SceneManager.GetActiveScene().name == "Scene4")
		{
			// savePontuacao(2);
			SceneManager.LoadScene("Scene5");
		} 
		else if(SceneManager.GetActiveScene().name == "Scene5")
		{
			// savePontuacao(3);
			SceneManager.LoadScene("Scene6");
		}
		else if(SceneManager.GetActiveScene().name == "Scene6")
		{
			// savePontuacao(4);
			SceneManager.LoadScene("Scene7");
		}
		else if(SceneManager.GetActiveScene().name == "Scene7")
		{
			// savePontuacao(5);
			SceneManager.LoadScene("Scene8");
		}
		else if(SceneManager.GetActiveScene().name == "Scene8")
		{
			// savePontuacao(6);
			SceneManager.LoadScene("Scene9");
		}
		else if(SceneManager.GetActiveScene().name == "Scene9")
		{
			// savePontuacao(7);
			SceneManager.LoadScene("Scene10");
		}
		else if(SceneManager.GetActiveScene().name == "Scene10")
		{
			// savePontuacao(8);
			SceneManager.LoadScene("Scene11");
		}

		bonusActive = false;
		powerUpActive = false;
		desistencia = false;
		// pontuacaoFaseAtual = new Pontuacao();
		readVecs();
	}

	private void RestartGameManager(){
		// faseAtual = 0;
		bonusActive = false;
		powerUpActive = false;
		// pontuacaoFaseAtual = new Pontuacao[9].Select(x => new Pontuacao()).ToArray();
		// pontuacaoFaseAtual = new Pontuacao();

		SceneManager.LoadScene("Scene1");
		FinalScore.resetPontuacoes();
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

	// void Awake()
    // {
    //     //Check if instance already exists
    //     if (finalScore == null){
            
    //         //if not, set instance to this
    //         finalScore = GameObject.FindGameObjectWithTag("Score");
	// 	}
    //     //If instance already exists and it's not this:
    //     // else if (finalScore != this){

    //     //     //Then destroy this. This enforces our singleton pattern, 
	// 	// 	// meaning there can only ever be one instance of a GameManager.
    //     //     Destroy(gameObject);    
	// 	// }
    //     //Sets this to not be destroyed when reloading scene / Switching scenes
    //     DontDestroyOnLoad(finalScore); // VERY IMPORTANT
    // }

    // Start is called before the first frame update
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("TagBall");
		theBonus = GameObject.FindGameObjectWithTag("TagBonus");

		audioSource = GetComponent<AudioSource>();

		setDecStyle();
		setEndStyle();
		setIncStyle();
		setPtsStyle();
		setTtrStyle();
		setSmaStyle();
		setDstStyle();

		// pontuacaoFaseAtual = new Pontuacao[9].Select(x => new Pontuacao()).ToArray();
		// Pontuacao[] pontuacaoFaseAtual = Pontuacao.InitializeArray<Pontuacao>(10);

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