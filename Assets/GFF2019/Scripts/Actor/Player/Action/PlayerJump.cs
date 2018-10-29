/*作成者     ：村上 和樹
 *機能説明   ：Playerのジャンプ状態
 *初回作成日 ：2018/10/27
 *更新日     ：2018/10/29
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class PlayerJump : IActorState<Player>
    {
        private Transform _tf;
        private Rigidbody _rigid;
        private float     _speed = 0f; 
        private float     _jumpForce = 10f;
               
        public Player Owner { get; set; }
        public string StateName { get { return "Jump"; } }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerJump(Player owner)
        {
            Owner  = owner;
            _tf    = owner.gameObject.transform;
            _rigid = owner.GetComponent<Rigidbody>();
            _speed = owner.State.Speed;
            
            _rigid.AddForce(Vector3.up * _jumpForce,ForceMode.Impulse);
        }
        
        public void Run()
        {
            ObserverIdle();
            Move();
        }

        /// <summary>
        /// Jump -> Idle
        /// </summary>
        private void ObserverIdle()
        {
            if(!Owner.IsGround) { return; }
            
            Owner.ChengeState(new PlayerIdle(Owner));
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
