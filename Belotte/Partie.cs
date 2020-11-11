using System;
using System.Collections.Generic;
using System.Text;

namespace Belotte
{
    public class PartieBelotte
    {
        private EquipeBelotte[] equipes_;


        private PaquetBelotte jeu_;
        private PaquetBelotte defausse_;
        private MainDeJeuBelotte tapis_;

        private int selectJoueur = 4;

        private void DebutPartie()
        {
            jeu_.Melange();
            selectJoueur = 0;
            for (int i = 0; i < 4; i++)// premier tour
            {

                this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Main.ajouterCarte(jeu_.popCarte());
                this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Main.ajouterCarte(jeu_.popCarte());
                selectJoueur = (selectJoueur + 1) % 4;
            }

            selectJoueur = 0;
            for (int i = 0; i < 4; i++)// deuxieme tour
            {
                this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Main.ajouterCarte(jeu_.popCarte());
                this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Main.ajouterCarte(jeu_.popCarte());
                this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Main.ajouterCarte(jeu_.popCarte());
                selectJoueur = (selectJoueur + 1) % 4;
            }

            selectJoueur = (new Random()).Next() % 4; // selectionne le premier joueur aleatoirement
            tapis_.ajouterCarte(this.jeu_.popCarte());// Selection de la carte d'atout 

        }

        private void Distribution()
        {
            this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Main.ajouterCarte(jeu_.popCarte()); // Distribue les deux cartes manquante au Joueur qui a pris
            this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Main.ajouterCarte(jeu_.popCarte());
            selectJoueur = (selectJoueur + 1) % 4;

            for (int i = 0; i < 3; i++)// deuxieme tour
            {
                this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Main.ajouterCarte(jeu_.popCarte());
                this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Main.ajouterCarte(jeu_.popCarte());
                this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Main.ajouterCarte(jeu_.popCarte());
                selectJoueur = (selectJoueur + 1) % 4;
            }



        }

        internal PartieBelotte(EquipeBelotte equipe1, EquipeBelotte equipe2)
        {
            this.equipes_ = new EquipeBelotte[] { equipe1, equipe2 };
            jeu_ = new PaquetBelotte();

            defausse_ = new PaquetBelotte(true);
            tapis_ = new MainDeJeuBelotte(4);
            selectJoueur = 0;
        }

        internal void Jouer()
        {
            Debut:
            DebutPartie();


            for (int joueur = 0; joueur < 4; joueur++)
            {
                this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].SetStrategie(new PrendreAtout());

                if (null != this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Jouer(jeu_, tapis_))
                {
                    goto Distribution;
                }
                else
                    
                    selectJoueur = (selectJoueur + 1) % 4;
            }
            for (int joueur = 0; joueur < 4; joueur++)
            {
                this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].SetStrategie(new ChoisirAtout());

                if (null != this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Jouer(jeu_, tapis_))
                {
                    goto Distribution;
                }
                selectJoueur = (selectJoueur + 1) % 4;

            }

            Console.WriteLine("Personne n'a pris, on melange et on recommence !");
            goto Debut;




        Distribution:
            Distribution();

            this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].SetStrategie(new Maitre());
            this.equipes_[(int)(selectJoueur & 1)][(int)((selectJoueur >> 1) & 1)].Jouer(jeu_, tapis_);


        }
    }


}
