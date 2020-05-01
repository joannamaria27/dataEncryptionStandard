﻿using System;
using System.Collections.Generic;

namespace BSK_DES
{
    class Program
    {


        public static char[] Permutacja(char[] tablica, int[] tabPer)
        {
            char[] tablicaPermutacji = new char[tabPer.Length];
            int pomocnicza = 0;
            for (int i = 0; i < tabPer.Length; i++)
            {
                pomocnicza = tabPer[i];
                tablicaPermutacji[i] = tablica[pomocnicza - 1];
            }

            return tablicaPermutacji;
        }

        public static char[] Xorowanie(char[] tablicaK, char[] tablicaR)
        {
            char[] XOR = new char[48];
            for (int i = 0; i < 48; i++)
            {
                if (tablicaK[i] == tablicaR[i])
                {
                    XOR[i] = '0';
                }
                else
                {
                    XOR[i] = '1';
                }
            }
            return XOR;
        }

        public static int[] ip = new int[64] { 58,50,42,34,26,18,10,2,
                                             60,52,44,36,28,20,12,4,
                                             62,54,46,38,30,22,14,6,
                                             64,56,48,40,32,24,16,8,
                                             57,49,41,33,25,17,9,1,
                                             59,51,43,35,27,19,11,3,
                                             61,53,45,37,29,21,13,5,
                                             63,55,47,39,31,23,15,7};

        public static int[] pc = new int[56] { 57,49,41,33,25,17,9,
                                             1,58,50,42,34,26,18,
                                             10,2,59,51,43,35,27,
                                             19,11,3,60,52,44,36,
                                             63,55,47,39,31,23,15,
                                             7,62,54,46,38,30,22,
                                             14,6,61,53,45,37,29,
                                             21,13,5,28,20,12,4};

        public static int[] przesuniecie6 = new int[16] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        public static int[] pc2 = new int[48] { 14,  17, 11, 24,  1,  5,
                                               3,  28, 15,  6, 21, 10,
                                               23, 19, 12,  4, 26,  8,
                                               16,  7, 27, 20, 13,  2,
                                               41, 52, 31, 37, 47, 55,
                                               30, 40, 51, 45, 33, 48,
                                               44, 49, 39, 56, 34, 53,
                                               46, 42, 50, 36, 29, 32};


        public static int[] E = new int[48] { 32,  1,  2,  3,  4,  5,
                                             4,  5,  6,  7,  8,  9,
                                             8,  9, 10, 11, 12, 13,
                                            12, 13, 14, 15, 16, 17,
                                            16, 17, 18, 19, 20, 21,
                                            20, 21, 22, 23, 24, 25,
                                            24, 25, 26, 27, 28, 29,
                                            28, 29, 30, 31, 32,  1};


