using UnityEngine;
using System.Collections;

public class Pursue : Action {
	Transform target;
	NavMeshAgent agent;

	public Pursue(Transform t) {
		target = t;
	}

	// Use this for initialization
	public override void Start (GameObject g) {
		agent = g.GetComponent<NavMeshAgent> ();
	}
	
	// Update the destination if it is further away from the target than
	// the stopping distance, return true if you have arrived at the target
	public override bool Update () {
		if(agent && (agent.destination - target.position).magnitude > agent.stoppingDistance) {
			agent.SetDestination(target.position);
		}
		return agent && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
	}
}
