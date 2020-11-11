using System;
using System.Collections.Generic;
using System.Text;

namespace Belotte
{
    public class Joueur
    {
        private String m_s_Nom_;
        public Joueur(String a_nomJoueur)
        {
            this.m_s_Nom_ = a_nomJoueur;
        }
        public Joueur(Joueur joueur_copie)
        {
            this.m_s_Nom_ = joueur_copie.Nom;
        }
        public String Nom { get { return m_s_Nom_; } }

        public override string ToString()
        {
            return m_s_Nom_;
        }

        internal void Affiche()
        {
            Console.WriteLine(m_s_Nom_);
        }
    }


    public class JoueurBelotte : Joueur
    {
        MainDeJeuBelotte m_main_;
        EquipeBelotte m_equipe_;
        Strategie strategie_;

        public MainDeJeuBelotte Main
        {
            get { return m_main_; }
        }
        public EquipeBelotte Equipe
        {
            get { return m_equipe_; }
        }
        public JoueurBelotte(Joueur joueur_copie, EquipeBelotte equipe) : base(joueur_copie)
        {
            m_main_ = new MainDeJeuBelotte();
            m_equipe_ = equipe;

        }
        public void AfficheMain()
        {
            this.Main.Affiche();
            Console.WriteLine("Main de " + this.Nom + " Equipe " + this.Equipe.Nom);
        }

        public Carte Jouer(PaquetBelotte a_paquet, MainDeJeuBelotte a_tapis)
        {
            Carte carteSelec = null;
            
                Console.WriteLine(" C'est le tour de : "+ this.Nom + " , Equipe :" + this.Equipe.Nom+" \n" +
                                     " tappez une touche quand vous etes pret");
                Console.ReadKey( true);
                carteSelec =  this.strategie_.Jouer(a_paquet, a_tapis, Main);
            return carteSelec;
        }

        public void SetStrategie( Strategie a_strat)
        {
            this.strategie_ = a_strat;
        }
    }

}
