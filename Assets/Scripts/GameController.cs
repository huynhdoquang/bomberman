using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    OnePlayer = 1,
    TowPlayer = 2
}

public class GameController : MonoBehaviour
{
    [SerializeField] private ViewController viewController;
    [SerializeField] private TimeCountDownController timeCountDownController;
    [SerializeField] private PlayerController player_1;
    [SerializeField] private PlayerController player_2;
    [SerializeField] private GameMode gameMode;

    [SerializeField] private GridManagerPlaceTiles grid;

    private int deadPlayers = 0;
    private int deadPlayerNumber = -1;

    public static GameController Inst;


    private void Awake()
    {
        if (Inst == null)
            Inst = this;
    }
    private void Start()
    {
        //Init();
    }

    public void SetGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
    }

    public void SetTime(int time)
    {
        timeCountDownController.SetTime(time);
    }

    public void StartNewGame()
    {
        deadPlayers = 0;
        deadPlayerNumber = -1;
        ImportMap();
        viewController.ShowIngameView();
        timeCountDownController.StarRunTime();
        timeCountDownController.OnTimeCountDownAction = OnTimeCountDown;

        //TODO:
        Init();
    }

    void OnTimeCountDown(int time)
    {
        //TODO:
        viewController.UpdateTimeView(time);

        if (time < 0) CheckPlayersDeath();
    }

    private void UpdatePlayerSttView()
    {
        viewController.UpdateSttView(player_1, player_2);
    }

    void Init()
    {
        player_1.OnPlayerDieAction = OnPlayerDie;
        player_1.OnRefeshSttAction = UpdatePlayerSttView;
        player_1.Init();

        player_2.OnPlayerDieAction = OnPlayerDie;
        player_2.OnRefeshSttAction = UpdatePlayerSttView;
        player_2.Init();
    }
    void OnPlayerDie(PlayerInput playerInput)
    {
        deadPlayers++;
        switch (playerInput)
        {
            case PlayerInput.P1:
                deadPlayerNumber = 1;
                break;
            case PlayerInput.P2:
                deadPlayerNumber = 2;
                break;
            default:
                break;
        }

        CheckPlayersDeath();
    }

    void CheckPlayersDeath()
    {
        if (deadPlayers == 1)
        {
            if (deadPlayerNumber == 1)
            {
                Debug.Log("Player 2 is the winner!");
                viewController.ShowResultView(2);
            }
            else
            {
                Debug.Log("Player 1 is the winner!");
                viewController.ShowResultView(1);
            }
        }
        else
        {
            Debug.Log("The game ended in a draw!");
            viewController.ShowResultView(0);
        }

        timeCountDownController.SetStt(false);
    }

    void ImportMap()
    {
        grid.gameObject.SetActive(true);
        grid.Init();
    }
}
