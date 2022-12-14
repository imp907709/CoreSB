using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace InfrastructureCheckers
{
    public class Check
    {
        public static void GO()
        {
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
}

namespace Algorithms
{
    public interface GOable
    {
        T[] GO<T>(T[] arr) where T : struct, IComparable;
    }

    public class ArrayCheckNGo : GOable
    {
        public T[] CheckNGo<T>(T[] arr) where T : struct, IComparable
        {
            if (arr?.Length <= 0)
                return arr;

            return GO(arr);
        }

        public virtual T[] GO<T>(T[] arr) where T : struct, IComparable
        {
            return arr;
        }
    }

    public class MergeSortOriginal
    {
        public int[] GO(int[] arr)
        {
            sort(arr, 0, arr.Length - 1);
            return arr;
        }

        // Merges two subarrays of []arr.
        // First subarray is arr[l..m]
        // Second subarray is arr[m+1..r]
        void merge(int[] arr, int l, int m, int r)
        {
            // Find sizes of two
            // subarrays to be merged
            int n1 = m - l + 1;
            int n2 = r - m;

            // Create temp arrays
            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j;

            // Copy data to temp arrays
            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            // Merge the temp arrays

            // Initial indexes of first
            // and second subarrays
            i = 0;
            j = 0;

            // Initial index of merged
            // subarray array
            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }

                k++;
            }

            // Copy remaining elements
            // of L[] if any
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            // Copy remaining elements
            // of R[] if any
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        // Main function that
        // sorts arr[l..r] using
        // merge()
        void sort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                // Find the middle
                // point
                int m = l + (r - l) / 2;

                // Sort first and
                // second halves
                sort(arr, l, m);
                sort(arr, m + 1, r);

                // Merge the sorted halves
                merge(arr, l, m, r);
            }
        }
    }

    public class MergeSort
    {
        public static MergeSort item = new MergeSort();

        public static void _GO()
        {
            var unsorted0 = new int[] {5, 3, 4, 2, 1, 9, 8, 7, 10};

            //var unsorted0 = new int[]{5,1,2,8,9};
            var sortedarr0 = new int[unsorted0.Length];
            unsorted0.CopyTo(sortedarr0, 0);
            var sorted0 = sortedarr0.ToList().OrderBy(s => s);
            var sortedResult0 = item.GO(unsorted0);

            var bool0 = sortedResult0.SequenceEqual(sorted0);
        }

        public int[] GO(int[] arr)
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

    public class SelectionSort
    {
        public int[] GO(int[] arr)
        {
            if (arr.Length <= 1)
                return arr;

            int l = arr.Length;

            for (int i = 0; i < l - 1; i++)
            {
                int min_idx = i;

                for (int j = i + 1; j < l; j++)
                {
                    if (arr[j] < arr[min_idx])
                    {
                        min_idx = j;
                    }
                }

                (arr[min_idx], arr[i]) = (arr[i], arr[min_idx]);
            }

            return arr;
        }
    }

    public class SelectionSortOriginal
    {
        public int[] GO(int[] arr)
        {
            int n = arr.Length;

            // One by one move boundary of unsorted subarray
            for (int i = 0; i < n - 1; i++)
            {
                // Find the minimum element in unsorted array
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                    if (arr[j] < arr[min_idx])
                    {
                        min_idx = j;
                    }

                // Swap the found minimum element with the first
                // element
                (arr[min_idx], arr[i]) = (arr[i], arr[min_idx]);
            }

            return arr;
        }
    }

    public class InsertionSort
    {
        public int[] GO(int[] arr)
        {
            if (arr.Length <= 1)
                return arr;
            
            for (int i = 1; i < arr.Length; i++)
            {
                var k = arr[i];
                
                int j = i - 1;
                while (j >= 0 && arr[j] > k)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = k;
            }

            return arr;
        }
    }

    public class InsertionSortOriginal
    {
        // Function to sort array
        // using insertion sort
        public int[] GO(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i) {
                int key = arr[i];
                int j = i - 1;
 
                // Move elements of arr[0..i-1],
                // that are greater than key,
                // to one position ahead of
                // their current position
                while (j >= 0 && arr[j] > key) {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }

            return arr;
        }
    }


    public class QuickSortInt
    {
        public int[] GO(int[] arr)
        {
            if (arr?.Length <= 1)
                return arr;

            pivotAndSort(arr, 0, arr.Length-1);
            return arr;
        }

        public int[] pivotAndSort(int[] arr, int st, int fn)
        {
            if (st < 0 || fn > arr.Length || st >= fn )
                return arr;

            var pvt = sort(arr, st, fn);
            pivotAndSort(arr, st, pvt-1);
            pivotAndSort(arr, pvt+1, fn);

            return arr;
        }

        public int sort(int[] arr, int st, int fn)
        {
            int i = st-1;

            int pvt = fn;

            for (var j = st; j <= fn; j++)
            {
                if (arr[j] < arr[pvt])
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
            
            (arr[i+1], arr[fn]) = (arr[fn], arr[i+1]);
            return i+1;
        }
    }

    public class QuickSortIntOriginal
    {

        public int[] GO(int[] arr)
        {
            quickSort(arr, 0, arr.Length - 1);
            return arr;
        }
        
        // A utility function to swap two elements
        static void swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        /* This function takes last element as pivot, places
             the pivot element at its correct position in sorted
             array, and places all smaller (smaller than pivot)
             to left of pivot and all greater elements to right
             of pivot */
        static int partition(int[] arr, int low, int high)
        {

            // pivot
            int pivot = arr[high];

            // Index of smaller element and
            // indicates the right position
            // of pivot found so far
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {

                // If current element is smaller
                // than the pivot
                if (arr[j] < pivot)
                {

                    // Increment index of
                    // smaller element
                    i++;
                    swap(arr, i, j);
                }
            }

            swap(arr, i + 1, high);
            return (i + 1);
        }

        /* The main function that implements QuickSort
                    arr[] --> Array to be sorted,
                    low --> Starting index,
                    high --> Ending index
           */
        static void quickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {

                // pi is partitioning index, arr[p]
                // is now at right place
                int pi = partition(arr, low, high);

                // Separately sort elements before
                // partition and after partition
                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }
    }


    public class ShellSortInt
    {
        public int[] GO(int[] arr)
        {
            for (var gap = arr.Length / 2; gap > 0; gap /=2)
            {
                for(var i = gap; i < arr.Length; i++)
                {
                    int j;
                    var pvt = arr[i];
                    for(j=i;j >= gap && arr[j-gap] > pvt; j-=gap)
                    {
                        arr[j] = arr[j - gap];
                    }
                    arr[j] = pvt;
                }
            }

            return arr;
        }
    }

    public class ShellSortOriginal
    {
        public int[] GO(int []arr)
        {
            int n = arr.Length;
 
            // Start with a big gap,
            // then reduce the gap
            for (int gap = n/2; gap > 0; gap /= 2)
            {
                // Do a gapped insertion sort for this gap size.
                // The first gap elements a[0..gap-1] are already
                // in gapped order keep adding one more element
                // until the entire array is gap sorted
                for (int i = gap; i < n; i += 1)
                {
                    // add a[i] to the elements that have
                    // been gap sorted save a[i] in temp and
                    // make a hole at position i
                    int temp = arr[i];
 
                    // shift earlier gap-sorted elements up until
                    // the correct location for a[i] is found
                    int j;
                    for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                        arr[j] = arr[j - gap];
 
                    // put temp (the original a[i])
                    // in its correct location
                    arr[j] = temp;
                }
            }
            return arr;
        }
    }
    

    public delegate int[] SortInt(int[] arr);

    public class SortChecker
    {
        private Random rnd = new Random();
        List<int> _ranges = new List<int>() {10000, 20000, 30000};
        private List<string> rep = new List<string>();
        private StringBuilder sb = new StringBuilder();


        public void Init(List<int> ranges)
        {
            if (ranges?.Any() != true)
                ranges = _ranges;

            MergeSort ms = new MergeSort();
            SelectionSort sst = new SelectionSort();
            InsertionSort iss = new InsertionSort();
            QuickSortInt qsi = new QuickSortInt();
            ShellSortInt ssi = new ShellSortInt();

            MergeSortOriginal mso = new MergeSortOriginal();
            SelectionSortOriginal sso = new SelectionSortOriginal();
            InsertionSortOriginal iso = new InsertionSortOriginal();
            QuickSortIntOriginal qso = new QuickSortIntOriginal();
            ShellSortOriginal slst = new ShellSortOriginal();

            // List<SortInt> algs = new List<SortInt>() { ms._GO, mso.GO, sso.GO,sst.GO, iss.GO, iso.GO };
            List<SortInt> algs = new List<SortInt>() {ssi.GO, slst.GO};

            //for (var rng = 5; rng <= 1000; rng += 10)
            foreach (var rng in _ranges)
            {
                var arr = getArr(rng);
                // arr = new List<int>() {5, 9, 0, 6, 2, 1};
                var sortedarr = copySorted(arr);

                var sw = new Stopwatch();
                // arr = new List<int>() { 2, 1, 4, 3, 5};
                foreach (var alg in algs)
                {
                    sw.Reset();
                    sw.Start();
                    var sorted = alg(arr.ToArray());
                    var res = sorted.SequenceEqual(sortedarr);
                    var etcks = sw.Elapsed.Ticks;
                    sw.Stop();
                    var ratio = etcks / (rng * Math.Log(rng));

                    //rep.Add($"Algorithm: {ms.GetType().Name}; Result: {res}; Range: {rng}; Elapsed:{sw.Elapsed}; Ratio:{ratio};");
                    rep.Add($"{alg.Target} {res} {rng} {etcks} {ratio}");
                }

                rep.Add(Environment.NewLine);
            }

            File.WriteAllLines(@"C:\files\test\rep.txt", rep);
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
