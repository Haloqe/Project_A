// �׷캰�� �� ������ Enums namespace�� ���ϵ� �����
namespace Enums.PlayerEnums
{
    #region Ability
    public enum EAbilityMetalType
    {
        Copper,
        Gold,
        Undefined,
    }
    public enum EAbilityType // AbilityType�� �� ��ȣ�ѵ� �ѵ� ������ �̸� ������ �ٲ���.. �������� ���ΰ�.
    {
        Passive,
        Active,
        Undefined,
    }
    public enum EAbilityState
    {
        Ready,
        Active,
        Cooldown,
    } 
    #endregion Ability
}