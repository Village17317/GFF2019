/*作成者     ：村上 和樹
 *機能説明   ：配列のindexを別名で指定
 *初回作成日 ：2018/10/25
 *更新日     ：2018/10/25
*/
namespace Village
{
    public class NamedArrayAttribute : UnityEngine.PropertyAttribute
    {
        /// <summary>
        /// 各Indexの名前
        /// </summary>
        public string[] IndexNames
        {
            get;
            private set;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="names">描画したい名前</param>
        public NamedArrayAttribute(params string[] names)
        {
            IndexNames = names;
        }

    }
}
