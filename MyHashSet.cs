using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHashSetApp
{
    internal class MyHashSet<T>
    {
        private LinkedList<T>[] _buckets;
        private int _count;

        int Count => _count;


        // конструктор с необходимым размером хеш-таблицы
        public MyHashSet() : this(100) { }
        public MyHashSet(int capacity)
        {
            _buckets = new LinkedList<T>[capacity];
            _count = 0;
        }

        public bool Contains(T item)
        {
            int bucketIndex = GetBucketIndex(item.GetHashCode());
            LinkedList<T> bucket = _buckets[bucketIndex];
            return bucket != null && bucket.Contains(item);
        }
        public void Add(T item)
        {
            int bucketIndex = GetBucketIndex(item.GetHashCode());
            LinkedList<T> bucket = _buckets[bucketIndex];

            if (bucket == null)
            {
                bucket = new LinkedList<T>();
                _buckets[bucketIndex] = bucket;
            }
            if (!bucket.Contains(item))
            {
                bucket.AddLast(item);
                _count++;
            }

            if (_count > _buckets.Length * 0.75)
            {
                var newBuckets = new LinkedList<T>[_buckets.Length * 2];

                foreach (LinkedList<T> lst in _buckets)
                {
                    if (lst != null)
                    {
                        foreach (T x in lst)
                        {
                            int newBucketIndex = GetBucketIndex(x.GetHashCode(), _buckets.Length * 2);
                            LinkedList<T> newBucket = newBuckets[newBucketIndex];

                            if (newBucket == null)
                            {
                                newBucket = new LinkedList<T>();
                                newBuckets[newBucketIndex] = newBucket;
                            }

                            newBucket.AddLast(x);
                        }
                    }
                }
            }
        }

        public void Remove(T item)
        {
            int bucketIndex = GetBucketIndex(item.GetHashCode());
            LinkedList<T> bucket = _buckets[bucketIndex];
            if (bucket != null && bucket.Remove(item))
                _count--;
        }

        public void Clear()
        {
            _buckets = new LinkedList<T>[_buckets.Length];
            _count = 0;
        }

        public List<T> ToArray() {
            List<T> values = new List<T>();
            foreach (LinkedList<T> lst in _buckets) {
                foreach (T item in lst)
                {
                    values.Add(item);
                    break;
                }
            }
            return values;
        }

        public MyHashSet<T> Intersect(MyHashSet<T> set1) {
            var res = new MyHashSet<T>();
            for (int i = 0; i < _buckets.Length; i++) {
                if (_buckets[i] == null) {
                    continue;
                }

                foreach (T item in _buckets[i])
                {
                    if (set1.Contains(item))
                        res.Add(item);
                }
            }
            return res;
        }

        public MyHashSet<T> Union(MyHashSet<T> set1) {
            var res = new MyHashSet<T>();
            for (int i = 0; i < _buckets.Length; i++)
            {
                if (_buckets[i] == null) continue;
                foreach (T item in _buckets[i])
                {
                    res.Add(item);
                }
            }

            for (int i = 0; i < set1._buckets.Length; i++)
            {
                if (set1._buckets[i] == null) continue;
                foreach (T item in set1._buckets[i])
                {
                    res.Add(item);
                }
            }
            return res;
        }

        public MyHashSet<T> SymmetricExcept(MyHashSet<T> set1) {
            var res = new MyHashSet<T>();
            for (int i = 0; i < _buckets.Length; i++)
            {
                if (_buckets[i] == null) continue;

                foreach (T item in _buckets[i])
                {
                    if (!set1.Contains(item))
                        res.Add(item);
                }
            }
            for (int i = 0; i < set1._buckets.Length; i++)
            {
                if (set1._buckets[i] == null) continue;

                foreach (T item in set1._buckets[i])
                {
                    if (!this.Contains(item))
                        res.Add(item);
                }
            }
            return res;
        }

        public MyHashSet<T> Except(MyHashSet<T> set1) {
            var res = new MyHashSet<T>();
            for (int i = 0; i < _buckets.Length; i++)
            {
                if (_buckets[i] == null) continue;

                foreach (T item in _buckets[i])
                {
                    if (!set1.Contains(item))
                        res.Add(item);
                }
            }


            return res;
        }

        public bool isSupset(MyHashSet<T> set1) {
            var count = 0;
            for (int i = 0; i < _buckets.Length; i++)
            {
                if (_buckets[i] == null) continue;

                foreach (T item in _buckets[i])
                {
                    if (set1.Contains(item)) {
                        count++;
                    }
                }

            }
            if (count == this.Count) return true;
            return false;
        }


        private int GetBucketIndex(int hashCode, int capacity = -1)
        {
            if (capacity == -1)
            {
                capacity = _buckets.Length;
            }

            int bucketIndex = hashCode % capacity;
            return bucketIndex >= 0 ? bucketIndex : -bucketIndex;
        }



        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (LinkedList<T> lst in _buckets) {
                if (lst != null)
                {
                    foreach (T item in lst)
                    {
                        sb.Append($"{item} ");
                    }
                    //sb.Append("");
                }
                
            }
            return sb.ToString();
        }
    }
}
