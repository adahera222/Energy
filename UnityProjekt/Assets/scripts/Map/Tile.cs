using UnityEngine;
using System.Collections;
using System;


public class Tile : MonoBehaviour
{

	//Type of tile
	private TileType type;
	// Map instance to get tiles around it
	private Map map;
	
	// GameManager instance
	private GameManager gameManager;
	
	public TileType Type
	{
		get
		{
			return type;	
		}
		set
		{
			// Find the texture collection
			GameObject go = GameObject.Find("Textures");
			Textures textures = go.GetComponent<Textures>();
			this.type = value;
			// If we have a texture for this type
			if(textures.GetTextureByType(this.type) != null)
			{
				// Set the texture
				renderer.material.mainTexture = textures.GetTextureByType(value);
			}
		}
	}
	
	// Coordinates for the tile
	public Vector2 Coords
	{
		get;
		set;
	}
	
	// Size of the tile
	public Vector2 Size
	{
		get;
		private set;
	}
	
	// Current building on this tile. Null if no building on it.
	public Building CurrentBuilding
	{
		get;
		private set;
	}
	
	// If the tile is free from a building or not
	public bool isFree
	{
		get;
		private set;
	}
	
	// The last pollution of this tile (incl. building and surroundings)
	public float Pollution
	{
		get { return pollution; }
		set
		{
			pollution = value;
			psys.emissionRate = pollution * maxEmissionRate;
		}
	}
	private float pollution;

	const float maxEmissionRate = 20.0f;
	ParticleSystem psys;
	
	
	// Initialization
	public void Start()
	{
		this.isFree = true;
		this.Pollution = 0;
		Vector3 size = transform.localScale;
		this.Size = new Vector2(size.x, size.z);
		GameObject gamemanagerObject = GameObject.Find("GameManager");
		this.map = gamemanagerObject.GetComponent<Map>();
		
		this.gameManager = gamemanagerObject.GetComponent<GameManager>();

		psys = gameObject.GetComponent<ParticleSystem>();
		
	}
	
	public string Save()
	{
		string json = "{";
		json += "\"coords\":[" +
			this.Coords.x + "," +
			this.Coords.y +
			"]," +
			"\"type:\"" + Enum.GetName(typeof(TileType), this.Type) + "\"," +
			"\"pollution\":" + this.Pollution + "," + 
			"\"currentBuilding\":" +
				(this.CurrentBuilding == null ? "\"null\"" : this.getBuildingJson()) +				
			"}";
		return json;
	}
	
	private string getBuildingJson()
	{
		string building = "{";
		building += "\"type\":" + (int)this.CurrentBuilding.getBuildingType() + "," + // TODO: Check for id
			"\"upgrades\":[" +
				this.getUpgradesString() +
			"]}";
		return building;
	}
	
	private string getUpgradesString()
	{
		string temp = "";
		foreach(Upgrade u in this.CurrentBuilding.Upgrades)
		{
			temp += "\"" + u.GetType().Name + ":" + 1 + "\",";//TODO get actual level or name of upgrade
		}
		if(temp.Length > 1)
			temp = temp.Substring(0, temp.Length - 1);
		return temp;
	}
	// Builds a building on this tile
	public Building Build(int Id)
	{
		GameObject newBuilding = (GameObject)Instantiate(gameManager.Prefabs[Id], transform.position, Quaternion.identity);
		
		this.CurrentBuilding = newBuilding.GetComponent<Building>();
		this.isFree = false;
		return this.CurrentBuilding;
	}
	
	// Removes the current building from this tile
	public void RemoveBuilding()
	{
		// this.CurrentBuilding.Clear(); //TODO
		this.CurrentBuilding = null;
		this.isFree = true;
	}
	
	// Updates the last pollution of this tile
	public void UpdatePollution()
	{
		//int tempPollution = (int)(this.Pollution + this.CurrentBuilding.updateOutput()[Building.ResourceType.Pollution]);

	  
		//TODO
		//foreach(Tile t in this.map.GetEnvironmentTiles(this))
		//{
		//    if(t.CurrentBuilding is PollutionReducer)
		//    {
		//        tempPollution -= ((PollutionReducer)t.CurrentBuilding).ReductionAmount;
		//    }
		//}
		//this.Pollution = tempPollution;
	}
	
	public void Load(string json)
	{
		if(json.ToCharArray()[0].Equals('['))
			json = json.Substring(1);
		
		if(json.ToCharArray()[json.Length - 1].Equals(']'))
			json = json.Substring(0, json.Length - 1);	
		
		//json = json.Substring(1, json.Length - 1); is this correct?
		string coords = json.Substring(0, json.IndexOf(",", json.IndexOf("]")) + 1);
		this.Coords = this.StringToVector2(json.Substring(json.IndexOf("["), json.IndexOf("]")));
		json = json.Substring(json.IndexOf("]") + 2);
		this.Type = (TileType)Enum.Parse(typeof(TileType), json.Substring(json.IndexOf(":") + 1, json.IndexOf(",") - json.IndexOf(":") - 1), true);
		json = json.Substring(json.IndexOf(",") + 1);
		try
		{
			this.Pollution = Double.Parse(json.Substring(json.IndexOf(":") + 1, json.IndexOf(",") - json.IndexOf(":") - 1));
		}
		catch(Exception e)
		{
			Debug.Log(e.Message);
		}
		
		
		json = json.Substring(json.IndexOf(':', json.IndexOf("currentBuilding")) + 1);
		if(json.StartsWith("null"))
		{
			this.CurrentBuilding = null;
			this.isFree = true;
		}
		else
		{
			json = json.Substring(1, json.Length - 1);
			int id = -1;
			try
			{
				id = Int32.Parse(json.Substring(json.IndexOf(":") + 1, json.IndexOf(",") - json.IndexOf(":") + 1));
			}
			catch(Exception e)
			{
				Debug.Log(e.Message);
			}
			json = json.Substring(json.IndexOf("[") + 1, json.Length - 1);
			if(id != -1)
			{
				Building b = this.Build(id);
				foreach(string upgrade in json.Split(','))
				{
					string tempupgrade = upgrade.Replace("\"", "");
					string[] upgradeSplit = tempupgrade.Split(':');
					string upgradeType = upgradeSplit[0];
					try
					{
						int upgradeLevel = int.Parse(upgradeSplit[1]);
					}
					catch(Exception e)
					{
						Debug.Log(e.Message);	
					}
					//TODO apply upgrade to building
				}
			}
		}
	}
	
	private Vector2 StringToVector2(string json)
	{
		json = json.Replace("[", "").Replace("]","");
		string[] pos = json.Split(',');
		try
		{
			int x = int.Parse(pos[0]);
			int y = int.Parse(pos[1]);
			return new Vector2(x, y);
		}
		catch(Exception e)
		{
			Debug.Log(e.Message);
		}
		return Vector2.zero;
	}
}
