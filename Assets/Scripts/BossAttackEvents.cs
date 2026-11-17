using UnityEngine;

public class BossAttackEvents : MonoBehaviour
{
    public BossAttack leftFist;
    public BossAttack rightFist;

    public void StartPunch()
    {
        leftFist.EnableDamage();
        rightFist.EnableDamage();
    }

    public void EndPunch()
    {
        leftFist.DisableDamage();
        rightFist.DisableDamage();
    }
}

