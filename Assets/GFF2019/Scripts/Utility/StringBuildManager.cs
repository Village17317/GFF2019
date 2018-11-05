/*作成者     ：村上 和樹
 *機能説明   ：
 *初回作成日 ：2018/11/04
 *更新日     ：2018/11/04
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Village
{
    public static class StringBuildManager
    {

        private static StringBuilder _builder;
        
        /// <summary>
        /// 各文字列を渡して、足して、返す
        /// </summary>
        /// <param name="str">各文字列</param>
        public static string Build(params string[] str)
        {
            if(_builder == null) { _builder = new StringBuilder(); }

            _builder.Remove(0, _builder.Length);
            
            foreach (var s in str)
            {
                _builder.Append(s);
            }
            
            return _builder.ToString();
        }

        /// <summary>
        /// 文字列を指定文字で分割する
        /// </summary>
        /// <param name="str">分割したい文字列</param>
        /// <param name="separator">区切りの文字</param>
        /// <returns></returns>
        public static string[] Split(string str,char separator)
        {
            return str.Split(separator);
        }
        
        /// <summary>
        /// 文字列を指定文字で分割する
        /// </summary>
        /// <param name="str">分割したい文字列</param>
        /// <param name="separator">区切りの文字列</param>
        /// <returns></returns>
        public static string[] Split(string str, params string[] separator)
        {
            return str.Split(separator,StringSplitOptions.None);
        }

    }
}
