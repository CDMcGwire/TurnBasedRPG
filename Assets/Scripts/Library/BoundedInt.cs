using UnityEngine;

[System.Serializable]
public struct BoundedInt {
	[SerializeField]
	private int min;
	[SerializeField]
	private int max;
	[SerializeField]
	private int current;

	public BoundedInt(BoundedInt other) 
		: this(other.min, other.max, other.current) { }
	public BoundedInt(int max, int current) 
		: this(0, max, current) { }
	public BoundedInt(int min, int max, int current) {
		this.min = min;
		this.max = 0;
		this.current = 0;
		Max = max;
		Current = current;
	}

	public int Min {
		get { return min; }
		set { min = value.CompareTo(max) > 0 ? max : value; }
	}
	public int Max {
		get { return max; }
		set { max = value.CompareTo(min) < 0 ? min : value; }
	}
	public int Current {
		get { return current; }
		set { current = value.CompareTo(min) < 0 ? min : (value.CompareTo(max) > 0 ? max : value); }
	}
}
