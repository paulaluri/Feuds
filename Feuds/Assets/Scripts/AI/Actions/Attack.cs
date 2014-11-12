using UnityEngine;
using System.Collections;

public class Attack : Action {
	// This should probably be some script for attacking
	GameObject target;

	Attack(GameObject t) {
		target = t;
	}

	// Use this for initialization
	public override void Start (GameObject g) {

	}
	
	// Return true if target is dead
	// If target is out of range, check stance and determine
	// whether you should pursue and attack again
	public override bool Update () {
		return true;
	}
}
