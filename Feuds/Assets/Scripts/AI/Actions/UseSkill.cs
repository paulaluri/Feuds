using UnityEngine;
using System.Collections;

public class UseSkill : Action
{
    public float guardSkillRadius = 10;
    public override bool Update()
    {
        CombatController attacker = ac.myCombat;
        CombatController target = ac.targetCombat;

        if (!attacker.CanUseSkill())
        {
            return true;
        }

        if (attacker.Class == Class.Guard)
        {
            //Aura for increasing defense . . .
            //Debug.Log(attacker.name+"Aura");
            Vector3 pos = attacker.transform.position;
            foreach (GameObject character in GameManager.characters[GameManager.player])
            {
                if ((character.transform.position - pos).magnitude < guardSkillRadius)
                {
                    CombatController cc = character.GetComponent<CombatController>();
                    cc.GetComponent<UISkill>().SparkleEverywhere(cc.gameObject);
                }
            }

            attacker.gameObject.GetComponent<Animator>().SetTrigger("use_skill");
            attacker.gameObject.GetComponent<AnimationUpdater>().networkView.RPC("NetUseSkill", RPCMode.Others);

            //set cooldown...
            attacker.startCD = Time.time;
        }
        else if (attacker.Class == Class.Archer)
        {
            if (target == null) return true;
            //animation...?
            if (attacker.CanAttack(target))
            {
                attacker.inCombat = true;
                attacker.attackedThisFrame = true;
                attacker.DoDamage(target);
                attacker.GetComponent<UISkill>().YouShallNotMove(target.gameObject);
                //set cooldown...
                attacker.startCD = Time.time;
                attacker.gameObject.GetComponent<ArcherArrow>().frostArrow = true;
            }


            

        }
        else if (attacker.Class == Class.Magician)
        {
            //Now that's a bit complicated . . .
            if ((attacker.gameObject.transform.position - ac.position).sqrMagnitude < attacker.Radius * attacker.Radius)
            {
                //set the animation
                attacker.gameObject.GetComponent<Animator>().SetTrigger("use_skill");
                attacker.gameObject.GetComponent<AnimationUpdater>().networkView.RPC("NetUseSkill", RPCMode.Others);
                //set cooldown...
                attacker.startCD = Time.time;
                attacker.GetComponent<UISkill>().LetThereBeFire(ac.position, attacker.skillValue);

                //for show
                attacker.GetComponent<UISkill>().LetThereBeFire(ac.position, 0);
                attacker.GetComponent<UISkill>().LetThereBeFire(ac.position, 0);
                attacker.GetComponent<UISkill>().LetThereBeFire(ac.position, 0);
                attacker.GetComponent<UISkill>().LetThereBeFire(ac.position, 0);
                Debug.Log(attacker.gameObject + " " + attacker.startCD);
            }
            else
            {

            }
        }

        return true;
    }
}
