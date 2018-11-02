/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/11/01
 *更新日     ：2018/11/01
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class CameraConstantMove : ICameraMove
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraConstantMove(Transform owner)
        {
            Owner = owner;
        }

        public Transform Owner { get; private set; }

        public void Move(Vector3 pos, Vector3 backLength)
        {
            Owner.transform.position = pos + backLength;
        }
    }
}
