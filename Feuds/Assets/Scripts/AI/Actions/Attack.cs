using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack : Action {
	// This should probably be some script for attacking
	private List<Action> actions;
	private CombatController target;

	private bool pursue;

	public Attack(GameObject target) : this(target,true) {
	}

	public Attack(GameObject target, bool pursue) {
		this.target = target.GetComponent<CombatController>();
		this.pursue = pursue;

		actions = new List<Action> ();
	}

	// Use this for initialization
	public override void Start (GameObject g) {
		if(pursue) {
			actions.Add(new Pursue(target.gameObject.transform));
		}
		actions.Add (new AttackDirect (target));
		if(g.GetComponent<ActionController>().CurrentStance == Stance.Defensive) {
			actions.Add(new MoveTo(g.transform.position));
		}
		foreach(Action action in actions) {
			action.Start(g);
		}
	}
	
	// Return true if target is dead
	// If target is out of range, check stance and determine
	// whether you should pursue and attack again
	public override bool Update () {
		foreach(Action action in actions) {
			if(!action.Update())
				return false;
		}
		return true;
	}
}
