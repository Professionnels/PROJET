using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Evasion.Affichage.Informations
{
    class BarreVie
    {
        private int vieMax;
        private int vieActu;
        private float tailleBarre;
        private float tailleVie;
        public Texture2D barre;

        public BarreVie(int vieMax, int vieActu, float tailleBarre, ContentManager Content)
        {
            this.vieMax = vieMax;
            this.vieActu = vieActu;
            this.tailleBarre = tailleBarre;
            this.barre = Content.Load<Texture2D>("ImageBarreVie");
            this.tailleVie = tailleBarre;
        }

        public void AjusteBarreVie()
        {
            if (vieActu > vieMax)
                vieActu = vieMax;

            if (vieActu < 0)
                vieActu = 0;

        }

        public void UpdateVie(int ajout)
        {
            vieActu = vieActu + ajout;
        }

        private void UpdateTailleVie()
        {
            tailleVie = (tailleBarre) * ((float)vieActu / (float)vieMax);
        }

        public void Draw()
        {
 
        }
    }
}
