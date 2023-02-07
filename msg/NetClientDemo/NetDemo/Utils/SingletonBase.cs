using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDemo.Utils
{
    internal abstract class SingletonBase<T> where T : SingletonBase<T>, new()
    {
        private static T _instance = null; // static 변수 _instance를 생성, 이 _instance를 netclient로 사용, 필요할 때 호출해서 사용
        public static T Get //static 메소드 Get을 생성
        {
            get
            {
                if (null == _instance) // _instance가 비어있으면 새로 만듬
                {
                    _instance = new T();
                    _instance.OnInit();
                }

                return _instance; // _instance를 리턴
            }
        }

        protected abstract void OnInit(); // 추상 클래스 OnInit 생성
    }
}
