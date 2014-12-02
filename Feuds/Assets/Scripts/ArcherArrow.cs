using UnityEngine;
using System.Collections;

public class ArcherArrow : MonoBehaviour {
	Animator anim;
	public GameObject projectile;
	public GameObject spawnpoint;
	private GameObject g;
	private GameObject target;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}

	public void LoadArrow(){
		g = (GameObject)GameObject.Instantiate(projectile, Vector3.zero, Quaternion.Euler (0, -90, 0));
		g.transform.parent = spawnpoint.transform;
		g.transform.localPosition = Vector3.zero;
		g.transform.localRotation = Quaternion.Euler (0, -90, 0);
        target = gameObject.GetComponent<ActionController>().targetCombat.gameObject;
	}

	public void Shoot(){
		g.transform.parent = null;
		g.GetComponent<Arrow>().Fire (this.transform, target.transform);
	}
}
