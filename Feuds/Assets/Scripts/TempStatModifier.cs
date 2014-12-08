using UnityEngine;
using System.Collections;

[System.Serializable]
public class StatMod {
	public string statName;
	public float v1;
	public float v2;
	public float duration;
}

public class TempStatModifier : MonoBehaviour
{
	public StatMod[] stats;
	public float duration;

    CombatController cc;
    // Use this for initialization
    void Start()
    {
        cc = gameObject.GetComponentInParent<CombatController>();
        if(cc!=null) {
			foreach(StatMod stat in stats) {
				cc.AddBuff(stat.statName,stat.v1,stat.v2,stat.duration);
			}
		} else {
			Debug.Log("nope");
		}
        //print(cc.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(gameObject);
        }
    }
}
