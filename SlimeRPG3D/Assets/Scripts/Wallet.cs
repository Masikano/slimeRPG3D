using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{  

    public static Wallet instance;
    public TMPro.TextMeshProUGUI coinsText;



    [SerializeField]
    private int _coins;
    
   
    public int Coins
    {
        get => _coins;
        set
        {
            _coins = value;
            if (_coins < 0) _coins = 0;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }
    public void AddCoins(int coins)
    {
        Coins += coins;
        coinsText.text = Coins.ToString();
    }

    public void SpendCoins(int coins)
    {
        Coins -= coins;
        coinsText.text = Coins.ToString();
    }

}
