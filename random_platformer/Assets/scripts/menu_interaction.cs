using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class menu_interaction : MonoBehaviour {

	private static int nbLevels;
	private Button niveau_facile;
	private Button niveau_moyen;
	private Button niveau_difficile;
	private int positionCurseurNiveau;

	void Start(){
		niveau_facile = GameObject.Find ("Button_Easy").GetComponent<Button> ();
		niveau_moyen = GameObject.Find ("Button_Medium").GetComponent<Button> ();
		niveau_difficile = GameObject.Find ("Button_Hard").GetComponent<Button> ();
		positionCurseurNiveau = 0;
		nbLevels = System.IO.Directory.GetFiles ("Json/Generated/", "*.json", System.IO.SearchOption.TopDirectoryOnly).Length;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Return))
			startGame (positionCurseurNiveau);
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (positionCurseurNiveau > 1){
				positionCurseurNiveau--;
				choixNiveau (positionCurseurNiveau);
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (positionCurseurNiveau < 3){
				positionCurseurNiveau++;
				choixNiveau (positionCurseurNiveau);
			}
		}
	}

	public void choixNiveau(int position){
		switch(position){
		case 1:
			niveau_facile.Select ();
			break;
		case 2:
			niveau_moyen.Select ();
			break;
		case 3:
			niveau_difficile.Select ();
			break;
		default:
			break;
		}
	}

	public void startGame(int difficulte){
		string pathScript = "Json/generate_json.py";
		string pathIn = "Json/";
		string pathOut = "Json/Generated/";
		switch(difficulte){
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
}
