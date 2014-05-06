#define DEBUG_BB

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

    class Mur
    {
        public Model mur;
        private Vector3 murPosition;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Vector3 Rotation;

        private GraphicsDeviceManager graphic;

        public int indexMur;

        private Matrix orientation;
        private float scale = 10f;
        public string informations;

        public TypeMur type;

        private Texture2D[] textures = new Texture2D[2];

#if DEBUG_BB
        public BoundingBox boundingBoxes = new BoundingBox();

        public short[] bBoxIndices = {
                                        0, 1, 1, 2, 2, 3, 3, 0, // Front edges
                                        4, 5, 5, 6, 6, 7, 7, 4, // Back edges
                                        0, 4, 1, 5, 2, 6, 3, 7 // Side edges connecting front and back
                                    };
#endif

        public Vector3 getPosition()
        {
            return murPosition;
        }

        public Vector3 getRotation()
        {
            return Rotation;
        }

        public Mur(ContentManager Content, Vector3 position, Matrix view, float aspectRatio, TypeMur type, GraphicsDeviceManager graphic)
        {
            this.mur = Content.Load<Model>("Models\\mur");
            this.murPosition = position;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            viewMatrix = view;
            orientation = Matrix.Identity;
            textures[0] = Content.Load<Texture2D>("Models\\briques");
            textures[1] = Content.Load<Texture2D>("Models\\mur_prison");
            this.type = type;
            this.initPhyMur();
            this.initMur();
            indexMur = 0;
            this.graphic = graphic;
        }

        public void initPhyMur()
        {
            Rotation = new Vector3(90.0f, 0f, 180f);
        }

        private void initMur()
        {
            Rotation = new Vector3(90.0f, 0f, 180f);
        }

        public void draw(Camera camera)
        {

            Matrix[] transforms = new Matrix[mur.Bones.Count];
            mur.CopyAbsoluteBoneTransformsTo(transforms);

            this.informations = "";

#if DEBUG_BB

            Vector3 meshMax = new Vector3(float.MinValue);
            Vector3 meshMin = new Vector3(float.MaxValue);
#endif

            foreach (ModelMesh mesh in mur.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.TextureEnabled = true;
                    effect.Texture = textures[(int)(type)];
                    effect.World = transforms[mesh.ParentBone.Index] *
                                    Matrix.CreateScale(new Vector3(10, 10, 10)) *
                                    Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(Rotation.X)) *
                                    Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(Rotation.Y)) *
                                    Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(Rotation.Z)) *
                                    Matrix.CreateTranslation(murPosition);
                    effect.View = camera.viewMatrix;
                    effect.Projection = camera.projectionMatrix;
                    mesh.Draw();
#if DEBUG_BB
                    BuildBoundingBox(mesh, transforms[mesh.ParentBone.Index], ref meshMin, ref meshMax);
#endif

                }
            }
#if DEBUG_BB
            boundingBoxes = new BoundingBox(Vector3.Min(meshMin, meshMax),
                Vector3.Max(meshMin, meshMax));

            foreach (ModelMesh mesh in mur.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    Vector3[] corners = boundingBoxes.GetCorners();
                    VertexPositionColor[] primitiveList = new VertexPositionColor[corners.Length];

                    // Assign the 8 box vertices
                    for (int i = 0; i < corners.Length; i++)
                    {
                        primitiveList[i] = new VertexPositionColor(corners[i], Color.White);
                    }

                    /* Set your own effect parameters here */

                    effect.LightingEnabled = false;
                    effect.TextureEnabled = false;
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
        }
#endif



#if DEBUG_BB
        private void BuildBoundingBox(ModelMesh mesh, Matrix meshTransform, ref Vector3 meshMin, ref Vector3 meshMax)
        {

            foreach (ModelMeshPart meshPart in mesh.MeshParts)
            {
                // Vertex buffer parameters
                int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                int vertexBufferSize = meshPart.NumVertices * vertexStride;

                // Get vertex data as float
                float[] vertexData = new float[vertexBufferSize / sizeof(float)];
                meshPart.VertexBuffer.GetData<float>(vertexData);

                // Iterate through vertices (possibly) growing bounding box, all calculations are done in world space
                for (int i = 0; i < vertexBufferSize / sizeof(float); i += vertexStride / sizeof(float))
                {
                    Vector3 transformedPosition = Vector3.Transform(new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]), meshTransform);

                    meshMin = Vector3.Min(meshMin, transformedPosition);
                    meshMax = Vector3.Max(meshMax, transformedPosition);
                }
            }

            meshMin = Vector3.Transform(meshMin, meshTransform);
            meshMax = Vector3.Transform(meshMax, meshTransform);

        }
#endif

    }
}