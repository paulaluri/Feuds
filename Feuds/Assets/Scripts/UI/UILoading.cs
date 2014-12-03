using UnityEngine;
using System.Collections;

public class UILoading : MonoBehaviour {
	public GameObject Cameras;
	public GameObject UIObject;
	public GUIStyle loadingStyle;

	private const string LOADING = "Waiting for other player...";
	private const float DELTA = .5f;
	private float alpha = 0;
	private bool ascend = true;

	// Update is called once per frame
	void Update () {

		//If game is ready, hide this screen and flip on other camera
		if(GameManager.ready){
			Cameras.SetActive(true);
			UIObject.SetActive(true);
			this.gameObject.SetActive(false);
		}
	}

	void OnGUI(){
		if(ascend){
			alpha += DELTA * Time.deltaTime;

			if(alpha > 1){
				ascend = false;
			}
		}
		else{
			alpha -= DELTA * Time.deltaTime;
			
			if(alpha <= 0.05f){
				ascend = true;
			}
		}


		GUIStyle shadow_style = new GUIStyle (loadingStyle);
		Color b = Color.black;
		Color c = loadingStyle.normal.textColor;
		shadow_style.normal.textColor = new Color(b.r, b.g, b.b, alpha);
		loadingStyle.normal.textColor = new Color(c.r, c.g, c.b, alpha);

		//Outline
		GUI.Label (new Rect(-1, -1, Screen.width, Screen.height), LOADING, shadow_style);
		GUI.Label (new Rect(-1, 1, Screen.width, Screen.height), LOADING, shadow_style);
		GUI.Label (new Rect(1, -1, Screen.width, Screen.height), LOADING, shadow_style);
		GUI.Label (new Rect(1, 1, Screen.width, Screen.height), LOADING, shadow_style);

		//Main
		GUI.Label (new Rect(0, 0, Screen.width, Screen.height), LOADING, loadingStyle);
	}
}
