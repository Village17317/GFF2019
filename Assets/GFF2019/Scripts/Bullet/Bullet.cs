/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：
 *更新日     ：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Village
{
    public class Bullet : OverReaction
    {
        private const int Lv1MaxCount = 10;
        
        private int      _maxCount;     //最大反射回数 チャージ数で異なる
        private CountInt _boundCounter; //反射回数をカウントする

        private Vector3 _direction;     //進む方向
        private float   _speed;         //進む速度
        private int     _level;         //弾のレベル
       
        ///<summary>
        /// 初期起動時
        ///</summary>
        protected override void Awake ()
        {
            base.Awake();
            
            _boundCounter = new CountInt();
            _maxCount     = Lv1MaxCount;
            _direction    = Vector3.zero;
            _speed        = 0f;
            
            UpdateManager.Instance.Add(this);
        }

        ///<summary>
        ///更新処理
        ///</summary>
        public override void Run ()
        {
            if (IsDeath) { Destroy(gameObject); }
            
            transform.position += NextAddPosition;          
        }

        /// <summary>
        /// 進行方向と移動速度を設定
        /// </summary>
        /// <param name="dir">進行方向</param>
        /// <param name="speed">移動速度</param>
        /// <param name="level">チャージレベル</param>
        public void Shot(Vector3 dir, float speed, int level)
        {
            _direction = dir;
            _speed     = speed;
            _level     = level;
        }

        /// <summary>
        /// 可視化するかどうか
        /// </summary>
        /// <param name="isVisible">表示フラグ</param>
        public void IsVisible(bool isVisible)
        {
            GetComponent<MeshRenderer>().enabled = isVisible;
            GetComponent<Collider>().enabled     = isVisible;
        }
        
        /// <summary>
        /// 死亡判定
        /// </summary>
        private bool IsDeath
        {
            get { return _boundCounter.ValueGreaterEqual(_maxCount); }
        } 
        
        /// <summary>
        /// 衝突時の当たり判定
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other)
        {            
            _boundCounter.CountUp(1);
            ReflectionCalc(other);
        }

        /// <summary>
        /// 反射の計算
        /// </summary>
        /// <param name="other"></param>
        private void ReflectionCalc(Collision other)
        {
            // 反射ベクトルを計算する            
            var refData = Reflection.ReflectionVector(transform.position, _direction); 
            _direction = refData.ReflectionVec;
        }

        /// <summary>
        /// 次のフレームの位置を求める
        /// </summary>
        /// <returns></returns>
        private Vector3 NextAddPosition
        {
            get
            {
                //移動方向にスピード分の長さのRayを飛ばす
                Ray        ray = new Ray(transform.position,_direction);
                RaycastHit hit;

                if (!Physics.Raycast(ray, out hit, _speed))
                {
                    //なにもなければ方向×速度
                    return _direction * _speed;
                }
            
                //なにか当たったら方向×（当たった位置から現在地までの長さ）
                Vector3 hitPos = hit.point - transform.localScale;
                float   length = (hitPos - transform.position).magnitude;
                return _direction * length;
            }
        }
        
    }
}
