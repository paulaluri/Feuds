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
        modifyStat(1);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            modifyStat(-1);
            Destroy(this.gameObject);
        }
    }

    void modifyStat(int mode)
    {
        cc.MovSpeed.max += mode * MovSpeed;
        cc.AtkSpeed.max += mode * AtkSpeed;
        cc.Attack += mode * Attack;
        cc.Defense += mode * Defense;
        cc.skillCD += mode * skillCD;
        cc.Radius += mode * Radius;
    }
}
