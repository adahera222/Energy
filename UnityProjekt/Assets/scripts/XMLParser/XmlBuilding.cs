using System;
using System.Collections.Generic;

namespace XMLParser
{
	public enum BuildingTypes {
		None = -1,
		PollutionReducer = 0,
		Powerplant,
		House,
		Storage
	};
	
	/// <summary>
	/// The start values (which are not dependent on location) for each building type are stored in this structure.
	/// </summary>
	public class Building
	{
		public BuildingTypes Type;
		public string Name = "";
		public string Identifier = "";
		
		public ValueGroup Values;
		
		public List<Upgrade> Upgrades;
		
		public Building ()
		{
			Type = BuildingTypes.None;
			Values = new ValueGroup();
			Upgrades = new List<Upgrade>();
		}
		
		public bool isValid ()
		{
			return Type != BuildingTypes.None && Name.Length != 0 && Identifier.Length != 0;
		}
	}
}

