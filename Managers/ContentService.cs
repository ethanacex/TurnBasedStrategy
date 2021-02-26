using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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

            SpriteFont fixedsys = _content.Load<SpriteFont>("fixedsys");
            Fonts.Add("FixedSys", fixedsys);

            Texture2D empty = _content.Load<Texture2D>("emptyblack");
            Texture2D test = _content.Load<Texture2D>("test");
            Texture2D red = _content.Load<Texture2D>("testred");
            Textures.Add("Test", test);
            Textures.Add("TestRed", red);
            Textures.Add("EmptyBlack", empty);

            SoundEffect menuHover = _content.Load<SoundEffect>("menuHover");
            SoundEffect menuFoward = _content.Load<SoundEffect>("forwardMenu");
            SoundEffect menuBack = _content.Load<SoundEffect>("backMenu");
            SFX.Add("MenuHover", menuHover);
            SFX.Add("MenuForward", menuFoward);
            SFX.Add("MenuBack", menuBack);
            
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
