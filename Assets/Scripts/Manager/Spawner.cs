using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    #region Prefabs
    [Header("Skill")]
    [SerializeField] private GameObject clone;
    [SerializeField] private GameObject dot;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject crystal;

    [Header("FX")]
    [SerializeField] private GameObject thunderStrike;
    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance.gameObject);
    }

    #region Skill
    public GameObject CreatClone(Vector3 _position)
    {
        return Instantiate(clone, _position, Quaternion.identity);
    }

    public GameObject CreatDot(Vector3 _position, Transform _parent)
    {
        return Instantiate(dot, _position, Quaternion.identity, _parent);
    }

    public GameObject CreatSword(Vector3 _position)
    {
        return Instantiate(sword, _position, Quaternion.identity);
    }

    public GameObject CreatCrystal(Vector3 _position)
    {
        return Instantiate(crystal, _position, Quaternion.identity);
    }
    #endregion

    #region FX
    public GameObject CreatThunderStrike(Vector3 _position, Transform _parent)
    {
        return Instantiate(thunderStrike, _position, Quaternion.identity, _parent);
    }
    #endregion
}
