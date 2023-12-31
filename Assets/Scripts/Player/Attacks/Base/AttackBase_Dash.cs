using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase_Dash : AttackBase
{
    private Legacy_Dash ActiveLegacy;
    private Rigidbody2D _rigidbody2D;
    private float _dashStrength = 8;

    public override void Reset()
    {
        base.Reset();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // Attach default VFX
        if (_vfxObject != null) Destroy(_vfxObject);
        var obj = Utility.LoadGameObjectFromPath("Prefabs/Player/AttackVFXs/Dash_Default");
        Transform transform = gameObject.transform.Find("AttackVFXs").transform;
        _vfxObject = Instantiate(obj, transform);
    }

    public override void Attack()
    {
        _animator.SetInteger("AttackIndex", (int)ELegacyType.Dash);
        _vfxObject.transform.localScale = new Vector3(Mathf.Sign(gameObject.transform.localScale.x), 1.0f, 1.0f);
        _vfxObject.SetActive(true);
        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        float prevGravity = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = new Vector2(-transform.localScale.x * _dashStrength, 0f);
        yield return new WaitForSeconds(0.35f);

        _vfxObject.SetActive(false);
        _playerAttack.OnAttackEnd(ELegacyType.Dash);
        _rigidbody2D.gravityScale = prevGravity;
        //_rigidbody2D.velocity = new Vector2(0f, 0f);
    }
}