using UnityEngine;
using System.Collections;

public class nav_LOBBY : MonoBehaviour {
	public string SceneMain;
	public string SceneLounge;
	public GUIStyle menu_btn;
	public GUIStyle menu_text;
	public GUIStyle menu_selected;
	public GameObject GameManager;

	private int scene_lobby;
	private string gameName = "";
	private HostData selectedHost = null;
	private Vector2 scrollPosition = Vector2.zero;

	void Start() {
		MasterServer.RequestHostList ("Feuds");
	}


	// Update is called once per frame
	void OnGUI(){
		HostData[] hosts = MasterServer.PollHostList ();

		scrollPosition = GUI.BeginScrollView (new Rect(10,10,Screen.width-20,Screen.height-68),
		                                            scrollPosition,
		                                            new Rect(0,0,100,100));
		foreach(HostData host in hosts) {
			GUILayout.BeginHorizontal();
			if(selectedHost != null && selectedHost.guid == host.guid) {
				if(GUILayout.Button(host.gameName,menu_selected, GUILayout.Width(Screen.width-20))) {
					Join ();
				}
			}
			else {
				if(GUILayout.Button(host.gameName,menu_text, GUILayout.Width(Screen.width-20))) {
					selectedHost = host;
				}
			}
			GUILayout.EndHorizontal();
		}
		GUI.EndScrollView ();

		//Back
		if(GUI.Button(new Rect(Screen.width-182, Screen.height-58, 172, 48), "Back", menu_btn)) {
			Application.LoadLevel (SceneMain);
		}
		
		//Options
		if(GUI.Button(new Rect(10, Screen.height-58, 172, 48), "Host", menu_btn)) {
			Host ();
		}

		gameName = GUI.TextField (new Rect(192, Screen.height-58, Screen.width-(182*3+20), 48), gameName, menu_text);

		if(GUI.Button(new Rect(Screen.width-182*2, Screen.height-58, 172, 48), "Join", menu_btn) && selectedHost != null) {
			Join ();
		}
		

	}

	void Join() {
		Network.Connect (selectedHost);
		Application.LoadLevel (SceneLounge);
	}

	void Host() {
		Network.InitializeServer (1, 15466, !Network.HavePublicAddress ());
		Debug.Log ("calling");
		Network.Instantiate (GameManager, Vector3.zero, Quaternion.identity, 0);
		MasterServer.RegisterHost ("Feuds", gameName);
		Application.LoadLevel (SceneLounge);
	}
}
