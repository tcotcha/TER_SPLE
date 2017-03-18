using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;


[System.Serializable]
public class Niveau {

	public int difficulte; 
	public int taille { get; set; }
	public List<int> hauteurBlocs { get; set; }
	public Joueur joueur { get; set; }
	public CheckPoint checkpoint { get; set; }
	public List<Pieges> pieges { get; set; }
	public IList<Plateforme> plateformes { get; set; }
	public List<Ennemis> ennemis { get; set; }
	public List<PowerUp> powerups { get; set; }

}
