using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	private const int SCROLL_THRESH = 100;
	private const float SCROLL_SPEED = 1f;
	private const float MAX_SPEED = .5f;
	private float TIME_SCROLL = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mouse_pos = Input.mousePosition;

		float x_off = 0;
		float z_off = 0;

		if(mouse_pos.y < SCROLL_THRESH){
			x_off += -SCROLL_SPEED;
			z_off += -SCROLL_SPEED;
		}
		else if(mouse_pos.y > Screen.height - SCROLL_THRESH){
			x_off += SCROLL_SPEED;
			z_off += SCROLL_SPEED;
		}

		if(mouse_pos.x < SCROLL_THRESH){
			x_off += -SCROLL_SPEED;
			z_off += SCROLL_SPEED;
		}
		else if(mouse_pos.x > Screen.width - SCROLL_THRESH){
			x_off += SCROLL_SPEED;
			z_off += -SCROLL_SPEED;
		}

		if(x_off == 0 && z_off == 0){
			TIME_SCROLL = 0;
		}
		else
			TIME_SCROLL += Time.deltaTime;

		x_off *= TIME_SCROLL;
		z_off *= TIME_SCROLL;

		if(Mathf.Abs (x_off) > MAX_SPEED)
			x_off = (x_off / Mathf.Abs (x_off)) * MAX_SPEED;
		if(Mathf.Abs (z_off) > MAX_SPEED)
			z_off = (z_off / Mathf.Abs (z_off)) * MAX_SPEED;

		this.transform.position = this.transform.position + new Vector3(x_off, 0, z_off);
	}
}
