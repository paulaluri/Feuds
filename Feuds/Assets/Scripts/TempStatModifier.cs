using UnityEngine;
using System.Collections;

public class TempStatModifier : MonoBehaviour
{
    public float MovSpeed;
    public float AtkSpeed;
    public Damage Attack;
    public Damage Defense;
    public float skillCD;
    public float Radius;

    public float time;

    CombatController cc;
    // Use this for initialization
    void Start()
    {
        cc = gameObject.GetComponentInParent<CombatController>();
        if(cc!=null)modifyStat(1);
        print(cc.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (cc == null)
        {
            cc = gameObject.GetComponentInParent<CombatController>();
            if (cc != null) modifyStat(1);
        }
        time -= Time.deltaTime;
        if (time < 0)
        {
            modifyStat(-1);
            Destroy(this.gameObject);
        }
    }

    void modifyStat(int mode)
    {
        if (mode == 1)
        {
            cc.MovSpeed.current *= MovSpeed;
        }
        else
        {
            cc.MovSpeed.current /= MovSpeed;
        }
        cc.AtkSpeed.max += mode * AtkSpeed;
        cc.Attack += mode * Attack;
        cc.Defense += mode * Defense;
        cc.SkillSpeed.max += mode * skillCD;
        cc.Radius += mode * Radius;
    }
}
