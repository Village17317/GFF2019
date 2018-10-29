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

        ///<summary>
        /// 初期起動時
        ///</summary>
        protected override void Awake ()
        {
            base.Awake();
            UpdateManager.Instance.Add(this);
        }

        ///<summary>
        ///更新処理
        ///</summary>
        public override void Run ()
        {
            
        }


    }
}
