using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraMonolitycznie
{
    class ExitAppException : Exception
    {

    }

    class Program
    {
        /// <summary>
        /// Losuje liczbe z podanego zakresu włącznie
        /// </summary>
        /// <remarks>Nie zwraca wyjątku</remarks>
        /// <param name="min">Dowolna liczba całkowita</param>
        /// <param name="max">Dowolna liczba całkowita</param>
        /// <returns>Zwraca wylosowaną liczbe calkowitą</returns>
        static int Losuj(int min = 1, int max = 100)
        {
            if (min > max)
            {
                int temp = max;
                max = min;
                min = temp;
            }

            Random generator = new Random();
            return generator.Next(min, max + 1);

        }

        static int wylosowana;

        static int WczytajDane(string prompt = "Podaj liczbę (X gdy koniec) : ")
        {
            int liczba = 0;
            while (true)
            {
                Console.Write(prompt);
                string tekst = Console.ReadLine();
                if (tekst.ToLower() == "x")
                    throw new ExitAppException();


                try
                {
                    liczba = Convert.ToInt32(tekst);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Nie podano liczby! Spróbuj ponownie");
                    continue;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Liczba nie mieści się w rejestrze! Spróbuj ponownie");
                    continue;
                }
            }
            return liczba;
        }

        static string Ocena(int propozycja)
        {
            if (propozycja < wylosowana)
                return ("za mało");
            else if (propozycja > wylosowana)
                return ("za dużo");
            else
                return ("trafiono");
        }

        static void Main(string[] args)
        {
            int min = WczytajDane("Podaj wartość minimalną do wylosowania : ");
            int max = WczytajDane("Podaj wartośc maksymalną do wylosowania : ");

            wylosowana = Losuj(min, max);

            Console.WriteLine($"Wylosowałem liczbę od {min} do {max}. \n Odgadnij ją");

#if (DEBUG)
            Console.WriteLine(wylosowana);
#endif

            //wykonuj
            do
            {
                int propozycja = 0;
                try
                {
                    propozycja = WczytajDane("Podaj swoją propozycję lub X jeśli się poddajesz : ");
                }
                catch (ExitAppException)
                {
                    Console.WriteLine("Szkoda, że już koniec... Do widzenia !");
                    break;
                }

                Console.WriteLine($"Przyjąłem wartość {propozycja}");

                #region Krok 3. Komputer ocenia propozycję
                string wynik = Ocena(propozycja);
                Console.WriteLine(wynik);
                if (wynik == "trafiono")
                    break;
                #endregion
            }
            while (true);
            //do momentu trafienia

            Console.WriteLine("Koniec gry");
        }
    }
}