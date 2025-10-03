namespace fzzzt_game
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxConveyorBeltDeck = new System.Windows.Forms.PictureBox();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.bottomMechanicPictureBox = new System.Windows.Forms.PictureBox();
            this.bottomDiscardPile = new System.Windows.Forms.PictureBox();
            this.bottomPlayerLabel = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.bottomPlayerOperationPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bottomBidButton = new System.Windows.Forms.Button();
            this.bottomProductionUnitPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bottomBidPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bottomStartAuction = new System.Windows.Forms.Button();
            this.labelChiefMechanicBottom = new System.Windows.Forms.Label();
            this.bottomCardInHandPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelOperations = new System.Windows.Forms.Panel();
            this.printGameStateButton = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.topProductionUnitPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.topBidPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.labelChiefMechanicTop = new System.Windows.Forms.Label();
            this.topPlayerLabel = new System.Windows.Forms.Label();
            this.topDiscardPile = new System.Windows.Forms.PictureBox();
            this.topMechanicPictureBox = new System.Windows.Forms.PictureBox();
            this.topCardInHandPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.conveyorBeltPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConveyorBeltDeck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomMechanicPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomDiscardPile)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.bottomPlayerOperationPanel.SuspendLayout();
            this.panelOperations.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topDiscardPile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topMechanicPictureBox)).BeginInit();
            this.panelMiddle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxConveyorBeltDeck
            // 
            this.pictureBoxConveyorBeltDeck.Location = new System.Drawing.Point(109, 21);
            this.pictureBoxConveyorBeltDeck.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxConveyorBeltDeck.Name = "pictureBoxConveyorBeltDeck";
            this.pictureBoxConveyorBeltDeck.Size = new System.Drawing.Size(100, 140);
            this.pictureBoxConveyorBeltDeck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxConveyorBeltDeck.TabIndex = 0;
            this.pictureBoxConveyorBeltDeck.TabStop = false;
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.AutoSize = true;
            this.buttonStartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartGame.Location = new System.Drawing.Point(28, 425);
            this.buttonStartGame.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(100, 60);
            this.buttonStartGame.TabIndex = 1;
            this.buttonStartGame.Text = "Start";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // bottomMechanicPictureBox
            // 
            this.bottomMechanicPictureBox.Location = new System.Drawing.Point(352, 151);
            this.bottomMechanicPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.bottomMechanicPictureBox.Name = "bottomMechanicPictureBox";
            this.bottomMechanicPictureBox.Size = new System.Drawing.Size(100, 140);
            this.bottomMechanicPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bottomMechanicPictureBox.TabIndex = 22;
            this.bottomMechanicPictureBox.TabStop = false;
            // 
            // bottomDiscardPile
            // 
            this.bottomDiscardPile.Location = new System.Drawing.Point(1167, 318);
            this.bottomDiscardPile.Margin = new System.Windows.Forms.Padding(2);
            this.bottomDiscardPile.Name = "bottomDiscardPile";
            this.bottomDiscardPile.Size = new System.Drawing.Size(50, 70);
            this.bottomDiscardPile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bottomDiscardPile.TabIndex = 21;
            this.bottomDiscardPile.TabStop = false;
            // 
            // bottomPlayerLabel
            // 
            this.bottomPlayerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomPlayerLabel.Location = new System.Drawing.Point(553, 337);
            this.bottomPlayerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.bottomPlayerLabel.Name = "bottomPlayerLabel";
            this.bottomPlayerLabel.Size = new System.Drawing.Size(76, 37);
            this.bottomPlayerLabel.TabIndex = 20;
            this.bottomPlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.bottomPlayerOperationPanel);
            this.panelBottom.Controls.Add(this.bottomProductionUnitPanel);
            this.panelBottom.Controls.Add(this.bottomBidPanel);
            this.panelBottom.Controls.Add(this.bottomStartAuction);
            this.panelBottom.Controls.Add(this.labelChiefMechanicBottom);
            this.panelBottom.Controls.Add(this.bottomPlayerLabel);
            this.panelBottom.Controls.Add(this.bottomDiscardPile);
            this.panelBottom.Controls.Add(this.bottomMechanicPictureBox);
            this.panelBottom.Controls.Add(this.bottomCardInHandPanel);
            this.panelBottom.Location = new System.Drawing.Point(169, 586);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(2);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1253, 400);
            this.panelBottom.TabIndex = 26;
            this.panelBottom.Visible = false;
            // 
            // bottomPlayerOperationPanel
            // 
            this.bottomPlayerOperationPanel.Controls.Add(this.bottomBidButton);
            this.bottomPlayerOperationPanel.Location = new System.Drawing.Point(775, 338);
            this.bottomPlayerOperationPanel.Margin = new System.Windows.Forms.Padding(0);
            this.bottomPlayerOperationPanel.Name = "bottomPlayerOperationPanel";
            this.bottomPlayerOperationPanel.Size = new System.Drawing.Size(116, 37);
            this.bottomPlayerOperationPanel.TabIndex = 34;
            // 
            // bottomBidButton
            // 
            this.bottomBidButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bottomBidButton.Location = new System.Drawing.Point(0, 0);
            this.bottomBidButton.Margin = new System.Windows.Forms.Padding(0);
            this.bottomBidButton.Name = "bottomBidButton";
            this.bottomBidButton.Size = new System.Drawing.Size(86, 36);
            this.bottomBidButton.TabIndex = 31;
            this.bottomBidButton.Text = "Bid";
            this.bottomBidButton.UseVisualStyleBackColor = true;
            this.bottomBidButton.Visible = false;
            this.bottomBidButton.Click += new System.EventHandler(this.bottomBidButton_Click);
            // 
            // bottomProductionUnitPanel
            // 
            this.bottomProductionUnitPanel.Location = new System.Drawing.Point(29, 318);
            this.bottomProductionUnitPanel.Margin = new System.Windows.Forms.Padding(0);
            this.bottomProductionUnitPanel.Name = "bottomProductionUnitPanel";
            this.bottomProductionUnitPanel.Size = new System.Drawing.Size(500, 70);
            this.bottomProductionUnitPanel.TabIndex = 33;
            // 
            // bottomBidPanel
            // 
            this.bottomBidPanel.Location = new System.Drawing.Point(454, 6);
            this.bottomBidPanel.Margin = new System.Windows.Forms.Padding(0);
            this.bottomBidPanel.Name = "bottomBidPanel";
            this.bottomBidPanel.Size = new System.Drawing.Size(796, 140);
            this.bottomBidPanel.TabIndex = 30;
            // 
            // bottomStartAuction
            // 
            this.bottomStartAuction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bottomStartAuction.Location = new System.Drawing.Point(633, 338);
            this.bottomStartAuction.Margin = new System.Windows.Forms.Padding(2);
            this.bottomStartAuction.Name = "bottomStartAuction";
            this.bottomStartAuction.Size = new System.Drawing.Size(86, 36);
            this.bottomStartAuction.TabIndex = 29;
            this.bottomStartAuction.Text = "Start Auction";
            this.bottomStartAuction.UseVisualStyleBackColor = true;
            this.bottomStartAuction.Click += new System.EventHandler(this.buttonStartAuctionBottom_Click);
            // 
            // labelChiefMechanicBottom
            // 
            this.labelChiefMechanicBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelChiefMechanicBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChiefMechanicBottom.Location = new System.Drawing.Point(730, 337);
            this.labelChiefMechanicBottom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelChiefMechanicBottom.Name = "labelChiefMechanicBottom";
            this.labelChiefMechanicBottom.Size = new System.Drawing.Size(26, 37);
            this.labelChiefMechanicBottom.TabIndex = 26;
            this.labelChiefMechanicBottom.Text = "CM";
            this.labelChiefMechanicBottom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelChiefMechanicBottom.Visible = false;
            // 
            // bottomCardInHandPanel
            // 
            this.bottomCardInHandPanel.Location = new System.Drawing.Point(454, 151);
            this.bottomCardInHandPanel.Margin = new System.Windows.Forms.Padding(0);
            this.bottomCardInHandPanel.Name = "bottomCardInHandPanel";
            this.bottomCardInHandPanel.Size = new System.Drawing.Size(796, 140);
            this.bottomCardInHandPanel.TabIndex = 30;
            // 
            // panelOperations
            // 
            this.panelOperations.Controls.Add(this.printGameStateButton);
            this.panelOperations.Controls.Add(this.buttonStartGame);
            this.panelOperations.Location = new System.Drawing.Point(0, 0);
            this.panelOperations.Margin = new System.Windows.Forms.Padding(2);
            this.panelOperations.Name = "panelOperations";
            this.panelOperations.Size = new System.Drawing.Size(165, 983);
            this.panelOperations.TabIndex = 27;
            // 
            // printGameStateButton
            // 
            this.printGameStateButton.AutoSize = true;
            this.printGameStateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printGameStateButton.Location = new System.Drawing.Point(28, 505);
            this.printGameStateButton.Margin = new System.Windows.Forms.Padding(2);
            this.printGameStateButton.Name = "printGameStateButton";
            this.printGameStateButton.Size = new System.Drawing.Size(100, 60);
            this.printGameStateButton.TabIndex = 3;
            this.printGameStateButton.Text = "Print";
            this.printGameStateButton.UseVisualStyleBackColor = true;
            this.printGameStateButton.Click += new System.EventHandler(this.printGameStateButton_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.topProductionUnitPanel);
            this.panelTop.Controls.Add(this.topBidPanel);
            this.panelTop.Controls.Add(this.labelChiefMechanicTop);
            this.panelTop.Controls.Add(this.topPlayerLabel);
            this.panelTop.Controls.Add(this.topDiscardPile);
            this.panelTop.Controls.Add(this.topMechanicPictureBox);
            this.panelTop.Controls.Add(this.topCardInHandPanel);
            this.panelTop.Location = new System.Drawing.Point(169, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(2);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1253, 400);
            this.panelTop.TabIndex = 27;
            this.panelTop.Visible = false;
            // 
            // topProductionUnitPanel
            // 
            this.topProductionUnitPanel.Location = new System.Drawing.Point(29, 9);
            this.topProductionUnitPanel.Margin = new System.Windows.Forms.Padding(0);
            this.topProductionUnitPanel.Name = "topProductionUnitPanel";
            this.topProductionUnitPanel.Size = new System.Drawing.Size(500, 70);
            this.topProductionUnitPanel.TabIndex = 32;
            // 
            // topBidPanel
            // 
            this.topBidPanel.Location = new System.Drawing.Point(457, 230);
            this.topBidPanel.Margin = new System.Windows.Forms.Padding(0);
            this.topBidPanel.Name = "topBidPanel";
            this.topBidPanel.Size = new System.Drawing.Size(796, 140);
            this.topBidPanel.TabIndex = 31;
            // 
            // labelChiefMechanicTop
            // 
            this.labelChiefMechanicTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelChiefMechanicTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChiefMechanicTop.Location = new System.Drawing.Point(730, 16);
            this.labelChiefMechanicTop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelChiefMechanicTop.Name = "labelChiefMechanicTop";
            this.labelChiefMechanicTop.Size = new System.Drawing.Size(26, 37);
            this.labelChiefMechanicTop.TabIndex = 27;
            this.labelChiefMechanicTop.Text = "CM";
            this.labelChiefMechanicTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelChiefMechanicTop.Visible = false;
            // 
            // topPlayerLabel
            // 
            this.topPlayerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topPlayerLabel.Location = new System.Drawing.Point(553, 17);
            this.topPlayerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.topPlayerLabel.Name = "topPlayerLabel";
            this.topPlayerLabel.Size = new System.Drawing.Size(76, 37);
            this.topPlayerLabel.TabIndex = 20;
            this.topPlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // topDiscardPile
            // 
            this.topDiscardPile.Location = new System.Drawing.Point(1167, 7);
            this.topDiscardPile.Margin = new System.Windows.Forms.Padding(2);
            this.topDiscardPile.Name = "topDiscardPile";
            this.topDiscardPile.Size = new System.Drawing.Size(50, 70);
            this.topDiscardPile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.topDiscardPile.TabIndex = 21;
            this.topDiscardPile.TabStop = false;
            // 
            // topMechanicPictureBox
            // 
            this.topMechanicPictureBox.Location = new System.Drawing.Point(352, 84);
            this.topMechanicPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.topMechanicPictureBox.Name = "topMechanicPictureBox";
            this.topMechanicPictureBox.Size = new System.Drawing.Size(100, 140);
            this.topMechanicPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.topMechanicPictureBox.TabIndex = 22;
            this.topMechanicPictureBox.TabStop = false;
            // 
            // topCardInHandPanel
            // 
            this.topCardInHandPanel.Location = new System.Drawing.Point(457, 84);
            this.topCardInHandPanel.Name = "topCardInHandPanel";
            this.topCardInHandPanel.Size = new System.Drawing.Size(793, 140);
            this.topCardInHandPanel.TabIndex = 29;
            // 
            // panelMiddle
            // 
            this.panelMiddle.Controls.Add(this.pictureBoxConveyorBeltDeck);
            this.panelMiddle.Controls.Add(this.conveyorBeltPanel);
            this.panelMiddle.Location = new System.Drawing.Point(169, 404);
            this.panelMiddle.Margin = new System.Windows.Forms.Padding(2);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(1253, 178);
            this.panelMiddle.TabIndex = 28;
            this.panelMiddle.Visible = false;
            // 
            // conveyorBeltPanel
            // 
            this.conveyorBeltPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.conveyorBeltPanel.Location = new System.Drawing.Point(215, 21);
            this.conveyorBeltPanel.Margin = new System.Windows.Forms.Padding(0);
            this.conveyorBeltPanel.Name = "conveyorBeltPanel";
            this.conveyorBeltPanel.Size = new System.Drawing.Size(872, 140);
            this.conveyorBeltPanel.TabIndex = 10;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 985);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelOperations);
            this.Controls.Add(this.panelBottom);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FzzztGame";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConveyorBeltDeck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomMechanicPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomDiscardPile)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.bottomPlayerOperationPanel.ResumeLayout(false);
            this.panelOperations.ResumeLayout(false);
            this.panelOperations.PerformLayout();
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topDiscardPile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topMechanicPictureBox)).EndInit();
            this.panelMiddle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxConveyorBeltDeck;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.PictureBox bottomMechanicPictureBox;
        private System.Windows.Forms.PictureBox bottomDiscardPile;
        private System.Windows.Forms.Label bottomPlayerLabel;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelOperations;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label topPlayerLabel;
        private System.Windows.Forms.PictureBox topDiscardPile;
        private System.Windows.Forms.PictureBox topMechanicPictureBox;
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.Label labelChiefMechanicBottom;
        private System.Windows.Forms.Label labelChiefMechanicTop;
        private System.Windows.Forms.Button bottomStartAuction;
        private System.Windows.Forms.FlowLayoutPanel bottomCardInHandPanel;
        private System.Windows.Forms.FlowLayoutPanel topCardInHandPanel;
        private System.Windows.Forms.FlowLayoutPanel conveyorBeltPanel;
        private System.Windows.Forms.FlowLayoutPanel bottomBidPanel;
        private System.Windows.Forms.FlowLayoutPanel topBidPanel;
        private System.Windows.Forms.Button bottomBidButton;
        private System.Windows.Forms.Button printGameStateButton;
        private System.Windows.Forms.FlowLayoutPanel bottomProductionUnitPanel;
        private System.Windows.Forms.FlowLayoutPanel topProductionUnitPanel;
        private System.Windows.Forms.FlowLayoutPanel bottomPlayerOperationPanel;
    }
}