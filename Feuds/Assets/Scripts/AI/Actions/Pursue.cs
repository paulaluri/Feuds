using UnityEngine;
using System.Collections;

public class Pursue : Action {
	
	// Update the destination if it is further away from the target than
	// the stopping distance, return true if you have arrived at the target
	public override bool Update () {
		if(!ac.targetCombat.isDead) {
			ac.agent.stoppingDistance = ac.myCombat.Radius * 0.8f;
			ac.agent.SetDestination (ac.targetCombat.transform.position);
			if(ac.CurrentStance == Stance.Aggressive || ac.CurrentCommand == Command.Attack) {
				ac.position = ac.transform.position;
			}
			return ac.agent.remainingDistance <= ac.agent.stoppingDistance;
		}
		return true;
	}
}
