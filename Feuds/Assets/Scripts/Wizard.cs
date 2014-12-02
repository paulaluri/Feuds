using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour {
	public GameObject projectile;
	public GameObject spawnpoint;
	// Use this for initialization
	void Start () {
	}

	public void Shoot () {
		if (this.gameObject.GetComponent<AnimationUpdater>().targetPos != null) {
			GameObject g = (GameObject)GameObject.Instantiate(projectile, spawnpoint, Quaternion.identity);
			g.transform.parent = null;
			g.GetComponent<MagicBullet> ().Fire (this.transform, this.gameObject.GetComponent<AnimationUpdater>().targetPos);
		}
	}
}
