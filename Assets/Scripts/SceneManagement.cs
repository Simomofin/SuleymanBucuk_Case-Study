using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    public bool GameBegan { get; set; } = false;
    private PlayerController playerController;

    [SerializeField]
    private Slider diamountAmountSlider;
    [SerializeField]
    private TextMeshProUGUI diamountAmountText;    
    public TextMeshProUGUI currencyText;
    [SerializeField]
    private TextMeshProUGUI currentLevelText;
    [SerializeField]
    private GameObject levelEndPanel;
    [SerializeField]
    private TextMeshProUGUI goldCollectedInCurrentLevelText;
    [SerializeField]
    private GameObject playerCanvas;
    [SerializeField]
    private GameObject tabToStartPanel;
    [SerializeField]
    private TextMeshProUGUI currencyNeedsToUpgradeText;

    private void OnEnable()
    {
        GameManager.instance.OnDiamondAmountChanged += SetDiamondSliderAndTextVariables;
        GameManager.instance.OnCurrencyAmountChanged += SetCurrenctTextVariable;
    }
    private void OnDisable()
    {
        GameManager.instance.OnDiamondAmountChanged -= SetDiamondSliderAndTextVariables;
        GameManager.instance.OnCurrencyAmountChanged -= SetCurrenctTextVariable;
    }

    // Start is called before the first frame update
    void Start()
    {        
        playerController = GameObject.FindGameObjectWithTag(Helper.Tags.player).GetComponent<PlayerController>();
        HandleUIVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    } // Update

    public void SetDiamondSliderAndTextVariables(int diamondAmount)
    {
        diamountAmountSlider.value = diamondAmount;
        diamountAmountText.text = diamondAmount.ToString();
    } // SetSliderAndTextVariables

    private void SetCurrenctTextVariable(int amount)
    {
        currencyText.text = amount.ToString();
    } // SetCurrenctTextVariable

    private void HandleUIVariables()
    {
        currencyText.text = GameManager.instance.TotalCurrencyAmount.ToString();
        currentLevelText.text = GameManager.instance.CurrentLevel.ToString();
        levelEndPanel.SetActive(false);
        playerCanvas.SetActive(true);
        SetDiamondSliderAndTextVariables(GameManager.instance.StartStackAmount); // get startstack amount from playerprefs and set it on UI
        tabToStartPanel.SetActive(true);
        currencyNeedsToUpgradeText.text = GameManager.instance.CurrencyNeedsToUpgradeStack.ToString();
        GameManager.instance.DiamondStackAmount = GameManager.instance.StartStackAmount;
    } // HandleUIVariables

    public void GameHasFinished()
    {
        GameManager.instance.CurrentLevel++;
        GameManager.instance.SavePlayerPrefs();
        levelEndPanel.gameObject.SetActive(true);
        goldCollectedInCurrentLevelText.text = GameManager.instance.CurrencyAmountInThislevel.ToString();
        playerCanvas.SetActive(false);
        //GameManager.instance.DiamondStackAmount = 0;
        GameManager.instance.CurrencyAmountInThislevel = 0;
        GameManager.instance.StackFullfilled = false;
    } // GameHasFinished

    public void OnnextLevelButtonPressed()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIndex++;

        if (sceneIndex == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(sceneIndex);
    } // OnnextLevelButtonPressed

    public void OnStartButtonPressed()
    {
        if (!GameBegan)
        {
            GameBegan = true;
            //if stack is fullfilled on tabtostart screen start running on the beginning.
            if (!GameManager.instance.StackFullfilled)
            {
                playerController.GetComponent<Animator>().SetBool(Helper.AnimationParameters.run1, true);
                playerController.GetComponent<Animator>().SetBool(Helper.AnimationParameters.run2, false);
            }
            else
            {
                playerController.GetComponent<Animator>().SetBool(Helper.AnimationParameters.run2, true);
                playerController.GetComponent<Animator>().SetBool(Helper.AnimationParameters.run1, false);
                playerController.speedMultiplier = 1.5f;

            }
        }
        tabToStartPanel.SetActive(false);
    } // OnStartButtonPressed

    public void OnUpgradeStackButtonPressed()
    {
        if (GameManager.instance.StartStackUpgrade())
            currencyNeedsToUpgradeText.text = GameManager.instance.CurrencyNeedsToUpgradeStack.ToString();
        else
            return;

        //if enough currency 
        //if (GameManager.instance.TotalCurrencyAmount >= GameManager.instance.CurrencyNeedsToUpgradeStack && GameManager.instance.DiamondStackAmount < GameManager.instance.maxDiamountAmount)
        //{
        //    GameManager.instance.TotalCurrencyAmount -= GameManager.instance.CurrencyNeedsToUpgradeStack;
        //    GameManager.instance.CurrencyNeedsToUpgradeStack *= 2;
        //    GameManager.instance.IncreaseStackAmount(1, false);
        //    GameManager.instance.StartStackAmount = GameManager.instance.DiamondStackAmount;
        //    //Handle UI
        //    SetCurrenctTextVariable(GameManager.instance.TotalCurrencyAmount);
        //    currencyNeedsToUpgradeText.text = GameManager.instance.CurrencyNeedsToUpgradeStack.ToString();
        //}
        //else if (GameManager.instance.TotalCurrencyAmount >= GameManager.instance.CurrencyNeedsToUpgradeStack && GameManager.instance.DiamondStackAmount >= GameManager.instance.maxDiamountAmount)
        //    Debug.Log("You have maximum stack amount");
        //else
        //    Debug.Log("Not enough coins");
    } // OnUpgradeStackButtonPressed
} // Class
