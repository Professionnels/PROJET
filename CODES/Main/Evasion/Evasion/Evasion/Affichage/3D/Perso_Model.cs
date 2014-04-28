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
        private Model persoModel;
        private Vector3 persoPosition;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Vector3 rotation;

        private Matrix orientation = Matrix.Identity;

        private float aspectRatio;
        private Vector3 cameraPosition;

        private float scale = 10f;

        public Vector3 getPosition()
        {
            return persoPosition;
        }

        public Perso_Model(Model model, Vector3 position, Vector3 rotation)
        {
            this.persoModel = model;
            this.persoPosition = position;
            this.rotation = rotation;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            viewMatrix = Matrix.CreateLookAt(cameraPosition, persoPosition, Vector3.Up);
        }

        private void initPerso()
        {
            persoPosition = new Vector3(0f, 3f, 0f);
            rotation = new Vector3(90.0f, 0f, 180f);
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
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(persoPosition) *
                        Matrix.CreateScale(scale) * Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(rotation.X)) *
                        Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(rotation.Y)) * Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(rotation.Z));

                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();
            }
        }

        public void updatePosition(GameTime gameTime, KeyboardState keyboardState)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            float vitesse = 0.01f;


            if (keyboardState.IsKeyDown(Keys.Right))
                persoPosition.X -= time * vitesse;
            if (keyboardState.IsKeyDown(Keys.Left))
                persoPosition.X += time * vitesse;


            if (keyboardState.IsKeyDown(Keys.Down))
                persoPosition.Y += time * vitesse;

            if (keyboardState.IsKeyDown(Keys.Up))
                persoPosition.Y -= time * vitesse;
        }

    }
}
