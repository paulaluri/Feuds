using UnityEngine;
using System.Collections;

public abstract class GameMode : MonoBehaviour {
	public int attacker;
	public int defender { get { return attacker ^ 1; } }
	public abstract int Winner { get; }
}
