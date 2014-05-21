using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Evasion.Affichage._3D;

namespace Evasion.Decors
{
    class ObjetDecors
    {
        protected int type;
        protected Vector2 taille;
        protected int identifiant;
        protected Vector3 position;
        protected Matrix view;

        public Vector3 Position { get { return position; } set { position = value; } }
        public Vector2 Taille { get { return taille; } set { taille = value; } } 

        public ObjetDecors(ContentManager Content, Vector3 position, Vector2 taille, Matrix view, float aspectRatio, GraphicsDeviceManager graphics)
        {
            this.position = position;
            this.taille = taille;
            this.type = type;
            this.view = view;
            this.identifiant = identifiant;
        }
        
        virtual public void Draw(Camera camera)
        {

        }
        
    }
}
