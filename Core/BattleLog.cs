using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StrategyGame.GUI;
using StrategyGame.Managers;
using StrategyGame.Media;

namespace StrategyGame.Core
{
    public class BattleLog : Panel
    {
        public List<string> Log { get; set; }

        private StringBuilder sb;

        private Panel playerTurnPanel;
        private Label messageTitleLabel;
        private Label messageLogLabel;
        private Label playerTurnLabel;

        private bool logUpdated;

        public BattleLog(Point location, Point size) : base(location, size)
        {
            Log = new List<string>();
            sb = new StringBuilder();
            SetCustomBorder(3, Color.White);
        }

        public void UpdatePlayerTurnLabel()
        {

        }

        public void UpdateLog(string message)
        {
            Log.Insert(0, message);
            if (Log.Count > 5)
                Log.RemoveAt(Log.Count - 1);
            logUpdated = true;
        }

        public void Initialize()
        {
            Point labelSize;
            
            Vector2 messageTitlePosition = new Vector2(Left + 20, Y - 15);
            messageTitleLabel = new Label(" Message Log: ", messageTitlePosition, Fonts.Small, Color.Cyan);
            messageLogLabel = new Label(sb.ToString(), new Vector2(Left + 30, messageTitleLabel.Bottom + 10), Fonts.Small, Color.White);

            UpdateLog("The game has started, it's " + GameState.CurrentPlayerName + "'s turn.");

            labelSize = Fonts.Small.MeasureString(" Blue's Turn ").ToPoint();
            playerTurnPanel = new Panel(new Point(Right - (labelSize.X + 20), messageTitleLabel.Y), labelSize);

            SetPlayerTurnLabel();
        }

        public void UpdatePlayerTurnLabel(object sender, EventArgs e)
        {
            SetPlayerTurnLabel();
        }

        private void SetPlayerTurnLabel()
        {
            switch (GameState.CurrentPlayer)
            {
                case PlayerTurn.Blue: GameState.CurrentPlayer = PlayerTurn.Red; break;
                case PlayerTurn.Red: GameState.CurrentPlayer = PlayerTurn.Blue; break;
                default: break;
            }
            Point labelSize = Fonts.Small.MeasureString(" " + GameState.CurrentPlayerName + "s Turn ").ToPoint();
            string updatedText = " " + GameState.CurrentPlayerName + "s Turn ";
            Vector2 position = new Vector2(playerTurnPanel.Bounds.Center.X - (labelSize.X / 2), messageTitleLabel.Y);
            playerTurnLabel = new Label(updatedText, position, Fonts.Small, GameState.CurrentPlayerColor);
        }

        public override void Update(GameTime gameTime)
        {
            if (logUpdated)
            {
                foreach (var message in Log)
                    sb.AppendLine(message);
                messageLogLabel.Body = sb.ToString();
            }
            logUpdated = false;
        }

        public override void Draw(SpriteBatch sb)
        {
            GraphicsManager.DrawGameObjectBorder(sb, this, BorderWidth, borderColor);
            playerTurnPanel.Draw(sb);
            messageTitleLabel.Draw(sb);
            messageLogLabel.Draw(sb);
            playerTurnLabel.Draw(sb);
        }

    }
}
