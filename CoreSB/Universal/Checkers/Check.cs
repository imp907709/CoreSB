using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LINQtoObjectsCheck;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NetPlatformCheckers;

namespace InfrastructureCheckers
{
    public class Check
    {
        public static void GO()
        {
            StringCompare.GO();
            LINQcheck.GO();
        }
    }

    public class OperatorOverloading
    {
        public int A;
        public int B;

        public OperatorOverloading()
        {
            A = 0;
            B = 0;
        }

        public OperatorOverloading(int a, int b)
        {
            A = a;
            B = b;
        }

        public static OperatorOverloading operator ++(OperatorOverloading o)
        {
            return new OperatorOverloading(o.A + 1, o.B + 1);
        }

        public static OperatorOverloading operator +(OperatorOverloading a)
        {
            return new OperatorOverloading(a.A, a.B);
        }

        public static OperatorOverloading operator +(OperatorOverloading a, int b)
        {
            return new OperatorOverloading(a.A, a.B + b);
        }

        public static OperatorOverloading operator +(OperatorOverloading o1, OperatorOverloading o2)
        {
            return new OperatorOverloading(o1.A + o2.A, o2.B + o2.B);
        }
    }


    public class StringCompare
    {
        public static void GO()
        {
            // == true
            string s0 = "s0";
            string s1 = "s0";

            // == true
            string s2 = "s1";
            object o1 = "s1";
            bool b1 = s2 == o1;

            string s3 = "s2";
            object s4 = new string("s2");
            string s5 = "s2";

            //false
            bool b4 = s3 == s4;

            //true
            bool b5 = s3 == s5;
        }
    }


