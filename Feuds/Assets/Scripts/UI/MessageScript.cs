using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MessageState{
	FadeIn,
	Normal,
	FadeOut,
	Dequeue
}

public class MessageItem{
	public string Text{get;set;}
	public float TimeStamp{get;set;} //initial time created (used for duration check)
	public float LifeSpan{get;set;} //seconds
	public float Alpha = 0; //0 to 1
	public bool Important{get{return important;}} //Uses important color if set
	public MessageState State{get{return state;}}

	private MessageState state = MessageState.FadeIn;
	private const float FADE_INCR = 0.025f;
	private bool important = false;

	//Constructor for message
	public MessageItem(string text, float lifespan, bool import){
		Text = text;
		TimeStamp = Time.realtimeSinceStartup;
		LifeSpan = lifespan;
		important = import;
	}

	//Hard switch for killing messages
	//Used primarily when message list exceeds capacity.
	public void Kill(){
		state = MessageState.FadeOut;
	}

	//Acts like a state machine.
	//Based on the current state, updates alpha
	//and maybe changes state.
	public void Refresh(){
		if(state == MessageState.FadeIn){
			if(Alpha < 1)
				Alpha += FADE_INCR;
			else
				state = MessageState.Normal;
		}
		else if(state == MessageState.Normal){
			if(Time.realtimeSinceStartup - TimeStamp >= LifeSpan)
				state = MessageState.FadeOut;
		}
		else if(state == MessageState.FadeOut){
			if(Alpha > 0)
				Alpha -= FADE_INCR;
			else
				state = MessageState.Dequeue;
		}
	}	
}

public class MessageScript : MonoBehaviour {
	public GUIStyle Style;
	public float Duration = 10; //seconds
	public int Capacity = 5; //# of messages
	public Color MainColor;
	public Color ImportantColor;
	public Color OutlineColor;
	public bool Outline = false;

	private const float X_PADDING = 20;  //pixels
	private const float Y_PADDING = 60;  //pixels

	private float line_height = 0;
	private List<MessageItem> messages = new List<MessageItem>();

	//Overloads Add by simplifying. Default duration is used, and important flag cleared.
	public void Add(string text){
		Add (text, Duration, false);
	}

	//Allows for full customization of message duration and importance.
	public void Add(string text, float duration, bool important){
		MessageItem m = new MessageItem(text, duration, important);
		messages.Add(m);
		
		int overage = messages.Count - Capacity;
		
		if(overage > 0){
			for(int i = 0; i < overage; i++){
				messages[i].Kill();
			}
		}
	}

	void Awake(){
		//Initiate height
		Vector2 label_size = Style.CalcSize (new GUIContent("Calculate height"));
		line_height = label_size.y;
	}
	
	void OnGUI () {
		float fading_offset = 0; //to allow items to slide up as items fade out

		// Calculate "slide up" amount based on percentage of fadeouts
		for(int i = 0; i < messages.Count; i++){
			if(messages[i].State == MessageState.FadeOut)
				fading_offset += (1-messages[i].Alpha);
		}

		// Draw all messages in list
		for(int i = 0; i < messages.Count; i++){
			MessageItem m = messages[i];
			GUIStyle alpha_style = new GUIStyle(Style);
			Color c = m.Important?ImportantColor:MainColor; //Check if important flag is set
			alpha_style.normal.textColor = new Color(c.r, c.g, c.b, m.Alpha);

			//If outline too
			if(Outline){
				GUIStyle shadow_style = new GUIStyle(alpha_style);
				Color s = OutlineColor;
				shadow_style.normal.textColor = new Color(s.r, s.g, s.b, m.Alpha);

				GUI.Label (new Rect(-1+X_PADDING, -1+Y_PADDING + line_height*i - (fading_offset)*line_height, Screen.width - X_PADDING, line_height), m.Text, shadow_style);
				GUI.Label (new Rect(1+X_PADDING, -1+Y_PADDING + line_height*i - (fading_offset)*line_height, Screen.width - X_PADDING, line_height), m.Text, shadow_style);
				GUI.Label (new Rect(-1+X_PADDING, 1+Y_PADDING + line_height*i - (fading_offset)*line_height, Screen.width - X_PADDING, line_height), m.Text, shadow_style);
				GUI.Label (new Rect(1+X_PADDING, 1+Y_PADDING + line_height*i - (fading_offset)*line_height, Screen.width - X_PADDING, line_height), m.Text, shadow_style);
			}

			GUI.Label (new Rect(X_PADDING, Y_PADDING + line_height*i - (fading_offset)*line_height, Screen.width - X_PADDING, line_height), m.Text, alpha_style);

			//CRUCIAL - updates message state
			m.Refresh();
		}

		//Remove message if its state is "Dequeue"
		for(int i = messages.Count-1; i >= 0; i--){
			if(messages[i].State == MessageState.Dequeue)
				messages.RemoveAt (i);
		}
	}
}
