using UnityEngine;

public class SaveInterface : MonoBehaviour {
	public void SetSaveFile(string name) {
		SaveFile.ActiveSaveFile = name;
	}
	public void SetProfile(string name) {
		SaveFile.ActiveProfile = name;
	}

	private void Start() {
		System.IO.File.Create(Application.persistentDataPath + "/testfile.txt");
	}

	public void Save() { SaveFile.Save(); }
	public void Load() {
		SaveFile.Load();
		Debug.Log(SaveFile.PlayerData.TestData);
	}
}
