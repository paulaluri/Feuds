using UnityEngine;
using System.Collections;

public class UseSkill : Action
{
    public float guardSkillRadius;
    public float archerSlowConstant;
    public override bool Update()
    {
        CombatController attacker = ac.myCombat;
        CombatController target = ac.targetCombat;

        if (attacker.Class == Class.Guard)
        {
            //Aura for increasing defense . . .
            Vector3 pos = attacker.transform.position;
            foreach (GameObject character in GameManager.characters[0])
            {
                if ((character.transform.position - pos).magnitude < guardSkillRadius)
                {
                    CombatController cc = character.GetComponent<CombatController>();
                    cc.Defense -= new Damage(-1.0f, -1.0f);
                }
            }
            //attacker.Defense -= new Damage(-1.0f, -1.0f);

            //set cooldown...
        }
        else if (attacker.Class == Class.Archer)
        {
            if (attacker.CanAttack(target))
            {
                if (target.MovSpeed.current > 5)
                {
                    //No stacking
                    target.MovSpeed.current *= archerSlowConstant;
                }
                //... start countdown to return to normal speed?

                attacker.inCombat = true;
                attacker.attackedThisFrame = true;
                attacker.DoDamage(target);
                //do attack?
                //how many damage?
            }
            //set cooldown...
        }
        else if (attacker.Class == Class.Magician)
        {
            //Now that's a bit complicated . . .
            //set the animation
            attacker.inSkill = true;
            //set cooldown...
        }

        return target.isDead;
    }
}
