using UnityEngine;
using System.Collections;

public class Attack : Action {

	public override bool Update() {
		CombatController attacker = ac.myCombat;
		CombatController target = ac.targetCombat;

		if(attacker.CanAttack(target)) {
			attacker.inCombat = true;
			attacker.attackedThisFrame = true;
			attacker.DoDamage(target);
		}
		return target.isDead;
	}
}
