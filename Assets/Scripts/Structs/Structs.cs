using System.Collections.Generic;

#region Common
public struct SStatusEffect
{
    public EStatusEffect Effect;
    public float Strength;
    public float Duration; // zero if continuous, auto-removed skill (e.g. ���ǽ�ų)
}

public struct SDamage
{
    public EDamageType Type;
    public float Amount;
    public float Duration; // zero if one-shot damage
    // public float Interval; // �ʹ� ���������� ��Ʈ���� 0.5�� tick���� ����
}

public struct SDamageInfo
{
    public int DamageSource;
    public EAbilityMetalType AbilityMetalType; // only for player's damage
    public List<SDamage> Damages;
    public List<SStatusEffect> StatusEffects;
}
#endregion Common

#region Player
public struct SAbilityData
{
    public bool ShouldOverride; // TEMP DEV PURPOSE ONLY

    public int Id;
    public string ClassName;
    public string Name_EN;
    public string Name_KO;
    public string Des_EN;
    public string Des_KO;
    public string IconPath;
    public string PrefabPath;
    public bool IsAttached;
    public EInteraction Interaction; // ���� slowtap duration, hold duration �� �ʿ������� string �Ǵ� separated field ����

    public EAbilityType Type;
    public EAbilityMetalType MetalType;
    public float CoolDownTime;
    public float LifeTime;
    public SDamageInfo DamageInfo;
}
#endregion Player

#region Enemy
public struct SEnemyData
{
    public int Id;
    public string Name_EN;
    public string Name_KO;
    public string PrefabPath;
    public float MoveSpeed;
    public float MaxHealth;
    // amount of gold to drop
    // percentage of drop
    // rank
}
#endregion Enemy
