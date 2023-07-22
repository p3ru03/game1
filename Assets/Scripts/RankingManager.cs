using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{

    public PlayFabController pfContoroller;

    // Start is called before the first frame update
    void Start()
    {
        pfContoroller.RequestLeaderBoard();
        pfContoroller.GetLeaderboardAroundPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
