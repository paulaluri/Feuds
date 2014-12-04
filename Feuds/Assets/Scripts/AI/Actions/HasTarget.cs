using UnityEngine;
using System.Collections;

public class HasTarget : Action {

	// Update is called once per frame
	public override bool Update () {
		return ac.targetCombat && !ac.targetCombat.isDead;
	}
}
