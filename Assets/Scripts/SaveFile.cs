using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveFile {
	// Establish Singleton
	private static SaveFile instance = new SaveFile();
	private SaveFile() { }

	// Filepath logic
	private string activeFile = "";
	public static string ActiveSaveFile {
		get { return instance.activeFile; }
		set { instance.activeFile = value; }
	}

	private string activeProfile = "";
	public static string ActiveProfile {
		get { return instance.activeProfile; }
		set { instance.activeProfile = value; }
	}

	private static string getProfileDirectory() {
		if (string.IsNullOrEmpty(ActiveProfile))
			throw new FileNotFoundException("Active File not set!!");
		return Application.persistentDataPath + "/" + ActiveProfile;
	}
	private static string getSaveFilepath() {
		if (string.IsNullOrEmpty(ActiveSaveFile))
			throw new FileNotFoundException("Active File not set!!");
		return getProfileDirectory() + "/" + ActiveSaveFile + ".save";
	}

	// Instance data and static accessor
	private PlayerSaveData playerData;
	public static PlayerSaveData PlayerData {
		get {
			if (instance.playerData == null) Load();
			return instance.playerData;
		}
		set {
			instance.playerData = value;
		}
	}

	// Serialization methods
	public static void Load() {
		string contents = File.ReadAllText(getSaveFilepath());
		JsonUtility.FromJsonOverwrite(contents, instance);
	}
	public static void Save() {
		string saveDirectory = getProfileDirectory();
		if (!Directory.Exists(saveDirectory)) Directory.CreateDirectory(saveDirectory);
		string saveFile = getSaveFilepath();
		if (!File.Exists(saveFile)) File.Create(saveFile);

		File.WriteAllText(saveFile, JsonUtility.ToJson(instance));
	}
}