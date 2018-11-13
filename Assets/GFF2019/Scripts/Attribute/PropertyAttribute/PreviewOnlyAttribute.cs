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
    public class PreviewOnlyAttribute : PropertyAttribute
    {
        public string IndexName { get; private set; }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PreviewOnlyAttribute(string name = "")
        {
            IndexName = name;
        }

    }
}
