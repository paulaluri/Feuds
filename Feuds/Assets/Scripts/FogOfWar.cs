using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FogOfWar : MonoBehaviour
{
    public float radius;
    public GameObject cameras;
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
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(0,0,0));
        cameraDir = ray.direction;
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
        int layerMask = 1 << 12;
        foreach (GameObject character in characters)
        {
            RaycastHit hit;
            Debug.DrawRay(character.transform.position-100*cameraDir, 100 * cameraDir);
            if (Physics.Raycast(character.transform.position - 100 * cameraDir, 100 * cameraDir, out hit, layerMask))
            {
                //print(hit.point);
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
            colors[i].a = 1;
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
