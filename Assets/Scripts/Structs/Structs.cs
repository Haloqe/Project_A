using Enums.PlayerEnums;

namespace Structs.PlayerStructs
{
    public struct SAbilityData
    {
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
    }
}

namespace Structs.EnemyStructs
{
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
}