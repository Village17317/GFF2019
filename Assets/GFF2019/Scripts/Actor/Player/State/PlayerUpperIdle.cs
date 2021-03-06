﻿/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/10/29
 *更新日     ：2018/10/29
*/
using UnityEngine;

namespace Village
{
    public class PlayerUpperIdle : IActorUpperState<Player>
    {
        public Player Owner     { get; set; }
        public string StateName { get{ return "Idle"; } }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerUpperIdle(Player owner)
        {
            Owner = owner;
        }
        
        public void Execute()
        {
            ObserveShot();
        }

        /// <summary>
        /// Idle -> shot
        /// </summary>
        private void ObserveShot()
        {
            if (!Owner.IsCharge) { return; }

            Owner.ChangeUpperState(new PlayerShot(Owner));                   
        }        
    }
}
