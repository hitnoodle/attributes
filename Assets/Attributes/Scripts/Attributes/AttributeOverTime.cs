using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeOverTime : Attribute
{
    [Header("Over Time")]

    [SerializeField]
    [Tooltip("Delay before start changing over time")]
    protected float _Delay = 0.5f;

    [SerializeField]
    [Tooltip("Rate per second when changing the value")]
    protected float _Rate = 0.25f;

    [SerializeField]
    protected bool _DecreaseOverTime = true;
    
    [SerializeField]
    protected bool _IncreaseOverTime = false;

    private float _TemporaryValue;
    private bool _IsChanging = false;
    private IEnumerator _OverTimeRoutine;

    protected override void Awake()
    {
        base.Awake();
        ChangeOverTimeDestination(_Value.Value);
    }

    public void ChangeOverTimeDestination(float val)
    {
        _TemporaryValue = val;
    }

    public override void IncreaseValue(float val)
    {
        _TemporaryValue = _TemporaryValue + val;
        if (_TemporaryValue > _MaxValue) _TemporaryValue = _MaxValue;

        if (_IncreaseOverTime)
        {
            // Start over if we're not increasing yet
            if (!_IsChanging) ResetOverTime();
        }
        else
        {
            _Value.Value = _TemporaryValue;
        }
    }

    public override void DecreaseValue(float val)
    {
        _TemporaryValue = _TemporaryValue - val;
        if (_TemporaryValue < 0) _TemporaryValue = 0;

        if (_DecreaseOverTime)
        {
            // Start over if we're not decreasing yet
            if (!_IsChanging) ResetOverTime();
        }
        else
        {
            _Value.Value = _TemporaryValue;
        }
    }

    private void ResetOverTime()
    {
        if (_OverTimeRoutine != null)
        {
            StopCoroutine(_OverTimeRoutine);
            _OverTimeRoutine = null;
        }

        _OverTimeRoutine = OverTimeRoutine();
        StartCoroutine(_OverTimeRoutine);
    }

    private IEnumerator OverTimeRoutine()
    {
        yield return new WaitForSeconds(_Delay);

        // Set our flag after we're waiting
        _IsChanging = true;
        yield return null;

        while (_Value.Value != _TemporaryValue)
        {
            float changeValue = _TemporaryValue - _Value.Value;
            float absChangeValue = Mathf.Abs(changeValue);
            absChangeValue = absChangeValue > _Rate ? _Rate : absChangeValue;

            if (changeValue > 0)
                base.IncreaseValue(absChangeValue);
            else if (changeValue < 0)
                base.DecreaseValue(absChangeValue);

            yield return null;
        }

        // Reset flag
        _IsChanging = false;
    }
}
