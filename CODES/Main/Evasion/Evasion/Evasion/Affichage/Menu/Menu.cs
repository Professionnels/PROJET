using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Evasion.Affichage.Menu
{
    class Menu : Game1
    {
        private Texture2D Image_Fond;
        private Texture2D[] Liste_boutons;

        public Menu(string type)
        {
            Image_Fond = Content.Load<Texture2D>("menu_accueil_fond");
            //this.Liste_boutons = ArbreMenu.Get_Boutons(type);
        }

        public void Affiche_Menu(SpriteBatch sb)
        {
            sb.Draw(Image_Fond, new Vector2(0, 0), new Rectangle(0, 0, 2000, 1000), Color.White);
        }

    }
}
