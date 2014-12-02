using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour {
	Animator anim;
	public GameObject projectile;
	public GameObject spawnpoint;
	public float time;
	bool fired = false;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(fired && anim.GetCurrentAnimatorStateInfo(0).normalizedTime%1 < time)
			fired = false;

		if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime%1 > time && !fired){
			fired = true;
			GameObject g = (GameObject)GameObject.Instantiate(projectile, Vector3.zero, Quaternion.identity);
			g.transform.parent = spawnpoint.transform;
			g.transform.localPosition = Vector3.zero;
			g.transform.localRotation = Quaternion.identity;
			g.transform.parent = null;
			g.GetComponent<MagicBullet>().Fire ();
		}
	}
}
