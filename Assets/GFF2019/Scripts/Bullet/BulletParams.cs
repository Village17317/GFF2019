/*作成者     ：村上 和樹
 *機能説明   ： 弾のデータ構造
 *初回作成日 ： 2018/10/29
 *更新日     ： 2018/10/29
*/

using UnityEngine;

namespace Village
{
    [System.Serializable]
    public class BulletParams
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform  _firePos;
        [SerializeField] private float    _force;
        
        public GameObject BulletInstance { get { return Object.Instantiate(_bulletPrefab); } }

        public Vector3 FirePos { get { return _firePos.position; } }

        public float Force
        {
            get { return _force; }
            set { _force = value; }
        }

    }
}
