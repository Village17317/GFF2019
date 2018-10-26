/*作成者     ：村上 和樹
 *機能説明   ： Playerの処理
 *初回作成日 ： 2018/10/26
 *更新日     ：2018/10/26
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class Player : Inheritor
    {

        [SerializeField] private ActorState _myState;
        
        ///<summary>
        /// 初期起動時
        ///</summary>
        private void Awake ()
        {
            
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public override void Run ()
        {
            
        }


    }
}
