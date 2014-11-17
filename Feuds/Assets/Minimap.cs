using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && camera.pixelRect.Contains(Input.mousePosition)){
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			Debug.Log("Step2");
			if (Physics.Raycast(ray, out hit)/* && hit.transform.name=="MinimapBackground"*/){
				//Camera.main.transform.position = hit.point;
				Debug.Log("Step3");
				// hit.point contains the point where the ray hits the
				// object named "MinimapBackground"
				Debug.Log(hit.point);
			}
		}
	}
}
