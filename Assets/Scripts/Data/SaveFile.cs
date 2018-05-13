using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveFile {
	private const string ACTIVE_GAME_PROP = "activeGame";
	private const string ACTIVE_PROFILE_PROP = "activeProfile";

	// Establish Singleton
	private static SaveFile instance = new SaveFile();
	private SaveFile() {
		if (PlayerPrefs.HasKey(ACTIVE_GAME_PROP))
			activeGame = PlayerPrefs.GetString(ACTIVE_GAME_PROP);
		if (PlayerPrefs.HasKey(ACTIVE_PROFILE_PROP))
			activeGame = PlayerPrefs.GetString(ACTIVE_PROFILE_PROP);
	}

	// Filepath logic
	[SerializeField]
	private string activeGame = "";
	public static string ActiveSaveFile {
		get { return instance.activeGame; }
		set {
			PlayerPrefs.SetString(ACTIVE_GAME_PROP, value);
			instance.activeGame = value;
		}
	}

	[SerializeField]
	private string activeProfile = "";
	public static string ActiveProfile {
		get { return instance.activeProfile; }
		set {
			PlayerPrefs.SetString(ACTIVE_PROFILE_PROP, value);
			instance.activeProfile = value;
		}
	}

	// Filepath shorthands
	private static string SaveFilepath { 
		get {
			return SaveDirectory + "/" + ActiveSaveFile;
		}
	}
	private static string SaveDirectory {
		get {
			return Application.persistentDataPath + "/saves/" + ActiveProfile;
		}
	}

	// Serialization methods
	public static void LoadActiveGame() {
		ValidateActiveFile();
		JsonUtility.FromJsonOverwrite(File.ReadAllText(SaveFilepath), instance.saveData);
	}
	public static void SaveActiveGame() {
		ValidateActiveFile();
		if (!Directory.Exists(SaveDirectory)) Directory.CreateDirectory(SaveDirectory);
		File.WriteAllText(SaveFilepath, JsonUtility.ToJson(instance.saveData));
	}

	private static void ValidateActiveFile() {
		if (string.IsNullOrEmpty(ActiveSaveFile))
			throw new FileNotFoundException("No active file set!!");
		if (string.IsNullOrEmpty(ActiveProfile))
			throw new DirectoryNotFoundException("No active profile set!!");
	}

	// Save File Data
	private class SaveData {
		public PlayerData playerData = new PlayerData();
		public List<RegionData> regionDataList = new List<RegionData>();
	}
	[NonSerialized]
	private SaveData saveData = new SaveData();

	public static PlayerData PlayerData {
		get { return instance.saveData.playerData; }
		set { instance.saveData.playerData = value; }
	}
	public static RegionData GetRegion(string target) {
		foreach (RegionData region in instance.saveData.regionDataList)
			if (region.RegionName == target) return region;
		return null;
	}
	public static RegionData CurrentRegion {
		get { return GetRegion(PlayerData.CurrentRegion); }
	}
}