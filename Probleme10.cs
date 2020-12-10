  static void myAlgo()
        {
            List<string> input = readInput();
            List<int> all = new List<int>();
            for (int t = 0; t < input.Count; t++)
            {
                all.Add(Convert.ToInt32(input[t]));
            }
            all.Add(0);
            all.Sort();
            all.Add(all[all.Count - 1] + 3);

            List<int> dif = new List<int>();

            for (int t = 1; t < all.Count; t++)
            {
                dif.Add(all[t] - all[t - 1]);
            }

            int nbcur = 0;
            
            long tot = 1;
            for (int i = 0; i < dif.Count; i++)
            {
                if (dif[i] == 1)
                {
                    nbcur++;
                }
                else
                {
                    if(nbcur == 2)
                    {
                        tot *= 2;
                    }
                     if(nbcur == 3)
                    {
                        tot *= 4;
                    }
                     if(nbcur == 4)
                    {
                        tot *= 7;
                    }
                    nbcur = 0;
                }
            }


            long result = tot;
        }
