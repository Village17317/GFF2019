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
        
        protected                  IActorUpperState<T> nowUpperAction;
        protected                  IActorLowerState<T> nowLowerAction;
        
        /// <summary>
        /// アクションの切替
        /// </summary>
        /// <param name="newAction"></param>
        public void ChengeState(IActorUpperState<T> newAction)
        {
            nowUpperAction = newAction;
        }
        
        public void ChengeState(IActorLowerState<T> newAction)
        {
            nowLowerAction = newAction;
        }
        
    }
}
