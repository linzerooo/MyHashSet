using MyHashSetApp;

var set = new MyHashSet<int>(5);
set.Add(1);
set.Add(2);



var set1 = new MyHashSet<int>(7);

set1.Add(1);
set1.Add(11);
set1.Add(5);
set1.Add(13);
set1.Add(2);
//var arr = set.ToArray();




var arr = set.isSupset(set1);
Console.WriteLine(arr);