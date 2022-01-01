using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
	public class Form1 : Form
	{
		private Config config;
        private Config config2;

        private Thread petWorker;

		private Thread animationWorker;

		private WinApiWrapper api;

		private Enemy[] enemies;

		private List<TextBox> enemiesInfo = new List<TextBox>();

		private IContainer components;

		private CheckBox checkBox1;

		private Button btnStart;

		private TextBox txtName;

		private MaskedTextBox txtHp;

		private Label label1;

		private Label label2;

		private TextBox txtEnemy1;

		private TextBox txtEnemy3;

		private TextBox txtEnemy2;

		private TextBox txtLog;

		private Label label3;

		private TextBox txtEnemy5;

		private TextBox txtEnemy4;
        private TextBox txtName2;
        private MaskedTextBox txtHp2;
        private Label label4;

		public Form1()
		{
			this.InitializeComponent();
			Random random = new Random();
			this.Text = "";
			int num = 0;
			int num1 = random.Next(10, 25);
			while (num < num1)
			{
				string text = this.Text;
				char chr = (char)random.Next(65, 122);
				this.Text = string.Concat(text, chr.ToString());
				num++;
			}
			this.Text = Regex.Replace(this.Text, "[^A-Za-z]", " ");
			this.petWorker = new Thread(() => {
			});
			this.animationWorker = new Thread(() => {
			});
			this.initEnemies();
			this.enemiesInfo.Add(this.txtEnemy1);
			this.enemiesInfo.Add(this.txtEnemy2);
			this.enemiesInfo.Add(this.txtEnemy3);
			this.enemiesInfo.Add(this.txtEnemy4);
			this.enemiesInfo.Add(this.txtEnemy5);
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (this.petWorker.ThreadState == System.Threading.ThreadState.Unstarted || this.petWorker.ThreadState == System.Threading.ThreadState.Stopped || this.petWorker.ThreadState == System.Threading.ThreadState.Aborted)
			{
				try
				{
					this.config = new Config(this.txtName.Text, this.txtHp.Text);
                    this.config2 = new Config(this.txtName2.Text, this.txtHp2.Text);
                }
				catch (Exception exception)
				{
					MessageBox.Show("Error Config.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.petWorker = new Thread(() => {
					Control.CheckForIllegalCrossThreadCalls = false;
					this.pettingLoop();
				});
				this.petWorker.Start();
				this.btnStart.Text = "멈춤";
				return;
			}
			else
			{
				this.petWorker.Abort();
				this.btnStart.Text = "스타트";
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			Process process = this.getProcess(Constant.exeName);
			this.api = this.initApi(process);
			if (!checkBox.Checked)
			{
				this.api.removeAnimation(false);
				this.animationWorker.Abort();
				this.log("모션 삭제: OFF");
				return;
			}
			this.api.removeAnimation(true);
			this.animationWorker = new Thread(() => {
				Control.CheckForIllegalCrossThreadCalls = false;
				this.api.animationHackLoop();
			});
			this.animationWorker.Start();
			this.log("모션 삭제: ON");
		}

		private void clearEnemiesInfo()
		{
			for (int i = 0; i < (int)this.enemies.Length; i++)
			{
				this.enemiesInfo[i].Clear();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Process.GetCurrentProcess().Kill();
		}

		private Enemy getPossibleEnemy()
		{
			for (int i = 0; i < (int)this.enemies.Length; i++)
			{
				this.api.refreshEnemy(ref this.enemies[i], i);
                if(this.enemies[i].getLevel() == 1)
                {
                    this.log(string.Format("체력 [{0}] 이름 [{1}", enemies[i].getHp(), enemies[i].getName()));

                    if (this.config.isNameMatch(this.enemies[i].getName()) && this.config.isInHpRange(this.enemies[i].getHp()) && this.enemies[i].getStatus())
                    {
                        return this.enemies[i];
                    }

                    if (this.config2.isNameMatch(this.enemies[i].getName()) && this.config2.isInHpRange(this.enemies[i].getHp()) && this.enemies[i].getStatus())
                    {
                        return this.enemies[i];
                    }

                }
                



            }
			return null;
		}

		private Process getProcess(string exeName)
		{
			Process process;
            process = Process.GetProcessesByName(Constant.exeName).First<Process>();
            try
			{

                int i = 0;
			}
			catch (Exception exception)
			{
				MessageBox.Show("error occurs.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Process.GetCurrentProcess().Kill();
				return null;
			}
			return process;
		}

		private WinApiWrapper initApi(Process process)
		{
			WinApiWrapper instance;
            int i = 0;
				instance = WinApiWrapper.getInstance(process);

			try
			{
				int j = 0;
				


			}
			catch (Exception exception)
			{
				MessageBox.Show("윈도우 API 에러.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Process.GetCurrentProcess().Kill();
				return null;
			}
			return instance;
		}

		private void initEnemies()
		{
			this.enemies = new Enemy[this.enemiesInfo.Count];
			for (int i = 0; i < (int)this.enemies.Length; i++)
			{
				this.enemies[i] = new Enemy(Constant.enemiesPosition[i]);
			}
		}

		private void InitializeComponent()
		{
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtHp = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEnemy1 = new System.Windows.Forms.TextBox();
            this.txtEnemy3 = new System.Windows.Forms.TextBox();
            this.txtEnemy2 = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEnemy5 = new System.Windows.Forms.TextBox();
            this.txtEnemy4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName2 = new System.Windows.Forms.TextBox();
            this.txtHp2= new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 65);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 21);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "모션 삭제";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnStart.Location = new System.Drawing.Point(12, 293);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(407, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "스타트";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.txtName.Location = new System.Drawing.Point(59, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(72, 24);
            this.txtName.TabIndex = 2;
            // 
            // txtHp
            // 
            this.txtHp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtHp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.txtHp.Location = new System.Drawing.Point(59, 38);
            this.txtHp.Mask = "00-00";
            this.txtHp.Name = "txtHp";
            this.txtHp.Size = new System.Drawing.Size(55, 24);
            this.txtHp.TabIndex = 3;
            this.txtHp.Text = "0199";
            this.txtHp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHp.ValidatingType = typeof(int);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "이름:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "내구력:";
            // 
            // txtEnemy1
            // 
            this.txtEnemy1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtEnemy1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.txtEnemy1.Location = new System.Drawing.Point(339, 115);
            this.txtEnemy1.Multiline = true;
            this.txtEnemy1.Name = "txtEnemy1";
            this.txtEnemy1.ReadOnly = true;
            this.txtEnemy1.Size = new System.Drawing.Size(79, 44);
            this.txtEnemy1.TabIndex = 2;
            // 
            // txtEnemy3
            // 
            this.txtEnemy3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtEnemy3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.txtEnemy3.Location = new System.Drawing.Point(339, 65);
            this.txtEnemy3.Multiline = true;
            this.txtEnemy3.Name = "txtEnemy3";
            this.txtEnemy3.ReadOnly = true;
            this.txtEnemy3.Size = new System.Drawing.Size(79, 44);
            this.txtEnemy3.TabIndex = 2;
            // 
            // txtEnemy2
            // 
            this.txtEnemy2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtEnemy2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.txtEnemy2.Location = new System.Drawing.Point(339, 165);
            this.txtEnemy2.Multiline = true;
            this.txtEnemy2.Name = "txtEnemy2";
            this.txtEnemy2.ReadOnly = true;
            this.txtEnemy2.Size = new System.Drawing.Size(79, 44);
            this.txtEnemy2.TabIndex = 2;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.txtLog.Location = new System.Drawing.Point(10, 102);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(323, 157);
            this.txtLog.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "로그";
            // 
            // txtEnemy5
            // 
            this.txtEnemy5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtEnemy5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.txtEnemy5.Location = new System.Drawing.Point(339, 15);
            this.txtEnemy5.Multiline = true;
            this.txtEnemy5.Name = "txtEnemy5";
            this.txtEnemy5.ReadOnly = true;
            this.txtEnemy5.Size = new System.Drawing.Size(79, 44);
            this.txtEnemy5.TabIndex = 2;
            // 
            // txtEnemy4
            // 
            this.txtEnemy4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtEnemy4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.txtEnemy4.Location = new System.Drawing.Point(339, 215);
            this.txtEnemy4.Multiline = true;
            this.txtEnemy4.Name = "txtEnemy4";
            this.txtEnemy4.ReadOnly = true;
            this.txtEnemy4.Size = new System.Drawing.Size(79, 44);
            this.txtEnemy4.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(146, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 17);
            this.label4.TabIndex = 4;
            // 
            // textBox1
            // 
            this.txtName2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtName2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.txtName2.Location = new System.Drawing.Point(169, 11);
            this.txtName2.Name = "txtName2";
            this.txtName2.Size = new System.Drawing.Size(72, 24);
            this.txtName2.TabIndex = 2;
            // 
            // txtHp2
            // 
            this.txtHp2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.txtHp2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.txtHp2.Location = new System.Drawing.Point(169, 39);
            this.txtHp2.Mask = "00-00";
            this.txtHp2.Name = "txtHp2";
            this.txtHp2.Size = new System.Drawing.Size(55, 24);
            this.txtHp2.TabIndex = 3;
            this.txtHp2.Text = "0199";
            this.txtHp2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHp2.ValidatingType = typeof(int);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ClientSize = new System.Drawing.Size(431, 328);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHp2);
            this.Controls.Add(this.txtHp);
            this.Controls.Add(this.txtEnemy4);
            this.Controls.Add(this.txtEnemy2);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtEnemy5);
            this.Controls.Add(this.txtEnemy3);
            this.Controls.Add(this.txtEnemy1);
            this.Controls.Add(this.txtName2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.checkBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private bool isPetFull()
		{
			return this.api.getPetCount() >= 4;
		}

		private void log(string text)
		{
			TextBox textBox = this.txtLog;
			string[] shortTimeString = new string[] { "[", null, null, null, null, null };
			shortTimeString[1] = DateTime.Now.ToShortTimeString();
			shortTimeString[2] = "] : ";
			shortTimeString[3] = text;
			shortTimeString[4] = " ";
			shortTimeString[5] = Environment.NewLine;
			textBox.AppendText(string.Concat(shortTimeString));
		}

		private void pettingLoop()
		{
			Form1.LoopStatus loopStatu = Form1.LoopStatus.Init;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
            
            while (true)
			{
				switch (loopStatu)
				{
					case Form1.LoopStatus.Init:
					{
						this.log("초기화중");
						Process process = this.getProcess(Constant.exeName);
						this.api = this.initApi(process);
                        
                            this.initEnemies();
						this.clearEnemiesInfo();
						this.api.initMouse();
						if (!this.isPetFull())
						{
							loopStatu = Form1.LoopStatus.Start;
							break;
						}
						else
						{
							loopStatu = Form1.LoopStatus.StorePet;
							break;
						}
					}
					case Form1.LoopStatus.Start:
					{
						this.log("반복전투시작");
						this.api.setAutoBattle(false);
                         this.api.setAutoEncounter(true);
                         loopStatu = Form1.LoopStatus.Battle;
						break;
					}
					case Form1.LoopStatus.Battle:
					{
						if (this.api.isOnBattleNow())
						{
							if (stopwatch.ElapsedMilliseconds >= (long)250)
							{
								this.readEnemiesInfo();
								stopwatch.Restart();
							}
						}
						else if (stopwatch.ElapsedMilliseconds >= (long)50)
						{
							this.clearEnemiesInfo();
							stopwatch.Restart();
						}
						if (!this.api.isCommandInputNow())
						{
							break;
						}
						Thread.Sleep(500);
						Enemy possibleEnemy = this.getPossibleEnemy();
                        if (this.isPetFull())
						{
							this.log("페트창 공간 풀");
                                this.api.setAutoEncounter(false);
							this.api.clickRunButton();
							loopStatu = Form1.LoopStatus.StorePet;
							Thread.Sleep(5000);
						}
						else if (possibleEnemy != null)
						{
							this.log(string.Format("포획중 [{0}] : [레벨{1}] : [내구력{2}]", possibleEnemy.getName(), possibleEnemy.getLevel(), possibleEnemy.getHp()));
							this.api.catchEnemy(possibleEnemy);
						}
						else
						{

                                //         if(possibleEnemy.getLevel()==1)
                                //           {
                                //             this.log(string.Format("체력 [{0}] 이름 [{1}", possibleEnemy.getHp(), possibleEnemy.getName()));

                                //                }
                                //         else
                                //          {
                                    // this.log("대상을 찾을수 없음");

                                //       }

                                this.api.clickRunButton();
						}
						Thread.Sleep(500);
						break;
					}
					case Form1.LoopStatus.StorePet:
					{
						this.log("페트 보관중");
						if (!this.api.isOnBattleNow())
						{
							this.api.openPetStorage();
							this.api.storePetToStorage();
							loopStatu = Form1.LoopStatus.Start;
							break;
						}
						else
						{
                              //  this.api.setAutoEncounter(false);

                                loopStatu = Form1.LoopStatus.Battle;
							break;
						}
					}
				}
				Thread.Sleep(10);
			}
		}

		private void readEnemiesInfo()
		{
			for (int i = 0; i < (int)this.enemies.Length; i++)
			{
				this.api.refreshEnemy(ref this.enemies[i], i);
				this.enemiesInfo[i].Clear();
				if (this.enemies[i].getName().Length != 0)
				{
					string str = "";
					str = string.Concat(str, this.enemies[i].getName(), Environment.NewLine);
					str = string.Concat(str, string.Format("레벨:{0}", this.enemies[i].getLevel()), Environment.NewLine);
					str = string.Concat(str, string.Format("내구력:{0}", this.enemies[i].getHp()));
					this.enemiesInfo[i].Text = str;
				}
			}
		}

		private enum LoopStatus
		{
			Init,
			Start,
			Battle,
			StorePet
		}
	}
}