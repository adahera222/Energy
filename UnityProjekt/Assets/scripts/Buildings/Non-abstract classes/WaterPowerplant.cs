using UnityEngine;
using System.Collections;

public class WaterPowerplant : Powerplant {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override Type getBuildingType() 
	{
		return Type.WaterPowerplant;
	}
	
	public override Building applyUpgrade()
	{
		return null;
	}
}