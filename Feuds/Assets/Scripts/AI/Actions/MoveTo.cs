using UnityEngine;
using System.Collections;

public class MoveTo : Action {
	private NavMeshAgent agent;
	private Vector3 target;

	public MoveTo(Vector3 pos) {
		target = pos;
	}
	
	public override void Start(GameObject g) {
		agent = g.GetComponent<NavMeshAgent> ();

	}

	// Set the destination if it is not the target,
	// return true if the target has been reached
	public override bool Update() {
		if(agent && (agent.destination - target).magnitude > agent.stoppingDistance) {
			agent.SetDestination(target);
		}
		return agent && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
	}
}
