using UnityEngine;
using System.Collections;


public class Tile : MonoBehaviour
{
	//Type of tile
	private TileType type;
	// Map instance to get tiles around it
	private Map map;
	
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
	/*public Building CurrentBuilding
	{
		get;
		private set;
	}*/
	
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
	
	// Builds a building on this tile
	/*public void Build(Building inBuilding)
	{
		this.CurrentBuilding = inBuilding;
		this.isFree = false;
	}*/
	
	// Removes the current building from this tile
	public void RemoveBuilding()
	{
		// this.CurrentBuilding.Clear();
		// this.CurrentBuilding = null;
		this.isFree = true;
	}
	
	// Updates the last pollution of this tile
	public void UpdatePollution()
	{
		/* int tempPollution = this.Pollution + this.CurrentBuilding.CurrentOutput[ResourceType.Pollution];
		 * 
		 * for(Tile t : <map>.GetEnvironmentTiles(this))
		 * {
		 * 		if(t.CurrentBuilding is PollutionReducer)
		 * 		{
		 * 			tempPollution -= ((PollutionReducer)t.CurrentBuilding).ReductionAmount;
		 * 		}
		 * }
		 * this.Polluition = tempPollution;
		 */
	}
	
	// Initialization
	public void Start()
	{
		this.isFree = true;
		this.Pullution = 0;
		Vector3 size = transform.localScale;
		this.Size = new Vector2(size.x, size.z);
		GameObject mapObject = GameObject.Find("Map");
		this.map = mapObject.GetComponent<Map>();
	}
	
	public string Save()
	{
		
	}
}