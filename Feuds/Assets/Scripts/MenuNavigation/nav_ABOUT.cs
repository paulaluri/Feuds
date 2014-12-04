using UnityEngine;
using System.Collections;

public class nav_ABOUT : MonoBehaviour {
	public string SceneMain;

	public Texture logo;
	public GUIStyle menu_btn;
	public GUIStyle text_style;

	private int scene_lobby;

	private string explain = "In Feuds, you are locked in a never-ending war with another feudal lord.  In one game, you play six rounds, switching off between attacking and defending the hillside town...blah blah blah";
	private string text = "Feuds is an RTS game created by Carnegie Mellon University Computer Science students Paul Aluri, Pasan Julsaksrisakul, and Jacob Slone for the Fall 2014 iteration of CS-466/666 Computer Game Programming.  This game demonstrates behavior trees, networking, and overall software design knowledge.";

	// Update is called once per frame
	void OnGUI(){

		if(GUI.Button(new Rect(Screen.width-182, Screen.height-58, 172, 48), "Back", menu_btn))
			Application.LoadLevel (SceneMain);

		GUI.BeginGroup (new Rect(200, 200, Screen.width-400, 600)); 
			
		GUI.Label (new Rect (0, 0, Screen.width - 400, 600), explain + " " + text, text_style);

		GUI.EndGroup ();
	}
}
