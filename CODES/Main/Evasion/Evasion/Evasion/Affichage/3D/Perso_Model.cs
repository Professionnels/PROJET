//#define DEBUG_BB

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
        public Vector3 persoPosition;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        public Vector3 Rotation;

        public string informations;

        private Matrix orientation = Matrix.Identity;
        private float scale = 10f;

        private KeyboardState currentKeyboardState;
        private Texture2D texture;
        private GraphicsDeviceManager graphic;

        private Dictionary<string, Keys> controles1;
        private Dictionary<string, Keys> controles2;

        private Dictionary<string, Keys> touches;

#if DEBUG_BB
        public BoundingBox boundingBoxes = new BoundingBox();

        short[] bBoxIndices = {
                                    0, 1, 1, 2, 2, 3, 3, 0, // Front edges
                                    4, 5, 5, 6, 6, 7, 7, 4, // Back edges
                                    0, 4, 1, 5, 2, 6, 3, 7 // Side edges connecting front and back
                                };
#endif

        public Vector3 getPosition()
        {
            return persoPosition;
        }

        public Vector3 getRotation()
        {
            return Rotation;
        }

        public Perso_Model(ContentManager Content, Vector3 position, Matrix view, float aspectRatio, GraphicsDeviceManager graphic, int nbJoueur)
        {
            this.persoModel = Content.Load<Model>("Models\\perso");
            this.persoPosition = position;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            viewMatrix = view;
            texture = Content.Load<Texture2D>("Models\\michael");
            this.initPhyPerso();
            this.initPerso();
            this.graphic = graphic;

            controles1 = new Dictionary<string, Keys>();
            controles1.Add("haut", Keys.Up);
            controles1.Add("bas", Keys.Down);
            controles1.Add("gauche", Keys.Left);
            controles1.Add("droite", Keys.Right);
            controles1.Add("tourneG", Keys.N);
            controles1.Add("tourneD", Keys.M);

            controles2 = new Dictionary<string, Keys>();
            controles2.Add("haut", Keys.W);
            controles2.Add("bas", Keys.S);
            controles2.Add("gauche", Keys.A);
            controles2.Add("droite", Keys.D);
            controles2.Add("tourneG", Keys.T);
            controles2.Add("tourneD", Keys.Y);

            if (nbJoueur == 1)
                touches = controles1;
            else
                touches = controles2;
        }

        public void initPhyPerso()
        {
            Rotation = new Vector3(90.0f, 0f, 180f);
        }

        private void initPerso()
        {
            Rotation = new Vector3(90.0f, 0f, 180f);

        }

        public void draw(Camera camera)
        {
            Matrix[] transforms = new Matrix[persoModel.Bones.Count];
            persoModel.CopyAbsoluteBoneTransformsTo(transforms);

#if DEBUG_BB

            Vector3 meshMax = new Vector3(float.MinValue);
            Vector3 meshMin = new Vector3(float.MaxValue);
#endif

            foreach (ModelMesh mesh in persoModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.TextureEnabled = true;
                    effect.Texture = texture;
                    effect.World = transforms[mesh.ParentBone.Index] *
                                    Matrix.CreateScale(new Vector3(6,6,6)) *
                                    Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(Rotation.X)) *
                                    Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(Rotation.Y)) *
                                    Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(Rotation.Z)) *
                                    Matrix.CreateTranslation(persoPosition);
                    effect.View = camera.viewMatrix;
                    effect.Projection = camera.projectionMatrix;

                    mesh.Draw();

#if DEBUG_BB
                    BuildBoundingBox(mesh, effect.World, ref meshMin, ref meshMax);
#endif

                }


            }

#if DEBUG_BB
            boundingBoxes = new BoundingBox(Vector3.Min(meshMin, meshMax),
                Vector3.Max(meshMin, meshMax));
#endif

#if DEBUG_BB
            foreach(ModelMesh mesh in persoModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                        this.informations += "/n" + boundingBoxes.ToString();
                        Vector3[] corners = boundingBoxes.GetCorners();
                        VertexPositionColor[] primitiveList = new VertexPositionColor[corners.Length];

                        // Assign the 8 box vertices
                        for (int i = 0; i < corners.Length; i++)
                        {
                            primitiveList[i] = new VertexPositionColor(corners[i], Color.White);
                        }

                        effect.TextureEnabled = false;
                        effect.LightingEnabled = false;

                        // Draw the box with a LineList
                        foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            graphic.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                                PrimitiveType.LineList, primitiveList, 0, 8,
                                bBoxIndices, 0, 12);
                        }
                    
                }
            }
#endif
                

            
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
                //Rotation.Y = (float)((float)(Mouse.GetState().X) / (float)(Evasion.Affichage.Constantes.SCREEN_WIDTH) * 360.0);
            }

            if (currentKeyboardState.IsKeyDown(touches["tourneG"]))
                Rotation.Y = (Rotation.Y - tour) % 360;
            if (currentKeyboardState.IsKeyDown(touches["tourneD"]))

                Rotation.Y = (Rotation.Y + tour) % 360;

            
            if (currentKeyboardState.IsKeyDown(touches["haut"]))
            {
                persoPosition.Z += (float)(deplacement * Math.Cos(Math.PI / 180 * Rotation.Y));
                persoPosition.X -= (float)(deplacement * Math.Sin(Math.PI / 180 * Rotation.Y));
            }
            if (currentKeyboardState.IsKeyDown(touches["bas"]))
            {
                persoPosition.Z -= (float)(deplacement * Math.Cos(Math.PI / 180 * Rotation.Y));
                persoPosition.X += (float)(deplacement * Math.Sin(Math.PI / 180 * Rotation.Y));
            }

            if (currentKeyboardState.IsKeyDown(touches["droite"]))
            {
                persoPosition.Z -= (float)(deplacement * Math.Sin(Math.PI / 180 * Rotation.Y));
                persoPosition.X -= (float)(deplacement * Math.Cos(Math.PI / 180 * Rotation.Y));
            }
            if (currentKeyboardState.IsKeyDown(touches["gauche"]))
            {
                persoPosition.Z += (float)(deplacement * Math.Sin(Math.PI / 180 * Rotation.Y));
                persoPosition.X += (float)(deplacement * Math.Cos(Math.PI / 180 * Rotation.Y));
            }

            informations = "";
            informations += "Perso.X = " + persoPosition.X.ToString() + "\n";
            informations += "Perso.Z = " + persoPosition.Z.ToString() + "\n";
        }


#if DEBUG_BB
        private void BuildBoundingBox(ModelMesh mesh, Matrix meshTransform, ref Vector3 meshMin, ref Vector3 meshMax)
        {

            foreach (ModelMeshPart part in mesh.MeshParts)
            {
                int stride = part.VertexBuffer.VertexDeclaration.VertexStride;

                VertexPositionNormalTexture[] vertexData = new VertexPositionNormalTexture[part.NumVertices];
                part.VertexBuffer.GetData(part.VertexOffset * stride, vertexData, 0, part.NumVertices, stride);

                Vector3 vertPosition = new Vector3();

                for (int i = 0; i < vertexData.Length; i++)
                {
                    vertPosition = vertexData[i].Position;

                    meshMin = Vector3.Min(meshMin, vertPosition);
                    meshMax = Vector3.Max(meshMax, vertPosition);
                }
            }

            meshMin = Vector3.Transform(meshMin, meshTransform);
            meshMax = Vector3.Transform(meshMax, meshTransform);

        }
#endif

    }
}