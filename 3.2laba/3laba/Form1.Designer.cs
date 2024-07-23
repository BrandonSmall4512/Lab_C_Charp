namespace _3laba
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AnT = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.PointlnGrap = new System.Windows.Forms.Timer(this.components);
            this.Ymaxim = new System.Windows.Forms.TextBox();
            this.Yminim = new System.Windows.Forms.TextBox();
            this.Xminim = new System.Windows.Forms.TextBox();
            this.Xmaxim = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Xmaximlab = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.colorFor = new System.Windows.Forms.ComboBox();
            this.colorSelect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.graphChoose = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // AnT
            // 
            this.AnT.AccumBits = ((byte)(0));
            this.AnT.AutoCheckErrors = false;
            this.AnT.AutoFinish = false;
            this.AnT.AutoMakeCurrent = true;
            this.AnT.AutoSwapBuffers = true;
            this.AnT.BackColor = System.Drawing.Color.Black;
            this.AnT.ColorBits = ((byte)(32));
            this.AnT.DepthBits = ((byte)(16));
            this.AnT.Location = new System.Drawing.Point(94, 51);
            this.AnT.Margin = new System.Windows.Forms.Padding(2);
            this.AnT.Name = "AnT";
            this.AnT.Size = new System.Drawing.Size(875, 476);
            this.AnT.StencilBits = ((byte)(0));
            this.AnT.TabIndex = 0;
            this.AnT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AnT_MouseMove);
            // 
            // PointlnGrap
            // 
            this.PointlnGrap.Interval = 30;
            this.PointlnGrap.Tick += new System.EventHandler(this.PointlnGrap_Tick);
            // 
            // Ymaxim
            // 
            this.Ymaxim.Location = new System.Drawing.Point(24, 51);
            this.Ymaxim.Multiline = true;
            this.Ymaxim.Name = "Ymaxim";
            this.Ymaxim.Size = new System.Drawing.Size(38, 23);
            this.Ymaxim.TabIndex = 1;
            this.Ymaxim.Text = "15";
            this.Ymaxim.TextChanged += new System.EventHandler(this.Ymaxim_TextChanged);
            // 
            // Yminim
            // 
            this.Yminim.Location = new System.Drawing.Point(27, 504);
            this.Yminim.Multiline = true;
            this.Yminim.Name = "Yminim";
            this.Yminim.Size = new System.Drawing.Size(35, 23);
            this.Yminim.TabIndex = 2;
            this.Yminim.Text = "-15";
            this.Yminim.TextChanged += new System.EventHandler(this.Yminim_TextChanged);
            // 
            // Xminim
            // 
            this.Xminim.Location = new System.Drawing.Point(94, 552);
            this.Xminim.Multiline = true;
            this.Xminim.Name = "Xminim";
            this.Xminim.Size = new System.Drawing.Size(37, 23);
            this.Xminim.TabIndex = 3;
            this.Xminim.Text = "-15";
            this.Xminim.TextChanged += new System.EventHandler(this.Xminim_TextChanged);
            // 
            // Xmaxim
            // 
            this.Xmaxim.Location = new System.Drawing.Point(930, 552);
            this.Xmaxim.Multiline = true;
            this.Xmaxim.Name = "Xmaxim";
            this.Xmaxim.Size = new System.Drawing.Size(39, 23);
            this.Xmaxim.TabIndex = 4;
            this.Xmaxim.Text = "15";
            this.Xmaxim.TextChanged += new System.EventHandler(this.Xmaxim_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Y max";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 486);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Y min";
            // 
            // Xmaximlab
            // 
            this.Xmaximlab.AutoSize = true;
            this.Xmaximlab.Location = new System.Drawing.Point(927, 534);
            this.Xmaximlab.Name = "Xmaximlab";
            this.Xmaximlab.Size = new System.Drawing.Size(36, 13);
            this.Xmaximlab.TabIndex = 7;
            this.Xmaximlab.Text = "X max";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 534);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "X min";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1027, 504);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Set";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // colorFor
            // 
            this.colorFor.FormattingEnabled = true;
            this.colorFor.Items.AddRange(new object[] {
            "Coordinate axes",
            "Running point",
            "Graph line",
            "Grid points",
            "Perpendiculars to axes",
            "Text of output"});
            this.colorFor.Location = new System.Drawing.Point(997, 33);
            this.colorFor.Name = "colorFor";
            this.colorFor.Size = new System.Drawing.Size(125, 21);
            this.colorFor.TabIndex = 10;
            this.colorFor.SelectedIndexChanged += new System.EventHandler(this.colorFor_SelectedIndexChanged);
            // 
            // colorSelect
            // 
            this.colorSelect.Location = new System.Drawing.Point(997, 75);
            this.colorSelect.Name = "colorSelect";
            this.colorSelect.Size = new System.Drawing.Size(125, 23);
            this.colorSelect.TabIndex = 11;
            this.colorSelect.Text = "Select color";
            this.colorSelect.UseVisualStyleBackColor = true;
            this.colorSelect.Click += new System.EventHandler(this.colorSelect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(997, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Set new color for:";
            // 
            // graphChoose
            // 
            this.graphChoose.FormattingEnabled = true;
            this.graphChoose.Items.AddRange(new object[] {
            "y = (float)Math.Sin(x) * 3 + 1",
            "y = (float)Math.Cos(x) * (-5) - 2",
            "y = (float)Math.Tan(Math.Abs(x))"});
            this.graphChoose.Location = new System.Drawing.Point(974, 258);
            this.graphChoose.Name = "graphChoose";
            this.graphChoose.Size = new System.Drawing.Size(159, 21);
            this.graphChoose.TabIndex = 13;
            this.graphChoose.Text = "y = (float)Math.Sin(x) * 3 + 1";
            this.graphChoose.SelectedIndexChanged += new System.EventHandler(this.graphChoose_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(997, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Choose graph:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 606);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.graphChoose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.colorSelect);
            this.Controls.Add(this.colorFor);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Xmaximlab);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Xmaxim);
            this.Controls.Add(this.Xminim);
            this.Controls.Add(this.Yminim);
            this.Controls.Add(this.Ymaxim);
            this.Controls.Add(this.AnT);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl AnT;
        private System.Windows.Forms.Timer PointlnGrap;
        private System.Windows.Forms.TextBox Ymaxim;
        private System.Windows.Forms.TextBox Yminim;
        private System.Windows.Forms.TextBox Xminim;
        private System.Windows.Forms.TextBox Xmaxim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Xmaximlab;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox colorFor;
        private System.Windows.Forms.Button colorSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox graphChoose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}

