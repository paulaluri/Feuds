using UnityEngine;
using System.Collections;

public class UISkill : MonoBehaviour
{
    public GameObject fireBall;

    void Start()
    {
    }


    public void LetThereBeFire(Vector3 position, float skillDamage)
    {
        print(position);
        GameObject fB = (GameObject)GameObject.Instantiate(fireBall);
        fB.GetComponent<Skill_Fireball>().target = position;
        fB.GetComponent<Skill_Fireball>().skillDamage = skillDamage;
        position.y += 15;
        fB.transform.position = position;
        fB.particleSystem.Play();
    }
}
