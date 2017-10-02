// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Singleton GameManager class. Handles game states and provides functions for loading levels and the menu. Bascially invincible
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public enum GameState
    {
        Running, Lost, LevelCleared, LevelMenu, TransitionToRunning, TransitionToLevelMenu
    }
    public GameState gameState = GameState.LevelMenu;
    public int MaxLives = 5;
    public int LifeRemaining { get { return Mathf.Max(0, MaxLives - livesLost); } }

    private GameObject LabelCollectorContainer;
    private GameObject LevelClearedCanvas;
    private GameObject LevelLostCanvas;
    private Text txtLife;
    private List<BubbleCollector> CollectorList;
    private int livesLost;
    private int currentLevelIndex = 1;

    private static readonly string[] levels =
    {
        "dummyLevelToMakeOthersStartAt1",
        "Level1",
        "Level2",
        "Level3",
        "Level4",
        "Level5",
        "Level6",
        "Level7",
        "Level8",
        "Level9",
        "Level10"
    };
    private static readonly bool[] levelsCompleted = new bool[levels.Length];

    void Awake()
    {
        //Singleton Immortal GameManager
        if (instance == null) instance = this;
        else if (instance != this) //any other instance is destroyed and doens't chagne anything
        {
            Destroy(gameObject);
            return;
        }


        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnLevelFinishedLoading;

        //check if we awoke in a level and need running state (happens only in editor)
        var index = Array.IndexOf(levels, SceneManager.GetActiveScene().name);
        if (index > 0)
        {
            currentLevelIndex = index;
            gameState = GameState.TransitionToRunning;
        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        print("loaded " + scene.name);
        InitGame();
    }

    // Use this for initialization
    void InitGame()
    {
        Time.timeScale = 1F;

        switch (gameState)
        {
            case GameState.TransitionToRunning:
                InitializeRunningState();
                break;
            case GameState.TransitionToLevelMenu:
                InitializeLevelMenu();
                break;
            default:
                print("Initgamestate was " + gameState);
                break;
        }
        print("gamestate=" + gameState);
    }

    void InitializeLevelMenu()
    {
        var galaxy1 = GameObject.Find("CanvasGalaxy1");
        if (levelsCompleted[1])
            galaxy1.transform.Find("Planet1").GetComponent<Image>().color = Color.yellow;
        if (levelsCompleted[2])
            galaxy1.transform.Find("Planet2").GetComponent<Image>().color = Color.yellow;
        if (levelsCompleted[3])
            galaxy1.transform.Find("Planet3").GetComponent<Image>().color = Color.yellow;


        var galaxy2 = GameObject.Find("CanvasGalaxy2");
        if (levelsCompleted[4])
            galaxy2.transform.Find("Planet1").GetComponent<Image>().color = Color.blue;
        if (levelsCompleted[5])
            galaxy2.transform.Find("Planet2").GetComponent<Image>().color = Color.blue;
        if (levelsCompleted[6])
            galaxy2.transform.Find("Planet3").GetComponent<Image>().color = Color.blue;

        var galaxy3 = GameObject.Find("CanvasGalaxy3");
        if (levelsCompleted[7])
            galaxy3.transform.Find("Planet1").GetComponent<Image>().color = Color.green;
        if (levelsCompleted[8])
            galaxy3.transform.Find("Planet2").GetComponent<Image>().color = Color.green;
        if (levelsCompleted[9])
            galaxy3.transform.Find("Planet3").GetComponent<Image>().color = Color.green;

        gameState = GameState.LevelMenu;
    }
    void InitializeRunningState()
    {
        var topCanvas = GameObject.Find("Canvas");
        LevelClearedCanvas = FindFirstChildIn(topCanvas, "LevelClearedCanvas"); //does not work if deactivated
        LevelLostCanvas = FindFirstChildIn(topCanvas, "LevelLost");
        txtLife = FindFirstChildIn(topCanvas,"Life").GetComponent<Text>();
        LabelCollectorContainer = GameObject.Find("ColourPalette");
        CollectorList = LabelCollectorContainer.GetComponentsInChildren<BubbleCollector>().ToList();

        livesLost = 0;
        UpdateLivesText();
        gameState = GameState.Running;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.LevelCleared:
                break;
            case GameState.Lost:
                break;
            case GameState.Running:
                Time.timeScale = 1F;
                if (CollectorList.TrueForAll(it => it.ColorBubblesRemaining == 0))
                {
                    OnLevelCompleted();
                }

                //lose the game
                if (LifeRemaining <= 0)
                {
                    OnLoseGame();
                }
                break;
            case GameState.LevelMenu:
                break;
        }

    }

    public void RemoveLife()
    {
        if (gameState != GameState.Running) throw new ArgumentException("illegal state");

        livesLost++;
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        txtLife.text = "Lifes left: " + LifeRemaining;
    }

    private void OnLoseGame()
    {
        gameState = GameState.Lost;
        Time.timeScale = 0F;
        LevelLostCanvas.SetActive(true);
    }

    private void OnLevelCompleted()
    {
        gameState = GameState.LevelCleared;
        print("level completed");
        levelsCompleted[currentLevelIndex] = true;
        Time.timeScale = 0F;
        LevelClearedCanvas.gameObject.SetActive(true);
    }

    public List<BubbleCollector> getColectors()
    {
        return CollectorList;
    }

    public void NextLevel()
    {
        currentLevelIndex++;
        currentLevelIndex = Mathf.Clamp(currentLevelIndex, 1, levels.Length - 1);
        LoadLevel(currentLevelIndex);
    }

    public void LoadMenu()
    {
        gameState = GameState.TransitionToLevelMenu;
        SceneManager.LoadScene("LevelSelection");
    }

    private static GameObject FindFirstChildIn(GameObject parent, string name)
    {
        //includes inactive
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (var t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public Boolean isLevelCompleted(int index)
    {
        return levelsCompleted[index];
    }

    public Boolean isLevelCompleted(String name)
    {
        var index = Array.IndexOf(levels, name);
        if (index < 0) throw new ArgumentException("level unknown");
        return levelsCompleted[index];
    }

    public void LoadLevel(int index)
    {
        currentLevelIndex = index;
        gameState = GameState.TransitionToRunning;
        SceneManager.LoadScene(levels[index]);
    }

    public void LoadLevel(String name)
    {
        var index = Array.IndexOf(levels, name);
        if (index < 0) throw new ArgumentException("level unknown");
        LoadLevel(index);
    }

    public void ReloadLvl()
    {
        LoadLevel(currentLevelIndex);
    }
}