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

            //print("Ok...");
            networkView.RPC("LetThereBeFireN", RPCMode.Others, position);
    }

    [RPC]
    public void LetThereBeFireN(Vector3 position)
    {
        //print("FIREBALLLL from network");
        GameObject fB = (GameObject)GameObject.Instantiate(fireBall);
        fB.GetComponent<Skill_Fireball>().target += position;
        fB.GetComponent<Skill_Fireball>().skillDamage = 0;
        fB.particleSystem.Play();
    }


    public void SparkleEverywhere(GameObject character)
    {
            GameObject sp = (GameObject)GameObject.Instantiate(guardAura);
            sp.transform.parent = character.transform;
            sp.transform.localPosition = new Vector3(0, 0, 0);

            networkView.RPC("SparkleEverywhereN", RPCMode.Others, character.transform.position);
    }

    [RPC]
    public void SparkleEverywhereN(Vector3 characterPos)
    {
        //print("Sparkleee from network");
        GameObject sp = (GameObject)GameObject.Instantiate(guardAura);
        sp.transform.position = characterPos;
    }

    public void YouShallNotMove(GameObject character)
    {

        GameObject frost = (GameObject)GameObject.Instantiate(archerFrost);
        frost.transform.parent = character.transform;
        frost.transform.localPosition = new Vector3(0, 0, 0);
    }
}
