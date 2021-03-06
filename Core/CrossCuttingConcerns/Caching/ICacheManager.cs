using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key); //bir objeyi cache de listleme
        object Get(string key);
        void Add(string key,object value,int duration); //cache ekleme yapma
        bool IsAdd(string key);// obje cache de var mı yoksa cache ekle
        void Remove(string key);// cacheden uçurma
        void RemoveByPattern(string pattern);// filtreleme yaparak cacheden uçurma;


    }
}
