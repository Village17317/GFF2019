﻿/*作成者     ：村上 和樹
 *機能説明   ：入力
 *初回作成日 ：2018/11/01
 *更新日     ：2018/11/02
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class Controller : IController
    {
        private const string LeftHorizontal  = "Horizontal";
        private const string LeftVertical    = "Vertical";
        private const string RightHorizontal = "Horizontal2";
        private const string RightVertical   = "Vertical2";
        private const string ShotKey         = "Shot";
        private const string JumpKey         = "Jump";
        private const string TargetAimingKey = "TargetAiming";
        
        private bool _isShotDown = false;

        private static Controller _instance;

        /// <summary>
        /// シングルトンインスタンス
        /// </summary>
        public  static Controller Instance
        {
            get { return _instance ?? (_instance = new Controller()); }
        }
               
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Controller() { }

        public Vector2 LeftAxis()
        {
            return Axis(LeftHorizontal, LeftVertical);
        }

        public Vector2 RightAxis()
        {
            return Axis(RightHorizontal, RightVertical);        
        }

        public Vector2 Axis(string xKey, string yKey)
        {
            return new Vector2()
                   {
                       x = Input.GetAxis(xKey),
                       y = Input.GetAxis(yKey)
                   };
        }

        public bool IsCharge()
        {
            if (Input.GetAxis(ShotKey) >= 1f)
            {
                _isShotDown = true;
            }
            
            return _isShotDown;
        }

        public bool IsShot()
        {
            if (Input.GetAxis(ShotKey) <= 0f && _isShotDown)
            {
                _isShotDown = false;
                return true;
            }

            return false;
        }

        public bool IsJump()
        {
            return Input.GetButtonDown(JumpKey);
        }

        public bool IsTargetAiming()
        {
            return Input.GetAxis(TargetAimingKey) >= 1f;
        }
    }
}
