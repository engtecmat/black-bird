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

        /// <summary>
        /// for displaying game informaiton
        /// </summary>
        private MessageLogForm _messageLogForm;

        /// <summary>
        /// build form without message log form
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
            _engine = new GameEngine(this);
        }

        /// <summary>
        /// build form with message log form
        /// </summary>
        /// <param name="messageLogForm"></param>
        public GameForm(MessageLogForm messageLogForm)
        {
            InitializeComponent();
            _engine = new GameEngine(this);
            _messageLogForm = messageLogForm;
        }

        /// <summary>
        /// Set up the game when the start button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            if (_engine.IsGameStarted())
            {
                return;
            }

            panelTop.Visible = true;
            panelMiddle.Visible = true;
            panelBottom.Visible = true;

            buttonStartGame.Enabled = false;

            _engine.StartGame();

            pictureBoxConveyorBeltDeck.Image = Properties.Resources.Conveyor_Belt_Deck;

            DisplayPlayers();
        }

        /// <summary>
        /// display the players, e.g, name, card, .etc.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void DisplayPlayers()
        {
            foreach (Player player in _engine.GetPlayers())
            {
                // player at the top
                if (player.AtTop())
                {
                    labelPlayerTop.Text = player.GetName();
                    pictureBoxPlayerTopMechanicFace.Image = player.GetMechanicFace();
                    topCardInHandPanel.Controls.Clear();
                    topCardInHandPanel.Controls.AddRange(CreateCardsInHandForPlayer(player));
                    continue;
                }

                // player at the bottom
                labelPlayerBottom.Text = player.GetName();
                pictureBoxPlayerBottomMechanicFace.Image = player.GetMechanicFace();
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
            foreach (Card card in player.GetCardsInHand())
            {
                PictureBox pictureBox = CreateDeafultPictureBox();

                if (player.IsAI())
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
            foreach (Card card in player.GetCardsInBid())
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
        /// handl double click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardsInHand_DoubleClick(object sender, EventArgs e)
        {
            if (!_engine.IsAuctionStarted())
            {
                return;
            }

            PictureBox clickedPictureBox = sender as PictureBox;

            CardContext cardContext = (CardContext)clickedPictureBox.Tag;
            cardContext.BidCard();

            DisplayCardsForPlayer(cardContext.Onwer);
        }

        /// <summary>
        /// double click a card in bid panel to cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardsInBid_DoubleClick(object sender, EventArgs e)
        {
            if (!_engine.IsAuctionStarted())
            {
                return;
            }

            PictureBox clickedPictureBox = sender as PictureBox;

            CardContext cardContext = (CardContext)clickedPictureBox.Tag;
            cardContext.CanelBidCard();
            DisplayCardsForPlayer(cardContext.Onwer);
        }

        /// <summary>
        /// display cards for player
        /// </summary>
        /// <param name="player"></param>
        private void DisplayCardsForPlayer(Player player)
        {
            PictureBox[] cardsInHand = CreateCardsInHandForPlayer(player);
            PictureBox[] cardsInBid = CreateCardsInBidForPlayer(player);

            Panel cardsInHandPanel = GetCardInHandPanel(player);
            cardsInHandPanel.Controls.Clear();
            cardsInHandPanel.Controls.AddRange(cardsInHand);

            Panel cardsInBidPanel = GetCardBidPanel(player);
            cardsInBidPanel.Controls.Clear();
            cardsInBidPanel.Controls.AddRange(cardsInBid);
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
            if (_engine.GetAllowedFacedUpCardCount() == 0)
            {
                if (!_engine.IsFirstCardOnConveyorBelt(clickedCard))
                {
                    return;
                }
                clickedPictureBox.Image = clickedCard.GetFace();
                clickedCard.Flip();
                _engine.AddFacedUpCard(clickedCard);
                return;
            }

            // if there are cards facing up, the first card should be faced down
            if ((_engine.IsFirstCardOnConveyorBelt(clickedCard) && _engine.GetFacedUpCards().Count > 1))
            {
                return;
            }

            // after the first card is turned up
            // if face-up cards is less than or equal to the allowed count, then turn up the card
            if (GameEngine.IsCardBack(clickedPictureBox.Image))
            {
                if (_engine.FacingUpAllowed())
                {
                    _engine.AddFacedUpCard(clickedCard);
                    clickedCard.Flip();
                    clickedPictureBox.Image = clickedCard.GetFace();
                    return;
                }
                return;
            }

            clickedPictureBox.Image = GameEngine.CardBack;
            _engine.RemoveFacedUpCard(clickedCard);
            clickedCard.Flip();
            _engine.UpdateAllowedFacedUpCardCount();
        }

        /// <summary>
        /// Updates the UI to reflect the selected chief mechanic.
        /// </summary>
        public void EnpowerChiefMechanic()
        {
            Player chiefMechanic = _engine.GetChiefMechanic();
            if (chiefMechanic.AtTop())
            {
                labelChiefMechanicTop.Visible = true;
                topStartAuction.Visible = true;
                return;
            }
            if (chiefMechanic.AtBottom())
            {
                labelChiefMechanicBottom.Visible = true;
                bottomStartAuction.Visible = true;
            }
        }

        /// <summary>
        /// Reset the visibility of elements and re-enable the Start Game button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            _engine.ResetGame();
        }

        /// <summary>
        /// Handles the click event for the "Start Auction Top" button.
        /// </summary>
        private void buttonStartAuctionTop_Click(object sender, EventArgs e)
        {
            _engine.StartAuction();
        }

        public void PrepareConveyorBelt()
        {
            // the first card is the furthest away from the conveyor belt deck
            List<Card> cards = _engine.GetAuctionCards();
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                Card card = cards[i];
                PictureBox pictureBox = CreateDeafultPictureBox();
                pictureBox.Image = GameEngine.CardBack;
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
            _engine.StartAuction();
        }

        /// <summary>
        /// reset UI to its initial state
        /// </summary>
        public void Reset()
        {
            panelTop.Visible = false;
            panelMiddle.Visible = false;
            panelBottom.Visible = false;

            labelChiefMechanicTop.Visible = false;
            labelChiefMechanicBottom.Visible = false;

            bottomStartAuction.Visible = false;
            topStartAuction.Visible = false;

            buttonStartGame.Enabled = true;

            topCardInHandPanel.Controls.Clear();
            conveyorBeltPanel.Controls.Clear();
            bottomCardInHandPanel.Controls.Clear();

            topBidPanel.Controls.Clear();
            bottomBidPanel.Controls.Clear();
        }

        /// <summary>
        /// update message in the textbox
        /// </summary>
        public void UpdateMessag(string message)
        {
            _messageLogForm.UpdateMessage(message);
        }

        /// <summary>
        /// disable start auction buttions
        /// </summary>
        public void HideStartAuctionButtons()
        {
            topStartAuction.Visible = false;
            bottomStartAuction.Visible = false;
        }

        /// <summary>
        /// this is for AI player to face up cards
        /// </summary>
        public void FlipCards()
        {
            int count = conveyorBeltPanel.Controls.Count;
            UpdateMessag(count + " cards on the conveyor belt.");
            if (count == 0)
            {
                return;
            }

            FaceUpCardOnConveyorBelt(conveyorBeltPanel.Controls.Count - 1);

            Random random = new Random();
            ISet<int> indcies = new HashSet<int>();

            /// get indcies based on the conveyor belt number
            while(indcies.Count < _engine.GetAllowedFacedUpCardCount() - 1)
            {
                indcies.Add(random.Next(0, Math.Min(_engine.GetAllowedFacedUpCardCount(), count - 1)));
            }

            // face up the cards
            foreach(int index in indcies)
            {
                FaceUpCardOnConveyorBelt(index);
            }
        }

        /// <summary>
        /// face up a card based on its index
        /// </summary>
        /// <param name="index"></param>
        private void FaceUpCardOnConveyorBelt(int index)
        {
            PictureBox pictureBox = (PictureBox)conveyorBeltPanel.Controls[index];
            Card firstCard = (Card)pictureBox.Tag;
            pictureBox.Image = firstCard.GetFace();
            _engine.AddFacedUpCard(firstCard);
        }

        /// <summary>
        /// bid for AI player
        /// </summary>
        public void Bid(CardContext cardContext)
        {
            cardContext.BidCard();
            DisplayCardsForPlayer(cardContext.Onwer);
        }

        private void bottomBidButton_Click(object sender, EventArgs e)
        {
            _engine.Bid();
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
        /// update UI after bidding
        /// </summary>
        public void UpdateUIAfterBidding(List<Player> players)
        {
            
        }

        /// <summary>
        /// print game state for debugging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printGameStateButton_Click(object sender, EventArgs e)
        {
            _engine.PrintGameState();
        }
    }
}
