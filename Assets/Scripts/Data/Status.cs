public class Status {
	public StatusType Type { get; private set; }
	public Status(StatusType type) { Type = type; }
}

public enum StatusType {
	Stunned,
	Recovered,
	Burning,
	Dead
}
