using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Evasion.Affichage;

namespace Evasion.Affichage._3D
{
    class PNJ
    {
        private Model persoModel;
        private Vector3 persoPosition;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Vector3 Rotation;

        public string informations;

        private Matrix orientation = Matrix.Identity;
        private float scale = 10f;

        private Texture2D[] texture = new Texture2D[4];

        private KeyboardState currentKeyboardState;

        private TypePerso type;

        public Vector3 getPosition()
        {
            return persoPosition;
        }

        public Vector3 getRotation()
        {
            return Rotation;
        }

        public PNJ(ContentManager Content, Vector3 position, Vector3 rotation, Matrix view, float aspectRatio, TypePerso type)
        {
            this.persoModel = Content.Load<Model>("Models\\perso");
            this.persoPosition = position;
            this.Rotation = rotation;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            viewMatrix = view;
            this.type = type;
            texture[0] = Content.Load<Texture2D>("Models\\michael");
            texture[1] = Content.Load<Texture2D>("Models\\ennemidehors");
            texture[2] = Content.Load<Texture2D>("Models\\gardien");
            texture[3] = Content.Load<Texture2D>("Models\\prisonnier-2");
            this.texture = texture;
            this.initPhyPerso();
        }

        public void initPhyPerso()
        {
            Rotation = new Vector3(90.0f, 0f, 180f);
        }

        private void initPerso()
        {
            persoPosition = new Vector3(0f, 3f, 0f);
            Rotation = new Vector3(90.0f, 0f, 180f);
        }

        public void draw()
        {

            Matrix[] transforms = new Matrix[persoModel.Bones.Count];
            persoModel.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in persoModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.TextureEnabled = true;

                    effect.Texture = texture[((int)(type))];

                    effect.World = transforms[mesh.ParentBone.Index] *
                                    Matrix.CreateScale(scale) *
                                    Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(Rotation.X)) *
                                    Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(Rotation.Y)) *
                                    Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(Rotation.Z)) *
                                    Matrix.CreateTranslation(persoPosition);
                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;

                }
                mesh.Draw();
            }
        }

    }
}
