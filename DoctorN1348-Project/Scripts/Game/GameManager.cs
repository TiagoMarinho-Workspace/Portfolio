using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "GameScene";
    public static GameManager Instance { get; private set; }

    [Header("Starting Resources")]
    public int startingCoins = 50;
    public int startingPopulation = 25;

    private int coins;
    public TMP_Text CoinText;
    public TMP_Text PopulationText;


    private int population;
    

    [Header("Item Starting Amounts")]
    public ItemData Cream;
    public int CreamAmount = 3;

    public ItemData Honey;
    public int HoneyAmount = 2;

    public ItemData Syringe;
    public int SyringeAmount = 2;

    public ItemData Vinegar;
    public int VinegarAmount = 3;

    public ItemData Chamomile;
    public int ChamomileAmount = 3;

    public ItemData Mint;
    public int MintAmount = 3;

    public ItemData Bread;
    public int BreadAmount = 2;

    public ItemData Water;
    public int WaterAmount = 5;

    private void Awake()
    {
        CoinText.text = startingCoins.ToString();
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize general gameplay stats
        coins = startingCoins;
        population = startingPopulation;

        // Initialize Inventory
        InitializeInventory();

        PopulationText.text = population.ToString();
    }

    private void InitializeInventory()
    {
        if (Cream != null) Inventory.Instance.AddItem(Cream, CreamAmount);
        if (Honey != null) Inventory.Instance.AddItem(Honey, HoneyAmount);
        if (Syringe != null) Inventory.Instance.AddItem(Syringe, SyringeAmount);
        if (Vinegar != null) Inventory.Instance.AddItem(Vinegar, VinegarAmount);
        if (Chamomile != null) Inventory.Instance.AddItem(Chamomile, ChamomileAmount);
        if (Mint != null) Inventory.Instance.AddItem(Mint, MintAmount);
        if (Bread != null) Inventory.Instance.AddItem(Bread, BreadAmount);
        if (Water != null) Inventory.Instance.AddItem(Water, WaterAmount);

        Debug.Log("Inventory initialized with starting items.");
    }

    // ----------------------
    //     GAME LOGIC
    // ----------------------

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log($"Coins added: {amount}. Total coins: {coins}");
        CoinText.text = coins.ToString();
    }

    public void DecreasePopulation()
    {
        if (population > 0)
        {
            population--;
            Debug.Log($"Population decreased. Current population: {population}");
        }
        else
        {
            Debug.Log("Population is already at zero.");
        }

        PopulationText.text = population.ToString(); // <-- update UI
    }


    public int GetCoinCount()
    {
        return coins;
    }

    public int GetPopulation()
    {
        return population;
    }

    public void Shop()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}

    
