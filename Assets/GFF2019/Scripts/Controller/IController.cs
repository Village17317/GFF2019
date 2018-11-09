/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：
 *更新日     ：
*/
using UnityEngine;

namespace Village
{
    interface IController
    {

        /// <summary>
        /// 左のスティックのAxis
        /// </summary>
        Vector2 LeftAxis();

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
        /// チャージ判定
        /// </summary>
        /// <returns></returns>
        bool IsCharge();
        
        /// <summary>
        /// 弾射出判定
        /// </summary>
        bool IsShot();

        /// <summary>
        /// ジャンプ判定
        /// </summary>
        /// <returns></returns>
        bool IsJump();

        /// <summary>
        /// 敵を狙うモードにする
        /// </summary>
        /// <returns></returns>
        bool IsTargetAiming();

    }
}
