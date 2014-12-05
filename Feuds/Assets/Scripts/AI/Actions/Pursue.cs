using UnityEngine;
using System.Collections;

public class Pursue : Action {

	// Update the destination if it is further away from the target than
	// the stopping distance, return true if you have arrived at the target
	public override bool Update () {
		if(!ac.targetCombat.isDead) {
			Vector3 to = ac.targetCombat.transform.position;
			float radius = ac.myCombat.Radius * 0.8f;
			
			if((ac.transform.position - to).sqrMagnitude >= radius*radius) {
				ac.agent.stoppingDistance = radius;
				ac.agent.SetDestination(to);
				return false;
			}
			else {
				return true;
			}
		}
		return true;
	}
}
