using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace fzzzt_game
{
    public partial class GameForm : Form, GameView
    {
        /// <summary>
        /// the game engine instance
        /// </summary>
        private GameEngine _engine;

        private List<PlayerViewContext> _playerViewContexts;

        /// <summary>
        /// for displaying game informaiton
        /// </summary>
        private MessageLogForm _messageLogForm;

        public GameEngine Engine { get => _engine; set => _engine = value; }
        public List<PlayerViewContext> PlayerViewContexts { get => _playerViewContexts; set => _playerViewContexts = value; }

        /// <summary>
        /// build form without message log form
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
            Engine = new GameEngine();
            Engine.GameView = this;
            PlayerViewContexts = new List<PlayerViewContext>();
        }

        /// <summary>
        /// build form with message log form
        /// </summary>
        /// <param name="messageLogForm"></param>
        public GameForm(MessageLogForm messageLogForm)
        {
            InitializeComponent();

            Engine = new GameEngine();
            Engine.GameView = this;

            PlayerViewContexts = new List<PlayerViewContext>();

            Engine.Players.ForEach(player => BindPlayerContext(player));

            _messageLogForm = messageLogForm;
        }

        /// <summary>
        /// create relacitonship between player and controls
        /// </summary>
        /// <param name="player"></param>
        private void BindPlayerContext(Player player)
        {
            if (player.AtTop())
            {
                PlayerViewContext playerViewContext = new PlayerViewContext
                {
                    Player = player,
                    NameLabel = topPlayerLabel,
                    CardInHandPanel = topCardInHandPanel,
                    MechanicPictureBox = topMechanicPictureBox,
                    CardInBidPanel = topBidPanel,
                    DiscardPilePictureBox = topDiscardPile,
                    ProductionUnitPanel = topProductionUnitPanel
                };
                PlayerViewContexts.Add(playerViewContext);
                return;
            }

            if (player.AtBottom())
            {
                PlayerViewContext playerViewContext = new PlayerViewContext
                {
                    Player = player,
                    NameLabel = bottomPlayerLabel,
                    CardInHandPanel = bottomCardInHandPanel,
                    MechanicPictureBox = bottomMechanicPictureBox,
                    BidButton = bottomBidButton,
                    StartAcutionButton = bottomStartAuction,
                    CardInBidPanel = bottomBidPanel,
                    DiscardPilePictureBox = bottomDiscardPile,
                    ProductionUnitPanel = bottomProductionUnitPanel
                };
                bottomBidButton.Tag = player;
                PlayerViewContexts.Add(playerViewContext);
            }
        }

        /// <summary>
        /// Set up the game when the start button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartGame_Click(object sender, EventArgs e)
        {

            panelTop.Visible = true;
            panelMiddle.Visible = true;
            panelBottom.Visible = true;

            Engine.StartGame();

            pictureBoxConveyorBeltDeck.Image = Properties.Resources.Conveyor_Belt_Deck;

            //DisplayPlayers();
        }

        /// <summary>
        /// display the players, e.g, name, card, .etc.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void DisplayPlayers()
        {
            foreach (Player player in Engine.Players)
            {
                // player at the top
                if (player.AtTop())
                {
                    topPlayerLabel.Text = player.Name;
                    topMechanicPictureBox.Image = player.MechanicFace;
                    topCardInHandPanel.Controls.Clear();
                    topCardInHandPanel.Controls.AddRange(CreateCardsInHandForPlayer(player));
                    continue;
                }

                // player at the bottom
                bottomPlayerLabel.Text = player.Name;
                bottomMechanicPictureBox.Image = player.MechanicFace;
                bottomCardInHandPanel.Controls.Clear();
                bottomCardInHandPanel.Controls.AddRange(CreateCardsInHandForPlayer(player));
            }
        }

        /// <summary>
        /// create cards that in player's hand
        /// </summary>
        /// <param name="player"></param>
        private PictureBox[] CreateCardsInHandForPlayer(Player player)
        {

            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (Card card in player.CardsInHand)
            {
                PictureBox pictureBox = CreateDeafultPictureBox();

                if (player.IsAI)
                {
                    /// AI's cards in hand facing down
                    pictureBox.Image = GameEngine.CardBack;
                }
                else
                {
                    pictureBox.Image = card.GetFace();
                    pictureBox.DoubleClick += new System.EventHandler(this.cardsInHand_DoubleClick);
                }

                pictureBox.Tag = new CardContext(card, player);
                pictureBoxes.Add(pictureBox);
            }
            return pictureBoxes.ToArray();
        }

        /// <summary>
        /// create cards that are chosen to bid
        /// </summary>
        /// <param name="player"></param>
        private PictureBox[] CreateCardsInBidForPlayer(Player player)
        {

            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (Card card in player.CardsInBid)
            {
                PictureBox pictureBox = CreateDeafultPictureBox();
                pictureBox.Image = card.GetFace();
                pictureBox.Tag = new CardContext(card, player);
                pictureBox.DoubleClick += new System.EventHandler(this.cardsInBid_DoubleClick);
                pictureBoxes.Add(pictureBox);
            }
            return pictureBoxes.ToArray();
        }

        /// <summary>
        /// create cards for production units
        /// </summary>
        /// <param name="player"></param>
        private PictureBox[] CreateCardsInProductionUnitForPlayer(Player player)
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (Card card in player.ProductionUnits)
            {
                PictureBox pictureBox = CreateDeafultPictureBox();
                pictureBox.Size = new Size(50, 70);
                pictureBox.Image = card.GetFace();
                pictureBox.Tag = new CardContext(card, player);
                pictureBoxes.Add(pictureBox);
            }
            return pictureBoxes.ToArray();
        }

        /// <summary>
        /// handl double click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardsInHand_DoubleClick(object sender, EventArgs e)
        {
            if (!Engine.AuctionState)
            {
                return;
            }

            PictureBox clickedPictureBox = sender as PictureBox;

            CardContext cardContext = (CardContext)clickedPictureBox.Tag;
            cardContext.BidCard();

            RefreshCardsForPlayer(cardContext.Onwer);
        }

        /// <summary>
        /// double click a card in bid panel to cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardsInBid_DoubleClick(object sender, EventArgs e)
        {
            if (!Engine.AuctionState)
            {
                return;
            }

            PictureBox clickedPictureBox = sender as PictureBox;

            CardContext cardContext = (CardContext)clickedPictureBox.Tag;
            cardContext.CanelBidCard();
            RefreshCardsForPlayer(cardContext.Onwer);
        }

        /// <summary>
        /// refresh cards for player,
        /// cards in hand, cards in bid, cards in discard pile, cards in production units
        /// </summary>
        /// <param name="player"></param>
        private void RefreshCardsForPlayer(Player player)
        {
            UpdateMessage(player.ToString());
            PictureBox[] cardsInHand = CreateCardsInHandForPlayer(player);
            PictureBox[] cardsInBid = CreateCardsInBidForPlayer(player);
            PictureBox[] cardsInProductionUnits = CreateCardsInProductionUnitForPlayer(player);

            Panel cardsInHandPanel = GetCardInHandPanel(player);
            cardsInHandPanel.Controls.Clear();
            cardsInHandPanel.Controls.AddRange(cardsInHand);

            Panel cardsInBidPanel = GetCardBidPanel(player);
            cardsInBidPanel.Controls.Clear();
            cardsInBidPanel.Controls.AddRange(cardsInBid);


            PictureBox discardPile = GetDiscardPilePictureBox(player);
            if (player.DiscardPile.Count > 0)
            {
                discardPile.Image = GameEngine.CardBack;
            }

            Panel productionUnitPanel = GetProductionUnitPanel(player);
            productionUnitPanel.Controls.Clear();
            productionUnitPanel.Controls.AddRange(cardsInProductionUnits);
        }

        /// <summary>
        /// get the panel to display the cards in hand for the player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private FlowLayoutPanel GetCardInHandPanel(Player player)
        {
            if (player.AtTop())
            {
                return topCardInHandPanel;
            }
            return bottomCardInHandPanel;
        }

        /// <summary>
        /// get the panel to display the cards to bid for the player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private FlowLayoutPanel GetCardBidPanel(Player player)
        {
            if (player.AtTop())
            {
                return topBidPanel;
            }
            return bottomBidPanel;
        }

        /// <summary>
        /// get the panel to display production units
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private FlowLayoutPanel GetProductionUnitPanel(Player player)
        {
            if (player.AtTop())
            {
                return topProductionUnitPanel;
            }
            return bottomProductionUnitPanel;
        }

        /// <summary>
        /// get the picture box to display discard pile 
        /// </summary>
        /// <param name="player"></param>
        private PictureBox GetDiscardPilePictureBox(Player player)
        {
            if (player.AtTop())
            {
                return topDiscardPile;
            }
            return bottomDiscardPile;
        }

        /// <summary>
        /// handle the click event of a card in hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardsInHand_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox == null)
            {
                return;
            }

            if (clickedPictureBox.Image == GameEngine.CardBack)
            {
                clickedPictureBox.Image = (Image)clickedPictureBox.Tag;
                clickedPictureBox.Tag = GameEngine.CardBack;
                return;
            }

            Image faceImage = clickedPictureBox.Image;
            clickedPictureBox.Image = GameEngine.CardBack;
            clickedPictureBox.Tag = faceImage;
        }

        /// <summary>
        /// handle the click event of a card on the conveyor belt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxOnConveyorBelt_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox == null)
            {
                return;
            }

            // if no card is faced up and clickedCard is not the first card, do nothing
            Card clickedCard = ((Card)clickedPictureBox.Tag);
            if (Engine.AllowedFacedUpCardCount == 0)
            {
                if (!Engine.IsFirstCardOnConveyorBelt(clickedCard))
                {
                    return;
                }
                clickedPictureBox.Image = clickedCard.GetFace();
                clickedCard.Flip();
                Engine.UpdateAllowedFacedUpCardCount();
                RefreshConveyorBelt();
                return;
            }

            // if there are cards facing up, the first card should be faced down
            if (Engine.IsFirstCardOnConveyorBelt(clickedCard) && Engine.GetFacedUpCards().Count > 1)
            {
                return;
            }

            // after the first card is turned up
            // if face-up cards is less than or equal to the allowed count, then turn up the card
            if (GameEngine.IsCardBack(clickedPictureBox.Image))
            {
                if (Engine.FacingUpAllowed())
                {
                    clickedCard.Flip();
                    clickedPictureBox.Image = clickedCard.GetFace();
                    Engine.UpdateAllowedFacedUpCardCount();
                    return;
                }
                return;
            }

            clickedPictureBox.Image = GameEngine.CardBack;
            clickedCard.Flip();
            Engine.UpdateAllowedFacedUpCardCount();
        }

        /// <summary>
        /// Updates the UI to reflect the selected chief mechanic.
        /// </summary>
        public void EnpowerChiefMechanic()
        {
            Player chiefMechanic = Engine.ChiefMechanic;
            if (chiefMechanic.AtTop())
            {
                labelChiefMechanicTop.Visible = true;
                return;
            }
            if (chiefMechanic.AtBottom())
            {
                labelChiefMechanicBottom.Visible = true;
            }
        }

        /// <summary>
        /// refreash the conveyor belt
        /// </summary>
        public void RefreshConveyorBelt()
        {
            conveyorBeltPanel.Controls.Clear();
            // the first card is the furthest away from the conveyor belt deck
            List<Card> cards = Engine.CardsInConveyorBelt;
            for (int i = 0; i < cards.Count; i++)
            {
                Card card = cards[i];
                UpdateMessage(card.ToString());
                PictureBox pictureBox = CreateDeafultPictureBox();
                pictureBox.Image = card.CurrentState == CardState.FaceDown ? GameEngine.CardBack : card.GetFace();
                pictureBox.Tag = card;
                pictureBox.Click += new System.EventHandler(this.pictureBoxOnConveyorBelt_Click);

                conveyorBeltPanel.Controls.Add(pictureBox);
            }
        }

        /// <summary>
        /// default picture box
        /// </summary>
        /// <returns></returns>
        private PictureBox CreateDeafultPictureBox()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(100, 140);
            pictureBox.Margin = new Padding(0);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            return pictureBox;
        }

        /// <summary>
        /// Handles the click event for the "Start Auction Bottom" button.
        /// </summary>
        private void buttonStartAuctionBottom_Click(object sender, EventArgs e)
        {
            Engine.StartAuction();
        }

        /// <summary>
        /// update message in the textbox
        /// </summary>
        public void UpdateMessage(string message)
        {
            _messageLogForm.UpdateMessage(message);
        }

        /// <summary>
        /// face up a card based on its index
        /// </summary>
        /// <param name="index"></param>
        private void FaceUpCardOnConveyorBelt(int index)
        {
            PictureBox pictureBox = (PictureBox)conveyorBeltPanel.Controls[index];
            Card card = (Card)pictureBox.Tag;
            pictureBox.Image = card.GetFace();
            card.Flip();
            Engine.AddFacedUpCard(card);
        }

        /// <summary>
        /// bid for AI player
        /// </summary>
        public void AIBid(CardContext cardContext)
        {
            cardContext.BidCard();
            RefreshCardsForPlayer(cardContext.Onwer);
        }

        private void bottomBidButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }
            Player player = button.Tag as Player;
            player.IsBid = true;

            Engine.AwardCard();
        }

        /// <summary>
        /// display bid button for human player
        /// </summary>
        /// <param name="cardContext"></param>
        public void DisplayBidButton()
        {
            bottomBidButton.Visible = true;
        }

        /// <summary>
        /// print game state for debugging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printGameStateButton_Click(object sender, EventArgs e)
        {
            Engine.PrintGameState();
        }

        /// <summary>
        /// refresh cards for players
        /// </summary>
        public void RefreshCardsForPlayers()
        {
            Engine.Players.ForEach(player => RefreshCardsForPlayer(player));
        }

        /// <summary>
        /// refrash UI
        /// </summary>
        public void RefreshUI()
        {
            PlayerViewContexts.ForEach(context =>
            {
                context.NameLabel.Text = context.Player.Name;

                context.MechanicPictureBox.Image = context.Player.MechanicFace;

                context.CardInHandPanel.Visible = true;
                context.CardInHandPanel.Controls.Clear();
                context.CardInHandPanel.Controls.AddRange(CreateCardsInHandForPlayer(context.Player));

                context.CardInBidPanel.Visible = true;
                context.CardInBidPanel.Controls.Clear();
                context.CardInBidPanel.Controls.AddRange(CreateCardsInBidForPlayer(context.Player));

                context.ProductionUnitPanel.Visible = true;
                context.ProductionUnitPanel.Controls.Clear();
                context.ProductionUnitPanel.Controls.AddRange(CreateCardsInProductionUnitForPlayer(context.Player));

                if (context.BidButton != null)
                {
                    context.BidButton.Visible = true;
                }

                // only human player has Start Auction button
                if (context.StartAcutionButton != null)
                {
                    if (!Engine.AuctionState)
                    {
                        context.StartAcutionButton.Visible = context.Player.IsChiefMechanic;
                    }
                    else
                    {
                        context.StartAcutionButton.Visible = false;
                    }
                }

                if (context.Player.DiscardPile.Count > 0)
                {
                    context.DiscardPilePictureBox.Image = GameEngine.CardBack;
                }
                else
                {
                    context.DiscardPilePictureBox.Image = null;
                }
            });

            RefreshConveyorBelt();
        }

        /// <summary>
        /// open a window to build production units
        /// </summary>
        public void StartBuildingWigets()
        {
            new WidgetForm().ShowDialog();
        }
    }
}
