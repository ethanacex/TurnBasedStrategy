using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using StrategyGame.Media;
using System.Collections.Generic;

namespace StrategyGame.Managers
{
    public sealed class ContentService
    {
        private static ContentManager _content = null;
        private static ContentService _instance = null;
        private static GraphicsDevice _graphics = null;
        private static readonly object _padlock = new object();

        public GraphicsDevice Graphics { get { return _graphics; } }
        public Dictionary<string, Texture2D> Textures { get; private set; }
        public Dictionary<string, SpriteFont> Fonts { get; private set; }
        public Dictionary<string, SoundEffect> SFX { get; private set; }
        public Dictionary<string, Song> Music { get; private set; }

        private ContentService()
        {
            Textures = new Dictionary<string, Texture2D>();
            Fonts = new Dictionary<string, SpriteFont>();
            Music = new Dictionary<string, Song>();
            SFX = new Dictionary<string, SoundEffect>();
        }

        public void LoadContent(ContentManager Content, GraphicsDevice graphics)
        {
            _content = Content;
            _graphics = graphics;

            SpriteFont menuFont = _content.Load<SpriteFont>("Fonts/fixedsys");
            SpriteFont inGameFont = _content.Load<SpriteFont>("Fonts/fixedsysUI");
            SpriteFont detailFont = _content.Load<SpriteFont>("Fonts/fixedsysDetail");
            SpriteFont tileFont = _content.Load<SpriteFont>("Fonts/tileText");
            Fonts.Add("MenuFont", menuFont);
            Fonts.Add("InGameFont", inGameFont);
            Fonts.Add("DetailFont", detailFont);
            Fonts.Add("TileFont", tileFont);

            Texture2D empty = _content.Load<Texture2D>("Textures/emptyblack");
            Texture2D test = _content.Load<Texture2D>("Textures/test");
            Texture2D red = _content.Load<Texture2D>("Textures/testred");
            Texture2D logo = _content.Load<Texture2D>("Textures/logo");
            Texture2D mine = _content.Load<Texture2D>("Textures/mine");
            Texture2D worldmap = _content.Load<Texture2D>("Textures/worldmap");
            Texture2D worldmap2 = _content.Load<Texture2D>("Textures/worldmap2");
            Texture2D dPad = _content.Load<Texture2D>("Textures/dPad");
            Textures.Add("Test", test);
            Textures.Add("Mine", mine);
            Textures.Add("TestRed", red);
            Textures.Add("Logo", logo);
            Textures.Add("EmptyBlack", empty);
            Textures.Add("WorldMap", worldmap);
            Textures.Add("WorldMap2", worldmap2);
            Textures.Add("DPad", dPad);

            SoundEffect menuHover = _content.Load<SoundEffect>("Audio/menuHover");
            SoundEffect menuFoward = _content.Load<SoundEffect>("Audio/forwardMenu");
            SoundEffect menuBack = _content.Load<SoundEffect>("Audio/backMenu");
            SoundEffect navSound = _content.Load<SoundEffect>("Audio/navSound");
            SFX.Add("MenuHover", menuHover);
            SFX.Add("MenuForward", menuFoward);
            SFX.Add("MenuBack", menuBack);
            SFX.Add("NavSound", navSound);

            Song menuMusic = _content.Load<Song>("Audio/superSuspense");
            Song mainTheme = _content.Load<Song>("Audio/mainBattleTheme");
            Music.Add("MenuTheme", menuMusic);
            Music.Add("MainTheme", mainTheme);

            GameState.AllowMusicHandler += Audio.ToggleMusic;
            GameState.MainMenuHandler += Audio.MenuMusic;
            GameState.InGameHandler += Audio.GameMusic;
        }

        public static ContentService Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new ContentService();
                    }
                    return _instance;
                }
            }
        }
    }
}
