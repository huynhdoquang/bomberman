﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCountDownController : MonoBehaviour
{
    int timeLeft = 30; //Seconds Overall

    public System.Action<int> OnTimeCountDownAction;
    //Simple Coroutine
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            if (OnTimeCountDownAction != null)
                OnTimeCountDownAction.Invoke(timeLeft);
        }
    }

    public void StarRunTime()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }

    public void SetTime(int time) {
        timeLeft = time;
    }
}