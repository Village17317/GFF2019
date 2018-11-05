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
    public class Bullet : OverReaction
    {
        private Count _boundCounter;
        

        ///<summary>
        /// 初期起動時
        ///</summary>
        protected override void Awake ()
        {
            base.Awake();
            _boundCounter = new Count();
            UpdateManager.Instance.Add(this);
        }

        ///<summary>
        ///更新処理
        ///</summary>
        public override void Run ()
        {
            if (IsDeath) { Destroy(gameObject); }
        }

        /// <summary>
        /// 死亡判定
        /// </summary>
        private bool IsDeath
        {
            get { return _boundCounter.Data >= 10; }
        } 
        
        
        /// <summary>
        /// 衝突時の当たり判定
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other)
        {
            _boundCounter.CountUp(1);
            isMeshDeformed = true;
        }
    }
}
