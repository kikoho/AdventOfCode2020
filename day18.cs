using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConsoleApplication2.MathBase;
using System.Windows.Forms;

namespace ConsoleApplication2
{

    public class day18
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

     

        static long evaluate(string line)
        {
            List<long> value = new List<long>();
            List<bool> oper = new List<bool>();
            List<int> nbParOp = new List<int>();

            int nbPar = 0;
            int nbParMax = 0;
            for (int t = 0; t < line.Length; t++)
            {
                if (line[t] == ' ')
                {
                }
                else if (line[t] == '(')
                {
                    nbPar++;
                    nbParMax = Math.Max(nbParMax, nbPar);
                }

                else if (line[t] == ')')
                {
                    nbPar--;
                }
                else if (line[t] == '*')
                {
                    oper.Add(true);
                    nbParOp.Add(nbPar);
                }
                else if (line[t] == '+')
                {
                    oper.Add(false);
                    nbParOp.Add(nbPar);
                }
                else
                {
                    value.Add(Convert.ToInt32(Convert.ToString(line[t])));
                }
            }

            for (int i = nbParMax; i >= 0; i--)
            {
                for (int p = 0; p < nbParOp.Count; )
                {

                    if (nbParOp[p] == i && !oper[p])
                    {
                        long t = 0;
                       
                            t = value[p] + value[p + 1];
                        value.RemoveAt(p);
                        value.RemoveAt(p);
                        value.Insert(p, t);
                        nbParOp.RemoveAt(p);
                        oper.RemoveAt(p);
                    }
                    else
                    {
                        p++;
                    }
                }

                for (int p = 0; p < nbParOp.Count; )
                {

                    if (nbParOp[p] == i && oper[p])
                    {
                        long t = 0;
                        t = value[p] * value[p + 1];
                      
                        value.RemoveAt(p);
                        value.RemoveAt(p);
                        value.Insert(p, t);
                        nbParOp.RemoveAt(p);
                        oper.RemoveAt(p);
                    }
                    else
                    {
                        p++;
                    }
                }
            }
            return value[0];
        }


        static long myAlgo()
        {

            List<string> input = readInput();
            long res = 0;
            //List<int> all = new List<int>();

            foreach (string lin in input)
            {
                res += evaluate(lin);
            }

            for (int t = 0; t < input.Count; t++)
            {

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

            return res;

        }

        static void result()
        {
            line = GetTestDebug();
            long resDebug = myAlgo();

            line = GetTest();
            long resFinal = myAlgo();
            Clipboard.SetText(resFinal + "");
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

        [STAThread]
        public static void Main18()
        {
            result();
        }
        #endregion
    }
}
