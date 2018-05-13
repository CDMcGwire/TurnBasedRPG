using UnityEngine;

public class TestSave : MonoBehaviour {
	public CharacterData testCharacter;

	public void SetTestSaveFile() {
		SaveFile.ActiveProfile = "Test";
		SaveFile.ActiveSaveFile = "TestSave";
	}
	public void InitializeTestSaveData() {
		var playerData = SaveFile.PlayerData;
		playerData.PlayerFirstName = "John";
		playerData.PlayerLastName = "Applebeeesy";
		playerData.CurrentRegion = "Home";
		
		playerData.Party = new Party(testCharacter.Character, "Player");
	}
	public void SaveTestFile() {
		SaveFile.SaveActiveGame();
	}
	public void LoadTestFile() {
		SaveFile.LoadActiveGame();
	}
}
