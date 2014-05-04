using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Evasion
{
    static class ChargerImages
    {
        public static Texture2D bouton_menu_principal;
        public static Texture2D bouton_multijoueur;
        public static Texture2D bouton_quitter;
        public static Texture2D bouton_nouveau_jeu;
        public static Texture2D bouton_sauvegarder;
        public static Texture2D bouton_charger_jeu;
        public static Texture2D bouton_reprendre;
        public static Texture2D bouton_options;
        public static Texture2D menu_accueil;

        public static Texture2D bouton_menu_principal_on;
        public static Texture2D bouton_multijoueur_on;
        public static Texture2D bouton_quitter_on;
        public static Texture2D bouton_nouveau_jeu_on;
        public static Texture2D bouton_sauvegarder_on;
        public static Texture2D bouton_charger_jeu_on;
        public static Texture2D bouton_reprendre_on;
        public static Texture2D bouton_options_on;

        static public void InitMenu(ContentManager cm)
        {
            bouton_menu_principal = cm.Load<Texture2D>("Main menu");
            bouton_multijoueur = cm.Load<Texture2D>("Multijoueur game");
            bouton_quitter = cm.Load<Texture2D>("Quit");
            bouton_nouveau_jeu = cm.Load<Texture2D>("New game");
            bouton_sauvegarder = cm.Load<Texture2D>("Save");
            bouton_charger_jeu = cm.Load<Texture2D>("Load game");
            bouton_reprendre = cm.Load<Texture2D>("Reprendre");
            bouton_options = cm.Load<Texture2D>("Options");
            menu_accueil = cm.Load<Texture2D>("menu_accueil_fond");

            bouton_menu_principal_on = cm.Load<Texture2D>("Main menu_on");
            bouton_multijoueur_on = cm.Load<Texture2D>("Multijoueur_on");
            bouton_quitter_on = cm.Load<Texture2D>("Quit_on");
            bouton_nouveau_jeu_on = cm.Load<Texture2D>("New game_on");
            bouton_sauvegarder_on = cm.Load<Texture2D>("Save_on");
            bouton_charger_jeu_on = cm.Load<Texture2D>("Load game_on");
            bouton_reprendre_on = cm.Load<Texture2D>("Reprendre_on");
            bouton_options_on = cm.Load<Texture2D>("Options_on");
        }

        static public void InitJeu(ContentManager cm)
        {
        }


    }
}
