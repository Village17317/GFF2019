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
    public class EnemyLowerIdle : IActorLowerState<Enemy>
    {
        public Enemy  Owner     { get; set; }
        public string StateName { get { return "Idle"; } }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EnemyLowerIdle(Enemy owner)
        {
            Owner = owner;
        }

        public void Execute()
        {
            
        }

    }
}
