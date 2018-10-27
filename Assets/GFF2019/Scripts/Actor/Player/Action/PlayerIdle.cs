/*作成者     ：村上 和樹
 *機能説明   ：Playerの待機状態
 *初回作成日 ：2018/10/27
 *更新日     ：2018/10/27
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class PlayerIdle : IActorAction<Player>
    {
        public Player Owner { get; set; }
        public string StateName { get { return "Idle"; } }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerIdle(Player owner)
        {
            Owner = owner;
        }
    
        public void Run()
        {
            ObserveWalk();
            ObserveJump();
        }

        /// <summary>
        /// Idle -> Walk
        /// </summary>
        private void ObserveWalk()
        {
            if(!Owner.IsMove) { return; }
            
            Owner.ChengeState(new PlayerWalk(Owner));
        }

        /// <summary>
        /// Idle -> Jump
        /// </summary>
        private void ObserveJump()
        {
            if (Owner.IsGround && Input.GetKeyDown(KeyCode.Space))
            {
                Owner.ChengeState(new PlayerJump(Owner));
            }
        }
        
    }
}
