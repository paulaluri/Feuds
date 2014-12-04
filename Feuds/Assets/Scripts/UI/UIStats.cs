using UnityEngine;
using System.Collections;

public class UIStats : MonoBehaviour {
	public Texture check;
	public Texture p1Track;
	public Texture p1End;
	public Texture p2Track;
	public Texture p2End;
	public GUIStyle menu_btn;
	public GUIStyle text_style;

	public string SceneLounge;
	public string SceneLobby;

	private const int BAR_HEIGHT = 64;
	private const int END_WIDTH = 32;

	// Use this for initialization
	void Start () {
		p2Track.wrapMode = TextureWrapMode.Repeat;
		p1Track.wrapMode = TextureWrapMode.Repeat;
		p2End.wrapMode = TextureWrapMode.Repeat;
		p1End.wrapMode = TextureWrapMode.Repeat;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		//Continue OR MAIN MENU if game done
		string scene;
		string text;
		bool endOfGame = GameManager.Rounds.current == GameManager.Rounds.max ||
						GameManager.wins [GameManager.winner] > GameManager.Rounds.max / 2;

		if(endOfGame) {
			scene = SceneLobby;
			text = "Exit";
		}
		else {
			scene = SceneLounge;
			text = "Continue";
		}
		if(GUI.Button(new Rect(Screen.width-182, Screen.height-58, 172, 48), text, menu_btn)) {
			Application.LoadLevel(scene);
		}

		if (!endOfGame) {
			//Feud Title Bar
			DrawText (new Rect (0, 10, Screen.width, 70), GameManager.winner == GameManager.player ? "Victory" : "Defeat", text_style, true, 50, Color.white);

			//Feud Balance bar
			float playerWins = (float)GameManager.wins [GameManager.player];
			float oppWins = (float)GameManager.wins [GameManager.other];
			float playerWinPercentage = playerWins / (playerWins + oppWins);
			DrawBar (playerWinPercentage);
		}
		else {
			//Feud Title Bar
			GUIStyle endOfGameStyle = new GUIStyle(text_style);
			endOfGameStyle.fontSize = 60;
			bool victor = GameManager.winner == GameManager.player;
			DrawText (new Rect (0, 10, Screen.width, 130), victor? "Victory" : "Defeat", endOfGameStyle, true, 50, victor?(new Color(1,216f/255f,0)):Color.white);
		}

		//Feud Rounds
		GUI.BeginGroup(new Rect(Screen.width/2 - 200, 180, 400, 400), text_style);

		DrawText(new Rect(0, 0, 100, 60), "Round", text_style, true, 30, Color.white);
		DrawText(new Rect(100, 0, 150, 60), "Your Forces", text_style, true, 30, Color.white);
		DrawText(new Rect(250, 0, 150, 60), "The Enemy", text_style, true, 30, Color.white);

		int latest = (int)GameManager.Rounds.current-1;
		for(int i = 0; i < (int)GameManager.Rounds.max; i++){
			DrawText(new Rect(0, (i+1)*50, 100, 60), (i+1).ToString(), text_style, true, 30, latest==i?(new Color(255f/255f, 216f/255f, 0)):Color.white);
			int x_pos = 150;

			if(GameManager.winners[i] == GameManager.other)
				x_pos = 300;

			if(i <= latest)
				GUI.DrawTexture(new Rect(x_pos,(i+1)*50, 50, 50), check);
		}

		GUI.EndGroup();
	}

	void DrawText(Rect r, string s, GUIStyle g, bool outline, int size, Color c){
		GUIStyle cp = new GUIStyle(g);
		cp.fontSize = size;
		cp.normal.textColor = c;

		if(outline){
			Rect br = new Rect(r.x-1, r.y-1, r.width, r.height);
			Rect ur = new Rect(r.x-1, r.y+1, r.width, r.height);
			Rect bl = new Rect(r.x+1, r.y-1, r.width, r.height);
			Rect ul = new Rect(r.x+1, r.y+1, r.width, r.height);

			GUIStyle shadow = new GUIStyle(cp);
			shadow.normal.textColor = Color.black;

			GUI.Label (br, s, shadow);
			GUI.Label (ur, s, shadow);
			GUI.Label (bl, s, shadow);
			GUI.Label (ul, s, shadow);
		}

		GUI.Label (r, s, cp);
	}

	void DrawBar(float val){
		float bar_width =  Screen.width - 100;
		float width_to_draw = val*bar_width;
		float final_width = width_to_draw;

		GUI.BeginGroup(new Rect(50, 100, bar_width, BAR_HEIGHT));

		//Draw Bottom
		GUI.DrawTexture (new Rect(0, 0, END_WIDTH*2, BAR_HEIGHT), p2End);
		GUI.DrawTexture (new Rect(bar_width-END_WIDTH*2, 0, END_WIDTH*2, BAR_HEIGHT), p2End);
		GUI.DrawTextureWithTexCoords(new Rect(END_WIDTH, 0, bar_width-END_WIDTH*2, BAR_HEIGHT), p2Track, new Rect(0, 0, bar_width / p1Track.width, 1));

		//Draw Top
		if(width_to_draw > 0){
			if(width_to_draw > END_WIDTH)
				GUI.DrawTextureWithTexCoords(new Rect(0, 0, END_WIDTH, BAR_HEIGHT), p1End, new Rect(0, 0, .5f, 1));
			else
				GUI.DrawTextureWithTexCoords(new Rect(0, 0, width_to_draw, BAR_HEIGHT), p1End, new Rect(0, 0, width_to_draw/END_WIDTH*2, 1));
			width_to_draw -= END_WIDTH;
		}

		if(END_WIDTH > (bar_width-final_width)){
			GUI.DrawTextureWithTexCoords(new Rect(bar_width-END_WIDTH*2, 0, END_WIDTH*2-(bar_width-final_width), BAR_HEIGHT), p1End, new Rect(0, 0, (END_WIDTH*2-(bar_width-final_width))/(END_WIDTH*2), 1));
		}

		if(width_to_draw > 0){
			float track_width = Mathf.Min (width_to_draw, bar_width - END_WIDTH*2);
			GUI.DrawTextureWithTexCoords(new Rect(END_WIDTH, 0, track_width, BAR_HEIGHT), p1Track, new Rect(0, 0, .1f, 1));

			width_to_draw -= track_width;
		}

		GUI.EndGroup ();
	}
}
