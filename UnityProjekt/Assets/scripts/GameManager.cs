using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XMLParser;

// The game manager is a singleton, which means that there can only be one
// instance at all
public class GameManager : MonoBehaviour 
{
	//public GameManager Instance
	//{
	//    get
	//    {
	//        if (instance == null)

	//        return instance;
	//    }

	//}

	// There are three science steps, they are managed here and can be unlocked
	// by paying with workforce

	//ScienceStep currentScienceStep;

	//TODO add UI

	public List<GameObject> prefabs;
	public List<XMLParser.Building> Buildings;
	
	//TODO add map

	// Use this for initialization
	void Start () 
	{
		prefabs = new List<GameObject>();
		Buildings = new List<XMLParser.Building>();
		//read game values xml data
		Parser parser = new Parser("gamevalues.xml");
		Buildings = parser.Buildings;
		
		//TODO create UI

		//TODO create map
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO update UI
	}
	
	
	
}
