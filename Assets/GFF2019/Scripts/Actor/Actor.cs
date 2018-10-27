/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ： 2018/10/27
 *更新日     ： 2018/10/27
*/
using UnityEngine;

namespace Village
{
    public class Actor<T> : Inheritor where T : Actor<T>
    {
        [SerializeField] protected ActorState MyState;
        public                     ActorState State { get { return MyState; } }
        
        protected                  IActorAction<T> NowAction;

        /// <summary>
        /// アクションの切替
        /// </summary>
        /// <param name="newAction"></param>
        public void ChengeState(IActorAction<T> newAction)
        {
            NowAction = newAction;
        }
        
        
    }
}
