using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evasion.Affichage
{
    public enum TypeMur { brique = 0, beton = 1, bois = 2 };

    public static class Constantes
    {
        public const int SCREEN_WIDTH = 800; // Cannot change while !fullScreen
        public const int SCREEN_HEIGHT = 600; // Cannot change while !fullScreen
        public const float BUTTON_LENGTH = 0.20f; // Compared to the screen
    }
}
