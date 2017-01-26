using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class AttributeUIBase : MonoBehaviour
{
    [Header("Data")]

    [SerializeField]
    protected GameObject _AttributeOwner;

    [SerializeField]
    protected string _AttributeID;

    // Reference to the attribute we want to use
    protected Attribute _Attribute;

    protected virtual void Awake()
    {
        // Find our attribute on the owner
        Attribute[] attributes = _AttributeOwner.GetComponents<Attribute>();
        _Attribute = attributes.Where(x => x.ID.Equals(_AttributeID)).FirstOrDefault();
    }
}
