using UnityEngine;
using System.Collections;

public class RotateTo : Action {
	private Transform rotater;
	private Transform target;
	bool lookedAt = false;

	public RotateTo(Transform target) {
		this.target = target;
	}

	public override void Start(GameObject g) {
		rotater = g.transform;
	}

	public override bool Update() {
		Vector3 forward = target.transform.position - rotater.transform.position;
		forward.y = 0.0f;
		forward.Normalize ();

		rotater.transform.forward = Vector3.Lerp (rotater.transform.forward, forward, Time.deltaTime * 10.0f);

		return Vector3.Dot(rotater.transform.forward, forward) > 0.9;
	}
}