        public static int[,] S1 = new int[4, 16] { { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                                                   { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                                                   { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                                                   { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 } };


        public static int[,] S2 = new int[4, 16] { { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
                                                   { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
                                                   { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                                                   { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } };

        public static int[,] S3 = new int[4, 16] { { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                                                   { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                                                   { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                                                   { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 } };

        public static int[,] S4 = new int[4, 16] { { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                                                   { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
                                                   { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                                                   { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 } };

        public static int[,] S5 = new int[4, 16] { { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                                                   { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
                                                   { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                                                   { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 } };

        public static int[,] S6 = new int[4, 16] { { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                                                   { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
                                                   { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                                                   { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } };

        public static int[,] S7 = new int[4, 16] { { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                                                   { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
                                                   { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                                                   { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 } };

        public static int[,] S8 = new int[4, 16] { { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
                                                   { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
                                                   { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                                                   { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } };


        public static int[] P = new int[32] { 16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10, 2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25 };

        public static char[] Dzielenie(char[] tablica, int rozmiar, int kolejne)
        {
            char[] blok = new char[rozmiar];
            int l = kolejne;
            for (int i = 0; i < rozmiar; i++)
            {
                blok[i] = tablica[l]; l++;
            }
            return blok;
        }

        static void Main(string[] args)
        {


            Console.WriteLine("DES!");


            Console.WriteLine("Menu\n 1-KODOWANIE");
            int wybor = int.Parse(Console.ReadLine());
            switch (wybor)
            {
                case 1:

                    ////////////////////////wprowadzenie liczb - narazie ręcznie 64 zera i jednynki
                    ///1234567890111111111111111111111111111111111111111111111111111111
                    Console.WriteLine("KODOWANIE");
                    Console.WriteLine("Wprowadz 64 bitowy tekst binarniy: ");
                    string tekstJawny = Console.ReadLine();
                    Console.WriteLine("Wprowadz 64 bitowy klucz binarniy: ");
                    string klucz = Console.ReadLine();
                    ////////////////////////podzielenie na bloki 64 bitowe - narazie tylko jeden blok wprowadzany jest


                    char[] tablicaPoczatkowa = new char[64];
                    char[] tablicaPoczatkowaKlucz = new char[64];

                    int m = 0;
                    for (int i = 0; i < 64; i++)
                    {
                        tablicaPoczatkowa[i] = tekstJawny[m];
                        tablicaPoczatkowaKlucz[i] = tekstJawny[m];
                        m++;
                    }

                    char[] tablicaPermutacji = Permutacja(tablicaPoczatkowa, ip); //2.
                    char[] blokR = Dzielenie(tablicaPermutacji, 32, 0); //3.
                    char[] blokL = Dzielenie(tablicaPermutacji, 32, 32); //3.

                    //klucz 4 -7 
                    char[] tablicaPermutacjiKlucza = Permutacja(tablicaPoczatkowaKlucz, pc); //4.
                    char[] kluczC = Dzielenie(tablicaPermutacji, 28, 0); //5.
                    char[] kluczD = Dzielenie(tablicaPermutacji, 28, 28); //5.

                    //6.
                    List<char> kluczC6 = new List<char>(kluczC);
                    List<char> kluczD6 = new List<char>(kluczD);
                    char[,] kluczeCpo6 = new char[16, 28];
                    char[,] kluczeDpo6 = new char[16, 28];
                    for (int i = 0; i < 16; i++)
                    {
                        for (int j = 0; j < 28; j++)
                        {
                            kluczC6.AddRange(kluczC6.GetRange(0, przesuniecie6[i]));
                            kluczC6.RemoveRange(0, przesuniecie6[i]);
                            kluczeCpo6[i, j] = kluczC6[j];
                            kluczD6.AddRange(kluczD6.GetRange(0, przesuniecie6[i]));
                            kluczD6.RemoveRange(0, przesuniecie6[i]);
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
                            pomocnicza = pc2[j];
                            tablicaPermutacjiKlucza2[i, j] = K[i, pomocnicza - 1];
                        }
                    }



                    char[] blokR8 = Permutacja(blokR, E); //8.
                    //9. xor 
                    char[] tablicaK = new char[48];
                    for (int i = 0; i < 48; i++)
                    {
                        tablicaK[i] = tablicaPermutacjiKlucza2[0, i];
                    }
                    char[] tablicaXOR = Xorowanie(tablicaK, blokR);

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
                    //11.
                    string wiersz1 = S[0, 0] + "" + S[0, 5];
                    string kolumna1 = S[0, 1] + "" + S[0, 2] + "" + S[0, 3] + "" + S[0, 4];
                    string wiersz2 = S[1, 0] + "" + S[1, 5];
                    string kolumna2 = S[1, 1] + "" + S[1, 2] + "" + S[1, 3] + "" + S[1, 4];
                    string wiersz3 = S[2, 0] + "" + S[2, 5];
                    string kolumna3 = S[2, 1] + "" + S[2, 2] + "" + S[2, 3] + "" + S[2, 4];
                    string wiersz4 = S[3, 0] + "" + S[3, 5];
                    string kolumna4 = S[3, 1] + "" + S[3, 2] + "" + S[3, 3] + "" + S[3, 4];
                    string wiersz5 = S[4, 0] + "" + S[4, 5];
                    string kolumna5 = S[4, 1] + "" + S[4, 2] + "" + S[4, 3] + "" + S[4, 4];
                    string wiersz6 = S[5, 0] + "" + S[5, 5];
                    string kolumna6 = S[5, 1] + "" + S[5, 2] + "" + S[5, 3] + "" + S[5, 4];


                    int w1 = Convert.ToInt32(wiersz1, 2);
                    int k1 = Convert.ToInt32(kolumna1, 2);
                    int w2 = Convert.ToInt32(wiersz2, 2);
                    int k2 = Convert.ToInt32(kolumna2, 2);
                    int w3 = Convert.ToInt32(wiersz3, 2);
                    int k3 = Convert.ToInt32(kolumna3, 2);
                    int w4 = Convert.ToInt32(wiersz4, 2);
                    int k4 = Convert.ToInt32(kolumna4, 2);
                    int w5 = Convert.ToInt32(wiersz5, 2);
                    int k5 = Convert.ToInt32(kolumna5, 2);
                    int w6 = Convert.ToInt32(wiersz6, 2);
                    int k6 = Convert.ToInt32(kolumna6, 2);

                    //12.

                    char[,] S6bitowe = new char[8, 6];
                    string l1 = Convert.ToString(S1[w1, k1], 2);
                    string l2 = Convert.ToString(S1[w2, k2], 2);
                    string l3 = Convert.ToString(S1[w3, k3], 2);
                    string l4 = Convert.ToString(S1[w4, k4], 2);
                    string l5 = Convert.ToString(S1[w5, k5], 2);
                    string l6 = Convert.ToString(S1[w6, k6], 2);


                    for (int j = 0; j < 6; j++)
                    {
                        S6bitowe[0, j] = l1[j];
                        S6bitowe[1, j] = l2[j];
                        S6bitowe[2, j] = l3[j];
                        S6bitowe[3, j] = l4[j];
                        S6bitowe[4, j] = l5[j];
                        S6bitowe[5, j] = l6[j];

                    }

                    //13.
                    char[] ciagR = new char[32];


                    int r = 0;
                    foreach (char i in S6bitowe)
                    {
                        ciagR[r++] = i;
                    }

                    //14.
                    char[] RPermutacja = new char[48];
                    for (int i = 0; i < 48; i++)
                    {
                        pomocnicza = P[i];
                        RPermutacja[i] = ciagR[pomocnicza - 1];
                    }

                    //15.










                    break;
            }













        }
    }
}
