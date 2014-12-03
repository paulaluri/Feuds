using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FogOfWar : MonoBehaviour
{
    public float radius;
    public Camera Camera;
	public LayerMask FogLayer;
    Mesh mesh;
    MeshFilter mf;
    Vector3[] vertices;
    Vector3 cameraDir;

    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mf = GetComponent<MeshFilter>();
        vertices = mesh.vertices;
		cameraDir = Camera.transform.forward;
        GetComponent<MeshRenderer>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = FindPositionToClearFog(GameManager.characters[GameManager.player]);
        ClearFog(positions);
    }

    Vector3[] FindPositionToClearFog(List<GameObject> characters)
    {
        Vector3[] tmp = new Vector3[characters.Count];
        int count = 0;
        foreach (GameObject character in characters)
        {
            RaycastHit hit;
			//Debug.DrawRay(character.transform.position, -1000.0f*cameraDir);
            if (Physics.Raycast(character.transform.position, -cameraDir, out hit, 1000.0f, FogLayer))
            {
				//Debug.Log(hit.collider.gameObject.name);
				//Debug.Log(hit.point);
                Vector3 point = mf.transform.InverseTransformPoint(hit.point);
                tmp[count] = point;
                count++;
            }
        }

        Vector3[] ret = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            ret[i] = tmp[i];
        }

        return ret;
    }

    void ClearFog(Vector3[] pos)
    {
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        { 
            colors[i].a = 0.7f;
            for (int j = 0; j < pos.Length; j++)
            {
                if ((vertices[i] - pos[j]).sqrMagnitude<radius)
                {
                    //print(pos[j]);
                    colors[i].a = 0;
                    break;
                }
            }
        }
        mesh.colors = colors;
    }
}
