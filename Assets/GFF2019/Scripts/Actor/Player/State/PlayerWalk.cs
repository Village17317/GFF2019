/*作成者     ：村上 和樹
 *機能説明   ：Playerの歩行状態
 *初回作成日 ：2018/10/27
 *更新日     ：2018/10/29
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class PlayerWalk : IActorLowerState<Player>
    {
        private Transform _tf;
        private float     _speed = 0f;

        public Player Owner     { get; set; }
        public virtual string StateName { get { return "Walk"; } }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerWalk(Player owner)
        {
            Owner  = owner;
            _tf    = owner.gameObject.transform;
            _speed = owner.State.Speed;
        }
        
        public virtual void Execute()
        {
            ObserveIdle();
            ObserveJump();
            
            Move();
        }

        /// <summary>
        /// Walk -> Idle
        /// </summary>
        private void ObserveIdle()
        {
            if(Owner.IsMove) { return; }

            Owner.ChangeLowerState(new PlayerLowerIdle(Owner));
        }
        
        /// <summary>
        /// Idle -> Jump
        /// </summary>
        private void ObserveJump()
        {
            if (Owner.IsJump)
            {
                Owner.ChangeLowerState(new PlayerJump(Owner));
            }
        }
        
        /// <summary>
        /// 移動
        /// </summary>
        protected void Move()
        {           
            ForwardAim();
                        
            _tf.position += MoveDirection() * _speed;            
        }

        /// <summary>
        /// 進行方向を向く
        /// </summary>
        private void ForwardAim()
        {
            if(Controller.Instance.LeftAxis().Equals(Vector2.zero)) { return; }
            
            _tf.LookAt(_tf.position + MoveDirection());
        }

        /// <summary>
        /// 移動方向を決める
        /// </summary>
        /// <returns></returns>
        private Vector3 MoveDirection()
        {
            //想定した横方向
            float x = Controller.Instance.LeftAxis().x;
            //想定した前方向
            float z = Controller.Instance.LeftAxis().y;
            
            //カメラから見た方向に直す
            Vector3 dir = (Owner.CameraRight * x + Owner.CameraForwerd * z);

            //Y軸方向にも移動してしまうので0で補正
            dir.y = 0f;

            return dir;
        }
    }
}
