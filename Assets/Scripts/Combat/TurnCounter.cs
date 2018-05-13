using System;
using System.Collections.Generic;

public class TurnCounter<T> {
	List<T> contents = new List<T>();
	Comparison<T> comparison;

	int currentTurn = 0;
	int currentRound = 0;
	bool updateAtEnd = false;

	// Properties
	public Comparison<T> Comparison {
		get { return comparison; }
		set { comparison = value; }
	}

	public T CurrentItem { get { return contents[currentTurn]; } }
	public int CurrentTurn { get { return currentTurn; } }
	public int CurrentRound { get { return currentRound; } }

	public List<T> Contents { get { return contents; } }

	// Constructors
	public TurnCounter(List<T> contents, Comparison<T> comparison) {
		this.contents.AddRange(contents);
		this.comparison = comparison;
	}
	public TurnCounter(List<T> contents) : this(contents, null) { }
	public TurnCounter(Comparison<T> comparison) { this.comparison = comparison; }

	// Private Methods
	private void Sort() {
		if (comparison == null) contents.Sort();
		else contents.Sort(comparison);
		updateAtEnd = false;
	}

	// Interface Methods
	public void EndTurn() {
		currentTurn++;
		if (currentTurn >= contents.Count) {
			if (updateAtEnd) Reset();
			else {
				currentTurn = 0;
				currentRound++;
			}
		}
	}

	public void Add(T item) {
		contents.Add(item);
		updateAtEnd = true;
	}

	public void Add(List<T> range) {
		contents.AddRange(range);
		updateAtEnd = true;
	}

	public bool Remove(T target) {
		if (target.Equals(CurrentItem))
			return contents.Remove(target);

		var preRemovalItem = CurrentItem;
		var wasRemoved = contents.Remove(target);
		if (wasRemoved && !CurrentItem.Equals(preRemovalItem)) {
			currentTurn--;
		}
		return wasRemoved;
	}

	public void Clear() {
		contents.Clear();
		currentTurn = 0;
		currentRound = 0;
	}

	public void Update() {
		var preUpdateItem = CurrentItem;
		Sort();

		if (!preUpdateItem.Equals(contents[currentTurn])) {
			currentTurn = 0;
			while (!CurrentItem.Equals(preUpdateItem)) currentTurn++;
		}
	}

	public void Reset() {
		Sort();
		currentTurn = 0;
		currentRound = 0;
		updateAtEnd = false;
	}
}
