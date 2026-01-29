using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int Water;
    public int Vinegar;
    public int Syringe;
    public int Cream;
    public int Mint;
    public int Chamomile;
    public int Honey;
    public int Bread;
    
    
    public Text Coin_text;
    public Text Water_text;
    public Text Vinegar_text;
    public Text Syringe_text;
    public Text Cream_text;
    public Text Mint_text;
    public Text Chamomile_text;
    public Text Honey_text;
    public Text Bread_text;

    void Start()    
    {
        UpdateUI();
        /*Coin = PlayerPrefs.GetInt("Coin");
        Water = PlayerPrefs.GetInt("Water");
        Vinegar = PlayerPrefs.GetInt("Vinegar");
        Syringe = PlayerPrefs.GetInt("Syringe");
        Cream = PlayerPrefs.GetInt("Cream");
        Mint = PlayerPrefs.GetInt("Mint");
        Chamomile = PlayerPrefs.GetInt("Chamomile");
        Honey = PlayerPrefs.GetInt("Honey");
        Bread = PlayerPrefs.GetInt("Bread");*/

    }

    void UpdateUI()
    {
        // Atualiza sempre o texto das moedas
        Coin_text.text = GameManager.Instance.GetCoinCount().ToString();
        Water_text.text = Water.ToString();
        Vinegar_text.text = Vinegar.ToString();
        Syringe_text.text = Syringe.ToString();
        Cream_text.text = Cream.ToString();
        Mint_text.text = Mint.ToString();
        Chamomile_text.text = Chamomile.ToString();
        Honey_text.text = Honey.ToString();
        Bread_text.text = Bread.ToString();
    }

    public void BuyWater() //Water
    {
        if (GameManager.Instance.GetCoinCount() >= 5)
        {
            GameManager.Instance.AddCoins(-5); // Remove 5 moedas
            Water += 1;
            UpdateUI();
        }
        else
        {
            print("Moedas insuficientes.");
        }
    }

    public void BuyVinegar() //Vinegar
    {
        if (GameManager.Instance.GetCoinCount() >= 10)
        {
            GameManager.Instance.AddCoins(-10);
            Vinegar += 1;
            UpdateUI();
        }
        else
        {
            print("Moedas insuficientes.");
        }
    }
    
    public void BuySyringe() //Syringe
    {
        if (GameManager.Instance.GetCoinCount() >= 15)
        {
            GameManager.Instance.AddCoins(-15);
            Syringe += 1;
            UpdateUI();
        }
        else
        {
            print("Moedas insuficientes.");
        }
    }

    public void BuyCream() //Cream
    {
        if (GameManager.Instance.GetCoinCount() >= 20)
        {
            GameManager.Instance.AddCoins(-20);
            Cream += 1;
            UpdateUI();
        }
        else
        {
            print("Moedas insuficientes.");
        }
    }

    public void BuyMint() //Mint
    {
        if (GameManager.Instance.GetCoinCount() >= 10)
        {
            GameManager.Instance.AddCoins(-10);
            Mint += 1;
            UpdateUI();
        }
        else
        {
            print("Moedas insuficientes.");
        }
    }

    public void BuyChamomile() //Chamomile
    {
        if (GameManager.Instance.GetCoinCount() >= 10)
        {
            GameManager.Instance.AddCoins(-10);
            Chamomile += 1;
            UpdateUI();
        }
        else
        {
            print("Moedas insuficientes.");
        }
    }

    public void BuyHoney() //Honey
    {
        if (GameManager.Instance.GetCoinCount() >= 15)
        {
            GameManager.Instance.AddCoins(-15);
            Honey += 1;
            UpdateUI();
        }
        else
        {
            print("Moedas insuficientes.");
        }
    }

    public void BuyBread() //Bread
    {
        if (GameManager.Instance.GetCoinCount() >= 10)
        {
            GameManager.Instance.AddCoins(-10);
            Bread += 1;
            UpdateUI();
        }
        else
        {
            print("Moedas insuficientes.");
        }
    }
}
