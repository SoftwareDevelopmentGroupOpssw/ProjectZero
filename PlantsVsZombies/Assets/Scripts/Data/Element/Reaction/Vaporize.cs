using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Vaporize : ElementsReaction
{
    public const float DAMAGE_INCREASE = 1.75f;
    public override string Name => "Vaporize";

    public override void Action(IElementalDamage damage, IMonsterData target)
    {
        damage.AtkDmg = (int) (DAMAGE_INCREASE * damage.AtkDmg);
    }
}
