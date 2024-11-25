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
            btn_aleatorio = new Button();
            label2 = new Label();
            qtdCorpos = new TextBox();
            qtdInterac = new TextBox();
            label4 = new Label();
            label5 = new Label();
            qtdTempoInterac = new TextBox();
            btn_executa = new Button();
            groupBox1 = new GroupBox();
            label10 = new Label();
            qtdCorposAtual = new TextBox();
            btn_carregaSimulacao = new Button();
            label8 = new Label();
            label7 = new Label();
            masMax = new TextBox();
            masMin = new TextBox();
            btn_grava_ini = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_aleatorio
            // 
            btn_aleatorio.Location = new Point(394, 67);
            btn_aleatorio.Name = "btn_aleatorio";
            btn_aleatorio.Size = new Size(75, 23);
            btn_aleatorio.TabIndex = 8;
            btn_aleatorio.Text = "Aleatório";
            btn_aleatorio.UseVisualStyleBackColor = true;
            btn_aleatorio.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 71);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 1;
            label2.Text = "Qtd. Corpos";
            // 
            // qtdCorpos
            // 
            qtdCorpos.Location = new Point(-2, 86);
            qtdCorpos.Name = "qtdCorpos";
            qtdCorpos.Size = new Size(100, 23);
            qtdCorpos.TabIndex = 1;
            qtdCorpos.Text = "0";
            // 
            // qtdInterac
            // 
            qtdInterac.Location = new Point(101, 87);
            qtdInterac.Name = "qtdInterac";
            qtdInterac.Size = new Size(100, 23);
            qtdInterac.TabIndex = 2;
            qtdInterac.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(100, 71);
            label4.Name = "label4";
            label4.Size = new Size(94, 15);
            label4.TabIndex = 6;
            label4.Text = "Num. Interações";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(200, 67);
            label5.Name = "label5";
            label5.Size = new Size(146, 15);
            label5.TabIndex = 7;
            label5.Text = "Tempo entre interações (s)";
            // 
            // qtdTempoInterac
            // 
            qtdTempoInterac.Location = new Point(207, 86);
            qtdTempoInterac.Name = "qtdTempoInterac";
            qtdTempoInterac.Size = new Size(100, 23);
            qtdTempoInterac.TabIndex = 3;
            qtdTempoInterac.Text = "0";
            // 
            // btn_executa
            // 
            btn_executa.Location = new Point(394, 38);
            btn_executa.Name = "btn_executa";
            btn_executa.Size = new Size(75, 23);
            btn_executa.TabIndex = 9;
            btn_executa.Text = "Executar";
            btn_executa.UseVisualStyleBackColor = true;
            btn_executa.Click += button1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(qtdCorpos);
            groupBox1.Controls.Add(qtdCorposAtual);
            groupBox1.Controls.Add(btn_carregaSimulacao);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(masMax);
            groupBox1.Controls.Add(masMin);
            groupBox1.Controls.Add(btn_grava_ini);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btn_executa);
            groupBox1.Controls.Add(qtdTempoInterac);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(qtdInterac);
            groupBox1.Controls.Add(btn_aleatorio);
            groupBox1.Location = new Point(12, 576);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(475, 157);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(0, 108);
            label10.Name = "label10";
            label10.Size = new Size(102, 15);
            label10.TabIndex = 22;
            label10.Text = "Qtd. Corpos Atual";
            // 
            // qtdCorposAtual
            // 
            qtdCorposAtual.Location = new Point(0, 126);
            qtdCorposAtual.Name = "qtdCorposAtual";
            qtdCorposAtual.Size = new Size(100, 23);
            qtdCorposAtual.TabIndex = 23;
            qtdCorposAtual.Text = "0";
            // 
            // btn_carregaSimulacao
            // 
            btn_carregaSimulacao.Location = new Point(313, 125);
            btn_carregaSimulacao.Name = "btn_carregaSimulacao";
            btn_carregaSimulacao.Size = new Size(156, 23);
            btn_carregaSimulacao.TabIndex = 13;
            btn_carregaSimulacao.Text = "Carregar Simulação";
            btn_carregaSimulacao.UseVisualStyleBackColor = true;
            btn_carregaSimulacao.Click += btn_carregaSimulacao_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(206, 112);
            label8.Name = "label8";
            label8.Size = new Size(110, 15);
            label8.TabIndex = 19;
            label8.Text = "Massa Máxima (kg)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(100, 113);
            label7.Name = "label7";
            label7.Size = new Size(108, 15);
            label7.TabIndex = 13;
            label7.Text = "Massa Mínima (kg)";
            // 
            // masMax
            // 
            masMax.Location = new Point(207, 126);
            masMax.Name = "masMax";
            masMax.Size = new Size(100, 23);
            masMax.TabIndex = 7;
            masMax.Text = "500000";
            // 
            // masMin
            // 
            masMin.Location = new Point(101, 126);
            masMin.Name = "masMin";
            masMin.Size = new Size(100, 23);
            masMin.TabIndex = 6;
            masMin.Text = "10000";
            // 
            // btn_grava_ini
            // 
            btn_grava_ini.Location = new Point(313, 96);
            btn_grava_ini.Name = "btn_grava_ini";
            btn_grava_ini.Size = new Size(156, 23);
            btn_grava_ini.TabIndex = 12;
            btn_grava_ini.Text = "Gravar Configuração Inicial";
            btn_grava_ini.UseVisualStyleBackColor = true;
            btn_grava_ini.Click += btn_grava_ini_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(1366, 745);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Universo 2D";
            Load += Form1_Load;
            Paint += Form1_Paint;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox masMax;
        private System.Windows.Forms.TextBox masMin;
        private System.Windows.Forms.Button btn_carregaSimulacao;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox qtdCorposAtual;
    }
}