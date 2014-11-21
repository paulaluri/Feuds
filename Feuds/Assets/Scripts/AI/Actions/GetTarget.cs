using UnityEngine;
using System.Collections;

public class GetTarget : Action {

	// Update is called once per frame
	public override bool Update () {
		Collider[] enemies = Physics.OverlapSphere(ac.position,30.0f,ac.attackables);
		if(enemies.Length > 0) {
			float d = float.MaxValue;
			GameObject closest = null;
			foreach(Collider enemy in enemies) {
				float nd = (ac.transform.position - enemy.transform.position).magnitude;
				if(nd < d) {
					d = nd;
					closest = enemy.gameObject;
				}
			}
			ac.targetCombat = closest.GetComponent<CombatController>();
			return true;
		}
		return false;
	}
}
