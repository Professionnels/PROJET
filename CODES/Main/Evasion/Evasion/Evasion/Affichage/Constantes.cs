

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evasion.Affichage
{
    public enum TypeMur { brique = 0, beton = 1, barreaux = 2};
    public enum TypeSol { evasion = 0, prison = 1, plafond = 2};
    public enum TypePerso { perso = 0, gardien = 1, bellick = 2, prisonnier = 3};

    public static class Constantes
    {
        public const int SCREEN_WIDTH = 800; // Cannot change while !fullScreen
        public const int SCREEN_HEIGHT = 600; // Cannot change while !fullScreen
        public const float BUTTON_LENGTH = 0.20f; // Compared to the screen
        public const int scale = 20;
    }
}
