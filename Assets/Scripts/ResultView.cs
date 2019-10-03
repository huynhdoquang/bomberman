using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    [SerializeField] private Text txtP1Win;
    [SerializeField] private Text txtP2Win;
    [SerializeField] private Text txtDraw;

    private void Start()
    {
        txtP1Win.gameObject.SetActive(false);
        txtP2Win.gameObject.SetActive(false);
        txtDraw.gameObject.SetActive(false);
    }

    public void SetResult(int result)
    {
        txtP1Win.gameObject.SetActive(false);
        txtP2Win.gameObject.SetActive(false);
        txtDraw.gameObject.SetActive(false);

        if (result == 1) txtP1Win.gameObject.SetActive(true);
        if (result == 2) txtP2Win.gameObject.SetActive(true);
        if (result == 0) txtDraw.gameObject.SetActive(true);
    }
}
