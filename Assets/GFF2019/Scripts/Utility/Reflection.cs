/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：
 *更新日     ：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public class Reflection
    {

        private Vector3 _reflectionVec;

        public Vector3 ReflectionVec
        {
            get { return _reflectionVec.normalized; }
        }

        public static Reflection ReflectionVector(Vector3 center,Vector3 dir)
        {
            var ret = new Reflection();
            
            // 反射ベクトルを計算する
            Ray        ray = new Ray(center,dir);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit)) { return ret; }

            Vector3 incomingVec = hit.point - center;
            ret._reflectionVec  = Vector3.Reflect(incomingVec, hit.normal);
            
            Debug.DrawLine(center, hit.point, Color.red);
            Debug.DrawRay (hit.point, ret._reflectionVec, Color.green);
            
            return ret;
        }

    }
}
