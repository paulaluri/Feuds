using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
    public float heightDistanceRatio;
	// Use this for initialization
	void Start () {
		//rigidbody.centerOfMass = new Vector3(-.2f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if(rigidbody.velocity != Vector3.zero)
			rigidbody.rotation = Quaternion.LookRotation(rigidbody.velocity);  
	}

	public void Fire(Transform init, Vector3 t){

        Vector3 target = t;
		float maxDistance = Vector3.Distance(init.position, target);//
        float maxHeight = maxDistance / heightDistanceRatio;

		float g = Physics.gravity.magnitude; // get the gravity value
		float vSpeed = Mathf.Sqrt(2 * g * maxHeight); // calculate the vertical speed
		float totalTime = 2 * vSpeed / g; // calculate the total time
		float hSpeed = maxDistance / totalTime; // calculate the horizontal speed
		Vector3 direction = (t.position - init.position).normalized;

		rigidbody.velocity = new Vector3(direction.x*hSpeed, vSpeed, direction.z*hSpeed); 
	}
}
