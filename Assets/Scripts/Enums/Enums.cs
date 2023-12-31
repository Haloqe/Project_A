using System;

#region Settings
public enum ELocalisation
{
    ENG,
    KOR,
    MAX,
}
#endregion Settings

#region Common
public enum EDamageType
{
    Base,
    Light,
    Fire,
    Poison,
    Stench, // TODO change name
    Electric,
    MAX,
}

public enum EStatusEffect
{
    // Base
    Sommer,
    Poison,
    Drugged,
    Swarm,
    Evade,

    // Upgraded
    Sleep,
    Leech,
    Indoctrinated,
    Cloud,
    Camouflage,

    // Others
    Blind,
    Stun,
    Slow,
    Airborne,
    Silence,
    Root,
    Pull,

    MAX,
}
#endregion Common

#region Legacy
public enum ELegacyType
{
    Melee,
    Range,
    Dash,
    Area,
    Passive,
    MAX,
}

public enum EAttackState
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

public enum ELegacyPreservation
{
    Weathered,  // 닳은
    Tarnished,  // 빛바랜
    Intact,     // 온전한
    Pristine,   // 완벽한
    
    MAX,
}

public enum EWarrior
{
    Sommer,
    Vernon,
    Euphoria,
    Turbela,
    Shade,
    
    MAX,
}

public enum ETiming
{
    Before,
    After,
    With,
}

[Flags] 
public enum EItemObtainMethod
{
    Field   = 1,
    Store   = 2,
    Monster = 4,
    Boss    = 8,
}
#endregion Legacy

#region Enemy
public enum EEnemyMoveType
{
    None,
    FollowStraightPath,
}
#endregion Enemy

#region Interaction
public enum EPortalType
{
    GeneralStore,
    FoodStore,
    NextLevel,
}
#endregion Interaction

#region LevelGeneration
public enum ERoomType
{
    Normal,
    Corridor,
    Entrance,
    Teleport,
    Treasure,
    Shop,
    MAX,
}

public enum EConnectionType
{
    Horizontal,
    Vertical,
}

public enum ECellType
{
    Empty,
    Filled,
    ToBeFilled,
}

public enum ETilemapType
{
    Wall,
    Collideable,
    Background,
    Other1,
    Other2,
    Other3,
    MAX,
}

public enum EDoorDirection
{
    Up,
    Down,
    Left,
    Right,
}
#endregion