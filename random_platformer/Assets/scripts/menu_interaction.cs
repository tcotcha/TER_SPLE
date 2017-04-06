using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class menu_interaction : MonoBehaviour {

	public Button button_start;
	public Button button_exit;
	public Button button_easy;
	public Button button_medium;
	public Button button_hard;
	public GameObject cadre;
	private int positionNiveau;

	private static int nbLevels;

	void Start(){
		choixNiveau (1);
		positionNiveau = 1;
		nbLevels = System.IO.Directory.GetFiles ("Json/Generated/", "*.json", System.IO.SearchOption.TopDirectoryOnly).Length;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape))
			exitGame ();
		if (Input.GetKeyDown (KeyCode.Return))
			startGame ();
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (positionNiveau > 1){
				positionNiveau--;
				choixNiveau (positionNiveau);
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (positionNiveau < 3){
				positionNiveau++;
				choixNiveau (positionNiveau);
			}
		}
	}

	public void startGame(){
		string pathScript = "Json/generate_json.py";
		string pathIn = "Json/";
		string pathOut = "Json/Generated/";
		switch(positionNiveau){
		case 1:
			pathIn += "FM_facile.json";
			pathOut = pathOut + "easy_" + nbLevels + ".json";
			nbLevels++;
			break;
		case 2:
			pathIn += "FM_moyen.json";
			pathOut = pathOut + "medium_" + nbLevels + ".json";
			nbLevels++;
			break;
		case 3:
			pathIn += "FM_difficile.json";
			pathOut = pathOut + "hard_" + nbLevels + ".json";
			nbLevels++;
			break;
		default:
			pathIn += "FM_moyen.json";
			break;
		}
		PlayerPrefs.SetString("Json", pathOut);
		string cmd = pathScript + " " + pathIn + " " + pathOut;

		Process p = new Process ();
		p.StartInfo.FileName = "Python/python.exe";
		p.StartInfo.Arguments = cmd;

		p.StartInfo.UseShellExecute = false;
		p.StartInfo.CreateNoWindow = true;
		p.Start ();
		p.WaitForExit ();
		p.Close ();

		SceneManager.LoadScene("main_scene");
	}

	public void exitGame(){
		Application.Quit();
	}

	public void choixNiveau(int position){
		Vector3 positionCadre = cadre.transform.localPosition;
		switch(position){
		case 1:
			positionCadre.y = 42.5f;
			break;
		case 2:
			positionCadre.y = -42.5f;
			break;
		case 3:
			positionCadre.y = -122.4f;
			break;
		default:
			break;
		}
		cadre.transform.localPosition = positionCadre;
	}
}
