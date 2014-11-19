using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack : Action {
	// This should probably be some script for attacking
	private List<Action> actions;
	private CombatController target;

	private bool pursue;
	private bool goback;


	public Attack(GameObject target) : this(target,true,false) {
	}

	public Attack(GameObject target, bool pursue, bool goback) {
		this.target = target.GetComponent<CombatController>();
		this.pursue = pursue;
		this.goback = goback;

		actions = new List<Action> ();
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
