using UnityEngine;
using System.Collections;

public class AttackDirect : Action {
	CombatController attacker;
	CombatController target;

	public AttackDirect(CombatController target) {
		this.target = target;
	}

	public override void Start(GameObject g) {
		attacker = g.GetComponent<CombatController> ();
	}

	public override bool Update() {
		if(attacker.CanAttack(target)) {
			attacker.DoDamage(target);
		}
		return target.isDead;
	}
}
