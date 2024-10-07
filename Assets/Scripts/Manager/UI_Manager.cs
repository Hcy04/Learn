using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.Mathematics;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public UI_InGame inGame;
    [SerializeField] private GameObject menu;

    [Header("Character")]
    public UI_ItemToolTip toolTip;
    public UI_StatInfo statInfo;

    [Header("Skill")]
    public UI_SKillToolTip skillTip;

    public UI_SkillSlot sword_Default;
    public UI_SkillSlot sword_DamageReturn;
    public UI_SkillSlot sword_Pierce;
    public UI_SkillSlot sword_Bouncing;
    public UI_SkillSlot sword_Spin;

    [Header("Craft")]
    public UI_CraftPanel craftPanel;

    #region Stats
    [Header("Ability Stats")]
    [SerializeField] private TextMeshProUGUI vitality;
    [SerializeField] private TextMeshProUGUI strength;
    [SerializeField] private TextMeshProUGUI agility;
    [SerializeField] private TextMeshProUGUI intelligence;
    
    [Header("Major Stats")]
    [SerializeField] private TextMeshProUGUI maxHealth;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI armor;

    [SerializeField] private TextMeshProUGUI critChance;
    [SerializeField] private TextMeshProUGUI critDamage;

    [Header("Elemental Stats")]
    [SerializeField] private TextMeshProUGUI fireDamage;
    [SerializeField] private TextMeshProUGUI iceDamage;
    [SerializeField] private TextMeshProUGUI lightningDamage;

    [SerializeField] private TextMeshProUGUI fireResistance;
    [SerializeField] private TextMeshProUGUI iceResistance;
    [SerializeField] private TextMeshProUGUI lightningResistance;
    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance.gameObject);
    }

    private void Start()
    {
        UpdateValue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) OpenOrCloseMenu();
    }

    private void OpenOrCloseMenu()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            inGame.gameObject.SetActive(true);
        }
        else
        {
            inGame.gameObject.SetActive(false);
            menu.SetActive(true);
        }
    }

    public void UpdateValue()
    {
        PlayerStats stats = PlayerManager.instance.player.stats;

        vitality.text = Mathf.RoundToInt(stats.vitality.GetBaseValue()).ToString();
        strength.text = Mathf.RoundToInt(stats.strength.GetBaseValue()).ToString();
        agility.text = Mathf.RoundToInt(stats.agility.GetBaseValue()).ToString();
        intelligence.text = Mathf.RoundToInt(stats.intelligence.GetBaseValue()).ToString();

        maxHealth.text = Mathf.RoundToInt(stats.maxHealth.GetBaseValue()).ToString();
        damage.text = Mathf.RoundToInt(stats.damage.GetBaseValue()).ToString();
        armor.text = Mathf.RoundToInt(stats.armor.GetBaseValue()).ToString();
        critChance.text = stats.critChance.GetBaseValue().ToString("F1");
        critDamage.text = stats.critDamage.GetBaseValue().ToString("F1");

        fireDamage.text = Mathf.RoundToInt(stats.fireDamage.GetBaseValue()).ToString();
        iceDamage.text = Mathf.RoundToInt(stats.iceDamage.GetBaseValue()).ToString();
        lightningDamage.text = Mathf.RoundToInt(stats.lightningDamage.GetBaseValue()).ToString();

        fireResistance.text = Mathf.RoundToInt(stats.fireResistance.GetBaseValue() * 100).ToString();
        iceResistance.text = Mathf.RoundToInt(stats.iceResistance.GetBaseValue() * 100).ToString();
        lightningResistance.text = Mathf.RoundToInt(stats.lightningResistance.GetBaseValue() * 100).ToString();
    }

    public void UpdateSkillBool()
    {
        SkillManager.instance.GetComponent<Sword_Skill>().UpdateBool();
    }
}
