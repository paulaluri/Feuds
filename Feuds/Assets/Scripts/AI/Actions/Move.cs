using UnityEngine;
using System.Collections;

public class Move : Action {

	// Set the destination if it is not the target,
	// return true if the target has been reached
	public override bool Update() {
		Vector3 to = ac.position;
		float radius = 2.5f;

		if((ac.transform.position - to).sqrMagnitude >= radius*radius) {
			ac.agent.stoppingDistance = radius;
			ac.agent.SetDestination(to);
			return false;
		}
		else {
			return true;
		}
	}
}
