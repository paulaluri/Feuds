using UnityEngine;
using System.Collections;

public abstract class Action {
	// takes the gameobject performing the action
	public abstract void Start(GameObject gameObject);
	// return true if action is done
	public abstract bool Update();
}
