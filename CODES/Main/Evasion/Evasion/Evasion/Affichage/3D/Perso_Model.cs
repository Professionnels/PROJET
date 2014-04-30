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
    class Perso_Model
    {
        public Model persoModel;
        private Vector3 persoPosition;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Vector3 Rotation;

        public string informations;

        private Matrix orientation = Matrix.Identity;
        private Vector3 cameraPosition;
        private float scale = 10f;

        private KeyboardState currentKeyboardState;

        public Vector3 getPosition()
        {
            return persoPosition;
        }

        public Vector3 getRotation()
        {
            return Rotation;
        }

        public Perso_Model(ContentManager Content, Vector3 position, Matrix view, float aspectRatio)
        {
            this.persoModel = Content.Load<Model>("Models\\perso");
            this.persoPosition = position;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            viewMatrix = view;
            this.initPhyPerso();
            this.initPerso();
        }

        public void initPhyPerso()
        {
            Rotation = new Vector3(90.0f, 0f, 180f);
        }

        private void initPerso()
        {
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

        public void UpdatePosition(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            float vitesse = 0.1f;
            float deplacement = time * vitesse;
            float rotV = 0.4f;
            float tour = time * rotV;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Rotation.Y = (float)((float)(Mouse.GetState().X) / (float)(Evasion.Affichage.Constantes.SCREEN_WIDTH) * 360.0);
            }

            if (currentKeyboardState.IsKeyDown(Keys.A))
                Rotation.Y = (Rotation.Y - tour) % 360;
            if (currentKeyboardState.IsKeyDown(Keys.Z))
                Rotation.Y = (Rotation.Y + tour) % 360;

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                //persoPosition.X += (float)(time * vitesse * Math.Cos((double)(MathHelper.ToRadians(Rotation.Y))));
                //persoPosition.X+=
            }
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                //persoPosition.X -= (float)(time * vitesse * Math.Cos((double)(MathHelper.ToRadians(Rotation.Y))));
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                persoPosition.Z += (float)(deplacement * Math.Cos(Math.PI / 180 * Rotation.Y));
                persoPosition.X -= (float)(deplacement * Math.Sin(Math.PI / 180 * Rotation.Y));
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                persoPosition.Z -= (float)(deplacement * Math.Cos(Math.PI / 180 * Rotation.Y));
                persoPosition.X += (float)(deplacement * Math.Sin(Math.PI / 180 * Rotation.Y));
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                persoPosition.Z -= (float)(deplacement * Math.Sin(Math.PI / 180 * Rotation.Y));
                persoPosition.X -= (float)(deplacement * Math.Cos(Math.PI / 180 * Rotation.Y));
            }
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                persoPosition.Z += (float)(deplacement * Math.Sin(Math.PI / 180 * Rotation.Y));
                persoPosition.X += (float)(deplacement * Math.Cos(Math.PI / 180 * Rotation.Y));
            }

            informations = "";
            informations += "Perso.X = " + persoPosition.X.ToString() + "\n";
            informations += "Perso.Z = " + persoPosition.Z.ToString() + "\n";

            informations += "Rotation.Y = " + Rotation.Y.ToString() + "\n";
            informations += "cos(Rotation.Y) = " + Math.Cos(Math.PI / 180 * Rotation.Y).ToString() + "\n";
            informations += "sin(Rotation.Y) = " + Math.Sin(Math.PI / 180 * Rotation.Y).ToString() + "\n";
        }

    }
}
