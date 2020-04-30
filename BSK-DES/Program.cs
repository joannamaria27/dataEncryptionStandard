using System;

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

            Console.WriteLine("DES!");


            Console.WriteLine("Menu\n 1-KODOWANIE");
            int wybor = int.Parse(Console.ReadLine());
            switch (wybor)
            {
                case 1:

                    ////////////////////////wprowadzenie liczb - narazie ręcznie 64 zera i jednynki
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
                        blokL[i] = tablicaPermutacji[i];
                    }
                    l = 32;
                    for (int i = 0; i < 32; i++)
                    {
                        blokR[i] = tablicaPermutacji[i]; l++;
                    }
                
                    break;
            }













        }
    }
}
