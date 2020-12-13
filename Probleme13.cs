using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConsoleApplication2.MathBase;

namespace ConsoleApplication2
{
    public class bus
    {
        public int startTime;
        public bool unactive  = false;
        public bus(string t)
        {
            if(t == "x")
            {
                unactive = true;
            }
            else
            {
                startTime = Convert.ToInt32(t);
            }
        }



    }

    public class day13
    {

        static List<string> readInput()
        {
            List<string> inputs = new List<string>();
            for (int t = 0; t < 5000000; t++)
            {
                string text = "";
                try
                {
                    text = getS();
                }
                catch
                {
                    break;
                }
                inputs.Add(text);
            }
            return inputs;
        }

        static long myAlgo()
        {

            int timeStart = getI();
            string t = getS();

            string[] all = t.Split(',');

            List<int> allBusStart = new List<int>();
            List<bus> allBus = new List<bus>();

            for (int i = 0; i < all.Length; i++)
            {
                allBus.Add(new bus(all[i]));
            }

            
            long bestDif = 50000000;

            long factCurrent = allBus[0].startTime;

            long res = 0;

            for (int j = 1; j < allBus.Count; j++)
            {
                bus b = allBus[j];
                if (b.unactive)
                    continue;


                while (true)
                {
                    if (b.startTime - (res % b.startTime) == j % b.startTime)
                    {
                        break;
                    }
                    else
                    {
                        res += factCurrent;
                    }
                }
                factCurrent = factCurrent * b.startTime / BaseMath.PGCD(factCurrent, b.startTime);


                factCurrent *= 1;

            }

            return res;

        }

        static void result()
        {
            line = GetTestDebug();
            long resDebug = myAlgo();

            line = GetTest();
            long resFinal = myAlgo();
            resFinal = 0;
        }




        #region main
        public static string getS()
        {
            return nextLine();
        }
        static int currentLine = -1;
        static List<string> line;
        public static string nextLine()
        {
            currentLine++;
            return line[currentLine];
        }
        public static int getI()
        {
            return Convert.ToInt32(nextLine());
        }
        public static List<string> GetTestDebug()
        {
            currentLine = -1;
            List<string> lines = new List<string>();

            StreamReader file = new System.IO.StreamReader(@"C:\Users\Henri\Desktop\CodeChallenge\InputTest.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();
            return lines;
        }
        public static List<string> GetTest()
        {
            currentLine = -1;
            List<string> lines = new List<string>();

            StreamReader file = new System.IO.StreamReader(@"C:\Users\Henri\Desktop\CodeChallenge\Input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();
            return lines;
        }
        public static void Main()
        {
            result();
        }
        #endregion
    }
}
