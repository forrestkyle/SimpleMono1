using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SimpleMono
{
    class TexturedPrimitive
    {
        protected Texture2D mImage;     // The UWB-JPG.jpg image to be loaded
        protected Vector2 mPosition;    // Center position of image
        protected Vector2 mSize;        // Size of the image to be drawn

        public TexturedPrimitive(String imageName, Vector2 position, Vector2 size)
        {
            mImage = Game1.gameContent.Load<Texture2D>(imageName);
            mPosition = position;
            mSize = size;
        }

        public virtual void Update(Vector2 deltaTranslate, Vector2 deltaScale)
        {
            mPosition += deltaTranslate;
            mSize += deltaScale;
        }

        public virtual void Draw()
        {
            // Defines where and size of the texture to show
            //Rectangle destRect = new Rectangle((int)mPosition.X, 
            //                                   (int)mPosition.Y,
            //                                   (int)mSize.X, (int)mSize.Y);

            Rectangle destRect = Camera.ComputePixelRectangle(mPosition, mSize);

            Game1.spriteBatch.Draw(mImage, destRect, Color.White);
        }

        public Vector2 GetPosition()
        {
            return mPosition;
        }

    }
}
