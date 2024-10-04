using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    [Header("Magic")]
    [SerializeField] private GameObject ice_fire;
    [SerializeField] private GameObject coldBeam;

    [Header("Item")]
    [SerializeField] private GameObject item;
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

    #region Magic
    public GameObject CreatIce_Fire(Vector3 _position)
    {
        return Instantiate(ice_fire, _position, Quaternion.identity);
    }

    public GameObject CreatColdBeam(Vector3 _position)
    {
        return Instantiate(coldBeam, _position, Quaternion.identity);
    }
    #endregion

    #region Item
    public GameObject CreatItem(Vector3 _position)
    {
        return Instantiate(item, _position, Quaternion.identity);
    }
    #endregion
}
