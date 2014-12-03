using UnityEngine;
using System.Collections;

public class UISkill : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject guardAura;
    public GameObject archerFrost;

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
        GameObject sp = (GameObject)GameObject.Instantiate(guardAura);
        sp.transform.parent = character.transform;
        sp.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void YouShallNotMove(Vector3 position)
    {
        
        foreach (GameObject character in GameManager.characters[GameManager.other])
        {
            if ((position - character.transform.position).magnitude < 1)
            {
                GameObject frost = (GameObject)GameObject.Instantiate(archerFrost); 
                frost.transform.parent = character.transform;
                frost.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
