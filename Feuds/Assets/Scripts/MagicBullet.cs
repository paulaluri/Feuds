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
	
	// Update is called once per frame
	void Update () {
		if(isFiring){
			this.transform.position = this.transform.position - this.transform.right*speed;
		}

		if(Time.realtimeSinceStartup - initTime > 10)
			GameObject.Destroy (this.gameObject);
	}

	public void Fire(){
		isFiring = true;
	}

	void OnTriggerEnter(Collider c){
		Debug.Log ("Test");
		isFiring = false;

		this.transform.parent = c.transform;
	}
}
