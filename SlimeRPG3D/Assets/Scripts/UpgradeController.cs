using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using static UpgradeManager;

public class UpgradeController : MonoBehaviour
{
    
    private Button _button;
    private TextMeshProUGUI _buttonText;
    [SerializeField]
    private int _cost;
    [SerializeField]
    private int _value;

    public int Cost { get { return _cost; } set { _cost = value;} }
    public int Value { get { return _value; } set { _value = value; } }
    private void Start()
    {
        _button = GetComponentInChildren<Button>();
        _buttonText = _button.GetComponentInChildren<TextMeshProUGUI>();
        SetCost(0);
        
        
    }
    private void Update()
    {
        if(_cost > Wallet.instance.Coins)
        {
            _button.enabled = false;
        }
        else
        {
            _button.enabled = true;
        }
    }
    public int Buy(int upgradeValue, int upgradeCost)
    {
        Wallet.instance.SpendCoins(_cost);
        SetValue(upgradeValue);
        SetCost(upgradeCost);
        return _value;
    }
    private void SetValue(int upgradeValue)
    {
        _value += upgradeValue;
    }
    public void SetCost(int upgradeCost)
    {
        _cost += upgradeCost;
        _buttonText.text = _cost.ToString();
    }
}
