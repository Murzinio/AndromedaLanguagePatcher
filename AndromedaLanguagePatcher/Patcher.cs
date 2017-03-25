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
	enum GameLanguage
	{
		Polish,
		Russian
	}
	public partial class Patcher : Form
	{
		string exePath;
		
		byte[] exePatched;
		GameLanguage gameLanguage;

		//offsets to change, first in array is all small letters - like "pl_pl"
		int[] bytesToOriginalLanguageOffset = new int[2];
		int[] bytesToEnglishOffset = new int[2];

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
				//exe = reader.ReadBytes(int.MaxValue);
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
				case GameLanguage.Polish:
					for (int i = 0; i < exeRead.Length - 3; i++)
					{
						if (exeRead[i] == 0x70 && exeRead[i + 1] == 0x6C && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x70 && exeRead[i + 4] == 0x6C)
							bytesToEnglishOffset[0] = i;
						else if (exeRead[i] == 0x70 && exeRead[i + 1] == 0x6C && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x50 && exeRead[i + 4] == 0x4C)
							bytesToEnglishOffset[1] = i;
						else if (exeRead[i] == 0x65 && exeRead[i + 1] == 0x6E && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x75 && exeRead[i + 4] == 0x73)
							bytesToOriginalLanguageOffset[0] = i;
						else if (exeRead[i] == 0x65 && exeRead[i + 1] == 0x6E && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x55 && exeRead[i + 4] == 0x53)
							bytesToOriginalLanguageOffset[1] = i;
					}
					break;
				case GameLanguage.Russian:
					for (int i = 0; i < exeRead.Length; i++)
					{
						if (exeRead[i] == 0x72 && exeRead[i + 1] == 0x75 && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x72 && exeRead[i + 4] == 0x75)
							bytesToEnglishOffset[0] = i;
						else if (exeRead[i] == 0x72 && exeRead[i + 1] == 0x75 && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x52 && exeRead[i + 4] == 0x55)
							bytesToEnglishOffset[1] = i;
						else if (exeRead[i] == 0x65 && exeRead[i + 1] == 0x6E && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x75 && exeRead[i + 4] == 0x73)
							bytesToOriginalLanguageOffset[0] = i;
						else if (exeRead[i] == 0x65 && exeRead[i + 1] == 0x6E && exeRead[i + 2] == 0x5F && exeRead[i + 3] == 0x55 && exeRead[i + 4] == 0x53)
							bytesToOriginalLanguageOffset[1] = i;
					}
					break;
			}
			exePatched = exeRead;
		}
		private void PatchExe()
		{
			//actual bytes to write, first in array is all small letters - like "pl_pl"
			byte[] bytesOriginalLanguageLower = new byte[5];
			byte[] bytesOriginalLanguageUpper = new byte[5];
			byte[] bytesEnglishLower = new byte[5] { 0x65, 0x6E, 0x5F, 0x75, 0x73 };
			byte[] bytesEnglishUpper = new byte[5] { 0x65, 0x6E, 0x5F, 0x55, 0x53 };

			switch (gameLanguage)
			{
				case GameLanguage.Polish:
					bytesOriginalLanguageLower[0] = 0x70;
					bytesOriginalLanguageLower[1] = 0x6C;
					bytesOriginalLanguageLower[2] = 0x5F;
					bytesOriginalLanguageLower[3] = 0x70;
					bytesOriginalLanguageLower[4] = 0x6C;

					bytesOriginalLanguageUpper[0] = 0x70;
					bytesOriginalLanguageUpper[1] = 0x6C;
					bytesOriginalLanguageUpper[2] = 0x5F;
					bytesOriginalLanguageUpper[3] = 0x50;
					bytesOriginalLanguageUpper[4] = 0x4C;
					break;
				case GameLanguage.Russian:
					bytesOriginalLanguageLower[0] = 0x72;
					bytesOriginalLanguageLower[1] = 0x75;
					bytesOriginalLanguageLower[2] = 0x5F;
					bytesOriginalLanguageLower[3] = 0x72;
					bytesOriginalLanguageLower[4] = 0x75;

					bytesOriginalLanguageUpper[0] = 0x72;
					bytesOriginalLanguageUpper[1] = 0x75;
					bytesOriginalLanguageUpper[2] = 0x5F;
					bytesOriginalLanguageUpper[3] = 0x52;
					bytesOriginalLanguageUpper[4] = 0x55;
					break;
			}
			
			using (BinaryWriter writer = new BinaryWriter(File.Open(exePath, FileMode.Open)))
			{

				for (int i = 0; i < 5; i++)
				{
					writer.BaseStream.Position = bytesToEnglishOffset[0] + i;
					writer.Write(bytesEnglishLower[i]);

					writer.BaseStream.Position = bytesToOriginalLanguageOffset[0] + i;
					writer.Write(bytesOriginalLanguageLower[i]);

					writer.BaseStream.Position = bytesToEnglishOffset[1] + i;
					writer.Write(bytesEnglishUpper[i]);

					writer.BaseStream.Position = bytesToOriginalLanguageOffset[1] + i;
					writer.Write(bytesOriginalLanguageUpper[i]);
				}
			}
		}

		private void comboBox_GameLanguage_SelectedIndexChanged(object sender, EventArgs e)
		{
			button_Path.Enabled = true;
			gameLanguage = (GameLanguage)comboBox_GameLanguage.SelectedIndex;
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
