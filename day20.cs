using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConsoleApplication2.MathBase;
using System.Windows.Forms;

namespace ConsoleApplication2
{
    public abstract class bloc
    {
        public abstract bool canDo(string mot);
        public abstract List<string> generateAllPoss();
        public abstract List<int> updateTailleAvailable();
    }
    public class simpleChar : bloc
    {
        public char c = 'c';
        public node node2 = null;
        List<string> allPoss;
        List<int> alltaille;

        public simpleChar(char c)
        {
            this.c = c;
            allPoss = new List<string>();
            allPoss.Add(Convert.ToString(c));
            alltaille = new List<int>();
            alltaille.Add(1);
        }

        public override bool canDo(string mot)
        {
            return mot.Length == 1 && mot[0] == c;
        }
        public override List<string> generateAllPoss()
        {
            return allPoss;
        }
        public override List<int> updateTailleAvailable()
        {
            return alltaille;
        }
    }

    public class node : bloc
    {
        public int val;
        public string codeS;

        List<bloc> code1 = new List<bloc>();
        List<bloc> code2 = new List<bloc>();

        public node(string line)
        {
            string[] pl = line.Split(':');
            val = Convert.ToInt32(pl[0]);
            codeS = pl[1];
        }

        public void fill(Dictionary<int, node> nodes)
        {
            string[] pl = codeS.Substring(1, codeS.Length - 1).Split(' ');
            int currr = -1;
            bool activeDual = false;
            for (int i = 0; i < pl.Length; i++)
            {
                if (pl[i] == "|")
                {
                    activeDual = true;
                }
                else if (pl[i][0] == '"')
                {
                    code1.Add(new simpleChar(pl[i][1]));
                }
                else
                {
                    currr = Convert.ToInt32(pl[i]);
                    if (activeDual)
                    {
                        code2.Add(nodes[currr]);
                    }
                    else
                    {
                        code1.Add(nodes[currr]);
                    }
                }
            }

        }

        public bool canDo(List<bloc> code, string mot)
        {
            if (code.Count == 0)
            {
                return false;
            }
            if (code.Count == 1)
            {
                return code[0].canDo(mot);
            }
            if (code.Count == 3)
            {
                return code[0].canDo(mot);
            }

            int lengthMot = mot.Length;

            bool find = false;
            for (int i = 1; i < lengthMot; i++)
            {
                if (code[0].updateTailleAvailable().Contains(i) && code[1].updateTailleAvailable().Contains(mot.Length - i))
                {
                    string mot1 = mot.Substring(0, i);
                    string mot2 = mot.Substring(i, mot.Length - i);

                    find = code[0].canDo(mot1) && code[1].canDo(mot2);
                    if (find)
                        break;
                }
            }
            return find;
        }
        public override bool canDo(string mot)
        {
            return canDo(code1, mot) || canDo(code2, mot);
        }


        bool isGenerateTaille = false;
        public List<int> tailleAvailable = new List<int>();
        public List<int> updateTailleAvailable(List<bloc> code)
        {
            List<int> gen = new List<int>();
            if (code.Count == 0)
                return gen;
            gen = code[0].updateTailleAvailable();
            for (int i = 1; i < code.Count; i++)
            {
                List<int> gen2 = code[i].updateTailleAvailable();
                List<int> test = new List<int>();

                foreach (int t1 in gen)
                {
                    foreach (int t2 in gen2)
                    {
                        if (!test.Contains(t1 + t2))
                            test.Add(t1 + t2);
                    }
                }
                gen = test;
            }
            return gen;

        }
        public override List<int> updateTailleAvailable()
        {
            if (!isGenerateTaille)
            {
                tailleAvailable = updateTailleAvailable(code1);
                List<int> gen2 = updateTailleAvailable(code2);
                foreach (int t2 in gen2)
                {
                    if (!tailleAvailable.Contains(t2))
                        tailleAvailable.Add(t2);
                }
                isGenerateTaille = true;
            }
            return tailleAvailable;
        }




