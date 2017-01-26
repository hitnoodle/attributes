using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class AttributeUIBar : AttributeUIBase
{
    private Transform _Transform;

    protected override void Awake()
    {
        base.Awake();
        _Transform = transform;
    }

	// Use this for initialization
	void Start ()
    {
        if (_Attribute != null)
        {
            // Assign the bar UI logic
            _Attribute.ObserveEveryValueChanged(x => x.ValuePercentage).Subscribe(valuePercentage =>
            {
                _Transform.localScale = new Vector3(valuePercentage, 1, 1);
            }).AddTo(this);
        }
        else
        {
            Debug.LogError("[AttributeUIBar] There is no attribute with ID " + _AttributeID + " at GameObject " + _AttributeOwner.name);
        }
	}
}
