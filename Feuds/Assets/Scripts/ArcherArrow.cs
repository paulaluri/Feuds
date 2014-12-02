using UnityEngine;
using System.Collections;

public class ArcherArrow : MonoBehaviour {
	public GameObject projectile;
	public GameObject spawnpoint;
	private GameObject g;
	private Vector3 target;
	// Use this for initialization
	void Start () {
	}

	public void LoadArrow(){
		g = (GameObject)GameObject.Instantiate(projectile, spawnpoint.transform.position, spawnpoint.transform.rotation);
		g.transform.parent = spawnpoint.transform;
		//g.transform.localPosition = Vector3.zero;
		//g.transform.localRotation = Quaternion.Euler (0, 0, 0);
		CombatController opp = gameObject.GetComponent<ActionController> ().targetCombat;
		target = this.gameObject.GetComponent<AnimationUpdater>().targetPos;
	}

	public void Shoot(){
		if (target != null) {
			g.transform.parent = null;
			g.GetComponent<Arrow> ().Fire (this.transform, target);
		}
	}
}
