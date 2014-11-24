using UnityEngine;
using System.Collections;

public enum Menu{
	Main,
	Multiplayer,
	Lounge
}

public class ui_mainmenu : MonoBehaviour {
	public Texture logo;
	public GUIStyle menu_btn;
	public GUIStyle guard_btn;
	public GUIStyle wizard_btn;
	public GUIStyle archer_btn;
	public GUIStyle credit_style;
	public GUIStyle menu_text;

	public Texture guard;
	public Texture wizard;
	public Texture archer;

	private Menu current = Menu.Main;
	private string ip = "127.0.0.1";

	// Update is called once per frame
	void OnGUI(){

		if(current == Menu.Main){
			//Logo
			GUI.DrawTexture (new Rect(Screen.width/2 - 125, 100, 250, 100), logo);

			//Options
			GUI.Button(new Rect(Screen.width/2 - 86, 250, 172, 48), "Single Player", menu_btn);
			if(GUI.Button(new Rect(Screen.width/2 - 86, 298, 172, 48), "Multiplayer", menu_btn))
				current = Menu.Multiplayer;
			GUI.Button(new Rect(Screen.width/2 - 86, 346, 172, 48), "About", menu_btn);

			//Credits
			GUI.Label (new Rect(10, Screen.height-50,Screen.width-20, 30), "Created by Paul Aluri, Pasan Julsaksrisakul, and Jacob Slone", credit_style);
		}
		else if(current == Menu.Multiplayer){
			//Logo
			GUI.DrawTexture (new Rect(10, 10, 250, 100), logo);

			//Back
			if(GUI.Button(new Rect(Screen.width-182, 10, 172, 48), "Back", menu_btn))
				current = Menu.Main;

			//Options
			if(GUI.Button(new Rect(30, 120, 172, 48), "Host", menu_btn))
				current = Menu.Lounge;
			GUI.Button(new Rect(202, 120, 172, 48), "Join", menu_btn);

			ip = GUI.TextField (new Rect(374, 120, 172, 48), ip, menu_text);
		}
		else if(current == Menu.Lounge){
			//Logo
			GUI.DrawTexture (new Rect(10, 10, 250, 100), logo);
			
			//Back
			if(GUI.Button(new Rect(Screen.width-182, 10, 172, 48), "Back", menu_btn))
				current = Menu.Main;

			//Soldiers
			GUI.Button(new Rect(400, 120, 64, 64), "", guard_btn);
			GUI.Button(new Rect(464, 120, 64, 64), "", wizard_btn);
			GUI.Button(new Rect(528, 120, 64, 64), "", archer_btn);
		}
	}
}
