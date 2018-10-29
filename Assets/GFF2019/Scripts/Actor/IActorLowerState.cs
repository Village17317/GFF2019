/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/10/29
 *更新日     ：2018/10/29
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public interface IActorLowerState<T> where T : Actor<T>
    {
        /// <summary>
        /// 利用者
        /// </summary>
        T Owner { get; set; }
        
        /// <summary>
        /// 各状態のアクション
        /// </summary>
        void Execute();
      
        /// <summary>
        /// 状態の名前
        /// <para>AnimationのKey</para>
        /// </summary>
        string StateName { get; }
    }
}
