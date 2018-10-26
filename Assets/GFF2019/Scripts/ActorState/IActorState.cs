/*作成者     ：村上 和樹
 *機能説明   ： Actorのステータス
 *初回作成日 ： 2018/10/26
 *更新日     ： 2018/10/26
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    interface IActorState
    {
        /// <summary>
        /// 体力
        /// </summary>
        float Life { get; set; }
        
        /// <summary>
        /// 移動速度
        /// </summary>
        float Speed { get; set; }
        
        /// <summary>
        /// 攻撃力
        /// </summary>
        float Power { get; set; }
        
        /// <summary>
        /// 防御力
        /// </summary>
        float Defense { get; set; }
    }
}
