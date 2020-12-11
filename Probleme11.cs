using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication2
{
    public class siege
    {
        public List<siege> voisin = new List<siege>();

        public state etat = state.vide;

        public state nextEtat = state.vide;
        public siege(char pos)
        {
            if (pos == '.')
            {
                etat = state.sol;
            }
            if (pos == '#')
            {
                etat = state.occupe;
            }
        }
        public int val()
        {
            if (etat == state.occupe)
                return 1;
            return 0;

        }
        public int exploreCote()
        {
            int res = 0;
            foreach (siege v in voisin)
            {
                res += v.val();
            }
            return res;
        }

        public void evaluateNextEtat()
        {
            if (etat == state.sol)
            {
                nextEtat = state.sol;
                return;
            }

            int cot = exploreCote();
            if (etat == state.vide && cot == 0)
            {
                nextEtat = state.occupe;
            }
            if (etat == state.occupe && cot >= 5)
            {
                nextEtat = state.vide;
            }
        }

        public override string ToString()
        {
            if (etat == state.vide)
            {
                return "L";
            }
            if (etat == state.occupe)
            {
                return "#";
            }
            else
            {
                return ".";
            }
        }


    }
    public enum state { vide, sol, occupe }
    public class day11
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
        static void myAlgo()
        {
            List<string> input = readInput();
            int w = input[0].Length;
            int h = input.Count;
            siege[,] allSiege = new siege[input.Count, input[0].Length];

            for (int t = 0; t < input.Count; t++)
            {
                for (int i = 0; i < input[0].Length; i++)
                {
                    allSiege[t, i] = new siege(input[t][i]);
                }
            }
            for (int t = 0; t < h; t++)
            {
                for (int i = 0; i < w; i++)
                {
                    for (int j = -1; j < 2; j++)
                    { 
                        if((t+j)>=0 &&  (t+j)< h )
                        {
                            for (int k = -1; k < 2; k++)
                            {
                                if ((i + k) >= 0 && (i + k) < w && (j != 0 || k != 0))
                                {

                                    for (int p = 1; p < 1000; p++)
                                    {
                                        try
                                        {
                                           if( allSiege[t + j * p, i + k * p].etat != state.sol)
                                           {
                                               allSiege[t, i].voisin.Add(allSiege[t + j * p, i + k * p]);
                                               break;
                                           }
                                        }catch
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }

            bool change = true;
            while (change)
            {
                for (int t = 0; t < h; t++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        allSiege[t, i].evaluateNextEtat();
                    }
                }
                change = false;
                for (int t = 0; t < h; t++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        if(allSiege[t, i].etat != allSiege[t, i].nextEtat)
                            change = true;
                        allSiege[t, i].etat = allSiege[t, i].nextEtat;
                    }
                }

            }

            int nbOcc = 0;
            for (int t = 0; t < h; t++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (allSiege[t, i].etat == state.occupe)
                        nbOcc++;
                }
            }

            for (int i = 0; i < input.Count; i++)
            {
            }
            for (int j = 0; j < input.Count; j++)
            {
            }
            for (int k = 0; k < input.Count; k++)
            {
            }

            long res = 5;
            res = 5;
        }

        static void myAlgo1()
        {
            List<string> input = readInput();
            int w = input[0].Length;
            int h = input.Count;
            siege[,] allSiege = new siege[input.Count, input[0].Length];

            for (int t = 0; t < input.Count; t++)
            {
                for (int i = 0; i < input[0].Length; i++)
                {
                    allSiege[t, i] = new siege(input[t][i]);
                }
            }
            for (int t = 0; t < h; t++)
            {
                for (int i = 0; i < w; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if ((t + j) >= 0 && (t + j) < h)
                        {
                            for (int k = -1; k < 2; k++)
                            {
                                if ((i + k) >= 0 && (i + k) < w && (j != 0 || k != 0))
                                    allSiege[t, i].voisin.Add(allSiege[t + j, i + k]);
                            }
                        }
                    }
                }
            }

            bool change = true;
            while (change)
            {
                for (int t = 0; t < h; t++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        allSiege[t, i].evaluateNextEtat();
                    }
                }
                change = false;
                for (int t = 0; t < h; t++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        if (allSiege[t, i].etat != allSiege[t, i].nextEtat)
                            change = true;
                        allSiege[t, i].etat = allSiege[t, i].nextEtat;
                    }
                }

            }

            int nbOcc = 0;
            for (int t = 0; t < h; t++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (allSiege[t, i].etat == state.occupe)
                        nbOcc++;
                }
            }

            for (int i = 0; i < input.Count; i++)
            {
            }
            for (int j = 0; j < input.Count; j++)
            {
            }
            for (int k = 0; k < input.Count; k++)
            {
            }

            long res = 5;
            res = 5;
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
        public static void Main11()
        {

            line = GetTest();
            myAlgo();
        }
        #endregion
    }
}
