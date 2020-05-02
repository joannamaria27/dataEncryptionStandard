using System;
using System.Collections.Generic;
using System.IO;

namespace BSK_DES
{
    class Program
    {




        public static char[,] RiL(char[] blokR, char[] blokL, char[,] tablicaPermutacjiKlucza2, int numer)
        {
            char[,] rl = new char[2, 32];
            char[] blokR8 = Des.Permutacja(blokR, Des.E); //8.
                                                      //9. xor Kn, Rn-1
            char[] tablicaK = new char[48];
            for (int i = 0; i < 48; i++)
            {
                tablicaK[i] = tablicaPermutacjiKlucza2[numer, i];
            }
            char[] tablicaXOR = Des.Xorowanie(tablicaK, blokR8);

            //10.
            int p = 0;
            char[,] S = new char[8, 6];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    S[i, j] = tablicaXOR[p]; p++;
                }
            }

            char[] ciag6bit1 = Des.Odczytywanie(0, S, Des.S1); //11-12.
            char[] ciag6bit2 = Des.Odczytywanie(1, S, Des.S2);
            char[] ciag6bit3 = Des.Odczytywanie(2, S, Des.S3);
            char[] ciag6bit4 = Des.Odczytywanie(3, S, Des.S4);
            char[] ciag6bit5 = Des.Odczytywanie(4, S, Des.S5);
            char[] ciag6bit6 = Des.Odczytywanie(5, S, Des.S6);
            char[] ciag6bit7 = Des.Odczytywanie(6, S, Des.S7);
            char[] ciag6bit8 = Des.Odczytywanie(7, S, Des.S8);

            //13.
            char[] ciagR = new char[32];
            int r = 0;
            foreach (char i in ciag6bit1)
            {
                ciagR[r++] = i;
            }
            foreach (char i in ciag6bit2)
            {
                ciagR[r++] = i;
            }
            foreach (char i in ciag6bit3)
            {
                ciagR[r++] = i;
            }
            foreach (char i in ciag6bit4)
            {
                ciagR[r++] = i;
            }
            foreach (char i in ciag6bit5)
            {
                ciagR[r++] = i;
            }
            foreach (char i in ciag6bit6)
            {
                ciagR[r++] = i;
            }
            foreach (char i in ciag6bit7)
            {
                ciagR[r++] = i;
            }
            foreach (char i in ciag6bit8)
            {
                ciagR[r++] = i;
            }

            char[] RPermutacja = Des.Permutacja(ciagR, Des.P); //14.
            //xor Ln-1 Rn-1
            char[] blokRn = Des.Xorowanie(RPermutacja, blokL); //15.
            char[] blokLn = blokR; //16.
            for (int i = 0; i < 32; i++)
            {
                rl[0, i] = blokRn[i];
                rl[1, i] = blokLn[i];
            }

