/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ： 2018/10/27
 *更新日     ： 2018/10/29
*/
using UnityEngine;

namespace Village
{
    public class Actor<T> : Inheritor where T : Actor<T>
    {
        [SerializeField] protected ActorParams MyState;
        public                     ActorParams State { get { return MyState; } }
        
        protected                  IActorState<T> NowAction;

        /// <summary>
        /// アクションの切替
        /// </summary>
        /// <param name="newAction"></param>
        public void ChengeState(IActorState<T> newAction)
        {
            NowAction = newAction;
        }
        
        
    }
}
