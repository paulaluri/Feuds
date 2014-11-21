using UnityEngine;
using System.Collections;

public class Rotate : Action {

	public override bool Update() {
		if(!ac.targetCombat.isDead) {
			Vector3 forward = ac.targetCombat.transform.position - ac.transform.position;
			forward.y = 0.0f;
			forward.Normalize ();

			ac.transform.forward = Vector3.Lerp (ac.transform.forward, forward, Time.deltaTime * 10.0f);

			return Vector3.Dot(ac.transform.forward, forward) > 0.9;
		}
		return true;
	}
}
