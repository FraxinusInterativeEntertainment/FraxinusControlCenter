using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusVO
{
    public string gameId { get; set; }
    public GameStatus gameStatus { get; set; }
    public string gameTime { get; set; }


    public GameStatusVO()
    {
        gameId = "";
        gameStatus = GameStatus.c;
    }

    public GameStatusVO(string _gameId, GameStatus _gameStatus)
    {
        gameId = _gameId;
        gameStatus = _gameStatus;
    }

    public GameStatusVO(string _gameId, GameStatus _gameStatus, string _gameTime)
    {
        gameId = _gameId;
        gameStatus = _gameStatus;
        gameTime = _gameTime;
    }
}
