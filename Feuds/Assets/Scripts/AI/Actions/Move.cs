using UnityEngine;
using System.Collections;

public class Move : Action {

	// Set the destination if it is not the target,
	// return true if the target has been reached
	public override bool Update() {
		ac.agent.stoppingDistance = 0.5f;
		ac.agent.SetDestination (ac.position);
		return ac.agent.remainingDistance <= ac.agent.stoppingDistance;
	}
}
