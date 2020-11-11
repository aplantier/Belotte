using System;

namespace Belotte
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World! ");
            Croupier CroupierBelotte = new Croupier();
     


            Joueur Joueur1, Joueur2, Joueur3, Joueur4;
            Joueur1 = new Joueur("Antoine");
            Joueur2 = new Joueur("Thomas");
            Joueur3 = new Joueur("Etienne");
            Joueur4 = new Joueur("Karim");


           PartieBelotte Partie =  CroupierBelotte.NouvellePartieBelotte(new EquipeBelotte("Griffu1", Joueur1, Joueur2),
                                                    new EquipeBelotte("Griffu2", Joueur3, Joueur4));
             Partie.Jouer();


            



            /*  Carte maCarte = new Carte((Val) 1 , (Col)3);
              Console.WriteLine(maCarte.ToString());
              Carte carteSVG;

              Paquet monpaquet = new Paquet32();
              Console.WriteLine("Affichage d'un paquet en To STring :\n" + monpaquet.ToString());
              Console.WriteLine("Appel de Affiche d'un paquet\n");
              monpaquet.Affiche();
              Console.WriteLine("Melange d' un paquet\n");
              monpaquet.Melange();
              monpaquet.Melange();
              monpaquet.Melange();
              monpaquet.Melange();
              Console.WriteLine("Appel de Affiche d'un paquet\n");
              monpaquet.Affiche();
  */


            /*            MainDeJeu Main1 = new MainDeJeu();
                        Console.WriteLine("Affichage d'une Main en To STring :\n" + Main1.ToString());
                        Console.WriteLine("Appel de Affiche d'une Main\n");
                        Main1.Affiche();
            */

            /*            carteSVG = monpaquet.popCarte();
                        Console.WriteLine("Pop d,une carte : " + carteSVG.ToString());
            */
            /*            Console.WriteLine("Ajout d'une carte");
                        Main1.ajouterCarte(carteSVG);
                        Console.WriteLine("Appel de Affiche d'une Main\n");
                        Main1.Affiche();

                        Console.WriteLine("Appel de Affiche d'un paquet\n");
                        monpaquet.Affiche();

            */
        }
    }
}
