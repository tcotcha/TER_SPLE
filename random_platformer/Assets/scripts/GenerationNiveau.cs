using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GenerationNiveau : MonoBehaviour {
	private static Niveau niveau;

	void Start () {
		DateTime start = DateTime.Now;
		chargerJson (PlayerPrefs.GetString ("Json"));
		chargerNiveau ();
        GameObject.Find("mur_fin").transform.position = new Vector3(niveau.taille+0.5f, transform.position.y, transform.position.z);
        Camera.main.GetComponent<Camera_follow>().setMaxX(niveau.taille);
        TimeSpan dur = DateTime.Now - start;
		Debug.Log ("Temps d'execution = " + dur.ToString());
		PlayerPrefs.SetInt("Y0", niveau.hauteurBlocs[0]);
		if(niveau.saison == 0){
			PlayerPrefs.SetString("Saison", "snow");
		}else{
			PlayerPrefs.SetString("Saison","grass");
		}
	}

	public Niveau getNiveau(){
		return niveau;
	}

	public void chargerNiveau(){
		/*
		 * Instanciation du sol
		 */
		for (int i = 0; i < niveau.taille; i++) {
			for (int j = 0; j < niveau.hauteurBlocs [i]; j++) {
                GameObject tmp = chooseTile(niveau.hauteurBlocs[i], j, niveau.saison);
                Instantiate (tmp, new Vector2 (i, j), Quaternion.identity);
			}
		}

		/*
		 * Player
		 */
		GameObject.Find("Player").transform.position = new Vector2 (0.5f, (float)niveau.hauteurBlocs [0] + 1.5f);

		/*
		 * powerups
		 */
		niveau.powerups.ForEach (delegate(PowerUp obj) {
			GameObject tmp = Resources.Load ("powerup") as GameObject;
		//	UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/prefabs/powerup.prefab", typeof(GameObject));
			Instantiate(tmp, new Vector2 (obj.getX(), obj.getY()), Quaternion.identity);
			tmp.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("powerup/"+obj.GetType());
			tmp.name = obj.GetType().ToString();
		});
		/*
		 * plateformes
		 */
		genererPlateformes ();

		/*
		 *Plateforme de fin 
		 */
		Instantiate(Resources.Load ("plateforme_fin_niveau") as GameObject,new Vector2 (niveau.taille - 5f, (float)niveau.hauteurBlocs [niveau.taille - 5] + 2f),Quaternion.identity);
		/*
		 * Ennemis
		 */
		foreach(var obj in niveau.ennemis){
			bool isBumper = (obj.GetType() == typeof(Bumper));
			GameObject tmp = Resources.Load("ennemis") as GameObject;
			if (isBumper) {
				(Instantiate (tmp, new Vector2 (obj.x + 0.5f, obj.y - 0.4f), Quaternion.identity)).GetComponent<ennemis_script> ().init (isBumper, niveau);
			} else {
				(Instantiate(tmp,new Vector2(obj.x+0.5f,obj.y-0.5f),Quaternion.identity)).GetComponent<ennemis_script>().init(isBumper,niveau);
			}
		}

		/*
		 *CheckPoint 
		 */
		//UnityEngine.Object flag = AssetDatabase.LoadAssetAtPath("Assets/prefabs/checkpoint.prefab", typeof(GameObject));
		GameObject myCheckpoint = Resources.Load ("checkpoint") as GameObject;
		Instantiate(myCheckpoint, new Vector2 (niveau.checkpoint.x+0.5f,niveau.hauteurBlocs[(int)niveau.checkpoint.x]-0.5f), Quaternion.identity);
		myCheckpoint.name = "Checkpoint";


		/*Pieces*/
		niveau.pieces.ForEach (delegate(Piece p) {
			//UnityEngine.Object tmp = AssetDatabase.LoadAssetAtPath("Assets/prefabs/coin.prefab", typeof(GameObject));
			GameObject coin = Resources.Load ("coin") as GameObject;
			Instantiate(coin, new Vector2(p.positionX, p.positionY), Quaternion.identity);
			coin.name = "coin";
		});

	}

	public void chargerJson(string _path){
		string path = _path;
		string jsonString = File.ReadAllText (path);

		/*
		 * On appelle la classe Niveau pour l'instanciation des objets
		*/
		niveau = new Niveau ();
		niveau.plateformes = new List<Plateforme> ();
		niveau.powerups = new List<PowerUp> ();
		niveau.ennemis = new List<Ennemis> ();
		niveau.pieges = new List<Pieges> ();
		niveau.hauteurBlocs = new List<int> ();
		niveau.checkpoint = new CheckPoint ();
		niveau.pieces = new List<Piece> ();

		/*
		 * On créer un JObject afin de parser notre json
		*/
		JObject element_niveau = JObject.Parse (jsonString);


		/*
		 * On débute l'instanciation de nos classes
		 * element_niveau ["difficulte"] représente le noeud "difficulte" dans notre json
		 * On va ensuite parcourir les fils de chaque noeud.
		 * Ici on fait la difficulte et la taille du niveau
		*/
		niveau.difficulte = element_niveau ["difficulte"].Value<int> ();
		niveau.taille = element_niveau ["taille"].Value<int> ();
		niveau.saison = element_niveau ["saison"].Value<int> ();
		/*
		 * HauteurBloc
		*/
		foreach (var i in element_niveau["hauteurBlocs"].Children()) {
			niveau.hauteurBlocs.Add (i.Value<int>());
		}

		/*
		 * Pièges
		*/
		foreach (var item_pieges in element_niveau["pieges"].Children()) {
			int largeur = item_pieges ["longueur"].Value<int> ();
			float positionX = item_pieges ["positionX"].Value<float> ();
			niveau.pieges.Add (new Pieges (largeur, positionX));
		}

		/*
		 * Pièces
		*/
		foreach (var item_pieces in element_niveau["pieces"].Children()) {
			float positionX = item_pieces ["positionX"].Value<float> ();
			float positionY = item_pieces ["positionY"].Value<float> ();
			niveau.pieces.Add (new Piece (positionX,positionY));
		}

		/*
		 * Checkpoint
		*/
		niveau.checkpoint.actif = element_niveau ["checkpoint"] ["actif"].Value<bool> ();
		niveau.checkpoint.x = element_niveau ["checkpoint"] ["x"].Value<int> ();
		niveau.checkpoint.y = element_niveau ["checkpoint"] ["y"].Value<int> ();

		/*
		 * Plateformes
		*/
		foreach (var item_plateforme in element_niveau["plateformes"].Children()) {
			if (((string)item_plateforme ["type"]).Equals ("Immobile")) {
				int largeur = item_plateforme ["largeur"].Value<int> ();
				float x = item_plateforme ["x"].Value<float> ();
				float y = item_plateforme ["y"].Value<float> ();
				bool friable = item_plateforme ["friable"].Value<bool> ();
				niveau.plateformes.Add (new Immobile (largeur, x, y, friable));

			} else {
				int largeur = item_plateforme ["largeur"].Value<int> ();
				float x = item_plateforme ["x"].Value<float> ();
				float y = item_plateforme ["y"].Value<float> ();
				float finX = item_plateforme ["finX"].Value<float> ();
				float finY = item_plateforme ["finY"].Value<float> ();
				niveau.plateformes.Add (new Mobile (largeur, x, y, finX, finY));
			}				
		}

		/*
		 * Ennemis
		*/
		foreach (var item_ennemis in element_niveau["ennemies"].Children()) {
			if (((string)item_ennemis ["type"]).Equals ("Tireur")) {
				int x = item_ennemis ["x"].Value<int> ();
				int y = item_ennemis ["y"].Value<int> ();
				niveau.ennemis.Add (new Tireur (x, y));

			} else {
				int x = item_ennemis ["x"].Value<int> ();
				int y = item_ennemis ["y"].Value<int> ();
				niveau.ennemis.Add (new Bumper (x, y));
			}				
		}

		/*
		 * PowerUps
		*/
		foreach (var item_powerup in element_niveau["items"].Children()) {
			float x, y;
			int duree;
			switch ((string)item_powerup ["type"]) {

			case "JumpBoost":
				x = item_powerup ["x"].Value<float>();
				y = item_powerup ["y"].Value<float>();
				duree = item_powerup ["duree"].Value<int>();
				niveau.powerups.Add (new JumpBoost (duree, x, y));
				break;
			case "Inversement":
				x = item_powerup ["x"].Value<float>();
				y = item_powerup ["y"].Value<float>();
				duree = item_powerup ["duree"].Value<int>();
				niveau.powerups.Add (new Inversement (duree, x, y));
				break;
			case "Invincibilite":
				x = item_powerup ["x"].Value<float>();
				y = item_powerup ["y"].Value<float>();
				duree = item_powerup ["duree"].Value<int>();
				niveau.powerups.Add (new Invincibilite (duree, x, y));
				break;
			case "VieMalus":
				x = item_powerup ["x"].Value<float>();
				y = item_powerup ["y"].Value<float>();
				niveau.powerups.Add (new VieMalus (x, y));
				break;
			case "VieBonus":
				x = item_powerup ["x"].Value<float>();
				y = item_powerup ["y"].Value<float>();
				niveau.powerups.Add (new VieBonus (x, y));
				break;		
			}
		}
	}

	private static void genererPlateformes(){
		niveau.plateformes.ForEach (delegate(Plateforme p) {
			//UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath ("Assets/prefabs/platform_" + p.largeur + ".prefab", typeof(GameObject));
			GameObject tmp = Resources.Load("platform_" + p.largeur) as GameObject;
			Instantiate (tmp, new Vector2 (p.positionX, p.positionY), Quaternion.identity);
			mouvement_platform script = tmp.GetComponent<mouvement_platform>();
			tmp.name = "platform_" + p.largeur;
			script.x = p.positionX;
			script.y = p.positionY;
			try
			{
				if(p.getFriable())
					script.friable = true;
			}
			catch(Exception e){
				script.friable = false;
			}
			if (p.GetType().ToString() == "Mobile"){
				script.fin_x = p.getPosFinX();
				script.fin_y = p.getPosFinY();
			}else{
				script.fin_x = p.positionX;
				script.fin_y = p.positionY;
			}
		});
	}

	public static void regenererPlateformes(){
		GameObject[] g = GameObject.FindGameObjectsWithTag ("Platform");
		foreach (GameObject p in g){
			Destroy(p);
		}
		genererPlateformes ();
	}

	private GameObject chooseTile(int h,int j,int s){
        GameObject ret;
		if (s == 0) {
			if (j < h - 1) {
				ret = Resources.Load ("sol/sol_bottom_winter") as GameObject;
			} else {
				ret =  Resources.Load ("sol/sol_top_winter") as GameObject;
			}
		} else {
			if (j < h - 1) {
				ret = Resources.Load ("sol/sol_bottom_summer") as GameObject;
			} else {
				ret = Resources.Load ("sol/sol_top_summer") as GameObject;
			}
		}
        return ret;
	}

}