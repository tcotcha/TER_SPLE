using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;


[System.Serializable]
public class Niveau {

	public int difficulte; 
	public int taille { get; set; }
	public int saison { get; set; }
	public List<int> hauteurBlocs { get; set; }
	public Joueur joueur { get; set; }
	public CheckPoint checkpoint { get; set; }
	public List<Pieges> pieges { get; set; }
	public List<Plateforme> plateformes { get; set; }
	public List<Ennemis> ennemis { get; set; }
	public List<PowerUp> powerups { get; set; }


	public string Affiche() {
		return "Difficulte  : " + difficulte + "\n" +
		"Taille : " + taille + "\n" +
		"Saison : " + saison + "\n" +
		"NbBlocs : " + hauteurBlocs.Count + "\n" +
		"NbPieges : " + pieges.Count + "\n" +
		"NbPlateformes : " + plateformes.Count + "\n" +
		"NbEnnemis : " + ennemis.Count + "\n" +
		"NbPowerUps : " + powerups.Count + "\n";
	}
}
