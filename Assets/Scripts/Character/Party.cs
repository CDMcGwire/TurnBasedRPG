using UnityEngine;
using System;
using System.Collections.Generic;

// TODO: Update to a centralized character access.
// Likely, the party will only keep track of CharacterData objects, 
// instantiating a list of Characters only when needed

[Serializable]
public class Party : ISerializationCallbackReceiver {
	private Character partyLeader;
	public Character PartyLeader {
		get { return partyLeader; }
		set { if (partyMembers.Contains(value)) partyLeader = value; }
	}
	[SerializeField]
	private CharacterList partyMembers = new CharacterList();
	public CharacterList PartyMembers {
		get { return new CharacterList(partyMembers); }
	}
	[SerializeField]
	private int maxRosterSize = 4;
	public int MaxRosterSize {
		get { return maxRosterSize; }
		set { maxRosterSize = value; }
	}
	private CharacterList activeRoster = new CharacterList();
	public CharacterList ActiveRoster {
		get { return new CharacterList(activeRoster); }
	}
	
	// Custom Serialization
	[SerializeField]
	private string s_PartyLeader;
	[SerializeField]
	private List<string> s_ActiveRoster;

	public void OnBeforeSerialize() {
		s_PartyLeader = partyLeader.Name;
		s_ActiveRoster = activeRoster.Names;
	}

	public void OnAfterDeserialize() {
		partyLeader = partyMembers.Find(member => member.Name == s_PartyLeader);
		foreach (var name in s_ActiveRoster) {
			activeRoster.Add(partyMembers.Find(member => member.Name == name));
		}
	}

	// Constructor
	public Party(Character leader, string faction) {
		partyMembers.Add(leader);
		partyLeader = leader;
		activeRoster.Add(leader);
	}

	// Public Methods
	public void AddMember(Character member) {
		if (!partyMembers.Contains(member)) partyMembers.Add(member);
	}
	public void RemoveMember(Character member) {
		partyMembers.Remove(member);
	}
	public bool AddRosterMember(Character member) {
		if (activeRoster.Count <= maxRosterSize && partyMembers.Contains(member)) {
			activeRoster.Add(member);
			return true;
		}
		else return false;
	}
	public bool RemoveRosterMember(Character member) {
		return activeRoster.Remove(member);
	}
	public bool SetActiveRoster(CharacterList newRoster) {
		foreach (var character in newRoster) {
			if (!partyMembers.Contains(character)) {
				return false;
			}
		}
		if (newRoster.Count <= maxRosterSize) {
			activeRoster = new CharacterList(newRoster);
			return true;
		}
		else return false;
	}
}
