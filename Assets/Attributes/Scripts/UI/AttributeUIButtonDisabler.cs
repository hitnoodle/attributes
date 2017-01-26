using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UniRx;

public class AttributeUIButtonDisabler : AttributeUIBase
{
    private Button _Button;

    protected override void Awake()
    {
        base.Awake();
        _Button = GetComponent<Button>();
    }

    // Use this for initialization
    void Start ()
    {
        if (_Attribute != null)
        {
            _Attribute.Value.Where(x => x == 0).Subscribe(x =>
            {
                _Button.interactable = false;
            }).AddTo(this);

            _Attribute.Value.Where(x => x > 0 && !_Button.IsInteractable()).Subscribe(x =>
            {
                _Button.interactable = true;
            }).AddTo(this);
        }
        else
        {
            Debug.LogError("[AttributeUIButtonDisabler] There is no attribute with ID " + _AttributeID + " at GameObject " + _AttributeOwner.name);
        }
    }
}
