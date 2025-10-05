using System.Windows.Forms;

namespace fzzzt_game
{
    partial class WidgetForm
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
            this.confirmBuildingButton = new System.Windows.Forms.Button();
            this.topRobotCardPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bottomRobotCardPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // confirmBuildingButton
            // 
            this.confirmBuildingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmBuildingButton.Location = new System.Drawing.Point(664, 945);
            this.confirmBuildingButton.Name = "confirmBuildingButton";
            this.confirmBuildingButton.Size = new System.Drawing.Size(79, 33);
            this.confirmBuildingButton.TabIndex = 0;
            this.confirmBuildingButton.Text = "Confirm";
            this.confirmBuildingButton.UseVisualStyleBackColor = true;
            this.confirmBuildingButton.Click += new System.EventHandler(this.confirmBuildingButton_Click);
            // 
            // topRobotCardPanel
            // 
            this.topRobotCardPanel.Location = new System.Drawing.Point(9, 13);
            this.topRobotCardPanel.Margin = new System.Windows.Forms.Padding(0);
            this.topRobotCardPanel.Name = "topRobotCardPanel";
            this.topRobotCardPanel.Size = new System.Drawing.Size(1402, 70);
            this.topRobotCardPanel.TabIndex = 1;
            // 
            // bottomRobotCardPanel
            // 
            this.bottomRobotCardPanel.Location = new System.Drawing.Point(9, 867);
            this.bottomRobotCardPanel.Margin = new System.Windows.Forms.Padding(0);
            this.bottomRobotCardPanel.Name = "bottomRobotCardPanel";
            this.bottomRobotCardPanel.Size = new System.Drawing.Size(1402, 70);
            this.bottomRobotCardPanel.TabIndex = 2;
            // 
            // WidgetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 985);
            this.Controls.Add(this.bottomRobotCardPanel);
            this.Controls.Add(this.topRobotCardPanel);
            this.Controls.Add(this.confirmBuildingButton);
            this.Name = "WidgetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WidgetForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button confirmBuildingButton;
        private System.Windows.Forms.FlowLayoutPanel topRobotCardPanel;
        private System.Windows.Forms.FlowLayoutPanel bottomRobotCardPanel;

        public FlowLayoutPanel TopRobotCardPanel { get => topRobotCardPanel; set => topRobotCardPanel = value; }
        public FlowLayoutPanel BottomRobotCardPanel { get => bottomRobotCardPanel; set => bottomRobotCardPanel = value; }
    }
}