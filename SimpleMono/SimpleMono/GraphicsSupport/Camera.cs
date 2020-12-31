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
    static public class Camera
    {
        static private Vector2 worldOrigin = Vector2.Zero;       // Origin of the world
        static private float worldWidth = 100f;                  // Width of the world
        static private float worldHeight = 100f;
        static private float ratio_pixelWidthToWorldWidth = -1f; // Ratio between camera window and pixel
        static private float ratio_pixelHeightToWorldHeight = -1f; // Ratio between camera window and pixel

        static private float cameraWidthToPixelRatio()
        {
            if (ratio_pixelWidthToWorldWidth < 0f)
            {
                ratio_pixelWidthToWorldWidth = (float)Game1.graphics.PreferredBackBufferWidth / worldWidth;
            }                
            return ratio_pixelWidthToWorldWidth;
        }

        static private float cameraHeightToPixelRatio()
        {
            if (ratio_pixelHeightToWorldHeight < 0f)
            {
                ratio_pixelHeightToWorldHeight = (float)Game1.graphics.PreferredBackBufferHeight / worldHeight;
            }
            return ratio_pixelHeightToWorldHeight;
        }

        static public void SetCameraWindow(Vector2 origin, float width, float height)
        {
            worldOrigin = origin;
            worldWidth = width;
            worldHeight= height;
        }

        static public void ComputePixelPosition(Vector2 cameraPosition, out int x, out int y)
        {
            float ratioWidth = cameraWidthToPixelRatio();
            float ratioHeight = cameraHeightToPixelRatio();
            
            // Convert the position to pixel space
            x = (int)(((cameraPosition.X - worldOrigin.X) * ratioWidth) + 0.5f);
            y = (int)(((cameraPosition.Y - worldOrigin.Y) * ratioHeight) + 0.5f);
            //y = Game1.graphics.PreferredBackBufferHeight - y;
        }

        static public Rectangle ComputePixelRectangle(Vector2 position, Vector2 size)
        {
            float ratioWidth = cameraWidthToPixelRatio();
            float ratioHeight = cameraHeightToPixelRatio();

            // Convert size from camera window space to pixel space.
            int width = (int)((size.X * ratioWidth) + 0.5f);
            int height = (int)((size.Y * ratioHeight) + 0.5f);

            // Convert the position to pixel space
            int x, y;
            ComputePixelPosition(position, out x, out y);

            // Reference position is the center
            y -= height / 2;
            x -= width / 2;

            return new Rectangle(x, y, width, height);
        }
    }
}
