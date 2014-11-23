using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	private const int SCROLL_THRESH = 20;
	private const float SCROLL_SPEED = 1f;
	private const float MAX_SPEED = .5f;
	private const float BOTTOM_MARGIN = 192;
	
	private const float leftMost = -100;
	private const float rightMost = 100;
	private const float topMost = -70;
	private const float bottomMost = 30;
	
	private float TIME_SCROLL = 0;

	public float zoomSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mouse_pos = Input.mousePosition;
		
		float height = Screen.height-BOTTOM_MARGIN;

		float x_off = 0;
		float z_off = 0;

		if(mouse_pos.y < SCROLL_THRESH + BOTTOM_MARGIN && mouse_pos.y >= BOTTOM_MARGIN){
			x_off += SCROLL_SPEED;
			z_off += -SCROLL_SPEED;
		}
		else if(mouse_pos.y > Screen.height - SCROLL_THRESH){
			x_off += -SCROLL_SPEED;
			z_off += SCROLL_SPEED;
		}

		if(mouse_pos.x < SCROLL_THRESH){
			x_off += -SCROLL_SPEED;
			z_off += -SCROLL_SPEED;
		}
		else if(mouse_pos.x > Screen.width - SCROLL_THRESH){
			x_off += SCROLL_SPEED;
			z_off += SCROLL_SPEED;
		}

		if(x_off == 0 && z_off == 0){
			TIME_SCROLL = 0;
		}
		else
			TIME_SCROLL += Time.deltaTime;

		if(mouse_pos.y < BOTTOM_MARGIN){
			x_off = 0;
			z_off = 0;
		}

		x_off *= TIME_SCROLL;
		z_off *= TIME_SCROLL;

		if(Mathf.Abs (x_off) > MAX_SPEED)
			x_off = (x_off / Mathf.Abs (x_off)) * MAX_SPEED;
		if(Mathf.Abs (z_off) > MAX_SPEED)
			z_off = (z_off / Mathf.Abs (z_off)) * MAX_SPEED;

		this.transform.position = this.transform.position + new Vector3(x_off, 0, z_off);

		//cameraLimit();
		//print (transform.position);
		

		float cam_size = Camera.main.orthographicSize-Input.GetAxis("Mouse ScrollWheel")*zoomSpeed;

		if (cam_size > 30)
			cam_size = 30;
		else if (cam_size < 5)
			cam_size = 5;

		Camera.main.orthographicSize = cam_size;
	}
	
	void cameraLimit(){
		if(transform.position.x < leftMost)transform.position = new Vector3(leftMost,transform.position.y,transform.position.z);
		if(transform.position.x > rightMost)transform.position = new Vector3(rightMost,transform.position.y,transform.position.z);
		if(transform.position.z > bottomMost)transform.position = new Vector3(transform.position.x,transform.position.y,bottomMost);
		if(transform.position.z < topMost)transform.position = new Vector3(transform.position.x,transform.position.y,topMost);
	}
}
