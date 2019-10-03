using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TittleView : MonoBehaviour
{
    [SerializeField] private Dropdown dropdownTimeSelect;
    [SerializeField] private Dropdown dropdownGameModeSelect;

    [SerializeField] private Button btnPlay;

    [SerializeField] private Button btnAdjustMap;

    [SerializeField] private ResultView resultView;

    public System.Action OnClickAdjustMap;

    private void Start()
    {
        dropdownTimeSelect.onValueChanged.RemoveListener(OnDropDownTimeSelect);
        dropdownTimeSelect.onValueChanged.AddListener(OnDropDownTimeSelect);

        dropdownGameModeSelect.onValueChanged.RemoveListener(OnDropDownGameModeSelect);
        dropdownGameModeSelect.onValueChanged.AddListener(OnDropDownGameModeSelect);

        btnPlay.onClick.RemoveListener(OnStartGame);
        btnPlay.onClick.AddListener(OnStartGame);

        btnAdjustMap.onClick.RemoveListener(AdjustMap);
        btnAdjustMap.onClick.AddListener(AdjustMap);

        //Set Default
        dropdownGameModeSelect.value = 0;
        dropdownTimeSelect.value = 0;

        resultView.gameObject.SetActive(false);
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
        OnDropDownTimeSelect(dropdownTimeSelect.value);
        OnDropDownGameModeSelect(dropdownGameModeSelect.value);
        //Start game, init map and game ui
        GameController.Inst.StartNewGame();
    }

    void AdjustMap()
    {
        if (OnClickAdjustMap != null) OnClickAdjustMap.Invoke();
    }

    public void ShowResult(int result)
    {
        resultView.gameObject.SetActive(true);
        resultView.SetResult(result);
    }
}
