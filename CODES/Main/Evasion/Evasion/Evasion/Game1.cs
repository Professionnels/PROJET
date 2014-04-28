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

namespace Evasion
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Personnage perso = new Personnage();
        Fenetre fenetre;

                //PERSONNAGE
        private Model persoModel;
        private Vector3 persoPosition;

        private Vector3 Rotation;
        private Vector3 Position;

        KeyboardState currentKeyboardState = new KeyboardState();

        private Vector3 cameraPosition;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Matrix orientation = Matrix.Identity;

        private float aspectRatio;
        private float scale = 10f;

        //MUR
        private Model mur;
        private Vector3 murPosition;
        private Vector3 murRotation;

        //MUR2
        private Model Tmur;
        private Vector3 TmurPosition;
        private Vector3 TmurRotation;

        //SOL
        private Model sol;
        private Vector3 solPosition;
        private Vector3 solRotation;

        public Game1()
        {
            fenetre = new Fenetre(this);
@@ -43,6 +76,7 @@ namespace Evasion
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            InitPhysique();
            base.Initialize();
        }

@@ -57,6 +91,7 @@ namespace Evasion
            Son.ChargerSon.Init(Content);
            ChargerImages.InitMenu(Content);
            fenetre.LoadContent(Content_t.Menu);
            LoadModel();
            // TODO: use this.Content to load your game content here
        }

@@ -97,6 +132,8 @@ namespace Evasion
            fenetre.Update(Keyboard.GetState(), Mouse.GetState());
            // TODO: Add your update logic here

            UpdatePosition(gameTime);

            base.Update(gameTime);
        }

@@ -106,13 +143,141 @@ namespace Evasion
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            fenetre.Display(spriteBatch);
            spriteBatch.End();
            if (fenetre.ok)
            {
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                DrawMeshes();
            }
            else
            {
                spriteBatch.Begin();
                fenetre.Display(spriteBatch);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            float vitesse = 0.01f;


            if (currentKeyboardState.IsKeyDown(Keys.Right))
                Rotation.Y -= time * vitesse * 10;
            if (currentKeyboardState.IsKeyDown(Keys.Left))
                Rotation.Y += time * vitesse * 10;


            if (currentKeyboardState.IsKeyDown(Keys.Down))
                persoPosition.Y += time * vitesse;

            if (currentKeyboardState.IsKeyDown(Keys.Up))
                persoPosition.Y -= time * vitesse;


        }

        private void InitPhysique()
        {
            persoPosition = new Vector3(0f, 3f, 0f);
            murPosition = new Vector3(1f, 0f, -1.37f);
            TmurPosition = new Vector3(-1f, 0f, -1.2f);
            solPosition = new Vector3(0f, 0f, -0.28f);

            cameraPosition = new Vector3(200.0f, 100f, 100f);

            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);

            Position = Vector3.Zero;
            Rotation = new Vector3(90.0f, 0f, 180f);

            murRotation = new Vector3(90.0f, 0f, 180f);
            TmurRotation = new Vector3(90.0f, 0f, 180f);
            solRotation = new Vector3(90.0f, 0f, 180f);

            viewMatrix = Matrix.CreateLookAt(cameraPosition, persoPosition, Vector3.Up);
        }

        private void LoadModel()
        {
            persoModel = Content.Load<Model>("Models/perso");
            mur = Content.Load<Model>("Models/mur");
            Tmur = Content.Load<Model>("Models/murtableau");
            sol = Content.Load<Model>("Models/sol");
        }

        private void DrawMeshes()
        {
            Matrix[] transforms = new Matrix[persoModel.Bones.Count];
            persoModel.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in persoModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(persoPosition) *
                        Matrix.CreateScale(scale) * Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(Rotation.X)) *
                        Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(Rotation.Y)) * Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(Rotation.Z));

                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();

            }

            foreach (ModelMesh mesh in mur.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(murPosition) *
                        Matrix.CreateScale(scale) * Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(murRotation.X)) *
                        Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(murRotation.Y)) * Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(murRotation.Z));

                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();
            }

            foreach (ModelMesh mesh in Tmur.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(TmurPosition) *
                        Matrix.CreateScale(scale) * Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(TmurRotation.X)) *
                        Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(TmurRotation.Y)) * Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(TmurRotation.Z));

                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();
            }

            foreach (ModelMesh mesh in sol.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(solPosition) *
                        Matrix.CreateScale(100) * Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(TmurRotation.X)) *
                        Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(TmurRotation.Y)) * Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(TmurRotation.Z));

                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();
            }
        }
    }
}

    }
}
