using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Used actions instead of SceneManagement variable bc these variable are lost when scene changes.
    public Action OnDiamondStackFullfilled;
    public Action OnDiamondStackNOTFullfilled;
    public Action<int> OnDiamondAmountChanged;
    public Action<int> OnCurrencyAmountChanged;

    
    public int DiamondStackAmount { get; set; }
    public int TotalCurrencyAmount { get; set; }
    public int CurrencyAmountInThislevel { get; set; }
    public int CurrencyNeedsToUpgradeStack { get; set; }
    public int StartStackAmount { get; set; }
    public bool StackFullfilled { get; set; } = false;
    public int CurrentLevel { get; set; }

    public int maxDiamountAmount = 10;         

    //This awake function was being called after PlayerController->OnEnable() func, so nullreferenceexecpiton was throwed.
    // Change script execution order from edit->Projectsettings to execute them in order.
    private void Awake()
    {
        if(instance == null)                    
            instance = this;                   
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);        
    } // Awake

    private void OnApplicationQuit()
    {
        SavePlayerPrefs(); // On app quit, save playerprefs to not lose any data.

        //PlayerPrefs.DeleteAll(); // For test, comment if not needed
    } // OnApplicationQuit

    // Start is called before the first frame update
    void Start()
    {        
        HandlePlayerPrefs();                
    } // Start

    public void IncreaseStackAmount(int amount, bool gameHasBegan)
    {
        DiamondStackAmount += amount;
        DiamondStackAmount = DiamondStackAmount <= maxDiamountAmount ? DiamondStackAmount : maxDiamountAmount; // Maximum 10 diamond can be stacked

        if (!StackFullfilled && DiamondStackAmount >= maxDiamountAmount && gameHasBegan)
        {
            OnDiamondStackFullfilled?.Invoke();
            StackFullfilled = true;
        }
        //if stack is fulled on tab to start screen, don't fire event
        else if(!StackFullfilled && DiamondStackAmount >= maxDiamountAmount && !gameHasBegan)
        {
            StackFullfilled = true;
        }
        OnDiamondAmountChanged(DiamondStackAmount);
    } // IncreaseDiamondAmount  

    public void DecreaseStackAmount(int amount)
    {
        if(DiamondStackAmount > 0) // to prevent negative values
            DiamondStackAmount -= amount;
        
        if (StackFullfilled && DiamondStackAmount < maxDiamountAmount)
        {
            OnDiamondStackNOTFullfilled?.Invoke();
            StackFullfilled = false;
        }
        OnDiamondAmountChanged(DiamondStackAmount);
    } // DecreaseDiamoundAmount
   
    public void IncreaseCurrency(int amount)
    {
        CurrencyAmountInThislevel += amount;
        TotalCurrencyAmount += amount;
        //Set curreny amount text UI
        OnCurrencyAmountChanged(TotalCurrencyAmount);
    } // IncreaseCurrency

    private void HandlePlayerPrefs()
    {
        //check if current level saved on playerpref and set it's value
        if (!PlayerPrefs.HasKey(Helper.PlayerPrefs.currentLevel))
        {
            CurrentLevel = 1;
            PlayerPrefs.SetInt(Helper.PlayerPrefs.currentLevel, CurrentLevel);
        }
        else
            CurrentLevel = PlayerPrefs.GetInt(Helper.PlayerPrefs.currentLevel);
        //check if currency amount saved on playerpref and set it' s value
        if (!PlayerPrefs.HasKey(Helper.PlayerPrefs.currencyAmount))
        {
            TotalCurrencyAmount = 0;
            PlayerPrefs.SetInt(Helper.PlayerPrefs.currencyAmount, TotalCurrencyAmount);
        }
        else
            TotalCurrencyAmount = PlayerPrefs.GetInt(Helper.PlayerPrefs.currencyAmount);
        //check if currencyNeedsToUpgradeStack amount on playerpref and set it's value
        if (!PlayerPrefs.HasKey(Helper.PlayerPrefs.currencyNeedsToUpgradeStack))
        {
            CurrencyNeedsToUpgradeStack = 100;
            PlayerPrefs.SetInt(Helper.PlayerPrefs.currencyNeedsToUpgradeStack, CurrencyNeedsToUpgradeStack);
        }
        else
            CurrencyNeedsToUpgradeStack = PlayerPrefs.GetInt(Helper.PlayerPrefs.currencyNeedsToUpgradeStack);
        if(!PlayerPrefs.HasKey(Helper.PlayerPrefs.startStackAmount))
        {
            StartStackAmount = 0;
            PlayerPrefs.SetInt(Helper.PlayerPrefs.startStackAmount, StartStackAmount);
        }
        else
            StartStackAmount = PlayerPrefs.GetInt(Helper.PlayerPrefs.startStackAmount);

        DiamondStackAmount = StartStackAmount;
        Debug.Log("Diamond Amount: " + DiamondStackAmount + " start stack: " + StartStackAmount);

        //For test, comment out if not needed
        //TotalCurrencyAmount = 100000;                
    } // HandlePlayerPrefs

    public bool StartStackUpgrade()
    {
        if (TotalCurrencyAmount >= CurrencyNeedsToUpgradeStack && DiamondStackAmount < maxDiamountAmount)
        {
            TotalCurrencyAmount -= CurrencyNeedsToUpgradeStack;
            CurrencyNeedsToUpgradeStack *= 2;
            IncreaseStackAmount(1, false);
            StartStackAmount++;
            //Handle UI
            OnCurrencyAmountChanged(TotalCurrencyAmount);
            return true;
        }
        else if (TotalCurrencyAmount >= CurrencyNeedsToUpgradeStack && DiamondStackAmount >= maxDiamountAmount)
        {
            Debug.Log("You have maximum stack amount");
            return false;
        }
        else
        {
            Debug.Log("Not enough coins");
            return false;
        }
    } // StartStackUpgrade

    //To remember where we leaved after re-openned app
    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt(Helper.PlayerPrefs.currentLevel, CurrentLevel);
        PlayerPrefs.SetInt(Helper.PlayerPrefs.currencyAmount, TotalCurrencyAmount);
        PlayerPrefs.SetInt(Helper.PlayerPrefs.currencyNeedsToUpgradeStack, CurrencyNeedsToUpgradeStack);
        PlayerPrefs.SetInt(Helper.PlayerPrefs.startStackAmount, StartStackAmount);
    } // SavePlayerPrefs      

} // Class
