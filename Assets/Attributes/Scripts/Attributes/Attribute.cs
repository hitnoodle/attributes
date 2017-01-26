using UnityEngine;
using UniRx;

public class Attribute : MonoBehaviour
{
    [Header("Data")]

    [SerializeField]
    protected string _ID;
    public string ID
    {
        get { return _ID; }
    }

    [SerializeField]
    protected FloatReactiveProperty _Value = new FloatReactiveProperty();
    public FloatReactiveProperty Value
    {
        get { return _Value; }
    }

    public float CurrentValue
    {
        get { return _Value.Value; }
    }

    protected float _MaxValue;
    public float MaxValue
    {
        get { return _MaxValue; }
    }

    public float ValuePercentage
    {
        get { return _Value.Value / _MaxValue; }
    }

    protected virtual void Awake()
    {
        _MaxValue = _Value.Value;
    }

    public virtual bool HaveValue(float val)
    {
        return _Value.Value >= val;
    }

    public virtual void IncreaseValue(float val)
    {
        float newVal = _Value.Value + val;
        if (newVal > _MaxValue) newVal = _MaxValue;

        _Value.Value = newVal;
    }

    public virtual void DecreaseValue(float val)
    {
        float newVal = _Value.Value - val;
        if (newVal < 0) newVal = 0;

        _Value.Value = newVal;
    }

    public virtual void ResetToMaxValue()
    {
        _Value.Value = _MaxValue;
    }
}
