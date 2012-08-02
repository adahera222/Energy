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
				Transform cube = transform.Find("Cube");
				cube.renderer.material.mainTexture = textures.GetTextureByType(value);
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
	public float Pullution
	{
		get;
		set;
	}
	
	
	
	// Initialization
	public void Start()
	{
		isFree = true;
		Pullution = 0;
		Vector3 size = transform.localScale;
		Size = new Vector2(size.x, size.z);
		//GameObject mapObject = GameObject.Find("Map");
		//map = mapObject.GetComponent<Map>();
		
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	public string Save()
	{
		string json = "{";
		json += "coords:[" +
			this.Coords.x + "," +
			this.Coords.y + "," +
			"]," +
			"type:\"" + Enum.GetName(typeof(TileType), this.Type) + "\"," +
			"pollution:" + this.Pullution + "," + 
			"currentBuilding:" +
				(this.CurrentBuilding == null ? "\"null\"" : this.getBuildingJson()) +				
			"}";
		return json;
	}
	
	private string getBuildingJson()
	{
	//	string building = "{";
	//	building += "type:" + this.CurrentBuilding.ID + "," + // TODO: Check for id
	//		"upgrades:[" +
	//			this.getUpgradesString() +
	//		"]}";
	//	return building;
		return "";
	}
	
	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Build(1);	
		}
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
	}// Builds a building on this tile
	
	public void Build(int Id)
	{
		/*GameObject newBuilding = (
=======
		UnityEngine.Object ob = Instantiate(gameManager.Prefabs[Id], transform.position, Quaternion.identity);
		
		GameObject newBuilding = (

>>>>>>> b8d98b52d46fc62e087032bcfb603e35d3139e78
			Instantiate(gameManager.Prefabs[Id], transform.position, Quaternion.identity) 
			as Transform).gameObject;

		
		CurrentBuilding = newBuilding.GetComponent<Building>();
		isFree = false;
	*/}
	
	// Removes the current building from this tile
	public void RemoveBuilding()
	{
		// this.CurrentBuilding.Clear();
		this.CurrentBuilding = null;
		this.isFree = true;
	}
	
	// Updates the last pollution of this tile
	public void UpdatePollution()
	{
		//int tempPollution = this.Pollution + this.CurrentBuilding.CurrentOutput[ResourceType.Pollution];
	  
		//foreach(Tile t in this.map.GetEnvironmentTiles(this))
		//{
		//    if(t.CurrentBuilding is PollutionReducer)
		//    {
		//        tempPollution -= ((PollutionReducer)t.CurrentBuilding).ReductionAmount;
		//    }
		//}
		//this.Polluition = tempPollution;
	}
	
	public void Load(string json)
	{
		//Debug.Log(json);
		//Debug.Log(json.Length);
		json = json.Substring(1, json.Length-1);
		string coords = json.Substring(0, json.IndexOf(",", 2) - 1);
		this.Coords = this.StringToVector2(json.Substring(json.IndexOf("["), json.IndexOf("]")));
		json = json.Substring(json.IndexOf("]") + 2);
		this.Type = (TileType)Enum.Parse(typeof(TileType), json.Substring(json.IndexOf(":") + 1, json.IndexOf(",")-json.IndexOf(":")-1), true);
		try
		{
			this.Pullution = int.Parse(json.Substring(json.IndexOf(":") + 1, json.IndexOf(",") - 1));
		}
		catch(Exception e)
		{
			//Debug.Log(e.Message);
		}
		json = json.Substring(json.IndexOf(":", 2) + 1);
		if(json.Equals("null"))
		{
			this.CurrentBuilding = null;
			this.isFree = true;
		}
		else
		{
			json = json.Substring(1, json.Length - 1);
			try
			{
				int id = int.Parse(json.Substring(json.IndexOf(":") + 1, json.IndexOf(",") - 1));
			}
			catch(Exception e)
			{
				//Debug.Log(e.Message);
			}
			json = json.Substring(json.IndexOf("[") + 1, json.Length - 1);
			foreach(string upgrade in json.Split(','))
			{
				string[] upgradeSplit = upgrade.Split(':');
				string upgradeType = upgradeSplit[0];
				try
				{
					int upgradeLevel = int.Parse(upgradeSplit[1]);
				}
				catch(Exception e)
				{
					//Debug.Log(e.Message);	
				}
				//TODO apply upgrade to building
			}
			//TODO create building
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
