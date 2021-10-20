using System;
using System.Diagnostics;

namespace SortSearchTime
{
    class Program
    {
        static void Main(string[] args)
        {   
            Stopwatch time = new Stopwatch();
            int size = 100;
            int[] arr = new int[size];
            SetRandom(arr, size);
            Console.WriteLine("Small Array");
            PrintArray(arr);
            time.Start();
            Console.WriteLine("The number 12 occurs at index " + LinearSearch(arr, 12));
            time.Stop();
            Console.WriteLine($"Linear search elapsed time: {time.ElapsedMilliseconds} ms");
            time.Reset();
            time.Start();
            SelectionSort(arr);
            Console.WriteLine("Sort array");
            PrintArray(arr);
            Console.WriteLine("The number 12 occurs at index " + BinarySearch(arr, 12));
            time.Stop();
            Console.WriteLine($"Binary search elapsed time: {time.ElapsedMilliseconds} ms");
            time.Reset();
            int testSize = 10000;
            int[] randomArr = new int[testSize];
            SetRandom(randomArr, testSize);            
            int[] copyArr = new int[testSize];

            time.Reset();
            randomArr.CopyTo(copyArr,0);
            //PrintArray(copyArr);
            time.Start();
            BubbleSort(copyArr);
            time.Stop();
            //PrintArray(bigArr);
            Console.WriteLine($"Bubble Sort {testSize} items Execution Time : {time.ElapsedMilliseconds} ms");
            
            time.Reset();
            randomArr.CopyTo(copyArr, 0);
            //PrintArray(copyArr);
            time.Start();
            SelectionSort(copyArr);
            time.Stop();
            //PrintArray(copyArr);
            Console.WriteLine($"Selection Sort {testSize} items Execution Time : {time.ElapsedMilliseconds} ms");

            time.Reset();
            randomArr.CopyTo(copyArr, 0);
            //PrintArray(copyArr);
            time.Start();
            InsertionSort(copyArr);
            time.Stop();
            //PrintArray(copyArr);
            Console.WriteLine($"Insertion Sort {testSize} items Execution Time : {time.ElapsedMilliseconds} ms");
        }

        /**
         * This method compares a search term to each element of an 
         * array. LinearSearch can be used on sorted and unsorted arrays.
         * When applied to large arrays, this method may take a long time.
         * It could be very quick depending on the index of the target element,
         * but worst case has it searching all elements.
         */
        public static int LinearSearch(int[] arr, int term)
        {
            if (arr.Length > 0)
            {   
                int i = 0;             
                foreach(int o in arr)   // Check each array element.
                {
                    if (o == term)      // Check that element == search term.
                    {
                        return i;       // Return index
                    }
                    i++;
                }
            }
            return -1;                  // Return -1 on empty array or no match.
        }

        /**
         * This method checks the middle element of a sorted array, between given index
         * values. Then the method compares the element to a search term. If the term 
         * is more or less than the middle element, half of the array is eliminated. 
         * BinarySearch is called again on the half of the array that contains the search term. 
         * This method can only be used on a sorted list. It is much faster than Linear search
         * as it can narrow in on the target element very quickly by elimating so much data in 
         * early iterations.
         */
        public static int BinarySearch(int[] arr, int left, int right, int term)
        {
            int mid = 0;                    // Midpoint variable
            if (arr.Length > 0)             // If array is not empty,
            {
                if (right >= left)              // If right is greater or equal to left,
                {
                    mid = (left + right) / 2;       // Midpoint is average of left and right.
                    if (term == arr[mid])           // If term == mid element,
                    {
                        return mid;                     // Return mid.
                    }
                    if (term < arr[mid])            // If term is less than mid element,
                    {                                   // Return result of BinarySearch of first half of this array.
                        return BinarySearch(arr, left, mid - 1, term);
                    }           
                                                    // When term is greater than mid element,
                                                        // Return result of BinarySearch of second half of this array.
                    return BinarySearch(arr, mid + 1, right, term);
                }
            }
            return -1;                      // Return -1 if array is empty or no match is found.
        }

        // Simplified call to search entire array.
        public static int BinarySearch(int[] arr, int term)
        {
            return BinarySearch(arr, 0, arr.Length - 1, term);
        }

        // Assigns unique random integers to a specified number of elements
        // or fills the array if n is larger than the array.
        public static void SetRandom(int[] arr, int n)
        {
            Random r = new Random();
            for (int i = 1; i <= n; i++)
            {
                int temp = r.Next(0, n);
                bool exit = false;
                while (!exit) {
                    if (temp < n && temp >= 0 && arr[temp] == 0) 
                    {
                        arr[temp] = i;
                        exit = true;
                    }
                    else
                    {
                        if (temp > n / 2)
                        {
                            temp++;
                        }
                        else
                        {
                            temp--;
                        }
                    }
                    if (temp < 0)
                    {
                        temp = n / 2 + 1;
                    }
                    if (temp >= n)
                    {
                        temp = n / 2;
                    }
                }
            }
        }

        // Sorting code from www.tutorialspoint.com
        public static void BubbleSort(int[] arr)
        {
            int temp;
            for (int j = 0; j <= arr.Length - 2; j++)
            {
                for (int i = 0; i <= arr.Length - 2; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        temp = arr[i + 1];
                        arr[i + 1] = arr[i];
                        arr[i] = temp;
                    }
                }
            }
        }

        // Sort code from www.geeksforgeeks.com
        public static void SelectionSort(int[] arr)
        {
            int n = arr.Length;

            // One by one move boundary of unsorted subarray
            for (int i = 0; i < n - 1; i++)
            {
                // Find the minimum element in unsorted array
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                    if (arr[j] < arr[min_idx])
                        min_idx = j;

                // Swap the found minimum element with the first
                // element
                int temp = arr[min_idx];
                arr[min_idx] = arr[i];
                arr[i] = temp;
            }
        }

        // Sort code from www.geeksforgeeks.com
        public static void InsertionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = arr[i];
                int j = i - 1;

                // Move elements of arr[0..i-1],
                // that are greater than key,
                // to one position ahead of
                // their current position
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }

        public static void PrintArray(int[] arr)
        {
            int count = 0;
            foreach (int i in arr)
            {
                Console.Write(i + " ");
                if ((count + 1) % 15 == 0)
                {
                    Console.WriteLine();
                }
                count++;
            }
            Console.WriteLine();
        }
    }
}
