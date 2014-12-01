using UnityEngine;
using System.Collections;

public class TestFire : MonoBehaviour {
	private bool isFiring = false;
	private float initTime = 0;
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
			this.transform.position = this.transform.position - this.transform.right*.08f;
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
        print(c.name);
		this.transform.parent = c.transform;
	}
}
