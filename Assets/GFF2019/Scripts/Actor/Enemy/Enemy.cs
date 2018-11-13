/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/11/11
 *更新日     ：2018/11/11
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class Enemy : Actor<Enemy>
    {

        [PreviewOnly("体力"),SerializeField] private float _hp = 0f;
        
        public bool IsDead { get { return _hp <= 0; } }
        
        
        ///<summary>
        /// 初期起動時
        ///</summary>
        private void Awake ()
        {
            ChangeUpperState(new EnemyUpperIdle(this));
            ChangeLowerState(new EnemyLowerIdle(this));

            _hp = maxParams.Hp;
            
            UpdateManager.Instance.Add(this);
        }

        ///<summary>
        ///更新処理
        ///</summary>
        public override void Run ()
        {
            if(IsDead) { Destroy(gameObject); }
        }

        private void OnCollisionEnter(Collision other)
        {
            string tag = other.gameObject.tag;
            
            if(tag == "Bullet") { BulletHit(); }
        }

        /// <summary>
        /// 弾が当たったとき
        /// </summary>
        private void BulletHit()
        {
            _hp--;
        }
        
        
    }
}
