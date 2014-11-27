using UnityEngine;
using System.Collections;

public abstract class GameMode : MonoBehaviour {
	public int attacker;
	public int defender { get { return attacker ^ 1; } }
	public int attackerLayer { get { return attacker + 10; } }
	public int defenderLayer { get { return defender + 10; } }
	public abstract int Winner { get; }
}
