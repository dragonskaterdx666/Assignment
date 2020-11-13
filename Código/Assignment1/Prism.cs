
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
    public class Prism
    {
        #region Variables
        private BasicEffect _effect;
        private GraphicsDevice _gd;
        private Color [] _colors;
        private VertexPositionColor [] _vertices;
        private VertexPositionColor [] _verticesB;
        private VertexPositionColor [] _faces;
        private int _vertexPerSide = 6;
        private int _height;
        private int _sides;
        private short [] _indices;
        private float _angle = 0;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor that receives the params to build the Prism ** lim_sides: 3-10
        /// </summary>
        /// <param name="height"></param>
        /// <param name="sides"> can only be set from 3-10 sides</param>
        public Prism(GraphicsDevice gd, int height, int sides)
        {
            float aspectRatio = (float)gd.Viewport.Width / gd.Viewport.Height;

            _gd = gd;
            _height = height;
            _sides = sides;

            /* filling array of colors*/
            _colors = new Color []
            {
                Color.Red,
                Color.AliceBlue,
                Color.HotPink,
                Color.Blue,
                Color.MediumBlue,
                Color.BlueViolet,
                Color.Aquamarine,
                Color.Purple,
                Color.MediumPurple,
                Color.DeepPink
            };

            _effect = new BasicEffect(gd);
            _effect.View = Matrix.CreateLookAt(new Vector3(4f, 4f, 4f), Vector3.Zero, Vector3.Up);
            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), aspectRatio, 0.01f, 1000f);
            _effect.LightingEnabled = false;
            _effect.VertexColorEnabled = true;

            CreatePrism();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the base of the prism
        /// </summary>
        private void CreatePrism()
        {
            //variables
            int verticesCount = _sides * 2;

            float x, z, y = 0f;
            float radius = 1;
            float arch = 360 / _sides;

            //sets the size of the vertices array accordingly
            _vertices = new VertexPositionColor [verticesCount + 1];
            _verticesB = new VertexPositionColor [verticesCount + 1];

            //iteration to create each vertex inside the array vertices
            for (int i = 0; i <= _sides; i++)
            {
                //calculating the values of x and z according to the number of vertices
                _angle = i * arch;
                _angle = MathHelper.ToRadians(_angle);
                x = radius * MathF.Cos(_angle);
                z = radius * MathF.Sin(_angle);

                _verticesB [i] = new VertexPositionColor(new Vector3(x, y, -z), Color.White);
                _vertices [i] = new VertexPositionColor(new Vector3(x, _height, -z), Color.White);
            }

            int indexCount = verticesCount + 2;

            _indices = new short [indexCount];

            for (short i = 0; i <= _sides; i++)
            {
                _indices [2 * i] = (short)(i % _sides);
                _indices [2 * i + 1] = (short)_sides;
            }

            int sideVertexCount = _vertexPerSide * _sides;

            _faces = new VertexPositionColor [sideVertexCount];

            //iteration for the triangles on the faces
            for (int i = 0; i < _sides; i++)
            {
                //the 1 vertex angle 
                float angle = (float)i * (float)(2 * Math.PI) / (float)_sides;
                float x1 = (float)Math.Cos(angle);
                float z1 = -(float)Math.Sin(angle);

                //2 vertex angle
                float nextAngle = (float)(i + 1) * (float)(2 * Math.PI) / (float)_sides;
                float a = (float)Math.Cos(nextAngle);
                float c = -(float)Math.Sin(nextAngle);

                // 1st triangle (down left corner -> upper left corner -> down right corner)
                _faces [_vertexPerSide * i] = new VertexPositionColor(new Vector3(x1, y, z1), _colors [i]);
                _faces [_vertexPerSide * i + 1] = new VertexPositionColor(new Vector3(x1, _height, z1), _colors [i]);
                _faces [_vertexPerSide * i + 2] = new VertexPositionColor(new Vector3(a, y, c), _colors [i]);

                // 2nd triangle (upper left corner ->  upper right corner -> down right corner)
                _faces [_vertexPerSide * i + 3] = new VertexPositionColor(new Vector3(x1, _height, z1), _colors [i]);
                _faces [_vertexPerSide * i + 4] = new VertexPositionColor(new Vector3(a, _height, c), _colors [i]);
                _faces [_vertexPerSide * i + 5] = new VertexPositionColor(new Vector3(a, y, c), _colors [i]);
            }
        }


        /// <summary>
        /// Draws the Primitives
        /// </summary>
        public void Draw()
        {
            _effect.CurrentTechnique.Passes [0].Apply();
        
            /*renders sides*/
            _gd.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, _faces, 0, 2 * _sides);
            
            /*renders Bottom base*/
            _gd.DrawUserIndexedPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleStrip,
                _vertices,
                0,
                _vertices.Length,
                _indices,
                0,
                _indices.Length - 2);

            /*renders Top base*/
            _gd.DrawUserIndexedPrimitives<VertexPositionColor>(
             PrimitiveType.TriangleStrip,
             _verticesB,
             0,
             _verticesB.Length,
             _indices,
             0,
             _indices.Length - 2);

            
        }
        #endregion
    }
}
