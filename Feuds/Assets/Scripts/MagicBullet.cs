using UnityEngine;
using System.Collections;

public class MagicBullet : MonoBehaviour {
	private bool isFiring = false;
	private float initTime = 0;
	public float speed = 1f;
	// Use this for initialization
	void Start () {
		initTime = Time.realtimeSinceStartup;
		ParticleSystem p = this.GetComponentInChildren<ParticleSystem>();
		if(p){
			p.Simulate (10);
			p.Play ();
		}
	}

	public void Fire (Transform init, Vector3 t) {
		this.transform.forward = (t - init.position).normalized;
		rigidbody.velocity = this.transform.forward * 10f;
	}
}
