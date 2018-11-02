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
    interface IController
    {

        /// <summary>
        /// 左のスティックのAxis
        /// </summary>
        Vector2 LeftGetAxis();

        /// <summary>
        /// 右のスティックのAxis
        /// </summary>
        Vector2 RightAxis();

        /// <summary>
        /// Axisの処理部分
        /// </summary>
        /// <param name="xKey">Horizontal</param>
        /// <param name="yKey">Vertical</param>
        Vector2 Axis(string xKey, string yKey);
        
        /// <summary>
        /// 弾射出判定
        /// </summary>
        bool IsShot();

        /// <summary>
        /// ジャンプ判定
        /// </summary>
        /// <returns></returns>
        bool IsJump();

    }
}
