using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UISelection : MonoBehaviour
{
    public List<GameObject> characters;
    public List<GameObject> selectedCharacters;
    public Vector3 mouseStartPosition;
    public bool mouseDown;
    public Rect selectionRect;
    public GUIStyle style;
    public GameObject Select;

    public int bottom_threshold;

    public InputManager inputManager;

    public Light lightForAoe;

    public const int LEFT_CLICK = 0;
    public const int RIGHT_CLICK = 1;

    public float skillRadius = 10;

    public enum SelectMode
    {
        NORMAL,
        AOESKILL
    }

    public static SelectMode selectMode;
    // Use this for initialization
    void Start()
    {
        characters = new List<GameObject>();
        Initialize(GameManager.characters[GameManager.player]);
        bottom_threshold = 192;
        lightForAoe.enabled = false;
        selectMode = SelectMode.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        if (characters == null || characters.Count == 0)
        {
            //Not Initialize yet
            return;
        }
        if (Input.mousePosition.y < bottom_threshold)
        {
            return;
        }
        if (selectMode == SelectMode.AOESKILL)
        {
            MoveAoeSkill(lightForAoe);
            if (Input.GetMouseButtonDown(LEFT_CLICK))
            {
                selectMode = SelectMode.NORMAL;
                lightForAoe.enabled = false;
                return;
            }
            if (Input.GetMouseButtonDown(RIGHT_CLICK))
            {
                selectMode = SelectMode.NORMAL;
                lightForAoe.enabled = false;
                Vector3 pos = GetWorldPositionFromMouse();
                List<GameObject> allcharacters = GameManager.characters[GameManager.other];

                foreach (GameObject character in allcharacters)
                {
                    if ((character.transform.position - pos).magnitude < skillRadius)
                    {
                        //initiate some kind of damage on the character

                    }
                }
                return;
            }
        }
        if (Input.GetMouseButtonDown(LEFT_CLICK))
        {
            mouseStartPosition = Input.mousePosition;
            mouseDown = true;

            //To do
            //ClearSelect()
            selectedCharacters.Clear();
            //Send it somewhere?
            inputManager.SelectCharacters(selectedCharacters);
        }
        if (Input.GetMouseButtonUp(LEFT_CLICK))
        {
            mouseDown = false;

        }

        if (Input.GetMouseButtonUp(RIGHT_CLICK))
        {
            //right click
            if (selectedCharacters.Count == 0)
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layer = 1 << 8;
            if (Physics.Raycast(ray, out hit, 1000, layer))
            {
                if (hit.collider.gameObject.layer == GameManager.otherLayer)
                {
                    //Enemy!!!
                    //Attack
                    inputManager.Attack(hit.collider.gameObject);
                    UIFogOfWar.DisableRenderer(hit.collider.gameObject);
                }
                else
                {
                    //Move to this position
                    GameObject.Instantiate(Select, hit.point, Quaternion.identity);
                    inputManager.MoveTo(hit.point);
                    print(hit.collider.name);
                }
            }
        }

        if (mouseDown || Input.GetMouseButtonDown(0))
        {
            selectionRect.xMin = mouseStartPosition.x < Input.mousePosition.x ? mouseStartPosition.x : Input.mousePosition.x;
            selectionRect.xMax = mouseStartPosition.x > Input.mousePosition.x ? mouseStartPosition.x : Input.mousePosition.x;
            selectionRect.yMin = Camera.main.pixelHeight - (mouseStartPosition.y > Input.mousePosition.y ? mouseStartPosition.y : Input.mousePosition.y);
            selectionRect.yMax = Camera.main.pixelHeight - (mouseStartPosition.y < Input.mousePosition.y ? mouseStartPosition.y : Input.mousePosition.y);

            characters.RemoveAll(item => item == null);
            foreach (GameObject c in characters)
            {
                Vector3 footPos = Camera.main.WorldToScreenPoint(c.transform.position + new Vector3(-0.5f, 0, 0));
                footPos.y = Camera.main.pixelHeight - footPos.y;
                //print (""+screenPos+" - "+Input.mousePosition);
                Vector3 headPos = Camera.main.WorldToScreenPoint(c.transform.position + new Vector3(0.5f, 2, 0));
                headPos.y = Camera.main.pixelHeight - headPos.y;
                Rect selectableArea = new Rect();
                selectableArea.xMin = footPos.x;
                selectableArea.xMax = headPos.x;
                selectableArea.yMax = footPos.y;
                selectableArea.yMin = headPos.y;
                if (selectionRect.Overlaps(selectableArea))
                {
                    //To do
                    //Select()
                    if (!selectedCharacters.Contains(c))
                    {
                        selectedCharacters.Add(c);
                        //Send it somewhere?
                        inputManager.SelectCharacters(selectedCharacters);
                    }
                }
                else
                {
                    if (selectedCharacters.Contains(c))
                    {
                        selectedCharacters.Remove(c);
                        //Send it somewhere?
                        inputManager.SelectCharacters(selectedCharacters);
                    }
                }
            }
        }

        
    }

    void OnGUI()
    {
        // Make a background box
        if (mouseDown)
        {
            if (selectionRect.width > 5 || selectionRect.height > 5)
            {
                GUI.Box(selectionRect, "");
            }
        }

    }

    public void Initialize(List<GameObject> c)
    {
        characters = c;
        selectedCharacters = new List<GameObject>();
    }

    public void MoveAoeSkill(Light light)
    {
        light.enabled = true;
        Vector3 pos = GetWorldPositionFromMouse();
        pos.y += skillRadius;
        light.transform.position = pos;
        //print(hit.point);
        //do something with the character in the area?

    }

    public Vector3 GetWorldPositionFromMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer = 1 << 8;
        if (Physics.Raycast(ray, out hit, 1000, layer))
        {
            return hit.point;
        }
        else return new Vector3();
    }
}
