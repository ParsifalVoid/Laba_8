using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба_8
{
    internal class SearchService
    {
        public enum SearchServiceVariable
        {
            FULL = 0,
            DYNAMIC = 1
        }
        public (string data, int position, int itterCount, long workTimeCount) LinearSearch(string[] data, string search,
            SearchServiceVariable ssv = SearchServiceVariable.FULL)
        {

            int itterCount = 0;

            long start = DateTime.Now.Ticks;

            if (ssv == SearchServiceVariable.FULL)
                for (int i = 0; i < data.Length; i++)
                {
                    itterCount++;
                    if (data[i] == search)
                    {
                        return (data[i], i, itterCount, DateTime.Now.Ticks - start);
                    }
                }

            if (ssv == SearchServiceVariable.DYNAMIC)
                for (int i = 0; i < data.Length; i++)
                {
                    itterCount++;
                    if (data[i].IndexOf(search) != -1)
                    {
                        return (data[i], i, itterCount, DateTime.Now.Ticks - start);
                    }
                }


            return (null, -1, itterCount, DateTime.Now.Ticks - start);
        }

        public (int data, int position, int itterCount, long workTimeCount) LinearSearch(int[] data, int search)
        {

            int itterCount = 0;

            long start = DateTime.Now.Ticks;

            for (int i = 0; i < data.Length; i++)
            {
                itterCount++;
                if (data[i] == search)
                {
                    return (data[i], i, itterCount, DateTime.Now.Ticks - start);
                }
            }

            return (-1, -1, itterCount, DateTime.Now.Ticks - start);
        }

        public (int data, int positon, int itterCount, long workTimeCount) BinarySearch(int[] array, int searchedValue, int first, int last, int itterCount = 0, long time = 0)
        {
            itterCount++;
            //границы сошлись
            if (first > last)
            {
                //элемент не найден
                return (-1, -1, itterCount, DateTime.Now.Ticks - time);
            }

            //средний индекс подмассива
            var middle = (first + last) / 2;

            //значение в средине подмассива
            var middleValue = array[middle];

            itterCount++;
            if (middleValue == searchedValue)
            {
                return (middleValue, middle, itterCount, DateTime.Now.Ticks - time);
            }
            else
            {
                itterCount++;
                if (middleValue > searchedValue)
                {
                    //рекурсивный вызов поиска для левого подмассива
                    return BinarySearch(array, searchedValue, first, middle - 1, itterCount, DateTime.Now.Ticks - time);
                }
                else
                {
                    //рекурсивный вызов поиска для правого подмассива
                    return BinarySearch(array, searchedValue, middle + 1, last, itterCount, DateTime.Now.Ticks - time);
                }
            }
        }

        public (string data, int position, int itterCount, long workTimeCount) BinarySearch(int left, int right, string[] array, string search, int itterCount = 0, long time = 0)
        {
            itterCount++;
            if (right - left < 0)
                return (null, -1, itterCount, DateTime.Now.Ticks - time);

            var index = (right - left) / 2 + left;
            var item = array[index];
            var compareResult = item.CompareTo(search);

            itterCount++;
            if (compareResult < 0)
                return BinarySearch(index + 1, right, array, search, itterCount, DateTime.Now.Ticks - time);
            itterCount++;
            if (compareResult > 0)
                return BinarySearch(left, index - 1, array, search, itterCount, DateTime.Now.Ticks - time);
            return (item, index, itterCount, DateTime.Now.Ticks - time);
        }

        public (int data, int position, int itterCount, long workTimeCount) interpolationSearch(int search, int[] array)
        {

            int itterCount = 0;
            long start = DateTime.Now.Ticks;
            int low = 0;
            int mid;
            int high = array.Length - 1;

            while (array[low] < search && array[high] > search)
            {
                itterCount++;
                mid = low + ((search - array[low]) * (high - low)) / (array[high] - array[low]);
                itterCount++;
                if (array[mid] < search)
                    low = mid + 1;

                itterCount++;
                if (array[mid] > search)
                    high = mid - 1;

                itterCount++;
                if (search == array[mid])
                {
                    return (search, mid + 1, itterCount, DateTime.Now.Ticks - start);

                }
            }

            return (search, -1, itterCount, DateTime.Now.Ticks - start);
        }

        static int[] GetPrefix(string s, ref int itterCount)
        {
            int[] result = new int[s.Length];
            result[0] = 0;
            int index = 0;

            for (int i = 1; i < s.Length; i++)
            {
                itterCount++;
                int k = result[i - 1];
                while (s[k] != s[i] && k > 0)
                {
                    itterCount++;
                    k = result[k - 1];
                }
                if (s[k] == s[i])
                {
                    itterCount++;
                    result[i] = k + 1;
                }
                else
                {
                    itterCount++;
                    result[i] = 0;
                }
            }
            return result;
        }

        public (int position, int itterCount, long workTimeCount) FindSubstring(string pattern, string text)
        {
            int itterCount = 0;

            long start = DateTime.Now.Ticks;

            int[] pf = GetPrefix(pattern, ref itterCount);
            int index = 0;

            for (int i = 0; i < text.Length; i++)
            {
                itterCount++;
                while (index > 0 && pattern[index] != text[i])
                {
                    index = pf[index - 1];
                    itterCount++;

                }
                if (pattern[index] == text[i])
                {
                    index++;
                    itterCount++;
                }
                if (index == pattern.Length)
                {
                    itterCount++;
                    return (i - index + 1, itterCount, DateTime.Now.Ticks - start);
                }
            }

            return (-1, itterCount, DateTime.Now.Ticks - start);
        }


        private void BadCharHeuristic(string str, int size, ref int[] badChar, ref int itterCount)
        {
            int i;

            for (i = 0; i < 256; i++)
            {
                itterCount++;
                badChar[i] = -1;
            }


            for (i = 0; i < size; i++)
            {
                itterCount++;
                badChar[(int)str[i]] = i;
            }

        }

        public (int[] arr, int itterCount, long workTimeCount)
            boyerMooreHorsepool(string pat, string str)
        {

            int itterCount = 0;

            long start = DateTime.Now.Ticks;

            List<int> retVal = new List<int>();
            int m = pat.Length;
            int n = str.Length;

            int[] badChar = new int[256];

            BadCharHeuristic(pat, m, ref badChar, ref itterCount);

            int s = 0;
            while (s <= (n - m))
            {
                int j = m - 1;
                itterCount++;

                while (j >= 0 && pat[j] == str[s + j])
                {
                    itterCount++;
                    --j;
                }


                if (j < 0)
                {
                    itterCount++;
                    retVal.Add(s);
                    s += (s + m < n) ? m - badChar[str[s + m]] : 1;
                }
                else
                {
                    itterCount++;

                    int index = Math.Min(255, (int)str[s + j]);
                    s += Math.Max(1, j - badChar[index]);
                }
            }


            return (retVal.ToArray(), itterCount, DateTime.Now.Ticks - start);
        }
    }
}
