using UnityEngine;
using System.Collections;

public class BioPowerplant : Building {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override Type getBuildingType() 
	{
		return Type.BioPowerplant;
	}
	
	public override void applyUpgrade()
	{
		
	}
}
