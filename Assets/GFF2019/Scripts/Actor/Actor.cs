/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ： 2018/10/27
 *更新日     ： 2018/10/29
*/
using UnityEngine;

namespace Village
{
    public abstract class Actor<T> : Inheritor where T : Actor<T>
    {
        [SerializeField] protected ActorParams myState;
        public                     ActorParams State { get { return myState; } }
        
        protected                  IActorState<T> nowAction;

        /// <summary>
        /// アクションの切替
        /// </summary>
        /// <param name="newAction"></param>
        public void ChengeState(IActorState<T> newAction)
        {
            nowAction = newAction;
        }
        
        
    }
}
