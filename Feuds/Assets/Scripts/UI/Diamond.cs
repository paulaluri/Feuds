using UnityEngine;
using System.Collections;

public class Diamond : MonoBehaviour {

	public Vector2 top;
	public Vector2 bottom;
	public Vector2 left;
	public Vector2 right;
	
	public Diamond(Vector2 top,Vector2 bottom, Vector2 left, Vector2 right){
		this.top = top;
		this.bottom = bottom;
		this.left = left;
		this.right = right;
	}
	
	public float GetHeight(){
		return top.y-bottom.y;
	}
	
	public float GetWidth(){
		return right.x-left.x;
	}
	
	public bool Contains(Vector2 pos){
		if(isPointUnderLine(pos,top,right) && isPointUnderLine(pos,top,left)
			&& !isPointUnderLine(pos,left,bottom) && !isPointUnderLine(pos,right,bottom)){
			return true;
		}
		return false;
		
	}
	
	public bool isPointUnderLine(Vector2 point, Vector2 one, Vector2 two){
		float m = (one.x-two.x)/(one.y-two.y);
		//y-y1 = m(x-x1)
		if(point.y-one.y >= m*(point.x-one.x)){
			return true;
		}
		else return false;
	}
}

