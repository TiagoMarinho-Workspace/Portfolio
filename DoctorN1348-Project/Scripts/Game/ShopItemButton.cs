using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemButton : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public TMP_Text ownedText;
    public Button buyButton;

    private ItemData item;
    private int price;
    private ShopManager shopManager;

    public void Setup(ItemData newItem, int itemPrice, ShopManager manager)
    {
        item = newItem;
        price = itemPrice;
        shopManager = manager;

        itemNameText.text = item.itemName;
        priceText.text = price.ToString();

        UpdateOwnedText();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyItem);
    }

    public void UpdateOwnedText()
    {
        ownedText.text = "x" + Inventory.Instance.GetQuantity(item);
    }

    public void BuyItem()
    {
        Debug.Log($"[SHOP] Buy button clicked for: {item.itemName}, Price: {price}");

        if (Inventory.Instance == null)
        {
            Debug.LogError("[SHOP] ERROR: Inventory.Instance is NULL!");
            return;
        }

        int playerCoins = GameManager.Instance.GetCoinCount();

        Debug.Log($"[SHOP] Player coins before buying: {playerCoins}");

        // Not enough coins
        if (playerCoins < price)
        {
            Debug.Log($"[SHOP] FAILED → Not enough coins to buy {item.itemName}. Required: {price}, Have: {playerCoins}");
            return;
        }

        // Successful purchase
        GameManager.Instance.AddCoins(-price);
        Inventory.Instance.AddItem(item, 1);

        Debug.Log($"[SHOP] SUCCESS → Bought 1x {item.itemName}");
        Debug.Log($"[SHOP] Player coins after buying: {GameManager.Instance.GetCoinCount()}");
        Debug.Log($"[SHOP] New quantity of {item.itemName}: {Inventory.Instance.GetQuantity(item)}");

        // Refresh display
        UpdateOwnedText();
        ShopManager.Instance.RefreshAllItems();
    }
}
