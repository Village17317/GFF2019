/*作成者     ：村上 和樹
 *機能説明   ：Playerの待機状態
 *初回作成日 ：2018/10/27
 *更新日     ：2018/10/29
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class PlayerLowerIdle : IActorLowerState<Player>
    {
        public Player Owner     { get; set; }
        public string StateName { get{ return "Idle"; } }

        private Transform _tf;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerLowerIdle(Player owner)
        {
            Owner = owner;
            _tf   = Owner.gameObject.transform;
        }
    
        public void Execute()
        {
            ObserveWalk();
            ObserveJump();
            
            ForwardAim();
        }

        /// <summary>
        /// Idle -> Walk
        /// </summary>
        private void ObserveWalk()
        {
            if(!Owner.IsMove) { return; }
            
            Owner.ChangeLowerState(new PlayerWalk(Owner));
        }

        /// <summary>
        /// Idle -> Jump
        /// </summary>
        private void ObserveJump()
        {
            if (!Owner.IsJump) { return; }
            
            Owner.ChangeLowerState(new PlayerJump(Owner));
        }
        
        /// <summary>
        /// 進行方向を向く
        /// </summary>
        private void ForwardAim()
        {
            if (Controller.Instance.IsCharge())
            {
                Vector3 forward = Owner.MyCamera.Forward;
                forward.y       = _tf.forward.y;
                
                _tf.LookAt(_tf.position + forward);

                _tf.Rotate(0,Controller.Instance.RightAxis().x,0);
            }
        }
    }
}
