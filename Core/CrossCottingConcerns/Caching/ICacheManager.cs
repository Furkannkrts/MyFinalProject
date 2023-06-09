using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCottingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value,int duration);//duration ne kadar süre tutulacağı cache'in
        bool IsAdd(string key);//bellekte cache değeri var mı metodu
        void Remove (string key);
        void RemoveByPattern(string pattern);
    }
}
