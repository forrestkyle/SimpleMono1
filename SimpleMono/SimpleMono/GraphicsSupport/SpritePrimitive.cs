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
    class SpritePrimitive :TexturedPrimitive
    {
        private int numRows, numColumns, paddings;
        private int spriteImageWidth, spriteImageHeight;

        private int userSpecifiedTicks;
        private int currentTick;
        private int currentRow, currentColumn;
        private int beginRow, endRow;
        private int beginCol, endCol;

        String direction = "right";

        public SpritePrimitive(String image, Vector2 position, Vector2 size,
                               int rowCounts, int columnCount, int padding) :
        base(image, position, size)
        {
            numRows = rowCounts;
            numColumns = columnCount;
            paddings = padding;
            spriteImageWidth = mImage.Width / numRows;
            spriteImageHeight = mImage.Height / numColumns;

            userSpecifiedTicks = 1;
            currentTick = 0;
            currentRow = 0;
            currentColumn = 0;
            beginRow = beginCol = endRow = endCol = 0;
        }

        public int SpriteBeginRow
        {
            get { return beginRow; }
            set { beginRow = value; currentRow = value; }
        }
        public int SpriteEndRow
        {
            get { return endRow; }
            set { endRow = value; }
        }
        public int SpriteBeginColumn
        {
            get { return beginCol; }
            set { beginCol = value; currentColumn = value; }
        }
        public int SpriteEndColumn
        {
            get { return endCol; }
            set { endCol = value; }
        }
        public int SpriteAnimationTicks
        {
            get { return userSpecifiedTicks; }
            set { userSpecifiedTicks = value; }
        }

        public void SetSpriteAnimation(int startingRow, int startingCol, int endOfRow, int endOfCol, int tickInterval)
        {
            userSpecifiedTicks = tickInterval;
            beginRow = startingRow;
            beginCol = startingCol;
            endRow = endOfRow;
            endCol = endOfCol;

            currentRow = beginRow;
            currentColumn = beginCol;
            currentTick = 0;
        }

        public override void Update(Vector2 deltaTranslate, Vector2 deltaScale)
        {   
            if (Math.Abs(deltaTranslate.X) > Math.Abs(deltaTranslate.Y))
            {
                mPosition.X += deltaTranslate.X;
                if (deltaTranslate.X < 0 && direction != "left")
                {
                    this.SetSpriteAnimation(2, 0, 2, 3, 5);
                    direction = "left";
                }
                if (deltaTranslate.X > 0 && direction != "right")
                {
                    this.SetSpriteAnimation(0, 0, 0, 3, 5);
                    direction = "right";
                }
            }
            else
            {
                mPosition.Y -= deltaTranslate.Y;
                if (deltaTranslate.Y < 0 && direction != "down")
                {
                    this.SetSpriteAnimation(1, 0, 1, 3, 5);
                    direction = "down";
                }
                if (deltaTranslate.Y > 0 && direction != "up")
                {
                    this.SetSpriteAnimation(3, 0, 3, 3, 5);
                    direction = "up";
                }
            }
            
            currentTick++;
            if (currentTick > userSpecifiedTicks)
            {
                currentTick = 0;
                currentColumn++;
                if (currentColumn > endCol)
                {
                    currentColumn = beginCol;
                    currentRow++;

                    if (currentRow > endRow)
                        currentRow = beginRow;
                }
            }
        }

        public override void Draw()
        {
            // Define location and size of the texture
            Rectangle destRect = Camera.ComputePixelRectangle(base.mPosition, base.mSize);

            int imageTop = currentRow * spriteImageWidth;
            int imageLeft = currentColumn * spriteImageHeight;
            // Define the area to draw from the spriteSheet
            Rectangle srcRect = new Rectangle(
                imageLeft + paddings,
                imageTop + paddings,
                spriteImageWidth, spriteImageHeight);

            // Define the rotation origin
            Vector2 org = new Vector2(spriteImageWidth / 2, spriteImageHeight / 2);

            // Draw the texture
            Game1.spriteBatch.Draw(
                mImage,
                destRect,           // Area to be drawn in pixel space
                srcRect,            // Rect on the spriteSheet
                Color.White,        //
                0,       // Angle to rotate (clockwise)
                org,                // Image reference position
                SpriteEffects.None, 0f);

        }



    }
}
