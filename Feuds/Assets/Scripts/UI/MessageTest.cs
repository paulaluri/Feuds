using UnityEngine;
using System.Collections;

public class MessageTest : MonoBehaviour {
	public MessageScript ms;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.frameCount % 80 == 0){
			ms.Add ("Hello!");
		}
		if(Time.frameCount % 99 == 0){
			ms.Add ("This message is important", 10, true);
		}
	}
}
