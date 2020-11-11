using System;

namespace Belotte
{
    public interface Strategie
    {
        public Carte Jouer(PaquetBelotte a_paquet, MainDeJeuBelotte a_tapis, MainDeJeuBelotte a_main);
    }


    public class Maitre : Strategie
    {
        public Carte Jouer(PaquetBelotte a_paquet, MainDeJeuBelotte a_tapis, MainDeJeuBelotte a_main)
        {
            a_main.Affiche();
            a_main.Menu();
            return null;

        }


    }
    public class NonMaitre : Strategie
    {
        public Carte Jouer(PaquetBelotte a_paquet, MainDeJeuBelotte a_tapis, MainDeJeuBelotte a_main)
        {
            return null;
        }


    }
    public class PrendreAtout : Strategie
    {
        public Carte Jouer(PaquetBelotte a_paquet, MainDeJeuBelotte a_tapis, MainDeJeuBelotte a_main)
        {
            Console.Clear();
            System.Console.WriteLine("Atout propose : ");
            a_tapis.Affiche();
            System.Console.WriteLine("===============================================");
            a_main.Affiche();
            if (this.Menu())
            {
                Console.WriteLine("Avant Bug ");
                Carte atout = a_tapis.retirerCarte(a_tapis.NbrCartesActuel - 1);
                a_paquet.Atout = atout.Couleur;
                a_main.ajouterCarte(atout);
                Console.Clear();

                return atout;
            }
            Console.Clear();
            return null;
        }

        private bool Menu()
        {
            string choix = "";
            while (choix == "")
            {

                System.Console.WriteLine("===============================================");
                System.Console.WriteLine("Prendre ? (O)ui, (N)on \n >");
                try
                {
                    choix = Console.ReadLine();
                    if (choix == "o" || choix == "O")
                    {

                        return true;
                    }
                    if (choix == "n" || choix == "N")
                    {
                        return false;
                    }
                    else throw new Exception(" Il faut faire un choix 1");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur dans le menu " + e.Message);
                    choix = "";
                }
            }
            return false;


        }

    }



    public class ChoisirAtout : Strategie
    {
        public Carte Jouer(PaquetBelotte a_paquet, MainDeJeuBelotte a_tapis, MainDeJeuBelotte a_main)
        {

            string choix = "";
            Console.Clear();
            a_tapis.Affiche();
            System.Console.WriteLine("===============================================");
            a_main.Affiche();
            while (choix == "")
            {

                System.Console.WriteLine("===============================================");
                System.Console.WriteLine("Personne n'a pris, \nChoisir de prendre a l'atout  ? (T)refle, (C)oeur, c(A)rreau, (P)ique, (L)aisser \n >");
                try
                {
                    choix = Console.ReadLine();
                    if (choix == "T" || choix == "t")
                    {
                        a_paquet.Atout = (Col)4;

                    }
                    if (choix == "C" || choix == "c")
                    {
                        a_paquet.Atout = (Col)1;

                    }
                    if (choix == "A" || choix == "a")
                    {
                        a_paquet.Atout = (Col)3;

                    }
                    if (choix == "P" || choix == "p")
                    {
                        a_paquet.Atout = (Col)2;

                    }
                    if (choix == "l" || choix == "L")
                    {
                        Console.Clear();
                        return null;
                    }
                    else throw new Exception(" Il faut faire un choix 1");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur dans le menu " + e.Message);
                    choix = "";
                }

            }
            Carte atout = a_tapis.retirerCarte(a_paquet.NbrCartesActuel - 1);
            a_main.ajouterCarte(atout);
            Console.Clear();

            return atout;


        }

    }
}