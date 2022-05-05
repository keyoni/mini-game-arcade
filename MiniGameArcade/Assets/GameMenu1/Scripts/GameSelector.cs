using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSelector : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] gamesToSelect;
    [SerializeField] private DisplayGame displayGame;
    private int _index;


    private void Awake()
    {
        _index = 0;
        changeSelectedGame(0);
    }

    public void changeSelectedGame(int change)
    {
        _index += change;

        //If user goes pass the first game, loop back to the last one.
        if (_index < 0) 
        {
            _index = gamesToSelect.Length - 1;
            //If user goes pass the last game, loop back to the first one.
        } else if (_index > gamesToSelect.Length - 1 )
        {
            _index = 0;
        }

        //Display game at current index
        if (displayGame != null)
        {
            displayGame.GameSelectionDisplay((GameSelectionScriptable) gamesToSelect[_index]);
        }
    }


}

