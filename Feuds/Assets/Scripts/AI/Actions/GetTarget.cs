using UnityEngine;
using System.Collections;

public class GetTarget : Action {

	// Update is called once per frame
	public override bool Update () {
		Vector3 position = ac.CurrentStance == Stance.Aggressive ? ac.transform.position : ac.position;
		Collider[] enemies = Physics.OverlapSphere(position,UIFogOfWar.visionLength,ac.attackables);
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
