using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchControl
{
    private static bool firstMove;
    private static Vector3 movePosition;
    private static float _firstFingerPositionX, _lastFingerPositionX;
    private static float _newMoveX, _moveX;
    private static readonly float playerSpeed = 3.33f;

    public static Vector3 Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstMove = true;
            _firstFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            if (firstMove)
            {
                _lastFingerPositionX = _firstFingerPositionX;
                firstMove = false;
            }
            else
            {
                _newMoveX = Input.mousePosition.x - _lastFingerPositionX; // movement on x axis on screen, moves player on x axis straight
                _moveX = Mathf.Lerp(_moveX, _newMoveX, Time.fixedDeltaTime * 2); // smoothing
                _moveX = Mathf.Clamp(_moveX, -3f, 3f); // clamping for smoothing
                _lastFingerPositionX = Input.mousePosition.x;
            }
        }
        else
        {
            //Reset values, if you dont, rb moves in last direction      
            _moveX = 0;
            _firstFingerPositionX = 0;
            _lastFingerPositionX = 0;
        }        

        movePosition = new Vector3(_moveX, 0, playerSpeed);      

        return movePosition;
    } // Movement
} // Class
