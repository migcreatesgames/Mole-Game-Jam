using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// singletone manager class that handles UI elements.
/// </summary>

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public static UIManager _instance;

    private PlayerController pc;

    // reference to canvas group alpha value for HUD gameobject.
    public CanvasGroup hud_CanvasGroup;

    // reference to health bar UI.
    [SerializeField] Image _healthBar;
    // reference to health value from playercontroller.
    private float _healthValue;

    // reference to mana bar UI
    [SerializeField] Image _staminaBar;
    // reference to mana value from playercontroller.
    private float _staminaValue;

    // reference to baby mole bar UI
    [SerializeField] Image _moleBabiesBar;
    // reference to mana value from playercontroller.
    private float _moleBabiesValue;

    // reference to baby mole bar UI
    [SerializeField] Image _foodSavedBar;
    // reference to mana value from playercontroller.
    private float _foodSavedValue;

    public Image StaminaBar { get => _staminaBar; set => _staminaBar = value; }

    private void Awake()
    {
        if (_instance != null)
            return;
        _instance = this;
    }

    private void OnEnable()
    {
        hud_CanvasGroup = GameObject.Find("Panel_HUD_V1").GetComponent<CanvasGroup>();
        GameEvents.OnDamageEvent += HandleHealthBar;
        GameEvents.OnStaminaUpdateEvent += UpdateStaminaBar;
        GameEvents.OnMoleBabiesHungerUpdateEvent += UpdateMoleBabiesBar;
        GameEvents.OnHelathUpdateEvent += HandleHealthBar;
        GameEvents.OnFoodSaved += UpdateFoodSavedBar;
        GameEvents.OnFoodRemoved += UpdateFoodSavedBar;
    }

    private void OnDisable()
    {
        GameEvents.OnDamageEvent -= HandleHealthBar;
        GameEvents.OnStaminaUpdateEvent -= UpdateStaminaBar;
        GameEvents.OnMoleBabiesHungerUpdateEvent -= UpdateMoleBabiesBar;
        GameEvents.OnHelathUpdateEvent -= HandleHealthBar;
        GameEvents.OnFoodSaved -= UpdateFoodSavedBar;
        GameEvents.OnFoodRemoved -= UpdateFoodSavedBar;
    }

    void Start()
    {
        pc = PlayerController.Instance;
        SetUIObjects();
        SetUIObjectValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            ToggleHUD();

        if (Input.GetKeyDown(KeyCode.C))
            PlayerController.Instance.DamageTaken(1);

        if (Input.GetKeyDown(KeyCode.V))
            PlayerController.Instance.RegainHealth(1);
    }

    private void SetUIObjects()
    {
        _healthBar = GameObject.Find("HealthBar_Fill").GetComponent<Image>();
        _staminaBar = GameObject.Find("StaminaBar_Fill").GetComponent<Image>();
        _moleBabiesBar = GameObject.Find("MoleBabiesBar_Fill").GetComponent<Image>();
        _foodSavedBar = GameObject.Find("FoodSavedBar_Fill").GetComponent<Image>();
    }

    public void SetUIObjectValues()
    {
        _healthValue = pc.Health / 100; // change 100 to maxHealth
        _healthBar.fillAmount = _healthValue;

        _staminaValue = pc.Stamina;
        _staminaBar.fillAmount = _staminaValue / 100; // change 100 to maxStealth

        _moleBabiesValue = GameManager.Instance.MoleBabiesHungerValue;
        _moleBabiesBar.fillAmount = _moleBabiesValue / 100; // change 100 to maxBabiesHunger
    }

    void ToggleHUD()
    {
        if (hud_CanvasGroup.alpha == 0)
            DisplayHUD();
        else
            HideHUD();
    }

    private void DisplayHUD()
    {
        if (UIEvents.OnHUDDisplay != null)
            UIEvents.OnHUDDisplay(hud_CanvasGroup);
        else
            hud_CanvasGroup.alpha = 1;
    }

    public void HideHUD()
    {
        if (UIEvents.OnHUDHide != null)
            UIEvents.OnHUDHide(hud_CanvasGroup);
        else
            hud_CanvasGroup.alpha = 0;
    }

    public void HandleHealthBar(float value) => _healthBar.fillAmount = value / 100;

    void UpdateStaminaBar(float value)
    {
        _staminaValue = value;
        _staminaBar.fillAmount = _staminaValue / 100; // change 100 to maxStamina
    }
    void UpdateMoleBabiesBar(float value)
    {
        _moleBabiesValue = value;
        _moleBabiesBar.fillAmount = _moleBabiesValue /100; // change 100 to maxMoleBabies value
    }

    void UpdateFoodSavedBar(int value)
    {
        _foodSavedValue += value;
        _foodSavedBar.fillAmount = _foodSavedValue /10; // change 100 to maxMoleBabies value
    }
}