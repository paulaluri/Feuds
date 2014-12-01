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
                target.MovSpeed.current *= archerSlowConstant;
                //do attack?
                //how many damage?
            }
            //set cooldown...
        }
        else if (attacker.Class == Class.Magician)
        {
            //Now that's a bit complicated . . .
            //Since it's an AOE, may be something els should handle this
            //set cooldown...
        }

        return target.isDead;
    }
}
