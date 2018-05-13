using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class ActiveObjectState : State<MonoBehaviour> {
	[SerializeField]
	private string name;
	public override string Name { get { return name; } }

	[SerializeField]
	private List<GameObject> activeObjects;

	public ActiveObjectState(string name, List<GameObject> activeObjects) {
		this.name = name;
		this.activeObjects = activeObjects;
	}
	public ActiveObjectState(string name, GameObject activeObject) 
		: this(name, new List<GameObject> { activeObject }) {}
	public ActiveObjectState(string name, params GameObject[] activeObjects)
		:this(name, new List<GameObject>(activeObjects)) {}

	public override void Enter(MonoBehaviour owner) {
		foreach (var gameObject in activeObjects) gameObject.SetActive(true);
	}
	public override void Exit(MonoBehaviour owner) {
		foreach (var gameObject in activeObjects) gameObject.SetActive(false);
	}
}
