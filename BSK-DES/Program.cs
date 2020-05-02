using System;
using System.Collections.Generic;
using System.IO;

namespace BSK_DES
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("halo!");
            Console.WriteLine("Menu\n 1-KODOWANIE\n 2-ODKODOWANIE");

            int wybor = int.Parse(Console.ReadLine());
            switch (wybor)
            {
                case 1:
                    {
                        Console.WriteLine("KODOWANIE");

                        Console.WriteLine("Podaj nazwę z rozszezenie do odczytu: ");
                        string plikb = Console.ReadLine();
                        string tekstJawny = FileHandler.ReadFromTextFile(plikb); //zm

                        Console.WriteLine("Podaj plik txt do odczytu klucza: ");
                        string plikk = Console.ReadLine();
                        string klucz = FileHandler.ReadFromTextFile(plikk);




                        while (tekstJawny.Length < 64) //zam
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
                          
                            Console.Write("KLUCZ (BIN): ");
                            foreach (char element in tablicaPoczatkowaKlucz)
                            {
                                Console.Write(element);
                            }
                            Console.Write("\n");
                            Console.Write("KLUCZ C: ");
                            foreach (char element in kluczC)
                            {
                                Console.Write(element);
                            }
                            Console.Write("\n");

                            Console.Write("KLUCZ D: ");
                            foreach (char element in kluczD)
                            {
                                Console.Write(element);
                            }
                            Console.Write("\n");
                            Console.WriteLine("-----------------------------------------------");
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
                            for (int u = 0; u < 16; u++)
                            {
                                Console.Write("KLUCZ C {0}: ", u + 1);
                                for (int i = 0; i < 28; i++)
                                {
                                    Console.Write(kluczeCpo6[u, i]);

                                }
                                Console.Write("KLUCZ D {0}: ", u + 1);
                                for (int i = 0; i < 28; i++)
                                {

                                    Console.Write(kluczeDpo6[u, i]);
                                }
                                Console.Write("\n");
                            }
                            Console.WriteLine("-----------------------------------------------");
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
                            for (int u = 0; u < 16; u++)
                            {
                                Console.Write("KLUCZ WYJSCIOWE " + "{0}: ", u + 1);
                                for (int i = 0; i < 48; i++)
                                {
                                    Console.Write(tablicaPermutacjiKlucza2[u, i]);
                                }
                                Console.Write("\n");
                            }
                            Console.WriteLine("-----------------------------------------------");
                            Console.WriteLine("TEKST POCZATKOWY: " + tablicaPoczatkowa);
                            /////////////////////////////////////////
                            /// 8-16. 
                            char[,] ril0 = Des.RiL(blokP, blokL, tablicaPermutacjiKlucza2, 0);
                            char[,] ril1 = Des.Laczenie(ril0, tablicaPermutacjiKlucza2, 1);
                            char[,] ril2 = Des.Laczenie(ril1, tablicaPermutacjiKlucza2, 2);
                            char[,] ril3 = Des.Laczenie(ril2, tablicaPermutacjiKlucza2, 3);
                            char[,] ril4 = Des.Laczenie(ril3, tablicaPermutacjiKlucza2, 4);
                            char[,] ril5 = Des.Laczenie(ril4, tablicaPermutacjiKlucza2, 5);
                            char[,] ril6 = Des.Laczenie(ril5, tablicaPermutacjiKlucza2, 6);
                            char[,] ril7 = Des.Laczenie(ril6, tablicaPermutacjiKlucza2, 7);
                            char[,] ril8 = Des.Laczenie(ril7, tablicaPermutacjiKlucza2, 8);
                            char[,] ril9 = Des.Laczenie(ril8, tablicaPermutacjiKlucza2, 9);
                            char[,] ril10 = Des.Laczenie(ril9, tablicaPermutacjiKlucza2, 10);
                            char[,] ril11 = Des.Laczenie(ril10, tablicaPermutacjiKlucza2, 11);
                            char[,] ril12 = Des.Laczenie(ril11, tablicaPermutacjiKlucza2, 12);
                            char[,] ril13 = Des.Laczenie(ril12, tablicaPermutacjiKlucza2, 13);
                            char[,] ril14 = Des.Laczenie(ril13, tablicaPermutacjiKlucza2, 14);
                            char[,] ril15 = Des.Laczenie(ril14, tablicaPermutacjiKlucza2, 15);


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
                            string str = new string(koniecPer);
                            Console.Write("TEKST WYJSCIOWY (BIN): " + str);

                            string wyjscie = FileHandler.BinaryStringToHexString(str);
                            Console.WriteLine("\nTEKST WYJSCIOWY: "+ wyjscie);
                            File.WriteAllText(@"zakodowane.txt", wyjscie);

                        }
                        break;
                    }
                case 2:
                    {

                        string tekstZakodowany = "";
                        Console.WriteLine("KODOWANIE");

                        Console.WriteLine("Podaj nazwę z rozszezenie do odczytu: ");
                        string plikb = Console.ReadLine();
                        tekstZakodowany = FileHandler.ReadFromTextFile(plikb); //zm

                        Console.WriteLine("Podaj plik txt do odczytu klucza: ");
                        string plikk = Console.ReadLine();
                        string klucz = FileHandler.ReadFromTextFile(plikk);
                        while (tekstZakodowany.Length < 64) //zm
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
                            Console.WriteLine("-----------------------------------------------");
                            Console.Write("KLUCZ (BIN): ");
                            foreach (char element in tablicaPoczatkowaKlucz)
                            {
                                Console.Write(element);
                            }
                            Console.Write("\n");
                            Console.Write("KLUCZ C: ");
                            foreach (char element in kluczC)
                            {
                                Console.Write(element);
                            }
                            Console.Write("\n");
                            Console.Write("KLUCZ D: ");
                            foreach (char element in kluczD)
                            {
                                Console.Write(element);
                            }
                            Console.Write("\n");
                            Console.WriteLine("-----------------------------------------------");
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
                            for (int u = 0; u < 16; u++)
                            {
                                Console.Write("KLUCZ C {0}: ", u + 1);
                                for (int i = 0; i < 28; i++)
                                {
                                    Console.Write(kluczeCpo6[u, i]);

                                }
                                Console.Write("KLUCZ D {0}: ", u + 1);
                                for (int i = 0; i < 28; i++)
                                {

                                    Console.Write(kluczeDpo6[u, i]);
                                }
                                Console.Write("\n");
                            }
                            Console.WriteLine("-----------------------------------------------");
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
                            for (int u = 0; u < 16; u++)
                            {
                                Console.Write("KLUCZ WYJSCIOWE " + "{0}: ", u + 1);
                                for (int i = 0; i < 48; i++)
                                {
                                    Console.Write(tablicaPermutacjiKlucza2[u, i]);
                                }
                                Console.Write("\n");
                            }
                            Console.WriteLine("-----------------------------------------------");
                            Console.WriteLine("TEKST POCZATKOWY: " + tablicaPoczatkowa);
                            /////////////////////////////////////////
                            /// 8-16. 
                            char[,] ril0 = Des.RiL(blokP, blokL, tablicaPermutacjiKlucza2, 15);
                            char[,] ril1 = Des.Laczenie(ril0, tablicaPermutacjiKlucza2, 14);
                            char[,] ril2 = Des.Laczenie(ril1, tablicaPermutacjiKlucza2, 13);
                            char[,] ril3 = Des.Laczenie(ril2, tablicaPermutacjiKlucza2, 12);
                            char[,] ril4 = Des.Laczenie(ril3, tablicaPermutacjiKlucza2, 11);
                            char[,] ril5 = Des.Laczenie(ril4, tablicaPermutacjiKlucza2, 10);
                            char[,] ril6 = Des.Laczenie(ril5, tablicaPermutacjiKlucza2, 9);
                            char[,] ril7 = Des.Laczenie(ril6, tablicaPermutacjiKlucza2, 8);
                            char[,] ril8 = Des.Laczenie(ril7, tablicaPermutacjiKlucza2, 7);
                            char[,] ril9 = Des.Laczenie(ril8, tablicaPermutacjiKlucza2, 6);
                            char[,] ril10 = Des.Laczenie(ril9, tablicaPermutacjiKlucza2, 5);
                            char[,] ril11 = Des.Laczenie(ril10, tablicaPermutacjiKlucza2, 4);
                            char[,] ril12 = Des.Laczenie(ril11, tablicaPermutacjiKlucza2, 3);
                            char[,] ril13 = Des.Laczenie(ril12, tablicaPermutacjiKlucza2, 2);
                            char[,] ril14 = Des.Laczenie(ril13, tablicaPermutacjiKlucza2, 1);
                            char[,] ril15 = Des.Laczenie(ril14, tablicaPermutacjiKlucza2, 0);


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

                            string str = new string(koniecPer);
                            Console.Write("TEKST WYJSCIOWY (BIN): "+ str);
                         
                            string wyjscie = FileHandler.BinaryStringToHexString(str);
                            Console.WriteLine("\nTEKST WYJSCIOWY: " + wyjscie);
                            File.WriteAllText(@"odkodowane.txt", wyjscie);

                        }

                        break;
                    }
            }
        }
    }
}
