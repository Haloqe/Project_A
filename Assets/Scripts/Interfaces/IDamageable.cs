using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    abstract void TakeDamage(SDamageInfo damageInfo);

    public static float CalculateRoughDamage(List<SDamage> damages)
    {
        if (damages == null) return 0;

        float total = 0; float duration = 0;
        foreach (var damage in damages)
        {
            duration = damage.Duration == 0 ? 1 : damage.Duration;
            total += (damage.Amount * duration);
        }

        return total;
    }
}
