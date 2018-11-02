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
        private float     _speed = 0f;
        private Transform _tf;

        public Player Owner     { get; set; }
        public string StateName { get { return "Walk"; } }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerWalk(Player owner)
        {
            Owner  = owner;
            _tf    = owner.gameObject.transform;
            _speed = owner.State.Speed;
        }
        
        public void Execute()
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

            Owner.ChengeState(new PlayerLowerIdle(Owner));
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
        
        /// <summary>
        /// 移動
        /// </summary>
        private void Move()
        {           
            ForwardAim();
            
            float addX = Controller.Instance.LeftGetAxis().x * _speed;
            float addZ = Controller.Instance.LeftGetAxis().y * _speed;
            
            _tf.position += new Vector3(addX,0,addZ); 
            
            
            
            
        }

        /// <summary>
        /// 進行方向を向く
        /// </summary>
        private void ForwardAim()
        {
            if(Input.GetAxis("Horizontal").Equals(0f)) { return; }
            if(Input.GetAxis("Vertical").Equals(0f))   { return; }

            float x = Input.GetAxis("Horizontal") * 10;
            float z = Input.GetAxis("Vertical")   * 10;

            Vector3 dir    = _tf.position + new Vector3(x,0,z);

            _tf.LookAt(dir);
        }
    }
}
