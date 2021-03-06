﻿using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using Microsoft.Xna.Framework;
using StardewValley.Menus;
using Microsoft.Xna.Framework.Graphics;

namespace CJBEndlessInventory {

    public class CJB {
        private static string GetSaveLocation() {
            FileInfo f = new FileInfo(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StardewValley"), "Saves"));
            return f.FullName;
        }

        public static void drawTextBox(int x, int y, SpriteFont font, string message, bool begin = false, int align = 0, float colorIntensity = 1F) {
            SpriteBatch b = Game1.spriteBatch;
            if(!begin)
                b.Begin();
            Vector2 bounds = font.MeasureString(message);
            int width = (int)bounds.X + Game1.tileSize / 2;
            int height = (int)font.MeasureString(message).Y + Game1.tileSize / 3;// SpriteText.getHeightOfString(message, width);
            if (align == 0) {
                IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), x, y, width, height + Game1.tileSize / 16, Color.White * colorIntensity, 1f, true);
                Utility.drawTextWithShadow(b, message, font, new Vector2((float)(x + Game1.tileSize / 4), (float)(y + Game1.tileSize / 4)), Game1.textColor);
            }
            if (align == 1) {
                IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), x - width / 2, y, width, height + Game1.tileSize / 16, Color.White * colorIntensity, 1f, true);
                Utility.drawTextWithShadow(b, message, font, new Vector2((float)(x + Game1.tileSize / 4 - width / 2), (float)(y + Game1.tileSize / 4)), Game1.textColor);
            }
            if (align == 2) {
                IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), x - width, y, width, height + Game1.tileSize / 16, Color.White * colorIntensity, 1f, true);
                Utility.drawTextWithShadow(b, message, font, new Vector2((float)(x + Game1.tileSize / 4 - width), (float)(y + Game1.tileSize / 4)), Game1.textColor);
            }

            if (!begin)
                b.End();
        }

        public static void removeLastHudMessage() {
            if (Game1.hudMessages?.Count() > 0) {
                Game1.hudMessages.RemoveAt(Game1.hudMessages.Count() - 1);
            }
        }
    }
}
