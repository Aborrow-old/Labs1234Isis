using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace TestApp
{
    public class Class1
    {
        public static string loadfile(string path)
        {
            string text = File.ReadAllText(path);
            return text;
        }

        public static int Difference(int a, int b)
        {
            return a - b;
        }

        public static bool  Same (string a, string b)
        {
            if (a.Equals(b))
                return true;
            else return false;
        }

        public static int MinNum(int[] arr)
        {
            int minimum = arr[0];
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < minimum)
                    minimum = arr[i];
            }
            return minimum;
        }

       
        public static int countlines(string path)
        {
            string[] File1Lines = File.ReadAllLines(path);
            return File1Lines.Length;
        }

        public static bool CompareSquare(int x, int y)
        {
            if (x*y == x + y)
                return true;
            else
                return false;
        }

        public static bool Palindrom(string s)
        {
            for (int i = 0; i < s.Length / 2; i++)

                if (s[i] != s[s.Length - i - 1])
                    return false;
            return true;
        }

        static bool SpaceFinder(char chr)
        {
            if (chr == ' ') return true;
            else return false;
        }

        static bool DotFinder(char chr)
        {
            if (chr == '.') return true;
            else return false;
        }

        public static int CountSpace(string str)
        {
            char[] chars = str.ToCharArray();
            char[] findChars = Array.FindAll(chars, SpaceFinder);
            int count = findChars.Length;
            return count;
        }

        public static int CountDot(string str)
        {
            char[] chars = str.ToCharArray();
            char[] findChars = Array.FindAll(chars, DotFinder);
            int count = findChars.Length;
            return count;
        }
    }
}
