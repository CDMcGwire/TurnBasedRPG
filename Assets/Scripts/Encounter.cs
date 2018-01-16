using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour {
	private List<Character> allies = new List<Character>();
	private List<Character> enemies = new List<Character>();

	private List<Character> turnOrder = new List<Character>();
	private int currentTurn = 0;
	private void SortTurnOrder() {
		turnOrder.Sort((Character a, Character b) => a.Speed.CompareTo(b.Speed));
	}

	private int NextTurn() {
		if (currentTurn >= turnOrder.Capacity) currentTurn = 0;
		else currentTurn++;
		return currentTurn;
	}

	public void InitializeTurnOrder() {
		turnOrder.AddRange(allies);
		turnOrder.AddRange(enemies);
		SortTurnOrder();
		currentTurn = 0;
	}

	public void AddToTurnOrder(Character character) {
		Character currentCombatant = turnOrder[currentTurn];
		turnOrder.Add(character);
		SortTurnOrder();
		if (currentCombatant != turnOrder[currentTurn]) currentTurn++;
	}

	public void UpdateTurnOrder() {
		Character currentCombatant = turnOrder[currentTurn];
		SortTurnOrder();
		if (currentCombatant != turnOrder[currentTurn]) {
			currentTurn = 0;
			while (turnOrder[currentTurn] != currentCombatant) currentTurn++;
		}
	}
}
