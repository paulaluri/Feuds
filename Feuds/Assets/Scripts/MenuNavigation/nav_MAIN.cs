﻿using UnityEngine;
using System.Collections;

public class nav_MAIN : MonoBehaviour {
	public string SceneLobby;
	public string SceneAbout;

	public Texture logo;
	public GUIStyle menu_btn;
	public GUIStyle credit_style;

	private int scene_lobby;

	void Start(){
		//Auto scale to resolution
		Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
	}

	// Update is called once per frame
	void OnGUI(){
		//Logo
		GUI.DrawTexture (new Rect(Screen.width/2 - 125, 100, 250, 100), logo);

		//Options
		//GUI.Button(new Rect(Screen.width/2 - 86, 250, 172, 48), "Single Player", menu_btn);
		GUI.BeginGroup (new Rect(0, Mathf.Max (Screen.height / 2 - (48), 210), Screen.width, (2*48))); 

		if(GUI.Button(new Rect(Screen.width/2 - 86, 0, 172, 48), "Multiplayer", menu_btn))
			Application.LoadLevel(SceneLobby);
		//if (GUI.Button (new Rect (Screen.width / 2 - 86, 48, 172, 48), "Settings", menu_btn))
			//Application.LoadLevel (SceneAbout);
		if (GUI.Button (new Rect (Screen.width / 2 - 86, 48, 172, 48), "About", menu_btn))
			Application.LoadLevel (SceneAbout);

		GUI.EndGroup ();

		//Credits
		GUI.Label (new Rect(10, Screen.height-50,Screen.width-20, 30), "Created by Paul Aluri, Pasan Julsaksrisakul, and Jacob Slone", credit_style);
	}
}
