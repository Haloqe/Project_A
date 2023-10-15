// �׷캰�� �� ������ Enums namespace�� ���ϵ� �����
namespace Enums.PlayerEnums
{
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
}