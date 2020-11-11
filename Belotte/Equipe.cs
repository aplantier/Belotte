using System;
using System.Collections.Generic;
using System.Text;

namespace Belotte
{
    

    /** Classe representant une Equipe de 2 joueurs 
     * 
     */
    public class EquipeBelotte
    {
        private int m_i_score;
        private string m_nom_;
        private JoueurBelotte joueur1;
        private JoueurBelotte joueur2;

        public EquipeBelotte(string nomEquipe, Joueur joueur1, Joueur joueur2)
        {
            this.m_nom_ = nomEquipe;
            this.joueur1 = new JoueurBelotte( joueur1,this);
            this.joueur2 = new JoueurBelotte( joueur2,this); 
        }

        public int Score { get { return m_i_score; } set { m_i_score += value; } }

        public JoueurBelotte this [int i]
        {
            get { if (i == 0) return joueur1; if (i == 1) return joueur2; else throw new Exception(" Erreur aces indexeur Joueur"); }
        }

        public String Nom
        {
            get { return m_nom_; }
        }

        public override string ToString()
        {
            return  " Equipe : "+ m_nom_ + 
                   " { "+ joueur1.ToString() + 
                   " , "+ joueur2.ToString()+
                   "}";
        }
    }
}
