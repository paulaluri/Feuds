using UnityEngine;
using System.Collections;

public class UISkill : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject guardAura;

    void Start()
    {
    }


    public void LetThereBeFire(Vector3 position, float skillDamage)
    {
        GameObject fB = (GameObject)GameObject.Instantiate(fireBall);
        fB.GetComponent<Skill_Fireball>().target += position;
        fB.GetComponent<Skill_Fireball>().skillDamage = skillDamage;
        fB.particleSystem.Play();
    }

    public void SparkleEverywhere(GameObject character)
    {
        if (character.GetComponentInChildren<TempStatModifier>() != null) return;
        GameObject sp = (GameObject)GameObject.Instantiate(guardAura);
        sp.transform.parent = character.transform;
        sp.transform.localPosition = new Vector3(0, 0, 0);
    }
}
