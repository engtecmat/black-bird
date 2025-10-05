using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Linq;
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
        private LogForm _messageLogForm;

        /// <summary>
        /// a form to build widgets
        /// </summary>
        private WidgetForm _widgetForm = new WidgetForm();

        public GameEngine Engine { get => _engine; set => _engine = value; }
        public List<PlayerViewContext> PlayerViewContexts { get => _playerViewContexts; set => _playerViewContexts = value; }
        public WidgetForm WidgetForm { get => _widgetForm; set => _widgetForm = value; }
        public LogForm MessageLogForm { get => _messageLogForm; set => _messageLogForm = value; }

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
        public GameForm(LogForm messageLogForm)
        {
            InitializeComponent();

            Engine = new GameEngine();
            Engine.GameView = this;

            PlayerViewContexts = new List<PlayerViewContext>();

            Engine.Players.ForEach(player => BindPlayerContext(player));

            MessageLogForm = messageLogForm;
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
                    ProductionUnitPanel = topProductionUnitPanel,
                    WidgetProductionUnitPanel = new FlowLayoutPanel(),
                    WidgetRobotPanel = WidgetForm.TopRobotCardPanel
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
                    ProductionUnitPanel = bottomProductionUnitPanel,
                    WidgetProductionUnitPanel = new FlowLayoutPanel(),
                    WidgetRobotPanel = WidgetForm.BottomRobotCardPanel
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
                    pictureBox.Image = card.Face;
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
                pictureBox.Image = card.CurrentState == CardState.FaceUp ? card.Face: GameEngine.CardBack;
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
                pictureBox.Image = card.Face;
                pictureBox.Tag = new CardContext(card, player);
                pictureBoxes.Add(pictureBox);
            }
            return pictureBoxes.ToArray();
        }



        /// <summary>
        /// create widget production unit
        /// </summary>
        /// <param name="player"></param>
        private PictureBox[] CreateWdigetProductionUnitsForPlayer(Player player)
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (Widget widget in player.Widgets)
            {
                PictureBox pictureBox = CreateDeafultPictureBox();
                pictureBox.Size = new Size(50, 70);
                pictureBox.Image = widget.ProductionUnit.Face;
                pictureBox.Tag = new CardContext(widget.ProductionUnit, player);
                pictureBoxes.Add(pictureBox);
            }
            return pictureBoxes.ToArray();
        }

        /// <summary>
        /// create robot cards for building widgets
        /// </summary>
        /// <param name="player"></param>
        private PictureBox[] CreateRobotCardsForPlayer(Player player)
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (Card card in player.CardsInHand.Concat(player.DiscardPile))
            {
                PictureBox pictureBox = CreateDeafultPictureBox();
                pictureBox.Size = new Size(50, 70);
                pictureBox.Image = card.Face;
                pictureBox.Tag = new CardContext(card, player);
                pictureBoxes.Add(pictureBox);
                pictureBox.Click += new System.EventHandler(this.HandleAddingRobotCard_Click);
            }
            return pictureBoxes.ToArray();
        }

        /// <summary>
        /// add a robot card to the widget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleAddingRobotCard_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox == null)
            {
                return;
            }
            CardContext cardContext = (CardContext)clickedPictureBox.Tag;

            if (cardContext.Owner.CurrentBuildingWidget == null)
            {
                MessageBox.Show("Please select a production unit to build first.");
                return;
            }

            cardContext.AddCardToWidget();
            RefreshWidgetFrom();
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

            RefreshCardsForPlayer(cardContext.Owner);
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
            RefreshCardsForPlayer(cardContext.Owner);
        }

        /// <summary>
        /// refresh cards for player,
        /// cards in hand, cards in bid, cards in discard pile, cards in production units
        /// </summary>
        /// <param name="player"></param>
        private void RefreshCardsForPlayer(Player player)
        {
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
                clickedPictureBox.Image = clickedCard.Face;
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
                    clickedPictureBox.Image = clickedCard.Face;
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
                PictureBox pictureBox = CreateDeafultPictureBox();
                pictureBox.Image = card.CurrentState == CardState.FaceDown ? GameEngine.CardBack : card.Face;
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
        /// add message to the message log form
        /// </summary>
        public void Log(string message)
        {
            MessageLogForm.AddMessage(message);
        }

        /// <summary>
        /// face up a card based on its index
        /// </summary>
        /// <param name="index"></param>
        private void FaceUpCardOnConveyorBelt(int index)
        {
            PictureBox pictureBox = (PictureBox)conveyorBeltPanel.Controls[index];
            Card card = (Card)pictureBox.Tag;
            pictureBox.Image = card.Face;
            card.Flip();
            Engine.AddFacedUpCard(card);
        }

        /// <summary>
        /// bid for AI player
        /// </summary>
        public void AIBid(CardContext cardContext)
        {
            cardContext.BidCard();
            RefreshCardsForPlayer(cardContext.Owner);
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
            RefreshWidgetFrom();
            WidgetForm.ShowDialog();
        }

        /// <summary>
        /// referesh the widget form
        /// </summary>
        private void RefreshWidgetFrom()
        {
            WidgetForm.Controls.Clear();
            WidgetForm.Controls.Add(WidgetForm.ConfirmBuildingButton);
            const int bottomY = 853;
            PlayerViewContexts.ForEach(context =>
            {
                if (context.Player.AtTop())
                {
                    for (int i = 0; i < context.Player.Widgets.Count; i++)
                    {
                        FlowLayoutPanel flowLayoutPanel = CreatePanelForWidget(context.Player.Widgets[i], 94 + 70 * i);

                        WidgetForm.Controls.Add(flowLayoutPanel);
                    }
                }
                else
                {
                    for (int i = 0; i < context.Player.Widgets.Count; i++)
                    {
                        FlowLayoutPanel flowLayoutPanel = CreatePanelForWidget(context.Player.Widgets[i], bottomY - 70 * (i + 1));
                        WidgetForm.Controls.Add(flowLayoutPanel);
                    }
                }
                context.WidgetRobotPanel.Controls.Clear();
                context.WidgetRobotPanel.Controls.AddRange(CreateRobotCardsForPlayer(context.Player));

                WidgetForm.Controls.Add(context.WidgetRobotPanel);

            });
        }

        /// <summary>
        /// Create a panel for each widget for the player
        /// </summary>
        private FlowLayoutPanel CreatePanelForWidget(Widget widget, int y)
        {
            FlowLayoutPanel flowLayoutPanel = CreatePanelForBuidlingPorductionUnit();
            flowLayoutPanel.Location = new Point(9, y);
            flowLayoutPanel.Size = new Size(1402, 70);
            PictureBox productionUnit = CreateDeafultPictureBox();
            productionUnit.Size = new Size(50, 70);
            productionUnit.Image = widget.ProductionUnit.Face;
            productionUnit.Tag = widget;
            productionUnit.Margin = new Padding(0, 0, 5, 0);
            productionUnit.Cursor = Cursors.Hand;
            productionUnit.Click += new System.EventHandler(this.HandleWidgetProductionUnit_Click);

            if (widget == widget.Owner.CurrentBuildingWidget)
            {
                productionUnit.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                productionUnit.BorderStyle = BorderStyle.None;
            }

            flowLayoutPanel.Controls.Add(productionUnit);

            foreach (Card card in widget.RobotCards)
            {
                PictureBox robotCard = CreateDeafultPictureBox();
                robotCard.Size = new Size(50, 70);
                robotCard.Image = card.Face;
                robotCard.Tag = widget;
                //robotCard.Click += new System.EventHandler(this.HandleAddingRobotCard_Click());

                flowLayoutPanel.Controls.Add(robotCard);
            }

            return flowLayoutPanel;
        }


        /// <summary>
        /// handle the click event of a production unit in widget form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleWidgetProductionUnit_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox == null)
            {
                return;
            }
            Widget widget = (Widget)clickedPictureBox.Tag;
            widget.Owner.CurrentBuildingWidget = widget;
            RefreshWidgetFrom();
        }

        /// <summary>
        /// create a panel for each production unit
        /// </summary>
        /// <returns></returns>
        private static FlowLayoutPanel CreatePanelForBuidlingPorductionUnit()
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Size = new Size(50, 70);
            flowLayoutPanel.Margin = new Padding(0);
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            return flowLayoutPanel;
        }
    }
}
