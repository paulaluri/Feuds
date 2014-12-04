using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
    public float heightDistanceRatio;
    float timer;
    bool isFired = false;
	// Use this for initialization
	void Start () {
		//rigidbody.centerOfMass = new Vector3(-.2f, 0, 0);
        timer = -1;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
        {
            if (Time.time - timer > 2)
            {
                Destroy(this.gameObject);
            }
        }
	}

	void FixedUpdate(){
        if (rigidbody == null) return;
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
		Vector3 direction = (t - init.position).normalized;

		rigidbody.velocity = new Vector3(direction.x*hSpeed, vSpeed, direction.z*hSpeed);
        //this.gameObject.layer = init.gameObject.layer;
        timer = Time.time;
        isFired = true;
	}

    void OnTriggerEnter(Collider collider)
    {

        //print(collider.gameObject.name + " " + collider.gameObject.layer + "[]" + gameObject.name + " " + gameObject.layer);
        if (!isFired || rigidbody == null) return;
        rigidbody.velocity = new Vector3(0,0,0);
        Destroy(rigidbody);
        gameObject.transform.parent = collider.gameObject.transform;
    }
}
