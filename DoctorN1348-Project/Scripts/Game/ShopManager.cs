using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopEntry
{
    public ItemData item;   // ScriptableObject
    public int price = 1;
}

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;   // ← ADICIONADO (agora é Singleton)

    public GameObject shopPanel;
    public TextMeshProUGUI shopButtonText;

    public Transform listParent;           
    public GameObject shopItemPrefab;      
    public List<ShopEntry> shopItems;      

    private void Awake()
    {
        shopButtonText.text = "Open Shop";

        // Singleton para funcionar em todas as cenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);   // ← loja permanece entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (shopPanel != null) shopPanel.SetActive(false);
    }

    public void ToggleShop()
    {
        bool isActive = shopPanel.activeSelf;
        shopPanel.SetActive(!isActive);

        if (!isActive)
        {
            InputLock.shopOpen = true;
            PopulateShop();
            shopButtonText.text = "Close Shop";
        }
        else
        {
            InputLock.shopOpen = false;
            shopPanel.SetActive(false);

            foreach (Transform t in listParent)
                Destroy(t.gameObject);

            shopButtonText.text = "Open Shop";
        }
    }

    private void PopulateShop()
    {
        // limpa anteriores
        foreach (Transform t in listParent)
            Destroy(t.gameObject);

        foreach (var entry in shopItems)
        {
            GameObject go = Instantiate(shopItemPrefab, listParent);
            ShopItemButton button = go.GetComponent<ShopItemButton>();

            if (button != null)
            {
                // Setup agora também envia o ShopManager.Instance
                button.Setup(entry.item, entry.price, this);
            }
        }
    }

    // Chamado quando um item é comprado
    public void BuyItem(ItemData item, int price)
    {
        if (GameManager.Instance.GetCoinCount() < price)
        {
            Debug.Log("Not enough coins!");
            return;
        }

        GameManager.Instance.AddCoins(-price);
        Inventory.Instance.AddItem(item, 1);

        // Atualiza tela da loja
        RefreshAllItems();
    }

    public void OnItemBought(ItemData item)
    {
        RefreshAllItems();
    }

    public void RefreshAllItems()
    {
        foreach (Transform child in listParent)
        {
            ShopItemButton btn = child.GetComponent<ShopItemButton>();
            if (btn != null)
                btn.UpdateOwnedText();
        }
    }
}
