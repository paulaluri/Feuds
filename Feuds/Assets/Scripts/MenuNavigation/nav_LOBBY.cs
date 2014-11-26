using UnityEngine;
using System.Collections;

public class nav_LOBBY : MonoBehaviour {
	public string SceneMain;
	public string SceneLounge;
	public GUIStyle menu_btn;
	public GUIStyle menu_text;

	private int scene_lobby;
	private string ip = "127.0.0.1";


	// Update is called once per frame
	void OnGUI(){
		//Back
		if(GUI.Button(new Rect(Screen.width-182, 10, 172, 48), "Back", menu_btn))
			Application.LoadLevel (SceneMain);
		
		//Options
		if(GUI.Button(new Rect(10, 10, 172, 48), "Host", menu_btn))
			Application.LoadLevel (SceneLounge);
		GUI.Button(new Rect(182, 10, 172, 48), "Join", menu_btn);
		
		ip = GUI.TextField (new Rect(354, 10, 172, 48), ip, menu_text);
	}
}
