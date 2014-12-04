using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Diamond {
	public Vector2 center;
	public float width;
	public float height;

	public Vector2 top { get { return center + new Vector2(0,height/2); } }
	public Vector2 bottom { get { return center + new Vector2(0,-height/2); } }
	public Vector2 left { get { return center + new Vector2 (-width/2, 0); } }
	public Vector2 right { get { return center + new Vector2 (width/2, 0); } }
	
	//public float length { get { return Mathf.Sqrt (Mathf.Pow (width / 2f, 2) + Mathf.Pow (width / 2f, 2)); } }
	
	public bool Contains(Vector2 pos){
		float x = (2 * pos.x) / width;
		float y = (2 * pos.y) / height;
		if(Mathf.Abs(x) + Mathf.Abs(y) > 1) {
			return false;
		}
		return true;
	}
}

