using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Evasion.Affichage;
using Microsoft.Xna.Framework.Media;

namespace Evasion.Jeu
{
    class Jeu
    {
        private Son.Son SonAmbiance1;
        private Son.Son SonAmbiance2;
        private Son.Son current;
        private Fenetre Fen;

        public Jeu(Fenetre fen)
        {
            Fen = fen;
            SonAmbiance1 = new Son.Son(Son.ChargerSon.theme1);
            SonAmbiance2 = new Son.Son(Son.ChargerSon.theme2);
            current = SonAmbiance1;
        }

        public void Update(KeyboardState keyboardState, MouseState mouseState)
        {
            // Touches
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Fen.LoadContent(Content_t.Menu);
            }

            // Sons
            if (MediaPlayer.State == MediaState.Stopped)
            {
                current.Play();
                current = current == SonAmbiance1 ? SonAmbiance2 : SonAmbiance1;
            }
        }

        public void Save()
        { }

        public void DisplaySaveProgressing()
        { }

        public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb)
        { }
    }
}
