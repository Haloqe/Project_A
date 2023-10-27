using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;

//This skill has two parts of execution: (1)striking chain, (2)retrieving chain
//The enemy could therefore get hit twice by the two parts of execution. 
//And the enemy that gets hit in the first part of execution could be different from the enemy
//that gets hit in the second exectuion. 
public class PiercingChain : ActiveAbilityBase
{
    private List<int> _affectedEnemies;
    private int _maxTargetNum;
    private SDamage _specificDamage;
    private SDamageInfo _halvedDamageInfo;
    private SStatusEffect _specificStatusEffect;
    private SStatusEffect temp;

    protected override void Initialise()
    {
        _affectedEnemies = new List<int>();
        _maxTargetNum = 5;
        _specificDamage = new SDamage();
        _halvedDamageInfo = new SDamageInfo();
        _specificStatusEffect = new SStatusEffect();
        temp = new SStatusEffect();
    }

    protected override void ActivateAbility()
    {
        _affectedEnemies.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") == false)
        {
            Debug.LogError(_data.Name_EN + " trigger with non-enemy");
            return;
        }
        if (_affectedEnemies.Count >= _maxTargetNum) return;
        if (Utility.IsObjectInList(collision.gameObject, _affectedEnemies)) return;

        IDamageable target = collision.gameObject.GetComponent<IDamageable>();
        if (target == null) return;


        _affectedEnemies.Add(collision.gameObject.GetInstanceID());

        _halvedDamageInfo = _data.DamageInfo;

        //Retrieve Damage and statuseffect info about Piercing Chain
        _specificDamage = _data.DamageInfo.Damages[0];
        _specificStatusEffect = _data.DamageInfo.StatusEffects[0];
        temp = _data.DamageInfo.StatusEffects[0];

        //change to half amount and binding duration 0
        _specificDamage.Amount = _specificDamage.Amount / 2;
        _specificStatusEffect.Duration = 0;

        _halvedDamageInfo.Damages[0] = _specificDamage;
        _halvedDamageInfo.StatusEffects[0] = _specificStatusEffect;

        //first Hit - get the first half of the damage
        _owner.DealDamage(target, _halvedDamageInfo);

        _halvedDamageInfo.StatusEffects[0] = temp;

        //second Hit - get the second half of the damage and stun effect
        _owner.DealDamage(target, _halvedDamageInfo);



    }
}