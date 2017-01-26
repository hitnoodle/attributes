using UnityEngine;
using System.Collections;

public class AttributeRegenerative : Attribute
{
    [Header("Regeneration")]

    [SerializeField]
    protected bool _RegenAfterDecrease = true;
    public bool RegenAfterDecrease
    {
        get { return _RegenAfterDecrease;  }
        set
        {
            _RegenAfterDecrease = value;
            if (!_RegenAfterDecrease)
            {
                if (_RegenerationRoutine != null)
                {
                    StopCoroutine(_RegenerationRoutine);
                    _RegenerationRoutine = null;
                }
            }
        }
    }

    [SerializeField]
    [Tooltip("Time it took from decrease to when it regen")]
    protected float _DelayBeforeRegen = 0.5f;

    [SerializeField]
    [Tooltip("Sometimes we want a longer value when we try to regen from zero")]
    protected float _DelayBeforeRegenWhenZero = 0.5f;

    [SerializeField]
    [Tooltip("How much it restores per second")]
    protected float _RegenRate = 20f;

    [SerializeField]
    [Tooltip("Modifier to the regen rate")]
    protected float _RegenModifier = 1f;
    public float RegenModifier
    {
        get { return _RegenModifier;  }
        set { _RegenModifier = value; }
    }

    private IEnumerator _RegenerationRoutine;

    public void Regen()
    {
        if (_RegenerationRoutine != null)
        {
            StopCoroutine(_RegenerationRoutine);
            _RegenerationRoutine = null;
        }

        _RegenerationRoutine = RegenRoutine();
        StartCoroutine(_RegenerationRoutine);
    }

    public override void DecreaseValue(float val)
    {
        base.DecreaseValue(val);
        Regen();
    }

    private IEnumerator RegenRoutine()
    {
        yield return new WaitForSeconds(_Value.Value == 0? _DelayBeforeRegenWhenZero : _DelayBeforeRegen);

        while (_Value.Value < _MaxValue)
        {
            IncreaseValue(_RegenRate * _RegenModifier * Time.deltaTime);
            yield return null;
        }
    }
}
