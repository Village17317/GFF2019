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
    public class PlayerWalk : IActorState<Player>
    {
        private float     _speed = 0f;
        private Transform _tf;
        
        public Player Owner { get; set; }
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
        
        public void Run()
        {
            ObserveIdle();
            ObserveJump();
            ObserveShot();
            
            Move();
        }

        /// <summary>
        /// Walk -> Idle
        /// </summary>
        private void ObserveIdle()
        {
            if(Owner.IsMove) { return; }

            Owner.ChengeState(new PlayerIdle(Owner));
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
        /// Idle -> Shot
        /// </summary>
        private void ObserveShot()
        {
            if (!Owner.IsAttack && Input.GetKeyDown(KeyCode.Z))
            {
                Owner.ChengeState(new PlayerShot(Owner));
            }
        }
        
        /// <summary>
        /// 移動
        /// </summary>
        private void Move()
        {           
            float addX = Input.GetAxis("Horizontal") * _speed;
            float addZ = Input.GetAxis("Vertical")   * _speed;
            
            _tf.position += new Vector3(addX,0f,addZ);   
        }
    }
}
