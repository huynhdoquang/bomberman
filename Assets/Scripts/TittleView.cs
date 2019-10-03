using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TittleView : MonoBehaviour
{
    [SerializeField] private Dropdown dropdownTimeSelect;
    [SerializeField] private Dropdown dropdownGameModeSelect;

    [SerializeField] private Button btnPlay;

    private void Start()
    {
        dropdownTimeSelect.onValueChanged.RemoveListener(OnDropDownTimeSelect);
        dropdownTimeSelect.onValueChanged.AddListener(OnDropDownTimeSelect);

        dropdownGameModeSelect.onValueChanged.RemoveListener(OnDropDownGameModeSelect);
        dropdownGameModeSelect.onValueChanged.AddListener(OnDropDownGameModeSelect);

        btnPlay.onClick.RemoveListener(OnStartGame);
        btnPlay.onClick.AddListener(OnStartGame);

        //Set Default
        dropdownGameModeSelect.value = 0;
        dropdownTimeSelect.value = 0;
    }

    void OnDropDownTimeSelect(int choice)
    {
        if (choice == 0)
            GameController.Inst.SetTime(30);
        if (choice == 1)
            GameController.Inst.SetTime(60);
        if (choice == 2)
            GameController.Inst.SetTime(90);
    }

    void OnDropDownGameModeSelect(int choice)
    {
        if (choice == 0)
            GameController.Inst.SetGameMode(GameMode.OnePlayer);
        if (choice == 1)
            GameController.Inst.SetGameMode(GameMode.TowPlayer);
    }

    void OnStartGame()
    {
        //Start game, init map and game ui
        GameController.Inst.StartNewGame();
    }
}
