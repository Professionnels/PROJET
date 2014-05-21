using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evasion.Affichage.Informations;
using Evasion.Personnages;
using Microsoft.Xna.Framework.Graphics;

namespace Evasion.Affichage
{
    class AffichageHUD
    {
        private BarreVie vie;
        private BarreVie vie2;

        public AffichageHUD(Microsoft.Xna.Framework.Content.ContentManager cm, SpriteBatch sb, bool multi)
        {
            vie = new BarreVie(100, 100, 200, cm, sb);
            if (multi)
                vie2 = new BarreVie(100, 100, 200, cm, sb);

        }

        public void Update(Joueur joueur1, Joueur joueur2, bool multi)
        {
            vie.UpdateVie(joueur1._vie);
            if (multi)
                vie2.UpdateVie(joueur2._vie);
        }

        public void Display(GraphicsDevice gd, bool multi, Viewport defaultView, Viewport leftView, Viewport rightView)
        {
           // gd.Viewport = defaultView;
            vie.Draw();
            if (multi)
            {
                //gd.Viewport = rightView;
                vie2.Draw();
            }
        }
    }
}
