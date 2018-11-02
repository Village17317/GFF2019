/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：
 *更新日     ：
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
        private const string RightHorizontal = "";
        private const string RightVertical   = "";
        private const string ShotKey         = "";
        private const string JumpKey         = "";
        
        private bool _isShotDown = false;

        private static Controller _instance;

        public static Controller Instance
        {
            get { return _instance ?? (_instance = new Controller()); }
        }
        
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        Controller()
        {
            
        }

        public Vector2 LeftGetAxis()
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

        public bool IsShot()
        {
            if (Input.GetAxis(ShotKey).Equals(1f) && !_isShotDown)
            {
                _isShotDown = true;
                return true;
            }
            else if (Input.GetAxis(ShotKey).Equals(0f) && _isShotDown)
            {
                _isShotDown = false;
                return false;
            }

            return false;
        }

        public bool IsJump()
        {
            return Input.GetButtonDown(JumpKey);
        }

        

    }
}
