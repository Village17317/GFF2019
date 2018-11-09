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
    public class PlayerJump : PlayerWalk
    {
        private const float JumpForce = 10f; // ジャンプ

        public override string StateName
        {
            get { return "Jump"; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlayerJump(Player owner) : base(owner)
        {
            var rigid = owner.GetComponent<Rigidbody>();
            rigid.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        public override void Execute()
        {
            ObserveIdle();
            
            ForwardAim();
            Move();
        }

        /// <summary>
        /// Jump -> Idle
        /// </summary>
        private void ObserveIdle()
        {
            if (!Owner.IsGround) { return; }

            Owner.ChangeLowerState(new PlayerLowerIdle(Owner));
        }
    }
}
