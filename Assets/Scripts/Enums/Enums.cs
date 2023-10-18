using System;

#region Common
public enum EDamageType
{
    Base,
    Light,
    Fire,
}

public enum EStatusEffect
{
    None,
    Blind,
    Stun,
    Slow,
    Airborne,
    Silence,
    Root,
}
#endregion Common

#region Ability
public enum EAbilityMetalType
{
    Copper,
    Gold,
    None,
}
public enum EAbilityType // AbilityType�� �� ��ȣ�ѵ� �ѵ� ������ �̸� ������ �ٲ���.. �������� ���ΰ�.
{
    Passive,
    Active,
    None,
}
public enum EAbilityState
{
    Ready,
    Active,
    Cooldown,
} 
public enum EInteraction
{
    Press,
    Hold,
    None,
}
#endregion Ability