            return rl;
        }
        public static char[,] Laczenie(char[,] ril, char[,] tablicaPermutacjiKlucza2, int numer)
        {
            char[] blokRn = new char[32];
            char[] blokLn = new char[32];
            for (int i = 0; i < 32; i++)
            {
                blokLn[i] = ril[0, i];
                blokRn[i] = ril[1, i];
            }
            char[,] riln = RiL(blokLn, blokRn, tablicaPermutacjiKlucza2, numer);
            return riln;
        }



        static void Main(string[] args)
        {
            //FileHandler.ReadFromTextFile("input.txt");
            //FileHandler.ReadFromBinFile("cat.jpg");

            Console.WriteLine("halo!");
            Console.WriteLine("Menu\n 1-KODOWANIE\n 2-ODKODOWANIE");

            int wybor = int.Parse(Console.ReadLine());
            switch (wybor)
            {
                case 1:
                    {
                        Console.WriteLine("KODOWANIE");

                        Console.WriteLine("Podaj nazwę z rozszezenie do odczytu\n");
                        string plikb = Console.ReadLine();
                        string tekstJawny = FileHandler.ReadFromTextFile(plikb); //zm

                        Console.WriteLine("Podaj plik txt do odczytu klucza\n");
                        string plikk = Console.ReadLine();
                        string klucz = FileHandler.ReadFromTextFile(plikk);

                        while(tekstJawny.Length<64)
                        {
                            tekstJawny = "0" + tekstJawny;
                        }
                        while (klucz.Length < 64)
                        {
                            klucz = "0" + klucz;
                        }
                        char[] tablicaPoczatkowa = new char[64];
                        char[] tablicaPoczatkowaKlucz = new char[64];

                        int m = 0;
                        for (int a = 0; a < (tekstJawny.Length / 64); a++)
                        {

                            for (int i = 0; i < 64; i++)
                            {
                                tablicaPoczatkowa[i] = tekstJawny[m++];
                                tablicaPoczatkowaKlucz[i] = klucz[i];

                            }

                            char[] tablicaPermutacji = Des.Permutacja(tablicaPoczatkowa, Des.ip); //2.
                            char[] blokL = Des.Dzielenie(tablicaPermutacji, 32, 0); //3.
                            char[] blokP = Des.Dzielenie(tablicaPermutacji, 32, 32); //3.

                            //klucz 4 -7 
                            char[] tablicaPermutacjiKlucza = Des.Permutacja(tablicaPoczatkowaKlucz, Des.pc); //4.
                            char[] kluczC = Des.Dzielenie(tablicaPermutacjiKlucza, 28, 0); //5.
                            char[] kluczD = Des.Dzielenie(tablicaPermutacjiKlucza, 28, 28); //5.

                            //6.
                            List<char> kluczC6 = new List<char>(kluczC);
                            List<char> kluczD6 = new List<char>(kluczD);
                            char[,] kluczeCpo6 = new char[16, 28];
                            char[,] kluczeDpo6 = new char[16, 28];
                            for (int i = 0; i < 16; i++)
                            {
                                kluczC6.AddRange(kluczC6.GetRange(0, Des.przesuniecie6[i]));
                                kluczC6.RemoveRange(0, Des.przesuniecie6[i]);

                                kluczD6.AddRange(kluczD6.GetRange(0, Des.przesuniecie6[i]));
                                kluczD6.RemoveRange(0, Des.przesuniecie6[i]);

                                for (int j = 0; j < 28; j++)
                                {
                                    kluczeCpo6[i, j] = kluczC6[j];
                                    kluczeDpo6[i, j] = kluczD6[j];
                                }
                            }

                            //7.
                            char[,] K = new char[16, 56];
                            for (int i = 0; i < 16; i++)
                            {
                                for (int j = 0; j < 28; j++)
                                {
                                    K[i, j] = kluczeCpo6[i, j];

                                }
                                for (int j = 28, k = 0; j < 56; j++, k++)
                                {
                                    K[i, j] = kluczeDpo6[i, k];

                                }
                            }

                            int pomocnicza = 0;
                            char[,] tablicaPermutacjiKlucza2 = new char[16, 48];
                            for (int i = 0; i < 16; i++)
                            {
                                for (int j = 0; j < 48; j++)
                                {
                                    pomocnicza = Des.pc2[j];
                                    tablicaPermutacjiKlucza2[i, j] = K[i, pomocnicza - 1];
                                }
                            }

                            /////////////////////////////////////////
                            /// 8-16. 
                            char[,] ril0 = RiL(blokP, blokL, tablicaPermutacjiKlucza2, 0);
                            char[,] ril1 = Laczenie(ril0, tablicaPermutacjiKlucza2, 1);
                            char[,] ril2 = Laczenie(ril1, tablicaPermutacjiKlucza2, 2);
                            char[,] ril3 = Laczenie(ril2, tablicaPermutacjiKlucza2, 3);
                            char[,] ril4 = Laczenie(ril3, tablicaPermutacjiKlucza2, 4);
                            char[,] ril5 = Laczenie(ril4, tablicaPermutacjiKlucza2, 5);
                            char[,] ril6 = Laczenie(ril5, tablicaPermutacjiKlucza2, 6);
                            char[,] ril7 = Laczenie(ril6, tablicaPermutacjiKlucza2, 7);
                            char[,] ril8 = Laczenie(ril7, tablicaPermutacjiKlucza2, 8);
                            char[,] ril9 = Laczenie(ril8, tablicaPermutacjiKlucza2, 9);
                            char[,] ril10 = Laczenie(ril9, tablicaPermutacjiKlucza2, 10);
                            char[,] ril11 = Laczenie(ril10, tablicaPermutacjiKlucza2, 11);
                            char[,] ril12 = Laczenie(ril11, tablicaPermutacjiKlucza2, 12);
                            char[,] ril13 = Laczenie(ril12, tablicaPermutacjiKlucza2, 13);
                            char[,] ril14 = Laczenie(ril13, tablicaPermutacjiKlucza2, 14);
                            char[,] ril15 = Laczenie(ril14, tablicaPermutacjiKlucza2, 15);


                            //17.
                            char[] koniec = new char[64];
                            for (int i = 0; i < 32; i++)
                            {
                                koniec[i] = ril15[0, i];
                            }
                            int d = 32;
                            for (int i = 0; i < 32; i++)
                            {
                                koniec[d++] = ril15[1, i];
                            }

                            char[] koniecPer = Des.Permutacja(koniec, Des.IP1minus1); //18.
                            Console.WriteLine(koniecPer);

                            string str = new string(koniecPer);


                            string wyjscie = FileHandler.BinaryStringToHexString(str);
                            File.WriteAllText(@"zakodowane.txt", wyjscie);
                          
                        }
                        break;
                    }
                case 2:
                    {

                        string tekstZakodowany = "";
                        Console.WriteLine("KODOWANIE");

                        Console.WriteLine("Podaj nazwę z rozszezenie do odczytu\n");
                        string plikb = Console.ReadLine();
                        tekstZakodowany = FileHandler.ReadFromTextFile(plikb); //zm

                        Console.WriteLine("Podaj plik txt do odczytu klucza\n");
                        string plikk = Console.ReadLine();
                        string klucz = FileHandler.ReadFromTextFile(plikk);
                        while (tekstZakodowany.Length < 64)
                        {
                            tekstZakodowany = "0" + tekstZakodowany;
                        }
                        while (klucz.Length < 64)
                        {
                            klucz = "0" + klucz;
                        }
                        char[] tablicaPoczatkowa = new char[64];
                        char[] tablicaPoczatkowaKlucz = new char[64];

                        int m = 0;
                        for (int a = 0; a < tekstZakodowany.Length / 64; a++)
                        {

                            for (int i = 0; i < 64; i++)
                            {
                                tablicaPoczatkowa[i] = tekstZakodowany[m];
                                tablicaPoczatkowaKlucz[i] = klucz[i];
                                m++;
                            }

                            char[] tablicaPermutacji = Des.Permutacja(tablicaPoczatkowa, Des.ip); //2.
                            char[] blokL = Des.Dzielenie(tablicaPermutacji, 32, 0); //3.
                            char[] blokP = Des.Dzielenie(tablicaPermutacji, 32, 32); //3.

                            //klucz 4 -7 
                            char[] tablicaPermutacjiKlucza = Des.Permutacja(tablicaPoczatkowaKlucz, Des.pc); //4.
                            char[] kluczC = Des.Dzielenie(tablicaPermutacjiKlucza, 28, 0); //5.
                            char[] kluczD = Des.Dzielenie(tablicaPermutacjiKlucza, 28, 28); //5.

                            //6.
                            List<char> kluczC6 = new List<char>(kluczC);
                            List<char> kluczD6 = new List<char>(kluczD);
                            char[,] kluczeCpo6 = new char[16, 28];
                            char[,] kluczeDpo6 = new char[16, 28];
                            for (int i = 0; i < 16; i++)
                            {
                                kluczC6.AddRange(kluczC6.GetRange(0, Des.przesuniecie6[i]));
                                kluczC6.RemoveRange(0, Des.przesuniecie6[i]);

                                kluczD6.AddRange(kluczD6.GetRange(0, Des.przesuniecie6[i]));
                                kluczD6.RemoveRange(0, Des.przesuniecie6[i]);

                                for (int j = 0; j < 28; j++)
                                {
                                    kluczeCpo6[i, j] = kluczC6[j];
                                    kluczeDpo6[i, j] = kluczD6[j];
                                }
                            }

                            //7.
                            char[,] K = new char[16, 56];
                            for (int i = 0; i < 16; i++)
                            {
                                for (int j = 0; j < 28; j++)
                                {
                                    K[i, j] = kluczeCpo6[i, j];

                                }
                                for (int j = 28, k = 0; j < 56; j++, k++)
                                {
                                    K[i, j] = kluczeDpo6[i, k];

                                }
                            }

                            int pomocnicza = 0;
                            char[,] tablicaPermutacjiKlucza2 = new char[16, 48];
                            for (int i = 0; i < 16; i++)
                            {
                                for (int j = 0; j < 48; j++)
                                {
                                    pomocnicza = Des.pc2[j];
                                    tablicaPermutacjiKlucza2[i, j] = K[i, pomocnicza - 1];
                                }
                            }

                            /////////////////////////////////////////
                            /// 8-16. 
                            char[,] ril0 = RiL(blokP, blokL, tablicaPermutacjiKlucza2, 15);
                            char[,] ril1 = Laczenie(ril0, tablicaPermutacjiKlucza2, 14);
                            char[,] ril2 = Laczenie(ril1, tablicaPermutacjiKlucza2, 13);
                            char[,] ril3 = Laczenie(ril2, tablicaPermutacjiKlucza2, 12);
                            char[,] ril4 = Laczenie(ril3, tablicaPermutacjiKlucza2, 11);
                            char[,] ril5 = Laczenie(ril4, tablicaPermutacjiKlucza2, 10);
                            char[,] ril6 = Laczenie(ril5, tablicaPermutacjiKlucza2, 9);
                            char[,] ril7 = Laczenie(ril6, tablicaPermutacjiKlucza2, 8);
                            char[,] ril8 = Laczenie(ril7, tablicaPermutacjiKlucza2, 7);
                            char[,] ril9 = Laczenie(ril8, tablicaPermutacjiKlucza2, 6);
                            char[,] ril10 = Laczenie(ril9, tablicaPermutacjiKlucza2, 5);
                            char[,] ril11 = Laczenie(ril10, tablicaPermutacjiKlucza2, 4);
                            char[,] ril12 = Laczenie(ril11, tablicaPermutacjiKlucza2, 3);
                            char[,] ril13 = Laczenie(ril12, tablicaPermutacjiKlucza2, 2);
                            char[,] ril14 = Laczenie(ril13, tablicaPermutacjiKlucza2, 1);
                            char[,] ril15 = Laczenie(ril14, tablicaPermutacjiKlucza2, 0);


                            //17.
                            char[] koniec = new char[64];
                            for (int i = 0; i < 32; i++)
                            {
                                koniec[i] = ril15[0, i];
                            }
                            int d = 32;
                            for (int i = 0; i < 32; i++)
                            {
                                koniec[d++] = ril15[1, i];
                            }

                            char[] koniecPer = Des.Permutacja(koniec, Des.IP1minus1); //18.

                            Console.WriteLine(koniecPer);

                            string str = new string(koniecPer);


                            string wyjscie = FileHandler.BinaryStringToHexString(str);
                            File.WriteAllText(@"odkodowane.txt", wyjscie);
                           
                        }

                        break;
                    }
            }
        }
    }
}
