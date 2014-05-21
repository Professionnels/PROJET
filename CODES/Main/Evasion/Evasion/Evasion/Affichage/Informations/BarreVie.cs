using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;


namespace Evasion.Affichage.Informations
{
    class BarreVie
    {
        private int vieMax;
        private int vieActu;
        private float tailleBarre;
        private float tailleVie;
        public Texture2D barre;
        public Texture2D bordure;
        private SpriteBatch sb;
        private SpriteFont font;
        private string pourcentageVie;
        private Vector2 position;
        

        public BarreVie(int vieMax, int vieActu, float tailleBarre, ContentManager Content, SpriteBatch sb)
        {
            this.position = Vector2.Zero;
            this.vieMax = vieMax;
            this.vieActu = vieActu;
            this.tailleBarre = tailleBarre;
            this.barre = Content.Load<Texture2D>("ImageBarreVie");
            this.bordure = Content.Load<Texture2D>("Bordure");
            this.tailleVie = 1;
            this.sb = sb;
            this.font = Content.Load<SpriteFont>("MyFont");
            pourcentageVie = vieActu.ToString() + " / " + vieMax.ToString();
        }

        public void AjusteBarreVie()
        {
            if (vieActu > vieMax)
                vieActu = vieMax;

            if (vieActu < 0)
                vieActu = 0;
        }

        public void UpdateVie(int vie)
        {
            vieActu = vie;
            AjusteBarreVie();
            UpdateTailleVie();
        }

        private void UpdateTailleVie()
        {
            tailleVie = ((float)vieActu / (float)vieMax);
            pourcentageVie = vieActu.ToString() + " / " + vieMax.ToString();  
        }

        public void Draw()
        {
            Rectangle rect = new Rectangle(0, 0, (int)(tailleVie * tailleBarre), 50);
            sb.Draw(bordure, Vector2.Zero, Microsoft.Xna.Framework.Color.White);          
            sb.Draw(barre, position, rect, Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, new Vector2(1,1), SpriteEffects.None, 0);
            sb.DrawString(font, pourcentageVie, new Vector2(30, 0), Color.Black);
        }
    }
}
