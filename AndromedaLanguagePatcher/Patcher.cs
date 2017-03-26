using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace AndromedaLanguagePatcher
{
	enum Language
	{
		Polish,
		Russian,
		English_US,
		English_UK
	}
	public partial class Patcher : Form
	{
		string exePath;
		
		Language gameLanguage;
		Language targetLanguage;

		// offsets to change
		int bytesToOriginalLanguageOffsetLower;
		int bytesToOriginalLanguageOffsetUpper;
		int bytesToTargetLanguageOffsetLower;
		int bytesToTargetLanguageOffsetUpper;

		public Patcher()
		{
			InitializeComponent();
		}

		private bool DoesBackupExeExist()
		{
			return File.Exists(exePath + ".backup");
		}

		private void BackupExe()
		{
			if (DoesBackupExeExist())
				File.Delete(exePath + ".backup");
			File.Copy(exePath, exePath + ".backup", true);
		}

		private void ReadExe()
		{
			byte[] exeRead;
			using (BinaryReader reader = new BinaryReader(File.Open(exePath, FileMode.Open)))
			{
				const int bufferSize = 4096;
				using (var ms = new MemoryStream())
				{
					byte[] buffer = new byte[bufferSize];
					int count;
					while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
						ms.Write(buffer, 0, count);
					exeRead =  ms.ToArray();
				}
			}

			switch (gameLanguage)
			{
				case Language.Polish:
					for (int i = 0; i < exeRead.Length - 3; i++)
					{
						if (exeRead[i] == 0x70 && exeRead[i + 1] == 0x6C && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x70 && exeRead[i + 4] == 0x6C)
							bytesToTargetLanguageOffsetLower = i;
						else if (exeRead[i] == 0x70 && exeRead[i + 1] == 0x6C && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x50 && exeRead[i + 4] == 0x4C)
							bytesToTargetLanguageOffsetUpper = i;
					}
					break;
				case Language.Russian:
					for (int i = 0; i < exeRead.Length; i++)
					{
						if (exeRead[i] == 0x72 && exeRead[i + 1] == 0x75 && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x72 && exeRead[i + 4] == 0x75)
							bytesToTargetLanguageOffsetLower = i;
						else if (exeRead[i] == 0x72 && exeRead[i + 1] == 0x75 && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x52 && exeRead[i + 4] == 0x55)
							bytesToTargetLanguageOffsetUpper = i;
					}
					break;
			}
			switch (targetLanguage)
			{
				case Language.Polish:
					for (int i = 0; i < exeRead.Length - 3; i++)
					{
						if (exeRead[i] == 0x70 && exeRead[i + 1] == 0x6C && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x70 && exeRead[i + 4] == 0x6C)
							bytesToOriginalLanguageOffsetLower = i;
						else if (exeRead[i] == 0x70 && exeRead[i + 1] == 0x6C && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x50 && exeRead[i + 4] == 0x4C)
							bytesToOriginalLanguageOffsetUpper = i;
					}
					break;
				case Language.Russian:
					for (int i = 0; i < exeRead.Length - 3; i++)
					{
						if (exeRead[i] == 0x72 && exeRead[i + 1] == 0x75 && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x72 && exeRead[i + 4] == 0x75)
							bytesToOriginalLanguageOffsetLower = i;
						else if (exeRead[i] == 0x72 && exeRead[i + 1] == 0x75 && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x52 && exeRead[i + 4] == 0x55)
							bytesToOriginalLanguageOffsetUpper = i;
					}
					break;
				case Language.English_US:
					for (int i = 0; i < exeRead.Length - 3; i++)
					{
						if (exeRead[i] == 0x65 && exeRead[i + 1] == 0x6E && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x75 && exeRead[i + 4] == 0x73)
							bytesToOriginalLanguageOffsetLower = i;
						else if (exeRead[i] == 0x65 && exeRead[i + 1] == 0x6E && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x55 && exeRead[i + 4] == 0x53)
							bytesToOriginalLanguageOffsetUpper = i;
					}
					break;
				case Language.English_UK:
					for (int i = 0; i < exeRead.Length - 3; i++)
					{
						if (exeRead[i] == 0x65 && exeRead[i + 1] == 0x6E && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x75 && exeRead[i + 4] == 0x6B)
							bytesToOriginalLanguageOffsetLower = i;
						else if (exeRead[i] == 0x65 && exeRead[i + 1] == 0x6E && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x47 && exeRead[i + 4] == 0x42)
							bytesToOriginalLanguageOffsetUpper = i;
					}
					break;
			}
		}
		private void PatchExe()
		{
			byte[] bytesOriginalLanguageLower = new byte[5];
			byte[] bytesOriginalLanguageUpper = new byte[5];
			byte[] bytesTargetLanguageLower = new byte[5] { 0x65, 0x6E, 0x5F, 0x75, 0x73 };
			byte[] bytesTargetLanguageUpper = new byte[5] { 0x65, 0x6E, 0x5F, 0x55, 0x53 };

			bytesOriginalLanguageLower[2] = 0x5F;
			bytesOriginalLanguageUpper[2] = 0x5F;
			bytesTargetLanguageLower[2] = 0x5F;
			bytesTargetLanguageUpper[2] = 0x5F;

			switch (gameLanguage)
			{
				case Language.Polish:
					bytesOriginalLanguageLower[0] = 0x70;
					bytesOriginalLanguageLower[1] = 0x6C;
					bytesOriginalLanguageLower[3] = 0x70;
					bytesOriginalLanguageLower[4] = 0x6C;

					bytesOriginalLanguageUpper[0] = 0x70;
					bytesOriginalLanguageUpper[1] = 0x6C;
					bytesOriginalLanguageUpper[3] = 0x50;
					bytesOriginalLanguageUpper[4] = 0x4C;
					break;
				case Language.Russian:
					bytesOriginalLanguageLower[0] = 0x72;
					bytesOriginalLanguageLower[1] = 0x75;
					bytesOriginalLanguageLower[3] = 0x72;
					bytesOriginalLanguageLower[4] = 0x75;

					bytesOriginalLanguageUpper[0] = 0x72;
					bytesOriginalLanguageUpper[1] = 0x75;
					bytesOriginalLanguageUpper[3] = 0x52;
					bytesOriginalLanguageUpper[4] = 0x55;
					break;
				case Language.English_US:
					bytesOriginalLanguageLower[0] = 0x65;
					bytesOriginalLanguageLower[1] = 0x6E;
					bytesOriginalLanguageLower[3] = 0x75;
					bytesOriginalLanguageLower[4] = 0x73;

					bytesOriginalLanguageUpper[0] = 0x65;
					bytesOriginalLanguageUpper[1] = 0x6E;
					bytesOriginalLanguageUpper[3] = 0x55;
					bytesOriginalLanguageUpper[4] = 0x53;
					break;
				case Language.English_UK:
					bytesOriginalLanguageLower[0] = 0x65;
					bytesOriginalLanguageLower[1] = 0x6E;
					bytesOriginalLanguageLower[3] = 0x75;
					bytesOriginalLanguageLower[4] = 0x6B;

					bytesOriginalLanguageUpper[0] = 0x65;
					bytesOriginalLanguageUpper[1] = 0x6E;
					bytesOriginalLanguageUpper[3] = 0x47;
					bytesOriginalLanguageUpper[4] = 0x42;
					break;
			}

			switch (targetLanguage)
			{
				case Language.Polish:
					bytesTargetLanguageLower[0] = 0x70;
					bytesTargetLanguageLower[1] = 0x6C;
					bytesTargetLanguageLower[3] = 0x70;
					bytesTargetLanguageLower[4] = 0x6C;

					bytesTargetLanguageUpper[0] = 0x70;
					bytesTargetLanguageUpper[1] = 0x6C;
					bytesTargetLanguageUpper[3] = 0x50;
					bytesTargetLanguageUpper[4] = 0x4C;
					break;
				case Language.Russian:
					bytesTargetLanguageLower[0] = 0x72;
					bytesTargetLanguageLower[1] = 0x75;
					bytesTargetLanguageLower[3] = 0x72;
					bytesTargetLanguageLower[4] = 0x75;

					bytesTargetLanguageUpper[0] = 0x72;
					bytesTargetLanguageUpper[1] = 0x75;
					bytesTargetLanguageUpper[3] = 0x52;
					bytesTargetLanguageUpper[4] = 0x55;
					break;
				case Language.English_US:
					bytesTargetLanguageLower[0] = 0x65;
					bytesTargetLanguageLower[1] = 0x6E;
					bytesTargetLanguageLower[3] = 0x75;
					bytesTargetLanguageLower[4] = 0x73;

					bytesTargetLanguageUpper[0] = 0x65;
					bytesTargetLanguageUpper[1] = 0x6E;
					bytesTargetLanguageUpper[3] = 0x55;
					bytesTargetLanguageUpper[4] = 0x53;
					break;
				case Language.English_UK:
					bytesTargetLanguageLower[0] = 0x65;
					bytesTargetLanguageLower[1] = 0x6E;
					bytesTargetLanguageLower[3] = 0x75;
					bytesTargetLanguageLower[4] = 0x6B;

					bytesTargetLanguageUpper[0] = 0x65;
					bytesTargetLanguageUpper[1] = 0x6E;
					bytesTargetLanguageUpper[3] = 0x47;
					bytesTargetLanguageUpper[4] = 0x42;
					break;
			}


			using (BinaryWriter writer = new BinaryWriter(File.Open(exePath, FileMode.Open)))
			{
				for (int i = 0; i < 5; i++)
				{
					writer.BaseStream.Position = bytesToTargetLanguageOffsetLower + i;
					writer.Write(bytesTargetLanguageLower[i]);

					writer.BaseStream.Position = bytesToTargetLanguageOffsetUpper + i;
					writer.Write(bytesTargetLanguageUpper[i]);

					writer.BaseStream.Position = bytesToOriginalLanguageOffsetLower + i;
					writer.Write(bytesOriginalLanguageLower[i]);

					writer.BaseStream.Position = bytesToOriginalLanguageOffsetUpper + i;
					writer.Write(bytesOriginalLanguageUpper[i]);
				}
			}
		}

		private void comboBox_GameLanguage_SelectedIndexChanged(object sender, EventArgs e)
		{
			gameLanguage = (Language)comboBox_GameLanguage.SelectedIndex;
			targetLanguage = (Language)comboBox_TargetLanguage.SelectedIndex;
			if (gameLanguage == targetLanguage)
			{
				DialogResult resultMessage = MessageBox.Show("Target language cannot be the same as game language.", "Andromeda Language Patcher",
				MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
				button_Path.Enabled = false;
			}
			else
				button_Path.Enabled = true;
		}

		private void comboBox_TargetLanguage_SelectedIndexChanged(object sender, EventArgs e)
		{
			gameLanguage = (Language)comboBox_GameLanguage.SelectedIndex;
			targetLanguage = (Language)comboBox_TargetLanguage.SelectedIndex;
			if (gameLanguage == targetLanguage)
			{
				DialogResult resultMessage = MessageBox.Show("Target language cannot be the same as game language.", "Andromeda Language Patcher",
				MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
				button_Path.Enabled = false;
			}
			else
				button_Path.Enabled = true;
		}

		private void button_Path_Click(object sender, EventArgs e)
		{
			using (var folderBrowser = new FolderBrowserDialog())
			{
				bool pathSelected = false;
				bool aborted = false;
				
				while (!pathSelected && !aborted)
				{
					DialogResult resultBrowse = folderBrowser.ShowDialog();
					
					if (resultBrowse == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
					{
						exePath = folderBrowser.SelectedPath + "\\MassEffectAndromeda.exe";
						if (!File.Exists(exePath))
						{
							DialogResult resultMessage = MessageBox.Show("Wrong path, MassEffectAndromeda.exe not found.", "Andromeda Language Patcher",
							MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

							if (resultMessage == DialogResult.Cancel)
								aborted = true;
						}
						else
						{
							pathSelected = true;
							textBox_Path.Text = exePath;
							button_Patch.Enabled = true;
						}
							
					}
					else
						aborted = true;
				}	
			}
			if (DoesBackupExeExist())
				button_Restore.Enabled = true;
		}

		private void button_Patch_Click(object sender, EventArgs e)
		{
			BackupExe();
			button_Restore.Enabled = true;

			ReadExe();
			PatchExe();

			DialogResult resultMessage = MessageBox.Show("Language changed.", "Andromeda Language Patcher",
			MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void button_Restore_Click(object sender, EventArgs e)
		{
			File.Delete(exePath);
			File.Move(exePath + ".backup", exePath);

			button_Restore.Enabled = false;
			DialogResult resultMessage = MessageBox.Show("Original exe restored.", "Andromeda Language Patcher",
			MessageBoxButtons.OK, MessageBoxIcon.Information);
		}


	}
}
