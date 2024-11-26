namespace Universo2D
{
    partial class Form1
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
            this.btn_aleatorio = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.qtdCorpos = new System.Windows.Forms.TextBox();
            this.qtdInterac = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.qtdTempoInterac = new System.Windows.Forms.TextBox();
            this.btn_executa = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.qtdCorposAtual = new System.Windows.Forms.TextBox();
            this.btn_carregaSimulacao = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.masMax = new System.Windows.Forms.TextBox();
            this.masMin = new System.Windows.Forms.TextBox();
            this.btn_grava_ini = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_aleatorio
            // 
            this.btn_aleatorio.Location = new System.Drawing.Point(6, 167);
            this.btn_aleatorio.Name = "btn_aleatorio";
            this.btn_aleatorio.Size = new System.Drawing.Size(75, 23);
            this.btn_aleatorio.TabIndex = 8;
            this.btn_aleatorio.Text = "Distribuir";
            this.btn_aleatorio.UseVisualStyleBackColor = true;
            this.btn_aleatorio.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nº de Corpos";
            // 
            // qtdCorpos
            // 
            this.qtdCorpos.Location = new System.Drawing.Point(6, 12);
            this.qtdCorpos.Name = "qtdCorpos";
            this.qtdCorpos.Size = new System.Drawing.Size(100, 20);
            this.qtdCorpos.TabIndex = 1;
            this.qtdCorpos.Text = "250";
            // 
            // qtdInterac
            // 
            this.qtdInterac.Location = new System.Drawing.Point(6, 36);
            this.qtdInterac.Name = "qtdInterac";
            this.qtdInterac.Size = new System.Drawing.Size(100, 20);
            this.qtdInterac.TabIndex = 2;
            this.qtdInterac.Text = "100000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nº de Interações";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Tempo entre interações (s)";
            // 
            // qtdTempoInterac
            // 
            this.qtdTempoInterac.Location = new System.Drawing.Point(6, 141);
            this.qtdTempoInterac.Name = "qtdTempoInterac";
            this.qtdTempoInterac.Size = new System.Drawing.Size(100, 20);
            this.qtdTempoInterac.TabIndex = 3;
            this.qtdTempoInterac.Text = "50";
            // 
            // btn_executa
            // 
            this.btn_executa.Location = new System.Drawing.Point(87, 167);
            this.btn_executa.Name = "btn_executa";
            this.btn_executa.Size = new System.Drawing.Size(75, 23);
            this.btn_executa.TabIndex = 9;
            this.btn_executa.Text = "Iniciar";
            this.btn_executa.UseVisualStyleBackColor = true;
            this.btn_executa.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.qtdCorposAtual);
            this.groupBox1.Controls.Add(this.btn_carregaSimulacao);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.masMax);
            this.groupBox1.Controls.Add(this.masMin);
            this.groupBox1.Controls.Add(this.btn_grava_ini);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_executa);
            this.groupBox1.Controls.Add(this.qtdTempoInterac);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.qtdInterac);
            this.groupBox1.Controls.Add(this.qtdCorpos);
            this.groupBox1.Controls.Add(this.btn_aleatorio);
            this.groupBox1.Location = new System.Drawing.Point(1261, 522);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 248);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(108, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Corpos Atuais";
            // 
            // qtdCorposAtual
            // 
            this.qtdCorposAtual.Location = new System.Drawing.Point(6, 62);
            this.qtdCorposAtual.Name = "qtdCorposAtual";
            this.qtdCorposAtual.Size = new System.Drawing.Size(100, 20);
            this.qtdCorposAtual.TabIndex = 23;
            this.qtdCorposAtual.Text = "0";
            // 
            // btn_carregaSimulacao
            // 
            this.btn_carregaSimulacao.Location = new System.Drawing.Point(6, 218);
            this.btn_carregaSimulacao.Name = "btn_carregaSimulacao";
            this.btn_carregaSimulacao.Size = new System.Drawing.Size(156, 23);
            this.btn_carregaSimulacao.TabIndex = 13;
            this.btn_carregaSimulacao.Text = "Carregar Arquivo";
            this.btn_carregaSimulacao.UseVisualStyleBackColor = true;
            this.btn_carregaSimulacao.Click += new System.EventHandler(this.btn_carregaSimulacao_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(108, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Massa Máxima (kg)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(108, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Massa Mínima (kg)";
            // 
            // masMax
            // 
            this.masMax.Location = new System.Drawing.Point(6, 88);
            this.masMax.Name = "masMax";
            this.masMax.Size = new System.Drawing.Size(100, 20);
            this.masMax.TabIndex = 7;
            this.masMax.Text = "500000";
            // 
            // masMin
            // 
            this.masMin.Location = new System.Drawing.Point(6, 115);
            this.masMin.Name = "masMin";
            this.masMin.Size = new System.Drawing.Size(100, 20);
            this.masMin.TabIndex = 6;
            this.masMin.Text = "10000";
            // 
            // btn_grava_ini
            // 
            this.btn_grava_ini.Location = new System.Drawing.Point(6, 195);
            this.btn_grava_ini.Name = "btn_grava_ini";
            this.btn_grava_ini.Size = new System.Drawing.Size(156, 23);
            this.btn_grava_ini.TabIndex = 12;
            this.btn_grava_ini.Text = "Gravar Configuração";
            this.btn_grava_ini.UseVisualStyleBackColor = true;
            this.btn_grava_ini.Click += new System.EventHandler(this.btn_grava_ini_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(1522, 782);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Universo 2D";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Button btn_aleatorio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox qtdCorpos;
        private System.Windows.Forms.TextBox qtdInterac;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox qtdTempoInterac;
        private System.Windows.Forms.Button btn_executa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_grava_ini;
        private System.Windows.Forms.Button btn_carregaSimulacao;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox qtdCorposAtual;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox masMax;
        private System.Windows.Forms.TextBox masMin;
    }
}
