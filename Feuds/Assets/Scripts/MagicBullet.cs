using UnityEngine;
using System.Collections;

public class MagicBullet : MonoBehaviour {
	public float speed = 1f;
	private GameObject shooter;
	private Vector3 origin = Vector3.zero;
	// Use this for initialization
	void Start () {
		ParticleSystem p = this.GetComponentInChildren<ParticleSystem>();
		if(p){
			p.Simulate (10);
			p.Play ();
		}
	}

	public void Fire (Transform init, Vector3 t) {
		this.transform.forward = (t - init.position).normalized;
		rigidbody.velocity = this.transform.forward * 10f;
		origin = init.position;
		shooter = init.gameObject;
	}

	void Update(){
		if (Vector3.Distance (this.transform.position, origin) > shooter.GetComponent<CombatController> ().Radius)
			GameObject.Destroy (this.gameObject);
	}
}
