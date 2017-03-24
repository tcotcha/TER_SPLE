using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class GenerationNiveau : MonoBehaviour {
	private Niveau niveau;

	void Start () {
		DateTime start = DateTime.Now;
		chargerJson (PlayerPrefs.GetString ("Json"));
		chargerNiveau ();
        GameObject.Find("mur_fin").transform.position = new Vector3(niveau.taille, transform.position.y, transform.position.z);
        Camera.main.GetComponent<Camera_follow>().setMaxX(niveau.taille - 1);
        TimeSpan dur = DateTime.Now - start;
		Debug.Log ("Temps d'execution = " + dur.ToString());
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
	}

	public void chargerJson(string _path){
		/*
		 * On met dans le path, le chemin du json créé aléatoirement.
		 * Il est dans le dossier streamingAssets car c'est une sorte de pré-requis unity
		 * On lit ensuite le fichier et on met le tout dans le string jsonString
		*/
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
		Debug.Log ("Liste_hauteurblocs : Fait");

		/*
		 * Pièges
		*/
		foreach (var item_pieges in element_niveau["pieges"].Children()) {
			int largeur = item_pieges ["longueur"].Value<int> ();
			float positionX = item_pieges ["positionX"].Value<float> ();
			niveau.pieges.Add (new Pieges (largeur, positionX));
		}
		Debug.Log ("Liste_pieges : Fait");

		/*
		 * Checkpoint
		*/
		niveau.checkpoint.actif = element_niveau ["checkpoint"] ["actif"].Value<bool> ();
		niveau.checkpoint.x = element_niveau ["checkpoint"] ["x"].Value<int> ();
		niveau.checkpoint.y = element_niveau ["checkpoint"] ["y"].Value<int> ();
		Debug.Log ("Checkpoint : Fait");

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
		Debug.Log ("Liste_plateformes : Fait");

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
		Debug.Log ("Liste_ennemis : Fait");

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
		Debug.Log ("Liste_powerups : Fait");
		Debug.Log (niveau.Affiche ());
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