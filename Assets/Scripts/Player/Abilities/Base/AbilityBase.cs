using Enums.PlayerEnums;
using UnityEngine;

namespace Player.Abilities.Base
{
    public abstract class AbilityBase : MonoBehaviour
    {
        // ���� abilityInfo��� struct�� ���Ĺ������� ����
        public int Id                       = 0;
        public string Name_EN               = string.Empty;
        public string Name_KO               = string.Empty;
        public string Description_EN        = string.Empty;
        public string Description_KO        = string.Empty;
        public string Icon                  = string.Empty;

        public EAbilityMetalType MetalType  = EAbilityMetalType.Undefined;
        public EAbilityType Type            = EAbilityType.Undefined;
        public EAbilityState State          = EAbilityState.Ready;

        // �нú� ��ų������ ����� �� �ִٰ� �Ǵ��Ͽ� �ϴ� baseClass�� ���� (e.g. n�ʸ��� ȸ��)
        // ���� ��Ƽ�� ��ų�� ���ѵ� �����Ͻ� activeBase�� �̵�
        public float CoolDownTime = 0.0f;
        public float Lifetime = 0.0f;

        // status effects
        // vfx

        void Update()
        {
            if (Type == EAbilityType.Passive) return;
            switch (State)
            {
                case EAbilityState.Ready:
                
                    break;
                case EAbilityState.Active: 
                                
                    break;
                case EAbilityState.Cooldown:
                
                    break;
            }
        }

        public abstract void Activate();
    }
}
