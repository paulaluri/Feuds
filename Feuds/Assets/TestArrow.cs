using UnityEngine;
using System.Collections;

public class TestArrow : MonoBehaviour {

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

	public void Fire(Vector3 init, Vector3 target){
		//this.rigidbody.velocity = this.transform.forward*20;
		float maxDistance = Vector3.Distance(init, target);//
		float maxHeight = maxDistance > 8f?2f:.5f;//

		float g = Physics.gravity.magnitude; // get the gravity value
		float vSpeed = Mathf.Sqrt(2 * g * maxHeight); // calculate the vertical speed
		float totalTime = 2 * vSpeed / g; // calculate the total time
		float hSpeed = maxDistance / totalTime; // calculate the horizontal speed
		rigidbody.velocity = new Vector3(0, vSpeed, -hSpeed); 
	}
}
