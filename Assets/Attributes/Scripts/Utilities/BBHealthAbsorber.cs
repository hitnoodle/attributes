using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BBHealthAbsorber : MonoBehaviour
{
    [Header("Data")]

    [SerializeField]
    private GameObject _AttributeOwner;

    [SerializeField]
    private string _AttributeHealthID;

    [SerializeField]
    private string _AttributeHealthTempID;

    [Header("Absorber")]

    [SerializeField]
    private float _HealthToGiveAfterHit = 5f;

    private Attribute _AttributeHealth;
    private AttributeOverTime _AttributeHealthTemp;

    void Awake()
    {
        Attribute[] attributes = _AttributeOwner.GetComponents<Attribute>();

        _AttributeHealth = attributes.Where(x => x.ID.Equals(_AttributeHealthID)).FirstOrDefault();
        if (_AttributeHealth == null) Debug.LogError("[BBHealthAbsorber] There is no attribute with ID " + _AttributeHealthID + " at GameObject " + _AttributeOwner.name);

        _AttributeHealthTemp = (AttributeOverTime)attributes.Where(x => x.ID.Equals(_AttributeHealthTempID)).FirstOrDefault();
        if (_AttributeHealthTemp == null) Debug.LogError("[BBHealthAbsorber] There is no attribute with ID " + _AttributeHealthTempID + " at GameObject " + _AttributeOwner.name);
    }

    public void Absorb()
    {
        // Assume that Health Max is Health Temp Max
        if (_AttributeHealth.CurrentValue < _AttributeHealthTemp.CurrentValue)
        {
            _AttributeHealth.IncreaseValue(_HealthToGiveAfterHit);
            _AttributeHealthTemp.ChangeOverTimeDestination(_AttributeHealth.CurrentValue);
        }
    }
}
