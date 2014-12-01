using UnityEngine;
using System.Collections;

public class TestArcher : MonoBehaviour {
	Animator anim;
	public GameObject projectile;
	public GameObject spawnpoint;
	private GameObject g;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}

	public void LoadArrow(){
		g = (GameObject)GameObject.Instantiate(projectile, Vector3.zero, Quaternion.identity);
		g.transform.parent = spawnpoint.transform;
		g.transform.localPosition = Vector3.zero;
		g.transform.localRotation = Quaternion.identity;
	}

	public void Shoot(){
		g.transform.parent = null;
		g.GetComponent<TestFire>().Fire ();
	}
}
