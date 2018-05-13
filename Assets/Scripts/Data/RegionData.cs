using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class RegionData {
	[SerializeField]
	private string regionName = "DefaultRegionName";
	public string RegionName { get { return regionName; } }
}
