using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameView : MonoBehaviour
{
    [SerializeField] private Text txtTimeleft;
    [SerializeField] private Text txtPause;
    [SerializeField] private PlayerStatusView playerStatus_1;
    [SerializeField] private PlayerStatusView playerStatus_2;

    public void UpdateSttView(PlayerController player_1, PlayerController player_2)
    {
        SetPlayerStt(player_1, playerStatus_1);
        SetPlayerStt(player_2, playerStatus_2);
    }

    void SetPlayerStt(PlayerController player, PlayerStatusView playerStatusView)
    {
        playerStatusView.Init(player.BombQuantity, player.BlastRange, player.PlayerRunSpeed);
    }

    public void UpdateTimeView(int time)
    {
        txtTimeleft.text = time.ToString();
    }

    public void UpdateGamePauseView(bool isPause)
    {
        txtPause.gameObject.SetActive(isPause);
    }
}
