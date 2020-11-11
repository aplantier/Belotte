using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Belotte
{
    // Enum decrivant les valeurs des cartes

    public enum Val { Deux, Trois, Quatre, Cinq, Six, Sept, Huit, Neuf, Dix, Valet, Reine, Roi, AS };
    public enum Col { Coeur = 1, Pique = 2, Carreau = 3, Trefle = 4 };

    /**
     * Clqsse Carte
     * Une carte est représentée par sa couleur et sa valeur 
     * */
    public class Carte
    {

        public static int[] point = { 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 4, 10, 11 };
        public static int[] pointAtout = { 0, 0, 0, 0, 0, 7, 8, 14, 10, 20, 3, 4, 11 };

        private Col m_couleur;
        private Val m_valeur;
        private bool atout_;
        private PaquetBelotte paquet_;
        public Col Couleur
        {
            get { return m_couleur; }
        }
        public Val Valeur
        {
            get { return m_valeur; }
        }
        public int Point
        {
            get
            {
                if (atout_) return pointAtout[(int)m_valeur];
                return point[(int)m_valeur];
            }
        }
        /** CONSTRUCTEUR 
         * */
        public Carte(PaquetBelotte a_paquet, Val a_Valeur, Col a_Couleur)
        {
            this.m_valeur = a_Valeur;
            this.m_couleur = a_Couleur;
            this.atout_ = false;
            this.paquet_ = a_paquet;
            this.paquet_.event_AtoutChoisit += this.HandleEvent;
        }
        /** CONSTRUCTEUR DE RECOPIE 
         */
        public Carte(Carte a_carte)
        {
            this.m_valeur = a_carte.Valeur;
            this.m_couleur = a_carte.Couleur;
            this.atout_ = a_carte.Atout;
            a_carte.paquet_.event_AtoutChoisit += this.HandleEvent;

        }

        public override string ToString()
        {
            return this.Valeur + " de " + this.Couleur;
        }

        public static int CompareParCouleur(Carte a_crt1, Carte a_crt2)
        {
            return a_crt1.Couleur.CompareTo(a_crt2.Couleur);
        }
        public static int CompareParValeur(Carte a_crt1, Carte a_crt2)
        {
            return a_crt1.Valeur.CompareTo(a_crt2.Valeur);
        }
        public static int Compare(Carte a_crt1, Carte a_crt2)
        {
            if (!(a_crt1.Atout && a_crt2.Atout)) // si aucune des cartes n'est un atout, l' ordre naturel prime
                return a_crt1.Valeur.CompareTo(a_crt2.Valeur);
            if (a_crt1.Atout ^ a_crt2.Atout)// si une des deux valeurs est de l'atout, c'est celle ci la plus grande
                return a_crt1.Atout ? 1 : -1;
            if (a_crt1.Valeur == (Val)6)
                return -1;
            if (a_crt2.Valeur == (Val)6)
                return 1;

            return Carte.pointAtout[(int)a_crt1.Valeur] < Carte.pointAtout[(int)a_crt2.Valeur] ? -1 : 1;



        }
        public void HandleEvent(object paquet, EventArgs evArgs)
        {
            atout_ = ((PaquetBelotte)paquet).Atout == this.Couleur;
        }
        public bool Atout
        {
            get { return atout_; }
        }

    }

}
