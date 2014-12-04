using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMinimap : MonoBehaviour
{
    public Diamond minimapDiamond; //-->>GUI Position
    public InputManager inputManager;
    public GUIStyle style;
    public GameObject cameras;

	public Vector2 anchor;
    public int UIHeight = 192;

    public bool mouseDown = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		anchor.x = Screen.width;
		anchor.y = Screen.height;

        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }
        if (mouseDown)
        {
            Vector2 diamondPos = ScreenToDiamond(FromMouseToGUIPosition(Input.mousePosition));
            if (minimapDiamond.Contains(diamondPos))
            {
				Vector3 cameraPos = DiamondToWorld(diamondPos);
				cameraPos.y = cameras.transform.position.y;
				cameras.transform.position = cameraPos;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            //Move selected characters to that place
            Vector2 guiPosition = FromMouseToGUIPosition(Input.mousePosition);
            if (minimapDiamond.Contains(guiPosition))
            {
                
                
            }
            //inputManager.MoveTo(hit.point);
        }

        //print (FromMouseToGUIPosition(Input.mousePosition));

        //center = GetGUICenterPoint();
        //print (center);


    }

    Vector2 GetRelativePosFromPoint(Vector2 point)
    {
        Vector2 tmp = new Vector2();
        tmp.x = point.x - minimapDiamond.left.x;
        tmp.y = point.y - minimapDiamond.top.y;
        //tmp.x *= Mathf.Sqrt(2) / minimapDiamond.GetLength();
        //tmp.y *= Mathf.Sqrt(2) / minimapDiamond.GetLength();

        Vector2 ans = new Vector2();
        ans.x = (tmp.x + tmp.y - 1) / 2f;
        ans.y = (tmp.x - tmp.y + 1) / 2f;
        return ans;
    }

    Vector2 GetGUICenterPoint()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, (Screen.height - UIHeight) / 2.0f, 0));
        return GetInMinimapPosition(worldPoint);
    }

    Vector2 GetInMinimapPosition(Vector3 worldPoint)
    {
        Vector2 relativePos = new Vector2();
        relativePos.x = (worldPoint.x - CameraMove.leftMost) / CameraMove.GetWidth();
        relativePos.y = (worldPoint.z - CameraMove.topMost) / CameraMove.GetHeight();
        Vector2 ret = new Vector2();
        //ret.x = minimapDiamond.left.x + (relativePos.x + relativePos.y) * minimapDiamond.GetLength() / Mathf.Sqrt(2);
        //ret.y = minimapDiamond.top.y + (relativePos.x + 1 - relativePos.y) * minimapDiamond.GetLength() / Mathf.Sqrt(2);
        return ret;
    }


    void OnGUI()
    {
        GUI.depth = 1;
		{
			Vector2 pos = WorldToScreen(cameras.transform.position);
			GUI.Box (new Rect(pos.x-2.5f, pos.y-2.5f, 5, 5),"");
		}
		foreach (GameObject character in GameManager.characters[GameManager.player])
        {
			Vector2 pos = WorldToScreen(character.transform.position);
			GUI.Box(new Rect(pos.x-2.5f, pos.y-2.5f, 5, 5), "");
        }

    }

	Vector2 WorldToDiamond(Vector3 v) {
		Vector3 localV = transform.InverseTransformPoint (v);
		Vector2 local2d = new Vector2 (localV.x, localV.z);
		local2d.x *= minimapDiamond.width/2;
		local2d.y *= minimapDiamond.height/2;
		return local2d;
	}

	Vector2 DiamondToScreen(Vector2 v) {
		return (anchor - minimapDiamond.center) + v;
	}

	Vector2 WorldToScreen(Vector3 v) {
		return DiamondToScreen (WorldToDiamond (v));
	}

	Vector2 ScreenToDiamond(Vector2 v) {
		return v - (anchor - minimapDiamond.center);
	}

	Vector3 DiamondToWorld(Vector2 v) {
		Vector2 local2d = v;
		local2d.x /= minimapDiamond.width / 2;
		local2d.y /= minimapDiamond.height / 2;
		Vector3 localV = new Vector3 (local2d.x, 0, local2d.y);
		return transform.TransformPoint (localV);
	}

	Vector3 ScreenToWorld(Vector2 v) {
		return DiamondToWorld (ScreenToDiamond (v));
	}

    Vector2 FromMouseToGUIPosition(Vector2 mP)
    {
        Vector2 ret = mP;
        ret.y = Screen.height - mP.y;
        return ret;
    }
}
