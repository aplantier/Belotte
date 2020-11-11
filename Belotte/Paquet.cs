using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Belotte
{
    public abstract class EnsembleDeCarte
    {
        private short m_nbrCartesMax;
        protected List<Carte> ensembleCarte;
        protected EnsembleDeCarte(short a_s_nbrMaxCarte = 0) { m_nbrCartesMax = a_s_nbrMaxCarte; this.ensembleCarte = new List<Carte>(); }
        public short NbrCartesMax { get { return m_nbrCartesMax; } }
        public short NbrCartesActuel { get { return (short)ensembleCarte.Count; } }

        public abstract void Affiche();
        public void Melange()
        {
            Random randGenerateur = new Random();
            int rand;
            Carte tmpCarte;

            for (int curseur = 0; curseur < this.NbrCartesActuel; curseur++)
            {
                rand = randGenerateur.Next(0, this.NbrCartesActuel - 1);
                tmpCarte = ensembleCarte[curseur];
                ensembleCarte[curseur] = ensembleCarte[rand];
                ensembleCarte[rand] = tmpCarte;

            }
        }
        public  bool GetAttribut() { return false; }

    }
    public abstract class Paquet : EnsembleDeCarte
    {
        public Paquet(short a_s_nbrMaxCarte = 0) : base(a_s_nbrMaxCarte) {
            
        }



        /**Retire et retourne la premiere carte du pauquet
         *  
         * */
        public Carte popCarte()
        {
            if (NbrCartesActuel - 1 < 0)
            {
                throw new Exception("Paquet vide ");
            }

            Carte head = new Carte(ensembleCarte[0]);
            ensembleCarte.RemoveAt(0);
            return head;
        }

        public override string ToString()
        {
            return "Paquet de" + this.NbrCartesMax + " Cartes" + " (" + this.NbrCartesActuel + '/' + this.NbrCartesMax + " )";
        }
        public override void Affiche()
        {
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+ Affichage de " + this.ToString());
            foreach (Carte cursorCarte in this.ensembleCarte)
            {
                Console.WriteLine("+  - " + cursorCarte.ToString() +" De type : " +cursorCarte.GetType() );
            }
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++\n\n");
        }
    }

    public class Paquet32 : Paquet
    {

        public Paquet32(bool vide = false) : base(32)
        {
          


        }
    }

    public class PaquetBelotte : Paquet32
    {
        private Col atout_;
        public event EventHandler event_AtoutChoisit;
        public void AtoutSet() =>
            event_AtoutChoisit?.Invoke(this, EventArgs.Empty);



        public PaquetBelotte(bool vide = false): base(true)
        {
            
            atout_ = 0;
            if (vide) return;
            foreach (Col couleur in (Col[])Enum.GetValues(typeof(Col)))
            {
                foreach (Val valeur in (Val[])Enum.GetValues(typeof(Val)))
                {
                    if (valeur < Val.Sept) continue;

                    Carte nouvelleCarte = new Carte(this, valeur, couleur);

                    this.event_AtoutChoisit += nouvelleCarte.HandleEvent;                   

                    this.ensembleCarte.Add(nouvelleCarte);
                }
            }

        }

        public Col Atout
        {
            get { return atout_; }
            set { atout_ =(Col) value; AtoutSet(); }
        }

    }


    public class MainDeJeuBelotte : EnsembleDeCarte
    {
        public MainDeJeuBelotte(short a_s_nbrMaxCarte = 7) : base(a_s_nbrMaxCarte)
        {

        }

        /**Methode d'affichage d'une main 
         * */
        public override void Affiche()// Affiche les cartes cotes a cotes
        {
            Console.WriteLine(this.ToString());
            for (int line = 0; line < 4; line++)// Affichage ligne par ligne 
            {
                Console.WriteLine();
                for (int col = 0; col < NbrCartesActuel; col++)// affichage en bloc  des cartes les uns après les autres 
                {
                    
                    switch (line)
                    {
                        case 0: Console.Write(",=============="); break;
                        case 1: Console.Write("|{0,-6} {1,-7}", ensembleCarte[col].Valeur, ensembleCarte[col].Couleur); break;
                        case 2: Console.Write("|       {0,-7}", ensembleCarte[col].Atout? "(Atout)":"") ; break;  
                        case 3: Console.Write("|({0,-1})           ", col + 1); break;
                    }


                }
                switch (line)
                {
                    case 0: Console.Write("====."); break;
                    case 1: Console.Write("    |"); break;
                    case 2: Console.Write("    |"); break;
                    case 3: Console.Write("    |"); break;
                }
            }

                Console.WriteLine("\n\n");
        }
        public override string ToString()
        {
            return "Main de " + this.NbrCartesActuel + " cartes ";
        }
        public void ajouterCarte(Carte a_cardToAdd)
        {
           
            this.ensembleCarte.Add(a_cardToAdd);
        }

        public Carte retirerCarte(int a_index)
        {
            Carte removedCard = this.ensembleCarte[a_index];
            this.ensembleCarte.RemoveAt(a_index);
            return removedCard;
        }

        internal Carte Menu()
        {
            String choix = "";
            int i_choix = 0;

            while (choix == "")
            {

                Console.WriteLine("===============================================");
                Console.WriteLine(" Trier le jeu : (C)ouleur, (V)aleur ou (M)elange Entrez le numerot de la carte aue vous souhaitez jouer ");
                Console.WriteLine("> ");
                try
                {

                    choix = Console.ReadLine();
                    if (choix == "C" || choix == "c")
                    {

                        this.ensembleCarte.Sort(Carte.CompareParCouleur);

                    }
                    else if (choix == "V" || choix == "v")
                    {
                        this.ensembleCarte.Sort(Carte.CompareParValeur);
                    }
                    else if (choix == "M" || choix == "m")
                    {
                        this.Melange();
                    }
                    else
                    {
                        i_choix = int.Parse(choix);
                        if (i_choix < 1 || i_choix > this.NbrCartesActuel)
                            throw new Exception(" erreur choix commande");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(" Erreur dans le choix du menu" + e.Message);
                    return null;
                }
            }
            return i_choix == 0 ? null : retirerCarte(i_choix - 1);

        }

        internal Carte Choix()
        {
            throw new NotImplementedException();
        }
    }

}

