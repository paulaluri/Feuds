using UnityEngine;
using System.Collections;

public class UIAoeSkill : MonoBehaviour
{
    public Light light;
    public bool isShowing;

    // Use this for initialization
    void Start()
    {
        isShowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (light == null) return;
        if (isShowing)
        {
            light.enabled = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layer = 1 << 8;
            if (Physics.Raycast(ray, out hit, 1000, layer))
            {
                setLightPositionFromWorldPosition(hit.point);
                //print(hit.point);
                //do something with the character in the area?
            }
        }
        else
        {
            light.enabled = false;
        }
    }

    void setLightPositionFromWorldPosition(Vector3 pos)
    {
        pos.y += 10;
        light.transform.position = pos;
    }
}
