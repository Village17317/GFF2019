/*作成者     ：村上 和樹
 *機能説明   ：Actorのステータス
 *初回作成日 ：2018/10/27
 *更新日     ： 2018/10/27
*/

namespace Village
{
    public interface IActorState<T> where T : Actor<T>
    {
        /// <summary>
        /// 利用者
        /// </summary>
        T Owner { get; set; }
        
        /// <summary>
        /// 各状態のアクション
        /// </summary>
        void Run();
      
        /// <summary>
        /// 状態の名前
        /// <para>AnimationのKey</para>
        /// </summary>
        string StateName { get; }
    }
}
