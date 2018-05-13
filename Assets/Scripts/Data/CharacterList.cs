using System.Collections.Generic;

[System.Serializable]
public class CharacterList : List<Character> {
	public List<string> Names {
		get {
			var names = new List<string>();
			foreach (var character in this)
				names.Add(character.Name);
			return names;
		}
	}

	public CharacterList() : base() { }
	public CharacterList(List<Character> entries) : base(entries) { }
}