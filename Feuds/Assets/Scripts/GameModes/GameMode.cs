using UnityEngine;
using System.Collections;

public abstract class GameMode : MonoBehaviour {
	public GameManager gm;
	public abstract int Winner { get; }
}
