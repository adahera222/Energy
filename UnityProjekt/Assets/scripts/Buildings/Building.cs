using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A building placed by the user.
/// </summary>
public class Building : MonoBehaviour
{
    public enum ResourceType { Energy, Work, Pollution };
    #region Vars

    protected Tile tileRef;
	protected bool enabled;

    protected Dictionary<ResourceType, float> Input;
    protected Dictionary<ResourceType, float> Output;
    protected Dictionary<ResourceType, float> CurrentInput;
    protected Dictionary<ResourceType, float> CurrentOutput;
     
	protected List<Upgrade> Upgrades;

    #endregion
	
	// Use this for initialization
	void Start () 
    {
        Input = new Dictionary<ResourceType, float>();
        Output = new Dictionary<ResourceType, float>();
        CurrentInput = new Dictionary<ResourceType, float>();
        CurrentOutput = new Dictionary<ResourceType, float>();
	}
	
	#region Properties
	
	public bool IsEnabled
	{
		get;
		set;
	}	
	
	public void updatePollution()
	{
	}
	
	public float updateEfficiency()
	{
		return 0.0f;
	}
	public void updateOutput()
	{
		
	}
	
	public void updateInput()
	{
	}
	
	#endregion

}