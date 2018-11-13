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
        [SerializeField] protected ActorParams maxParams;
        public                     ActorParams Params { get { return maxParams; } }
        
        protected                  IActorUpperState<T> nowUpperAction;
        protected                  IActorLowerState<T> nowLowerAction;
        
        /// <summary>
        /// 上半身のアクションの切替
        /// </summary>
        /// <param name="newAction"></param>
        public void ChangeUpperState(IActorUpperState<T> newAction)
        {
            nowUpperAction = newAction;
        }
        
        /// <summary>
        /// 下半身のアクションの切替
        /// </summary>
        /// <param name="newAction"></param>
        public void ChangeLowerState(IActorLowerState<T> newAction)
        {
            nowLowerAction = newAction;
        }
        
    }
}