        bool isGenerate = false;
        public List<string> allPoss;
        public List<string> generateAllPoss(List<bloc> code)
        {
            List<string> gen = new List<string>();
            if (code.Count == 0)
                return gen;
            gen = code[0].generateAllPoss();
            for (int i = 1; i < code.Count; i++)
            {
                List<string> gen2 = code[i].generateAllPoss();
                List<string> test = new List<string>();

                foreach (string t1 in gen)
                {
                    foreach (string t2 in gen2)
                    {
                        if (t1 != "" && t2 != "" && !test.Contains(t1 + t2))
                            test.Add(t1 + t2);
                    }
                }
                gen = test;
            }
            return gen;

        }
        public override List<string> generateAllPoss()
        {
            if (!isGenerate)
            {
                allPoss = generateAllPoss(code1);
                List<string> gen2 = generateAllPoss(code2);
                foreach (string t2 in gen2)
                {
                    if (!allPoss.Contains(t2))
                        allPoss.Add(t2);
                }
                isGenerate = true;
            }
            return allPoss;
        }



    }


    public class day20
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

        static long myAlgo1()
        {

            List<string> input = readInput();
            long res = 0;
            //List<int> all = new List<int>();

            Dictionary<int, node> nodes = new Dictionary<int, node>(); ;

            List<string> allMot = new List<string>();
            bool Mot = false;
            for (int t = 0; t < input.Count; t++)
            {
                if (input[t].Length < 1)
                {
                    Mot = true;
                    continue;
                }
                if (!Mot)
                {
                    node node = new node(input[t]);
                    nodes.Add(node.val, node);
                }
                else
                {
                    allMot.Add(input[t]);
                }
            }
            foreach (node n in nodes.Values)
            {
                n.fill(nodes);
            }

            List<string> gen8 = nodes[8].generateAllPoss();
            List<string> gen11 = nodes[11].generateAllPoss();

            foreach (string mot in allMot)
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    string mot1 = mot.Substring(0, i);
                    string mot2 = mot.Substring(i, mot.Length - i);
                    if (gen8.Contains(mot1) && gen11.Contains(mot2))
                    {
                        res++;
                        break;
                    }
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

            return res;

        }

        static long myAlgo()
        {

            List<string> input = readInput();
            long res = 0;

            Dictionary<int, node> nodes = new Dictionary<int, node>(); ;

            List<string> allMot = new List<string>();
            bool Mot = false;
            for (int t = 0; t < input.Count; t++)
            {
                if (input[t].Length < 1)
                {
                    Mot = true;
                    continue;
                }
                if (!Mot)
                {
                    node node = new node(input[t]);
                    nodes.Add(node.val, node);
                }
                else
                {
                    allMot.Add(input[t]);
                }
            }
            foreach (node n in nodes.Values)
            {
                n.fill(nodes);
            }
            nodes[0].updateTailleAvailable();

            /*foreach (string mot in allMot)
            {
                if (nodes[0].canDo(mot))
                {
                    res++;
                }
            }*/

            /*List<string> gen0 = nodes[0].generateAllPoss();
            foreach (string mot in allMot)
            {
                if (gen0.Contains(mot))
                {
                    res++;
                }
            }*/

            //retourne un resultat faux ....
            List<string> gen42 = nodes[42].generateAllPoss();
            List<string> gen31 = nodes[31].generateAllPoss();



            node node42 = nodes[42];
            node node31 = nodes[31];


            for (int i = 0; i < gen42.Count; )
            {
                if (!node42.canDo(gen42[i]))
                {
                    gen42.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
            for (int i = 0; i < gen31.Count; )
            {
                if (!node31.canDo(gen31[i]))
                {
                    gen31.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
            res = 0;
            foreach (string mot in allMot)
            {
                if (nodes[0].canDo(mot))
                {
                }
                bool isOK = false;
                for (int p = 1; p < 100; p++)
                {
                    for (int k = 1; k < p; k++)
                    {
                        if (p * 8 + k * 8 == mot.Length)
                        {
                            bool oneFalse = false;
                            for (int a = 0; a < p; a++)
                            {
                                string ssMot = mot.Substring(a * 8, 8);
                                if (!gen42.Contains(ssMot))
                                {
                                    oneFalse = true;
                                    break;
                                }
                            }
                            for (int a = 0; a < k; a++)
                            {
                                string ssMot = mot.Substring(p * 8 + a * 8, 8);
                                if (!gen31.Contains(ssMot))
                                {
                                    oneFalse = true;
                                    break;
                                }
                            }
                            if (!oneFalse)
                            {
                                isOK = true;
                                break;
                            }
                        }
                    }
                    if (isOK)
                    {
                        break;
                    }
                }
                if (isOK)
                {
                    res++;
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

            return res;

        }


        static void result()
        {
            /*line = GetTestDebug();
             long resDebug = myAlgo();
             */
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
        public static void Main()
        {
            result();
        }
        #endregion
    }
}
