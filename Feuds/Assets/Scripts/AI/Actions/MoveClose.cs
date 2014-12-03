using UnityEngine;
using System.Collections;

public class MoveClose : Action {

	// Set the destination if it is not the target,
	// return true if the target has been reached
	public override bool Update() {
		Vector3 to = ac.position;

		if((ac.transform.position - to).sqrMagnitude > ac.myCombat.Radius * ac.myCombat.Radius) {
            ac.agent.stoppingDistance = ac.myCombat.Radius;
			ac.agent.SetDestination(to);
			return false;
		}
		else {
			return true;
		}
	}
}
