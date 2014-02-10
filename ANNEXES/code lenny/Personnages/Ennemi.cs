using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evasion
{
    class Ennemi
    {
        private int argent_recompense;
        private int joueur_cible;
        private int timer_reperer;
        private int champs_vision;
        private bool Assomer;
        private int temps_assomer;
        private bool se_deplace;

        public int _argent_recompense { get { return argent_recompense; } }
        public int _joueur_cible { get { return joueur_cible; } }
        public int _timer_reperer { get { return timer_reperer; } }
        public int _champs_vision { get { return champs_vision; } }
        public bool _assomer { get { return Assomer; } }
        public int _temps_assomer { get { return temps_assomer; } }
        public bool _se_deplace { get { return se_deplace; } }

        public Ennemi() { }

        public Ennemi(int argent_recompense, int joueur_cible, int timer_reperer, int champs_vision, bool Assomer, int temps_assomer, bool se_deplace)
        {
            argent_recompense = _argent_recompense;
            joueur_cible = _joueur_cible;
            timer_reperer = _timer_reperer;
            champs_vision = _champs_vision;
            Assomer = _assomer;
            temps_assomer = _temps_assomer;
            se_deplace = _se_deplace;
        }
        private void attaquer(int attaquer)
        {
            attaquer = attaquer ^ 1;
        }
        private void reperer(int reperer)
        {
            reperer = reperer ^ 1;
        }
        private void Assomer(int Assomer)
        {
            Assomer = Assomer ^ 1;
        }
        private void alerter(int alerter)
        {
            alerter = alerter ^ 1;
        }
        private void deplacer(int deplacer)
        {
            deplacer = deplacer ^ 1;
        }
    }
}
