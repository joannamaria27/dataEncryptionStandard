using System;
using System.Collections.Generic;

namespace BSK_DES
{
    class Program
    {
        //Initial Permutation Table  

        static void Main(string[] args)
        {
            int[] ip = new int[] { 58,50,42,34,26,18,10,2,
                                        60,52,44,36,28,20,12,4,
                                        62,54,46,38,30,22,14,6,
                                        64,56,48,40,32,24,16,8,
                                        57,49,41,33,25,17,9,1,
                                        59,51,43,35,27,19,11,3,
                                        61,53,45,37,29,21,13,5,
                                        63,55,47,39,31,23,15,7 };
            int[] pc = new int[] { 57, 49, 41, 33, 25, 17,  9,
                1, 58, 50, 42, 34, 26, 18,
                10,  2, 59, 51, 43, 35, 27,
                19, 11,  3, 60, 52, 44, 36,
                63, 55, 47, 39, 31, 23, 15,
                7, 62, 54, 46, 38, 30, 22,
                14,  6, 61, 53, 45, 37, 29,
                21, 13,  5, 28, 20,12,  4 };
            int[] przesuniecie6 = new int[] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1, };


            int[] pc2 = new int[] { 14, 17, 11, 24, 1, 5, 3,
                28, 15, 6, 21, 10, 23,
                19, 12, 4, 26, 8, 16,
                7, 27, 20, 13, 2, 41,
                52, 31, 37, 47, 55, 30,
                40, 51, 45, 33, 48, 44,
                49, 39, 56, 34, 53, 46,
                42, 50, 36, 29, 32 };

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
                    ////////////////////////permutacja wstepna
                    char[] tablicaPoczatkowa = new char[64];
                    char[] tablicaPoczatkowaKlucz = new char[64];
                    char[] tablicaPermutacji = new char[64];
                    char[] tablicaPermutacjiKlucza = new char[64];
                    int m = 0;
                    for (int i = 0; i < 64; i++)
                    {

                        tablicaPoczatkowa[i] = tekstJawny[m];
                        tablicaPoczatkowaKlucz[i] = tekstJawny[m];
                        m++;

                    }
                    /////////////////////////tablica permutacji - działa
                    int pomocnicza;


                    for (int i = 0; i < 64; i++)
                    {
                        pomocnicza = ip[i];
                        tablicaPermutacji[i] = tablicaPoczatkowa[pomocnicza - 1];
                    }

                    ///////////////////////////////dzielenie na dwa 32 bloki
                    char[] blokR = new char[32];
                    char[] blokL = new char[32];
                    for (int i = 0; i < 32; i++)
                    {
                        blokL[i] = tablicaPermutacji[i];
                    }
                    int l = 32;
                    for (int i = 0; i < 32; i++)
                    {
                        blokR[i] = tablicaPermutacji[l]; l++;
                    }
                    /////////////////////////klucz - permutacja wstępna

                    for (int i = 0; i < 56; i++)
                    {
                        pomocnicza = pc[i];
                        tablicaPermutacjiKlucza[i] = tablicaPoczatkowaKlucz[pomocnicza - 1];
                    }

                    ////////////dzielenie na dwie czesci
                    char[] kluczC = new char[28];
                    char[] kluczD = new char[28];
                    for (int i = 0; i < 28; i++)
                    {
                        kluczC[i] = tablicaPermutacjiKlucza[i];
                    }
                    l = 28;
                    for (int i = 0; i < 28; i++)
                    {
                        kluczD[i] = tablicaPermutacjiKlucza[l]; l++;
                    }
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
                    // List<char> ListaK = new List<char>(K);
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
                    char[,] tablicaPermutacjiKlucza2 = new char[16,56];
                    for (int i = 0; i < 16; i++)
                    {
                        for (int j = 0; j < 56; j++)
                        {
                            pomocnicza = pc2[j];
                            tablicaPermutacjiKlucza2[i,j] = K[i, pomocnicza - 1];
                        }
                    }

















                    break;
            }













        }
    }
}
