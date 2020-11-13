using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;
using SharpDX.Direct2D1;

namespace Assignment1
{
    public class Axis3D
    {
        private GraphicsDevice _gd;
        private VertexPositionColor [] _vertices;
        private BasicEffect _effect;

        public Axis3D(GraphicsDevice gd)
        {
            _gd = gd;
            _effect = new BasicEffect(gd);

            float aspectRatio = (float)gd.Viewport.Width / gd.Viewport.Height;

            //camera
            _effect.View = Matrix.CreateLookAt(new Vector3(5f, 5f, 5f), Vector3.Zero, Vector3.Up);
            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), aspectRatio, 0.1f, 1000f);
            _effect.LightingEnabled = false;
            _effect.VertexColorEnabled = true;

            CreateAxis();
        }

        private void CreateAxis()
        {
            float axisLength = 8f;
            int verticesCount = 6;
            _vertices = new VertexPositionColor [verticesCount];

            //X
            _vertices [0] = new VertexPositionColor(new Vector3(-axisLength, 0f, 0f), Color.Purple);
            _vertices [1] = new VertexPositionColor(new Vector3(axisLength, 0f, 0f), Color.Purple);

            //Y
            _vertices [2] = new VertexPositionColor(new Vector3(0f, -axisLength, 0f), Color.Pink);
            _vertices [3] = new VertexPositionColor(new Vector3(0f, axisLength, 0f), Color.Pink);

            //Z
            _vertices [4] = new VertexPositionColor(new Vector3(0f, 0f, -axisLength), Color.Cyan);
            _vertices [5] = new VertexPositionColor(new Vector3(0f, 0f, axisLength), Color.Cyan);
        }

        public void Draw()
        {
            _effect.World = Matrix.Identity;

            /*RENDER*/
            _effect.CurrentTechnique.Passes [0].Apply();
            _gd.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, _vertices, 0, 3);
        }

    }
}

