using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private bool _isWindows; //追加する
    private bool _isMac; //追加する

    public QuizManager _quizManager = null;

    private void Start()
    {
        //InitializeQuestion();

        // 以下を追加する
        if (SystemInfo.operatingSystem.Contains("Windows"))
        {
            _isWindows = true;
        }

        if (SystemInfo.operatingSystem.Contains("Mac"))
        {
            _isMac = true;
        }
    }

    public char GetChar()
    {
        //string inputKey = "";

        //if (Input.anyKeyDown) // 何かキーが押されたとき
        //{
        //    // 入力されたキーを取得
        //    inputKey = Input.GetKeyDown();

        //    if (!char.IsLetterOrDigit(inputKey)) { return; }

        //    // 複数文字が入力される場合があるため、最初に入力された文字を返す
        //    return inputKey[0];
        //}

        // アルファベットのキーに対応する文字を格納する変数
        char input = '\0';

        // A-Zキーを直接チェック
        if (Input.GetKeyDown(KeyCode.A)) input = 'a';
        else if (Input.GetKeyDown(KeyCode.B)) input = 'b';
        else if (Input.GetKeyDown(KeyCode.C)) input = 'c';
        else if (Input.GetKeyDown(KeyCode.D)) input = 'd';
        else if (Input.GetKeyDown(KeyCode.E)) input = 'e';
        else if (Input.GetKeyDown(KeyCode.F)) input = 'f';
        else if (Input.GetKeyDown(KeyCode.G)) input = 'g';
        else if (Input.GetKeyDown(KeyCode.H)) input = 'h';
        else if (Input.GetKeyDown(KeyCode.I)) input = 'i';
        else if (Input.GetKeyDown(KeyCode.J)) input = 'j';
        else if (Input.GetKeyDown(KeyCode.K)) input = 'k';
        else if (Input.GetKeyDown(KeyCode.L)) input = 'l';
        else if (Input.GetKeyDown(KeyCode.M)) input = 'm';
        else if (Input.GetKeyDown(KeyCode.N)) input = 'n';
        else if (Input.GetKeyDown(KeyCode.O)) input = 'o';
        else if (Input.GetKeyDown(KeyCode.P)) input = 'p';
        else if (Input.GetKeyDown(KeyCode.Q)) input = 'q';
        else if (Input.GetKeyDown(KeyCode.R)) input = 'r';
        else if (Input.GetKeyDown(KeyCode.S)) input = 's';
        else if (Input.GetKeyDown(KeyCode.T)) input = 't';
        else if (Input.GetKeyDown(KeyCode.U)) input = 'u';
        else if (Input.GetKeyDown(KeyCode.V)) input = 'v';
        else if (Input.GetKeyDown(KeyCode.W)) input = 'w';
        else if (Input.GetKeyDown(KeyCode.X)) input = 'x';
        else if (Input.GetKeyDown(KeyCode.Y)) input = 'y';
        else if (Input.GetKeyDown(KeyCode.Z)) input = 'z';

        return input; // プレイヤーの入力を返す
    }
}