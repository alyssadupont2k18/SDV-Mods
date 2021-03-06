﻿using System;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System.Reflection;
using StardewModdingAPI.Events;
using StardewModdingAPI.Inheritance;
using Microsoft.Xna.Framework.Graphics;
using xTile;

namespace CJBCheatsMenu
{
    public class CJBCheatsMenu : Mod {

        public static Settings config;
        public override void Entry(params object[] objects) {

            config = new Settings().InitializeConfig(BaseConfigPath);
            GameEvents.UpdateTick += Events_UpdateTick;
            GameEvents.OneSecondTick += GameEvents_OneSecondTick;
            TimeEvents.TimeOfDayChanged += TimeEvents_TimeOfDayChanged;
            ControlEvents.KeyPressed += Events_KeyPressed;
            ControlEvents.ControllerButtonPressed += ControlEvents_ControllerButtonPressed;
            GraphicsEvents.OnPostRenderEvent += GraphicsEvents_DrawTick;
            TimeEvents.DayOfMonthChanged += TimeEvents_DayOfMonthChanged;
        }

        private void GameEvents_OneSecondTick(object sender, EventArgs e) {
            if (Game1.hasLoadedGame) {
                Cheats.OneSecondUpdate();
            }
        }

        private void ControlEvents_ControllerButtonPressed(object sender, EventArgsControllerButtonPressed e) {
            if (e.ButtonPressed.ToString().Equals(CJBCheatsMenu.config.openMenuKey)) {
                if (Game1.hasLoadedGame && Game1.activeClickableMenu == null && Game1.player.CanMove && !Game1.dialogueUp && !Game1.eventUp) {
                    CheatsMenu.Open();
                }
                return;
            }

            if (e.ButtonPressed.ToString().Equals(CJBCheatsMenu.config.freezeTimeKey)) {
                if (Game1.hasLoadedGame && Game1.activeClickableMenu == null) {
                    CJBCheatsMenu.config.freezeTime = !CJBCheatsMenu.config.freezeTime;
                }
                return;
            }

            if (Game1.activeClickableMenu is GameMenu) {
                GameMenu menu = (GameMenu)Game1.activeClickableMenu;
                List<IClickableMenu> pages = (List<IClickableMenu>)typeof(GameMenu).GetField("pages", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(menu);

                if (pages[menu.currentTab] is CheatsMenu) {
                    pages[menu.currentTab].receiveGamePadButton(e.ButtonPressed);
                }
            }
        }

        private void Events_KeyPressed(object sender, EventArgsKeyPressed e) {
            Cheats.onKeyPress(e.KeyPressed);
        }

        private void TimeEvents_DayOfMonthChanged(object sender, EventArgsIntChanged e) {
            if (!Game1.hasLoadedGame)
                return;

            Cheats.onDayOfMonthChanged();
        }

        private void GraphicsEvents_DrawTick(object sender, EventArgs e) {
            if (!Game1.hasLoadedGame)
                return;

            Cheats.onDrawTick();
        }

        private void TimeEvents_TimeOfDayChanged(object sender, EventArgsIntChanged e) {
            if (!Game1.hasLoadedGame)
                return;

            Cheats.onTimeOfDayChanged();
        }

        private void Events_UpdateTick(object sender, EventArgs e) {
            if (!Game1.hasLoadedGame)
                return;
            Cheats.onUpdate();
        }
    }
}
