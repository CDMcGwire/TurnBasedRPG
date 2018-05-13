using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
	[SerializeField]
	private string currentRegion = "";
	public string CurrentRegion {
		get { return currentRegion; }
		set { currentRegion = value; }
	}
	[SerializeField]
	private string playerFirstName = "John";
	public string PlayerFirstName {
		get { return playerFirstName; }
		set { playerFirstName = value; }
	}
	[SerializeField]
	private string playerLastName = "Clyde";
	public string PlayerLastName {
		get { return playerLastName; }
		set { playerLastName = value; }
	}

	public string PlayerFullName {
		get { return playerFirstName + " " + playerLastName; }
	}

	[SerializeField]
	private Party playerParty;
	public Party Party {
		get { return playerParty; }
		set { playerParty = value; }
	}
}