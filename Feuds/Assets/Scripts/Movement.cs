using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	//This is the class for handling basic movement for characters
	//To simplify movement, the character will automatically face where it want to go
	
	public float maxSpeed;
	public float maxAcceleration;
	
	public Vector3 currVelo;
	
	public Vector3 target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setTarget(Vector3 t){
		target = t;
	}
}
