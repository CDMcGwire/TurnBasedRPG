public interface ICombatant {
	bool Alive { get; }
	bool Vulnerable { get; }
	int Speed { get; }
	string Faction { get; }
}