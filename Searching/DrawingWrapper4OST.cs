using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Searching
{
    public class DrawingWrapper4OST<T, K, V>
        : CertWrapper4OST<T, K, V>, IOrderedSymbolTable<K, V>
        where T : IOrderedSymbolTable<K, V>
        where K : IComparable
    {
        protected BST<K, V> BST => _ost as BST<K, V>;

        protected string _fileName;

        public DrawingWrapper4OST(T ost, string fileName) : base(ost)
        {
            _ost = ost;
            _fileName = fileName;
            File.Delete(_fileName);
        }

        public override void Put(K key, V value)
        {
            _ost.Put(key, value);
            File.AppendAllText(_fileName, BST.DrawTree());
            Cert.Certificate();
        }

        public override bool Delete(K key)
        {
            bool result = _ost.Delete(key);
            File.AppendAllText(_fileName, BST.DrawTree());
            Cert.Certificate();
            return result;
        }

        public override void DeleteMax()
        {
            _ost.DeleteMax();
            File.AppendAllText(_fileName, BST.DrawTree());
            Cert.Certificate();
        }

        public override void DeleteMin()
        {
            _ost.DeleteMin();
            File.AppendAllText(_fileName, BST.DrawTree());
            Cert.Certificate();
        }
    }
}