    public class Property
    {
        public int ID { get; set; }
        public string PropertyName { get; set; }
    }

    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Property> Property { get; set; }
    }

    public class LINQcheck
    {
        public static void GO()
        {
            var propsIntersectAny = new List<Property>()
            {
                new Property() {ID = 1, PropertyName = "PropName1"},
                new Property() {ID = 2, PropertyName = "PropName2"}
            };

            var itemsList1 = new List<Item>()
            {
                // ALL STRICT
                // intersect all
                new Item() {ID = 1, Name = "Name1", Property = propsIntersectAny},

                // ALL AT LEAST
                // intersect all +
                new Item()
                {
                    ID = 3,
                    Name = "Name3",
                    Property = new List<Property>()
                    {
                        new Property() {ID = 5, PropertyName = "PropName5"},
                        propsIntersectAny[0],
                        propsIntersectAny[1]
                    }
                },

                // ANY AT LEAST
                // intersect 1 +
                new Item()
                {
                    ID = 4,
                    Name = "Name4",
                    Property = new List<Property>()
                    {
                        new Property() {ID = 6, PropertyName = "PropName6"}, propsIntersectAny[0]
                    }
                },

                // ANY STRICT
                // intersect 1
                new Item() {ID = 10, Name = "Name10", Property = new List<Property>() {propsIntersectAny[0]}}

                // ANY NOT INTERSECT
                // intersect non 1
                ,
                new Item()
                {
                    ID = 5,
                    Name = "Name5",
                    Property = new List<Property>()
                    {
                        new Property() {ID = 7, PropertyName = "PropName7"}, propsIntersectAny[0]
                    }
                }

                // ALL NOT INTERSECT
                // intersect non all 
                ,
                new Item()
                {
                    ID = 11,
                    Name = "Name11",
                    Property = new List<Property>()
                    {
                        new Property() {ID = 8, PropertyName = "PropName8"},
                        new Property() {ID = 9, PropertyName = "PropName9"}
                    }
                }
            };

            var itemsList2 = new List<Item>()
            {
                // intersect all
                new Item() {ID = 1, Name = "Name1", Property = propsIntersectAny}
            };

            // any of
            // !11
            var r0 = itemsList1.Where(s =>
                    itemsList2.Any(x => s.Property.Any(c => x.Property.Any(v => v.PropertyName == c.PropertyName))))
                .ToList();

            // all any - any without excess
            //1 10
            var r1 = itemsList1.Where(s =>
                    itemsList2.Any(x => s.Property.All(c => x.Property.Any(v => v.PropertyName == c.PropertyName))))
                .ToList();

            // any excess exist
            //3 4 5 11
            var r2 = itemsList1.Where(s =>
                    itemsList2.Any(x => !s.Property.All(c => x.Property.Any(v => v.PropertyName == c.PropertyName))))
                .ToList();

            // no intersect
            // 11
            var r3 = itemsList1.Where(s =>
                    itemsList2.Any(x => s.Property.All(c => x.Property.All(v => v.PropertyName != c.PropertyName))))
                .ToList();

            var gpjn = itemsList1
                .GroupJoin(itemsList2, lk => lk.Name, rk => rk.Name, (l, r) => new {l, r = r?.DefaultIfEmpty()})
                .SelectMany(k => k.r, (l, r) => new {l.l.ID, l.l.Name, rName = r?.Name ?? "Not found"}).ToList();

            File.WriteAllTextAsync(@"C:\files\test\gpjn.json", JsonSerializer.Serialize(gpjn));

            File.WriteAllTextAsync(@"C:\files\test\r0.json", JsonSerializer.Serialize(r0));
            File.WriteAllTextAsync(@"C:\files\test\r1.json", JsonSerializer.Serialize(r1));
            File.WriteAllTextAsync(@"C:\files\test\r2.json", JsonSerializer.Serialize(r2));

            File.WriteAllTextAsync(@"C:\files\test\r3.json", JsonSerializer.Serialize(r3));
        }
    }


    public class MergeSort
    {
        public static MergeSort item = new MergeSort();

        public static void GO()
        {
            var unsorted0 = new int[] {5, 3, 4, 2, 1, 9, 8, 7, 10};

            //var unsorted0 = new int[]{5,1,2,8,9};
            var sortedarr0 = new int[unsorted0.Length];
            unsorted0.CopyTo(sortedarr0, 0);
            var sorted0 = sortedarr0.ToList().OrderBy(s => s);
            var sortedResult0 = item._GO(unsorted0);

            var bool0 = sortedResult0.SequenceEqual(sorted0);
        }

        public int[] _GO(int[] arr)
        {
            var result = split(arr);
            return result;
        }

        int[] split(int[] arr)
        {
            if (arr.Length <= 1)
                return arr;

            var mg = arr.Length / 2;

            var lg = 0;
            var rg = arr.Length - 1;
            var rLng = arr.Length - mg;

            var arrL = new int[mg];
            var arrR = new int[rLng];
            Array.Copy(arr, 0, arrL, 0, mg);
            Array.Copy(arr, mg, arrR, 0, rLng);

            arrL = split(arrL);
            arrR = split(arrR);

            var res = compareMerge(arrL, arrR);
            return res;
        }

        int[] compareMerge(int[] arrL, int[] arrR)
        {
            var result = new int[arrL.Length + arrR.Length];

            int i1 = 0;
            int i2 = 0;

            while (i1 < arrL.Length && i2 < arrR.Length)
            {
                if (i1 < arrL.Length && i2 < arrR.Length && arrL[i1] <= arrR[i2])
                {
                    result[i1 + i2] = arrL[i1];
                    i1++;
                }

                if (i1 < arrL.Length && i2 < arrR.Length && arrR[i2] < arrL[i1])
                {
                    result[i1 + i2] = arrR[i2];
                    i2++;
                }
            }

            while (i1 < arrL.Length && i1 <= i2)
            {
                result[i1 + i2] = arrL[i1];
                i1++;
            }

            while (i2 < arrR.Length && i2 <= i1)
            {
                result[i1 + i2] = arrR[i2];
                i2++;
            }

            return result;
        }
    }

    public class SortChecker
    {
        private Random rnd = new Random();
        List<int> _ranges = new List<int>() {10, 1000000, 10000000, 20000000};
        private List<string> rep = new List<string>();
        private StringBuilder sb = new StringBuilder();

        public void Init(List<int> ranges)
        {
            if (ranges?.Any() != true)
                ranges = _ranges;

            for (var rng = 5; rng <= 1000; rng += 10)
            {
                var arr = getArr(rng);
                var sortedarr = copySorted(arr);

                var sw = new System.Diagnostics.Stopwatch();

                sw.Start();
                MergeSort ms = new MergeSort();
                var sorted = ms._GO(arr.ToArray());
                var res = sorted.SequenceEqual(sortedarr);
                sw.Stop();

                var ratio = sw.Elapsed.Ticks / (rng * Math.Log(rng));

                //rep.Add($"Algorithm: {ms.GetType().Name}; Result: {res}; Range: {rng}; Elapsed:{sw.Elapsed}; Ratio:{ratio};");
                rep.Add($"{ms.GetType().Name} {res} {rng} {sw.Elapsed.Ticks} {ratio}");

                //rep.Add(Environment.NewLine);
            }

            File.WriteAllLines(@"C:\files\test\rep.txt", rep);

            foreach (var r in rep)
            {
                System.Diagnostics.Trace.WriteLine(r);
            }
        }

        List<int> getArr(int rng = 10)
        {
            var arr = new List<int>(rng);
            for (int i = 0; i < rng; i++)
            {
                arr.Add(rnd.Next(-rng, rng));
            }

            return arr;
        }

        List<int> copySorted(List<int> arr)
        {
            var sortedarr0 = new int[arr.Count];
            arr.CopyTo(sortedarr0, 0);
            arr.CopyTo(sortedarr0, 0);
            var sorted0 = sortedarr0.ToList().OrderBy(s => s).ToList();
            return sorted0;
        }
    }

    public class AlgCheck
    {
        public static void GO()
        {
            SortChecker ch = new SortChecker();
            ch.Init(null);
        }
    }
}
