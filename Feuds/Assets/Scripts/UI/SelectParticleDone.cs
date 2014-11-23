using UnityEngine;
using System.Collections;

public class SelectParticleDone : MonoBehaviour {
	private const float DURATION = 1.5f;
	private float start_time = 0;

	// Use this for initialization
	void Start () {
		start_time = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.realtimeSinceStartup - start_time > DURATION){
			GameObject.Destroy (this.gameObject);
		}
	}
}
