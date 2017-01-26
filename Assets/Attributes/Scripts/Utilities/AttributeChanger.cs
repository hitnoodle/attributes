using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttributeChanger : MonoBehaviour
{
    [Header("Data")]

    [SerializeField]
    private GameObject _AttributeOwner;

    [SerializeField]
    private string[] _AttributeIDs;

    [Header("Changer")]

    [SerializeField]
    private float _ChangeValue;

    private List<Attribute> _Attributes;

    void Awake()
    {
        _Attributes = new List<Attribute>();
        Attribute[] attributes = _AttributeOwner.GetComponents<Attribute>();

        for (int i = 0; i < _AttributeIDs.Length; i++)
        {
            Attribute attribute = attributes.Where(x => x.ID.Equals(_AttributeIDs[i])).FirstOrDefault();
            if (attribute != null)
                _Attributes.Add(attribute);
            else
                Debug.LogError("[AttributeChanger] There is no attribute with ID " + _AttributeIDs[i] + " at GameObject " + _AttributeOwner.name);
        }
    }

    public void Change()
    {
        if (_Attributes.Count > 0)
        {
            for (int i = 0; i < _Attributes.Count; i++)
            {
                if (_ChangeValue > 0)
                    _Attributes[i].IncreaseValue(_ChangeValue);
                else if (_ChangeValue < 0)
                    _Attributes[i].DecreaseValue(_ChangeValue * -1f);
            }
        }
    }
}
