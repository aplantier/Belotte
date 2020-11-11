using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Belotte
{
    /** Classe Croupier
    * Cette Classe assure le bon deroulement d'une partie de belotte. Plusieurs stades dans une partie 
    * 
    * I. Initialisation 
    *   Creation des cartes et du paquet de carte (cf classe Paquet et classe carte )
    *   Creation des joueurs et des equipes 
    * II. Debut de partie 
    *   Distribution du premier volet de cartes (2+3 cartes) a tous les joueurs
    *   Affichage de la carte d' appel a l'atout 
    *   Premier tour de choix de l'atout 
    *   (Second tour de choix de l'atout )
    *   Distribution du reste des cartes 
    * III. Deroulement de la partie 
    *
    * 
    * */


    public class JoueurTuple
    {
        public JoueurTuple(Joueur a_Jou, bool a_etat = true)
        {
            Item1 = a_Jou;
            Item2 = a_etat;
        }
        public Joueur Item1 { get; set; }
        public bool Item2 { get; set; }
    }
    public class Croupier
    {
        private List<JoueurTuple> listeJoueurs = new List<JoueurTuple>();
        PartieBelotte Partie;

        public Croupier()
        {

        }

        public void SalledeJeu() { 

            bool exit = false;
            while (!exit)
            {

                Console.WriteLine(" Bienvenue dans la salle de Belotte ");
                switch (Menu())
                {
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("\nUne nouvelle partie va être Lancee");
                            Partie = CreationPartieBelotte();
                            Partie.Jouer();

                        }
                        break;
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("\nAffichage de la liste des joueurs ");
                            AfficherJoueurs();
                        }
                        break;
                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("\nCreation d'un nouveau joueur ");
                            listeJoueurs.Add(new JoueurTuple(CreationNouveauJoueur(), true));


                        }
                        break;
                    case 4:
                        {
                            Console.Clear();
                            Console.WriteLine("\nAu revoir ! ");
                            exit = true;
                        }
                        break;
                }

            }

            // Creation des joueurs 
            // Preparation a la partie 
            //   - Melange
            //   - Choix des partenaires 
            //   - Distribution des jeux 
            //Debut de lq pqrtie 
            //      - Affichage de l'atout 
            //      - choix des joeurs de prendre ou non q l'qtout 
            //     -  choix des joeurs de prendre a autre choses 

        }

        private PartieBelotte CreationPartieBelotte()
        {
            System.Console.Clear();
            Console.WriteLine("La Belotte se joue a 2 equipes de 2 joueurs\n > Creation des equipes \n");
            EquipeBelotte Equipe1 = CreationEquipeBelotte();
            EquipeBelotte Equipe2 = CreationEquipeBelotte();
            Console.WriteLine(Equipe1.ToString());
            Console.WriteLine(Equipe2.ToString());
            return new PartieBelotte(Equipe1, Equipe2);
        }

        private EquipeBelotte CreationEquipeBelotte()
        {
            String s_nomEquipe = "";
            Joueur Joueur1 = null;
            Joueur Joueur2 = null;


            while (s_nomEquipe == "")
            {
                Console.Write("Entrez le nom de votre Equipe validez par [enter]: ");
                try
                {
                    s_nomEquipe = Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(" Nom equipe non valide " + e.Message);
                    s_nomEquipe = "";
                }
            }

            Joueur1 = SelectionJoueur(s_nomEquipe, 1);

            Joueur2 = SelectionJoueur(s_nomEquipe, 2);

            return new EquipeBelotte(s_nomEquipe, Joueur1, Joueur2);




        }

        private Joueur SelectionJoueur(String a_NomEquipe, int a_numJoueur)
        {
            int choixJoueur = 0;
            Joueur JoueurTmp = null;
            while (choixJoueur == 0)
            {
                Console.WriteLine("\n\tChoix Joueur :" + a_numJoueur + " Equipe :" + a_NomEquipe + "\n " +
                    "\t1/ Selection parmis la liste des joueurs \n" +
                    "\t2/ Selection par nom\n" +
                    "\t3/ creation d'un nouveau joueur\n");

                try
                {
                    choixJoueur = int.Parse(Console.ReadLine());
                    if (choixJoueur < 1 || choixJoueur > 3) throw new Exception("Choix selection joueur non valide");
                }
                catch (Exception e)
                {
                    Console.WriteLine("** Err selection joueur: " + e.Message);
                    choixJoueur = 0;
                }

                switch (choixJoueur)
                {
                    case 1:
                        JoueurTmp = ChoixJoueurParListe();
                        break;
                    case 2:
                        JoueurTmp = ChoixParNom();
                        break;
                    case 3:
                        {
                            Joueur newJoueur = CreationNouveauJoueur();
                            listeJoueurs.Add(new JoueurTuple(newJoueur, false));
                            JoueurTmp = newJoueur;
                        }
                        break;
                    default: return null;

                }
                if (JoueurTmp == null)
                    choixJoueur = 0;


            }
            return JoueurTmp;
        }

        private Joueur ChoixParNom()
        {

            String s_NomJoueur = "";

            Console.WriteLine("Entrez le nom du joueur a selectionner et validez par [enter]");
            try
            {
                s_NomJoueur = Console.ReadLine();
                foreach (JoueurTuple cursor in listeJoueurs)
                {
                    if (cursor.Item1.Nom == s_NomJoueur)
                    {
                        if (cursor.Item2 == false)
                            throw new Exception("Joueur deja selectionné par quelqu'un");
                        cursor.Item2 = false;
                        return cursor.Item1;
                    }
                }
                throw new Exception("Le joueur " + s_NomJoueur + " n'existe pas ");

            }
            catch (Exception e)
            {
                Console.WriteLine("** Erreur Selection Joueur par Nom : " + e.Message);
                return null;
            }


        }

        private Joueur ChoixJoueurParListe()
        {
            if (listeJoueurs.Count == 0)
            {
                Console.WriteLine(" Aucun Joueur disponible ");
                return null;

            }
            int choixJoueur = 0;
            while (choixJoueur == 0)
            {
                this.AfficherJoueurs();
                Console.WriteLine(" Entrez le numerot du joueur que vous souhaitez selectionner");
                try
                {
                    choixJoueur = int.Parse(Console.ReadLine());
                    if (choixJoueur < 1 || choixJoueur > listeJoueurs.Count + 1)
                    {
                        throw new Exception("Vous devez choisir un personnage parmis la liste");
                    }
                    if (listeJoueurs[choixJoueur - 1].Item2 == false )
                    {
                        throw new Exception("Joueur deja selectionné par quelqu'un");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("**Err Selection joueur: " + e.Message);
                    return null;
                }

            }
            listeJoueurs[choixJoueur - 1].Item2 = false;
            return listeJoueurs[choixJoueur - 1].Item1;


        }

        public void AfficherJoueurs()
        {
            System.Console.WriteLine("\nListe des joueurs : \n --------------------");
            if (listeJoueurs.Count == 0)
            {
                System.Console.WriteLine(" Aucuns joueurs enregistrés\n--------------------\n");
                return;
            }
            int lineCount = 1;
            foreach (JoueurTuple cursor in listeJoueurs)
            {
                System.Console.WriteLine(lineCount++ + "- " + cursor.Item1.ToString() + (cursor.Item2 ? "" : "(non Disponible)"));
            }
            System.Console.WriteLine(" --------------------\n");
        }

        public Joueur CreationNouveauJoueur()
        {
            String s_NomNouveauJoueur = "";

            while (s_NomNouveauJoueur == "")
            {
                Console.WriteLine("\nEntrez Nom nouveau Joueur puis validez par [enter]: ");
                try
                {
                    s_NomNouveauJoueur = Console.ReadLine();
                    foreach (JoueurTuple cursor in listeJoueurs)
                    {
                        if (cursor.Item1.Nom == s_NomNouveauJoueur)
                        {
                            throw new Exception(" Le joueur : " + s_NomNouveauJoueur + " Existe déja");
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("** Erreur creation Joueur : " + e.Message);
                    s_NomNouveauJoueur = "";
                }

            }
            Console.WriteLine("Creation du joueur " + s_NomNouveauJoueur + "\n--------------------");
            return new Joueur(s_NomNouveauJoueur);

        }

        private int Menu()
        {
            int i_choix = 0;
            while (i_choix == 0)
            {

                System.Console.WriteLine("Que voulez vous faire ?\n" +
                    "1/ Nouvelle Partie\n" +
                    "2/ Lister Le joueurs existants\n" +
                    "3/ Creer un nouveau joueur\n" +
                    "4/ Quitter\n" +
                        "choix ? (entrez le nombre correspondant et validez par[enter}>");
                try
                {
                    i_choix = int.Parse(System.Console.ReadLine());
                    if (i_choix <= 0 || i_choix > 4) throw new Exception(" Le choix doit etre compris entre 1 et 4");
                }
                catch (Exception e)
                {
                    Console.WriteLine("** erreur dans le choix du menu : " + e.Message);
                    i_choix = 0;
                }
            }
            return i_choix;


        }

        /**Initialisation du jeu avant que la partie ne commence 
            * Melange du paquet de carte 
            * Distribution des cartes TODO : reflechir a une distribution "offcielle "
            * 
            */


        public void NouveauJoueur(String New_Joueur)
        {
            listeJoueurs.Add(new JoueurTuple(new Joueur(New_Joueur), true));
        }

        public EquipeBelotte NouvelleEquipeBelotte( String Nom_Equipe, JoueurBelotte joueur1, JoueurBelotte joueur2)
        {

            return new EquipeBelotte(Nom_Equipe, joueur1, joueur2);
        }

        public PartieBelotte NouvellePartieBelotte(EquipeBelotte equipe1,EquipeBelotte equipe2)
        {
            return new PartieBelotte(equipe1, equipe2);
        }

           



    }
}
