using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Web;
using System.Net.NetworkInformation;
using NETWORKLIST;
using System.Media;

namespace Alligator_Toolkit
{
    public partial class AlligatorForm : Form
    {
        //Zmienne tylko do odczytu
        readonly int ControlVersionNumber = 2;
        readonly string CurrentVersion = "v3.1";
        readonly bool IsItBeta = false;
        readonly int BetaVersion = 0;

        //Ciągi globalne niezmienne
        public readonly char quote = '"';
        public readonly char BackSlash = '\\';

        //Ciągi globalne zmienne
        public string AppData;
        public string SHData;
        public string SHaData;
        public string AlligatorDIR;
        public string TempDir;
        public string CompressedAdbPath;
        public string DriversPath;
        public static string adb;
        public static string RootStringSDK;
        public string UpdateExePath;
        public string IPAdress;
        public string IPConnect;
        public string PhoneModel;
        public string SUFile;
        public string SULPZIP;
        public string DMLeave;
        public string DMEnter;
        public string DMRoot;
        public string DMBusybox;
        public string DirOfDeath;
        public string G2Security;
        public string SelfKiller;
        public string CompressedDeath;
        public string HydraDownloader;
        public string HydraEXE;
        public string PortLG;
        public string BetaFile;

        //Katalogi z bootloaderami
        public string BLDirOriginalLollipop;
        public string BLDirBumpLollipop;
        public string BLDirLokiLollipop;
        public string BLDirKitKat;
        public string BLDirJellyBean;
        public string BLDir;
        public int SideloadV;

        //Pliki bootloadera
        public string aboot;
        public string dbi;
        public string laf;
        public string persist;
        public string rpm;
        public string sbl1;
        public string tz;
        public string SideloadZIP;

        public int DownloadedLenght;

        //Zmienne Jose Mourinho
        public int JosePictureNumber;
        public int IntStr;
        public string JoseString;

        public string[] DownloadedTable;

        public string PhilZPath;
        public string DownloadedInfo;
        public string OwnAPKPath;
        public string OwnAPKFile;
        public string BumperPath;
        public string BumpingBinary;
        public string BTWRPPath;
        public string datrootPath;
        public string SndCmdExe;
        public string OfcTWRPPath;
        public string OwnRckPath;
        public string LGRootSH;
        public string OwnRckName;
        public string ZippingDIR;
        public string BumpedRckWritePath;
        public string SideloadZipPath;
        public string SideloadZipName;
        public string TempedSideloadZipPath;
        public string FilesD802;
        public string MetaZip;
        public string TempedRCK;
        public string SuperuserAPK;
        public string AlligatorVER;
        public string SndCmd2;
        public string TempedOwnRCK;
        public string OneClickExe;
        public string q = "\"";
        public static string Hangometr;
        public static string CMDOutput;

        public bool AssistLogical;
        public bool AreBootloadersDownloaded;

        //Wartości logiczne do funkcji "Idioto-odporning"
        public bool BoolChooseApkBtn;
        public bool BoolInstallApkBtn;
        public bool BoolDevicesBtn;
        public bool BoolCvWiFi;
        public bool BoolKillServerBtn;
        public bool BoolReboot2DMBtn;
        public bool BoolLeaveDMBtn;
        public bool BoolRebootBtn;
        public bool BoolRebootRckBtn;
        public bool BoolDriversInstall;
        public bool BoolFlashRckBtn;
        public bool BoolFlashOwnRckBtn;
        public bool BoolBumpRckImgBtn;
        public bool BoolChoosenBootloaderStartBtn;
        public bool BoolBootDwnBtnD802;
        public bool BoolRootStartBtn;
        public bool BoolSideloadZipFlashBtn;
        public bool BoolSideloadZipPathBtn;
        public bool IsToolkitDisabled;

        public Thread MultiKulti;
        public Thread ConChecker;

        //Invołkersy
        public string AlliCon = "AlligatorConsole";

        //Komendy ADB
        public static string WaitForDevice = "wait-for-device";
        public static string RebootToRecovery = "reboot recovery";
        public static string Connect = "connect";
        public static bool IsItConnected = false;
        public static bool IsItRooted;
        public static bool IsItUSB;
        public static bool IsBetaAllowed;

        public AlligatorForm()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyCode)
        {
            if (KeyCode == Keys.J || KeyCode == Keys.J)
            {
                JosePictureNumber = new Random().Next(0, 18);
                SetJose(JosePictureNumber);
            }

            if (KeyCode == Keys.M || KeyCode == Keys.J)
            {
                JosePictureNumber = JosePictureNumber + 1;
                if (JosePictureNumber == 19)
                    JosePictureNumber = 0;
                SetJose(JosePictureNumber);
            }
            return base.ProcessCmdKey(ref msg, KeyCode);
        }

        private void DriversInstall_Click(object sender, EventArgs e)
        {
            MakeProgress(10);
            AlligatorConsole.Text += "\r\nI've just executed LG Mobile Driver installer...";
            RunProgram.RunExe(DriversPath, false, true);
            MakeProgress(0);
        }

        private void AlligatorForm_Load(object sender, EventArgs e)
        {
            //Generowanie ścieżki dostępu do katalogu Alligator Toolkita
            SHData = "Sees Hard";
            SHaData = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), SHData);
            AlligatorDIR = SHaData + "\\Alligator Toolkit";

            //Generowanie pozostałych ścieżek
            AppData = AlligatorDIR;
            CompressedAdbPath = AppData + "\\adb.zip";
            DownloadedInfo = AppData + "\\Downloaded.alg";
            TempDir = AppData + "\\Temp";
            G2Security = AppData + "\\g2_security";
            SuperuserAPK = AppData + "\\Superuser.apk";
            LGRootSH = AppData + "\\lg_root.sh";
            SUFile = AppData + "\\su";
            SULPZIP = AppData + "\\UPDATE-SuperSU-v2.46.zip";
            DMBusybox = AppData + "\\busybox";
            DMLeave = AppData + "\\leaveDownload";
            DMEnter = AppData + "\\enterDownload";
            DMRoot = AppData + "\\rootDownload";
            TempedSideloadZipPath = TempDir + "\\sideload.zip";
            datrootPath = AppData + "\\datroot.zip";
            BumperPath = AppData + "\\Bumping";
            SndCmdExe = AppData + "\\SC.exe";
            SndCmd2 = AppData + "\\Send_Command.exe";
            DriversPath = AppData + "\\LGDriver.exe";
            adb = TempDir + "\\adb.exe";
            OneClickExe = AppData + "\\OneClick.bat";
            ZippingDIR = AppData + "\\Zipping";
            BumpingBinary = BumperPath + "\\bump.exe";
            DirOfDeath = AppData + "\\Death";
            SelfKiller = DirOfDeath + "\\killer.bat";
            CompressedDeath = DirOfDeath + "\\killer.zip";
            BTWRPPath = AppData + "\\BTWRP.img";
            PhilZPath = AppData + "\\PhilZ.img";
            OfcTWRPPath = AppData + "\\OfcTwrp.img";
            TempedRCK = TempDir + "\\recovery.img";
            TempedOwnRCK = TempDir + "\\OwnRck.img";
            BetaFile = AppData + "\\beta.alg";
            HydraDownloader = SHaData + "\\Hydra Downloader";
            FilesD802 = AppData + "\\D802 Files";
            BLDirOriginalLollipop = FilesD802 + "\\Lollipop stock";
            BLDirBumpLollipop = FilesD802 + "\\Lollipop bump";
            BLDirLokiLollipop = FilesD802 + "\\Lollipop Loki";
            BLDirKitKat = FilesD802 + "\\KitKat";
            BLDirJellyBean = FilesD802 + "\\JellyBean";
            HydraEXE = AppData + "\\Hydra.exe";
            MetaZip = ZippingDIR + "\\MetaZip.zip";
            AlligatorVER = AppData + "\\AlligatorVER.alg";


            //Uruchamianie silnika interpretacyjnego, jeżeli katalog istnieje.
            if (System.IO.Directory.Exists(AppData))
            {
                //Silnik interpretacyjny pliku "Downloaded.alg"
                DownloadedLenght = File.ReadAllLines(DownloadedInfo).Count();
                DownloadedTable = new string[DownloadedLenght];
                DownloadedTable = File.ReadAllLines(DownloadedInfo);
                foreach (string ElementPobrany in DownloadedTable)
                {
                    if (ElementPobrany == "D802-Bootloaders")
                    {
                        DownloadedBootloaders();
                    }

                    if (ElementPobrany.Contains("Version Number "))
                    {
                        string StrInt;
                        StrInt = ElementPobrany.Replace("Version Number ", null);
                        IntStr = Convert.ToInt32(StrInt);
                        IntStr = int.Parse(StrInt);
                        if (IntStr > ControlVersionNumber)
                        {
                            MessageBox.Show("You can't use old Alligator Toolkit, after You used newer version! Please run the newest Alligator Toolkit!", "ERROR!");
                            Application.Exit();
                        }
                        //Aktualizujemy pliki Toolkita
                        if (IntStr < ControlVersionNumber)
                        {
                            File.WriteAllBytes(SndCmdExe, Properties.Resources.Send_Command);
                            File.WriteAllBytes(SndCmd2, Properties.Resources.Send_Command);
                            File.AppendAllText(OneClickExe, Properties.Resources.LGOneClickRoot);
                            File.WriteAllBytes(LGRootSH, Properties.Resources.lg_root);
                            File.WriteAllBytes(G2Security, Properties.Resources.g2_security);
                            File.WriteAllBytes(SUFile, Properties.Resources.su);
                            File.WriteAllBytes(SuperuserAPK, Properties.Resources.Superuser);
                            File.WriteAllBytes(datrootPath, Properties.Resources.datroot);
                            File.WriteAllBytes(SULPZIP, Properties.Resources.SuperSULP);
                            File.WriteAllBytes(DMRoot, Properties.Resources.root);
                            File.WriteAllBytes(DMLeave, Properties.Resources.leaveDownload);
                            File.WriteAllBytes(DMEnter, Properties.Resources.enterDownload);
                            File.WriteAllBytes(DMBusybox, Properties.Resources.busybox);
                            ZipFile.ExtractToDirectory(CompressedAdbPath, AppData);
                            InvoAC("Toolkit files has been updated!");
                        }
                    }
                }
            }

            //Tworzenie katalogu, jeżeli nie istnieje
            if (System.IO.Directory.Exists(AppData))
            {
                Cleaning();
            }
            else
            {
                System.IO.Directory.CreateDirectory(DirOfDeath);
                System.IO.Directory.CreateDirectory(AppData);
                System.IO.Directory.CreateDirectory(TempDir);
                System.IO.Directory.CreateDirectory(BumperPath);
                Directory.CreateDirectory(HydraDownloader);
                Directory.CreateDirectory(FilesD802);
                File.WriteAllBytes(SndCmd2, Properties.Resources.Send_Command);
                File.WriteAllBytes(CompressedAdbPath, Properties.Resources.adb);
                File.WriteAllBytes(CompressedDeath, Properties.Resources.killer);
                File.WriteAllBytes(BTWRPPath, Properties.Resources.BTWRP);
                File.WriteAllBytes(LGRootSH, Properties.Resources.lg_root);
                File.AppendAllText(OneClickExe, Properties.Resources.LGOneClickRoot);
                File.WriteAllBytes(PhilZPath, Properties.Resources.PhilZ);
                File.WriteAllBytes(BumperPath + "\\zipped.zip", Properties.Resources.bumper);
                File.WriteAllBytes(HydraEXE, Properties.Resources.Hydra);
                ZipFile.ExtractToDirectory(CompressedDeath, DirOfDeath);
                ZipFile.ExtractToDirectory(BumperPath + "\\zipped.zip", BumperPath);
                ZipFile.ExtractToDirectory(CompressedAdbPath, TempDir);
                ZipFile.ExtractToDirectory(CompressedAdbPath, DirOfDeath);
                ZipFile.ExtractToDirectory(CompressedAdbPath, AppData);
                File.WriteAllBytes(DriversPath, Properties.Resources.LGDriver);
                File.WriteAllBytes(BetaFile, Properties.Resources.Beta);
                File.WriteAllBytes(SndCmdExe, Properties.Resources.Send_Command);
                File.WriteAllBytes(G2Security, Properties.Resources.g2_security);
                File.WriteAllBytes(SULPZIP, Properties.Resources.SuperSULP);
                File.WriteAllBytes(DMRoot, Properties.Resources.root);
                File.WriteAllBytes(DMLeave, Properties.Resources.leaveDownload);
                File.WriteAllBytes(DMEnter, Properties.Resources.enterDownload);
                File.WriteAllBytes(DMBusybox, Properties.Resources.busybox);
                File.WriteAllBytes(SuperuserAPK, Properties.Resources.Superuser);
                File.WriteAllBytes(datrootPath, Properties.Resources.datroot);
                File.WriteAllBytes(SUFile, Properties.Resources.su);
                File.Delete(BumperPath + "\\zipped.zip");

                Cleaning();
            }

            //Tworzenie pliku "Downloaded.alg"
            if (IntStr < ControlVersionNumber)
                File.Delete(DownloadedInfo);
            using (StreamWriter DCW = File.CreateText(DownloadedInfo))
            {
                DCW.WriteLine("Sees Hard Alligator Toolkit " + CurrentVersion);
                if (IsItBeta == true)
                    DCW.WriteLine("BETA " + BetaVersion);
                DCW.WriteLine("Version Number " + ControlVersionNumber);
                if (AreBootloadersDownloaded == true)
                    DCW.WriteLine("D802-Bootloaders");
            }

            ConnectionPicture.Image = Properties.Resources.AlligatorUSB;
            ConnectionStatus.Image = Properties.Resources.ConNot;
            ConOkLabel.Visible = false;
            ConNotLabel.Visible = true;
            WiFiConnection.Enabled = true;
            USBConnection.Enabled = false;
            IPBox.Visible = false;
            DevicesBtn.Visible = true;
            AreBootloadersDownloaded = false;
            DevicesBtn.Enabled = true;
            IPBoxLabel.Visible = false;
            CvWiFi.Visible = false;
            IsToolkitDisabled = false;

            //Inicjowanie ProgressBarów
            MainProgressBar.Minimum = 0;
            MainProgressBar.Maximum = 10;
            RecoveryProgressBar.Minimum = 0;
            RecoveryProgressBar.Maximum = 10;
            OtherProgress.Minimum = 0;
            OtherProgress.Maximum = 10;
            RecoveryProgressBar.ForeColor = Color.SeaGreen;

            //Losowanie zdjęcia z Jose
            JosePictureNumber = new Random().Next(0, 18);
            SetJose(JosePictureNumber);

            //Nadawanie wartości poszczególnym zmiennym
            PortLG = "Jose Mourinho!";

            //Sprawdzanie zezwolenia na aktualizacje beta
            if (File.Exists(BetaFile) == true)
            {
                IsBetaAllowed = true;
                BetaSwitchBtn.Text = "Disable beta updates";
            }
            else
            {
                IsBetaAllowed = false;
                BetaSwitchBtn.Text = "Enable beta updates";
            }

            //Zabijanie ADB
            RunProgram.RunParaExe(adb, "kill-server", true, false);

            //Sprawdzanie połączenia z internetem
            if (InternetConnection.PingHttp("www.google.com") == true)
            {
                Update_ENGINE();
            }
            else
                MessageBox.Show("Internet connections are not available");
        }

        private void Cleaning()
        {
            RunProgram.RunParaExe(adb, "kill-server", true, false);
            Thread.Sleep(1000);
            Directory.Delete(TempDir, true);
            Directory.CreateDirectory(TempDir);
            ZipFile.ExtractToDirectory(CompressedAdbPath, TempDir);
        }

        private void SetJose(int PicNum)
        {
            switch (PicNum)
            {
                case 0:
                    JoseBox.Image = Properties.Resources.jose0;
                    break;
                case 1:
                    JoseBox.Image = Properties.Resources.jose1;
                    break;
                case 2:
                    JoseBox.Image = Properties.Resources.jose2;
                    break;
                case 3:
                    JoseBox.Image = Properties.Resources.jose3;
                    break;
                case 4:
                    JoseBox.Image = Properties.Resources.jose4;
                    break;
                case 5:
                    JoseBox.Image = Properties.Resources.jose5;
                    break;
                case 6:
                    JoseBox.Image = Properties.Resources.Jose6;
                    break;
                case 7:
                    JoseBox.Image = Properties.Resources.jose7;
                    break;
                case 8:
                    JoseBox.Image = Properties.Resources.jose8;
                    break;
                case 9:
                    JoseBox.Image = Properties.Resources.jose9;
                    break;
                case 10:
                    JoseBox.Image = Properties.Resources.jose10;
                    break;
                case 11:
                    JoseBox.Image = Properties.Resources.jose11;
                    break;
                case 12:
                    JoseBox.Image = Properties.Resources.jose12;
                    break;
                case 13:
                    JoseBox.Image = Properties.Resources.jose13;
                    break;
                case 14:
                    JoseBox.Image = Properties.Resources.jose14;
                    break;
                case 15:
                    JoseBox.Image = Properties.Resources.jose15;
                    break;
                case 16:
                    JoseBox.Image = Properties.Resources.jose16;
                    break;
                case 17:
                    JoseBox.Image = Properties.Resources.jose17;
                    break;
                case 18:
                    JoseBox.Image = Properties.Resources.jose18;
                    break;

                default:
                    JoseBox.Image = Properties.Resources.jose3;
                    break;
            }
        }

        private void DownloadedBootloaders()
        {
            InfoBox.Text = "You've already downloaded bootloaders for D802! :)";
            InfoBox.ForeColor = Color.SeaGreen;
            BootDwnBtnD802.Visible = false;
            BootloadersBox.Enabled = true;
            AreBootloadersDownloaded = true;
            ChoosenBootloaderStartBtn.Visible = true;
        }

        private void USBConnection_Click(object sender, EventArgs e)
        {
            ConnectionPicture.Image = Properties.Resources.AlligatorUSB;
            WiFiConnection.Enabled = true;
            USBConnection.Enabled = false;
            IPBox.Visible = false;
            IPBoxLabel.Visible = false;
            CvWiFi.Visible = false;
            DevicesBtn.Visible = true;
            DevicesBtn.Enabled = true;
        }

        private void WiFiConnection_Click(object sender, EventArgs e)
        {
            ConnectionPicture.Image = Properties.Resources.AlligatorWiFi;
            USBConnection.Enabled = true;
            WiFiConnection.Enabled = false;
            IPBox.Visible = true;
            IPBoxLabel.Visible = true;
            CvWiFi.Visible = true;
            DevicesBtn.Visible = false;
            DevicesBtn.Enabled = false;
        }

        private void CvWiFiBtnMethod()
        {
            MakeProgress(1);
            if (string.IsNullOrEmpty(IPBox.Text))
            {
                MakeProgress(0);
                MessageBox.Show("Give me Your IP firstly!", "You didn't bring me IP");
            }
            else
            {
                IPAdress = IPBox.Text;
                RunProgram.RunParaExe(adb, "connect " + IPAdress, true, false);
                MakeProgress(2);
                if (RunProgram.HangOutput.Contains("connected"))
                {
                    Connected();
                    InvoAC("Successfully connected Android device via WiFi!");
                    IsItUSB = false;
                    MakeProgress(3);
                    PhoneInfoCounter();
                    MakeProgress(0);
                }
            }
        }

        private void CvWiFi_Click(object sender, EventArgs e)
        {
            MultiKulti = new Thread(CvWiFiBtnMethod);
            MultiKulti.Start();
        }

        private void PhoneInfoCounter()
        {
            MakeProgress(4);

            //Liczniki prób
            int ModelCounter = 0;
            int SDKCounter = 0;

            //Pobieramy model urządzenia
            AndroidPhoneInformations.ModelInfo();
            if (AndroidPhoneInformations.ModelState == true)
            {
                MakeProgress(6);
                InvoAC(AndroidPhoneInformations.RoModel);
                ModelLabel.Invoke((MethodInvoker)(() => ModelLabel.Text = AndroidPhoneInformations.LabelRoModel));
            }
            else
            {
                while (ModelCounter <= 5)
                {
                    MakeProgress(5);
                    AndroidPhoneInformations.ModelInfo();
                    if (AndroidPhoneInformations.ModelState == true)
                    {
                        MakeProgress(6);
                        InvoAC(AndroidPhoneInformations.RoModel);
                        ModelLabel.Invoke((MethodInvoker)(() => ModelLabel.Text = AndroidPhoneInformations.LabelRoModel));
                        break;
                    }
                    ModelCounter++;
                    if (ModelCounter == 6)
                    {
                        InvoAC("I was unable to get Your device model!");
                        ModelLabel.Invoke((MethodInvoker)(() => ModelLabel.Text = "UNKOWN"));
                    }
                }
            }

            //Pobieramy wersję Androida
            AndroidPhoneInformations.SDKInfo();
            if (AndroidPhoneInformations.SDKState == true)
            {
                MakeProgress(8);
                InvoAC(AndroidPhoneInformations.RoSDK);
                SDKLabel.Invoke((MethodInvoker)(() => SDKLabel.Text = AndroidPhoneInformations.LabelRoSDK));
                RootSDKLabel.Invoke((MethodInvoker)(() => RootSDKLabel.Text = RootStringSDK));
            }
            else
            {
                while (SDKCounter <= 5)
                {
                    MakeProgress(7);
                    AndroidPhoneInformations.SDKInfo();
                    if (AndroidPhoneInformations.SDKState == true)
                    {
                        MakeProgress(8);
                        InvoAC(AndroidPhoneInformations.RoSDK);
                        SDKLabel.Invoke((MethodInvoker)(() => SDKLabel.Text = AndroidPhoneInformations.LabelRoSDK));
                        break;
                    }
                    ModelCounter++;
                    if (ModelCounter == 6)
                    {
                        InvoAC("I was unable to get Your Android version!");
                        SDKLabel.Invoke((MethodInvoker)(() => SDKLabel.Text = "UNKNOWN"));
                    }
                }

                //Pobieramy port urządzenia
                string COMloc;
                COMloc = GetLocationCOM();
                COMLabel.Invoke((MethodInvoker)(() => COMLabel.Text = COMloc));
                InvoAC("I've found Your phone at " + COMloc);
            }

            //Sprawdzamy poziom uprawnień na urządzeniu
            if (AndroidPhoneInformations.SDKState == true)
            {
                AndroidPhoneInformations.PermsLevelInfo();


                if (AndroidPhoneInformations.PermissionsState == true)
                {
                    MakeProgress(10);
                    InvoAC("Your device is already ROOTED!");
                    RootLabel.Invoke((MethodInvoker)(() => RootLabel.Text = "ROOTED"));
                    IsItRooted = true;
                }
                else
                {
                    MakeProgress(10);
                    InvoAC("Your device isn't ROOTED!");
                    RootLabel.Invoke((MethodInvoker)(() => RootLabel.Text = "Non-Rooted"));
                    IsItRooted = false;
                }
            }
            else
            {
                MakeProgress(10);
                InvoAC("I'm unable to get permissions level of Your device!");
                RootLabel.Invoke((MethodInvoker)(() => RootLabel.Text = "UNKNOWN"));
                IsItRooted = false;
            }

            //Na samym końcu resetujemy ProgressBary
            Thread.Sleep(2500);
            MakeProgress(0);
        }

        private void DevicesMethod()
        {
            RunProgram.RunParaExe(adb, "start-server", true, false);
            RunProgram.RunParaExe(adb, "devices", true, false);
            RunProgram.HangOutput = RunProgram.HangOutput.Replace("List of devices attached", null);
            if (!RunProgram.HangOutput.Contains("daemon"))
                RunProgram.RunParaExe(adb, "devices", true, false);
            if (RunProgram.HangOutput.Contains("."))
            {
                IsItUSB = false;
            }
            else
            {
                IsItUSB = true;
            }

            if (RunProgram.HangOutput.Contains("device"))
            {
                Connected();
                PhoneInfoCounter();
            }
            else
            {
                InvoAC("I am not able to find any connected Android device!");
                DisConnected();
            }
        }

        private void devices_Click(object sender, EventArgs e)
        {
            IsItUSB = true;
            MultiKulti = new Thread(DevicesMethod);
            MultiKulti.Start();
            MakeProgress(0);
        }

        private void MakeProgress(int stan)
        {
            MainProgressBar.Invoke((MethodInvoker)(() => MainProgressBar.Value = stan));
            RecoveryProgressBar.Invoke((MethodInvoker)(() => RecoveryProgressBar.Value = stan));
            BootloaderProgress.Invoke((MethodInvoker)(() => BootloaderProgress.Value = stan));
            RootProgress.Invoke((MethodInvoker)(() => RootProgress.Value = stan));
            OtherProgress.Invoke((MethodInvoker)(() => OtherProgress.Value = stan));
        }

        private void KillServerBtn_Click(object sender, EventArgs e)
        {
            RunProgram.RunParaExe(adb, "kill-server", true, false);
            if (RunProgram.HangOutput == "")
                AlligatorConsole.Text += "\r\nSuccsessfull stoped ADB Server!";
            else
                AlligatorConsole.Text += "\r\nSomething gone wrong while killing ADB!";
            if (IsItConnected == true)
            {
                DisConnected();
            }
        }

        private void AlligatorConsole_TextChanged(object sender, EventArgs e)
        {
            AlligatorConsole.SelectionStart = AlligatorConsole.Text.Length;
            AlligatorConsole.ScrollToCaret();
        }

        private void Connected()
        {
            IsItConnected = true;
            ConnectionStatus.Invoke((MethodInvoker)(() => ConnectionStatus.Image = Properties.Resources.ConOk));
            ConOkLabel.Invoke((MethodInvoker)(() => ConOkLabel.Visible = true));
            ConNotLabel.Invoke((MethodInvoker)(() => ConNotLabel.Visible = false));
            InvoAC("Device is already connected!");
        }

        private void DisConnected()
        {
            IsItConnected = false;
            ConnectionStatus.Invoke((MethodInvoker)(() => ConnectionStatus.Image = Properties.Resources.ConNot));
            ConOkLabel.Invoke((MethodInvoker)(() => ConOkLabel.Visible = false));
            ConNotLabel.Invoke((MethodInvoker)(() => ConNotLabel.Visible = true));
            InvoAC("Disconnected Android device!");
            //ModelLabel.Text = " ";
            ModelLabel.Invoke((MethodInvoker)(() => ModelLabel.Text = " "));
            //SDKLabel.Text = " ";
            SDKLabel.Invoke((MethodInvoker)(() => SDKLabel.Text = " "));
            //RootLabel.Text = " ";
            RootLabel.Invoke((MethodInvoker)(() => RootLabel.Text = " "));
        }

        private void FlashRckBtn_Click(object sender, EventArgs e)
        {
            if (IsItConnected == true)
            {
                DisableToolkit();
                //TWRP Blastagatora
                if (RadioBlastTWRP.Checked == true)
                {
                    MakeProgress(1);
                    AlligatorConsole.Text += "\r\nI'm preparing TWRP recovery flash process...";
                    File.Copy(BTWRPPath, TempedRCK, true);
                    MakeProgress(2);
                    MultiKulti = new Thread(() => Copy2SD("BumpedRecovery"));
                    MultiKulti.Start();
                }

                else
                {
                    //ClockWorkMod recovery od PhilZa
                    if (RadioPhilz.Checked == true)
                    {
                        MakeProgress(1);
                        AlligatorConsole.Text += "\r\nI'm preparing PhilZ recovery flash process...";
                        File.Copy(PhilZPath, TempedRCK, true);
                        MakeProgress(2);
                        MultiKulti = new Thread(() => Copy2SD("BumpedRecovery"));
                        MultiKulti.Start();
                    }

                    else
                    {
                        EnableToolkit();
                        InvoAC("Error! Please choose recovery to install!");
                        MessageBox.Show("Please choose recovery to install! I won't make for You special recovery, becouse You're to lazy to choose one of prepared recoveries! :P", "ERROR!");
                    }
                }
            }

            else
            {
                EnableToolkit();
                MessageBox.Show("Please connect Your device first!", "ERROR!");
                InvoAC("Error! Device isn't connected!");
            }
        }

        private void Copy2SD(string what)
        {
            if (what == "BumpedRecovery")
            {
                MakeProgress(3);
                RunProgram.RunParaExe(adb, "push " + q + TempedRCK + q + " /sdcard/Alligator/recovery.img", true, false);
                MakeProgress(4);
                FlashBumpedRCK();
            }

            if (what == "OwnRecovery")
            {
                MakeProgress(3);
                RunProgram.RunParaExe(adb, "push " + q + TempedOwnRCK + q + " /sdcard/Alligator/recovery.img", true, false);
                MakeProgress(4);
                FlashBumpedRCK();
            }
        }

        private void FlashBumpedRCK()
        {
            AlligatorConsole.Invoke((MethodInvoker)(() => AlligatorConsole.Text += "\r\nI'm flashing recovery..."));
            //AlligatorConsole.Text += "\r\nI'm flashing recovery...";
            string BCommand = "shell " + q + "su -c 'dd if=/mnt/shell/emulated/0/Alligator/recovery.img of=/dev/block/platform/msm_sdcc.1/by-name/recovery'" + q;
            RunProgram.RunParaExe(adb, BCommand, true, false);
            AlligatorConsole.Invoke((MethodInvoker)(() => AlligatorConsole.Text += "\r\nNew recovery is installed!"));
            //AlligatorConsole.Text += "\r\nNew recovery is installed!";
            MakeProgress(7);
            if (IsItUSB == true)
                RunProgram.RunParaExe(adb, "reboot", true, false);
            AlligatorConsole.Invoke((MethodInvoker)(() => AlligatorConsole.Text += "\r\nI'm waiting for Your device..."));
            //AlligatorConsole.Text += "\r\nI'm waiting for Your device...";
            RunProgram.RunParaExe(adb, "shell rm -r /sdcard/Alligator/", true, false);
            MakeProgress(8);
            RunProgram.RunParaExe(adb, "wait-for-device", true, false);
            MakeProgress(9);
            if (IsItUSB == true)
                RunProgram.RunParaExe(adb, "reboot recovery", true, false);
            AlligatorConsole.Invoke((MethodInvoker)(() => AlligatorConsole.Text += "\r\nRecovery istalation process is finished!"));
            if (IsItUSB == false)
                InvoAC("Now please reboot Your device, cause I'm unable to do it, when You're connected to Toolkit by Wi-Fi!");
            //AlligatorConsole.Text += "\r\nRecovery istalation process is finished!";
            EnableToolkit();
            MakeProgress(10);
            Thread.Sleep(2000);
            MakeProgress(0);
        }

        public void InvoAC(string Komunikat)
        {
            AlligatorConsole.Invoke((MethodInvoker)(() => AlligatorConsole.Text += "\r\n" + Komunikat));
        }

        private void ChooseRecovery_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void ChooseRecoveryImgButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ChooseRecovery = new OpenFileDialog();
            ChooseRecovery.Filter = "Image Files|*.img";
            ChooseRecovery.Title = "Select a recovery image";
            if (ChooseRecovery.ShowDialog() == DialogResult.OK)
            {
                //Pobieranie nazwy pliku *.img oraz ścieżki do niego
                OwnRckPath = ChooseRecovery.FileName;
                OwnRckName = ChooseRecovery.SafeFileName;

                //Przenoszenie pliku do tempa w AppData
                System.IO.File.Copy(OwnRckPath, TempedOwnRCK, true);
                RckImgPathBox.Text = OwnRckPath;

                //Odblokowanie przycisku flashowania i instalacji.
                FlashOwnRckBtn.Enabled = true;
                BumpRckImgBtn.Enabled = true;
            }
        }

        private void FlashOwnRckBtn_Click(object sender, EventArgs e)
        {
            if (IsItConnected == true)
            {
                DisableToolkit();
                AlligatorConsole.Text += "\r\nI'm preparig recovery flash process!";
                MultiKulti = new Thread(() => Copy2SD("OwnRecovery"));
                MultiKulti.Start();
            }

            else
            {
                MessageBox.Show("Please connect Your device first!", "ERROR!");
                InvoAC("Error! Device isn't connected!");
            }
        }

        private void Update_ENGINE()
        {
            int LinesNumber;
            int NewestBetaVersion = 0;
            string[] LinesCache;
            string NewVersionController = "";
            string NewVersionLevel = "";
            string NewVersionSLink = "";
            string NewVersionBLink = "";
            string BetaVerController = "";
            string BetaVerLevel = "";
            bool IsBetaAvailble = false;

            //Tworzenie skryptu Hydra
            using (StreamWriter sw = File.CreateText(HydraDownloader + "\\metadata.hya"))
            {
                sw.WriteLine("Manifest = Alligator Toolkit");
                sw.WriteLine("Dest = " + TempDir + "\\Alligator-Updater.alg");
                sw.WriteLine("URL = " + "https://drive.google.com/uc?export=download&id=0B0FPF13M3TkAYkpYeXk4TXJSZjg");
                sw.WriteLine("FileSize = " + "0");
                sw.WriteLine("TextLabel = " + "Checking updates for Toolkit...");
                sw.WriteLine("DownloadID = " + "aktualizator");
            }

            //Wykonanie Hydry
            RunProgram.RunExe(HydraEXE, true, false);

            if (File.Exists(TempDir + "\\Alligator-Updater.alg") == false)
            {
                MessageBox.Show("Something's gone wrong! If this problem won't disappear, pleas send me e-mail - equablepanic4@hotmail.com");
                return;
            }
            else
            {
                LinesNumber = File.ReadLines(TempDir + "\\Alligator-Updater.alg").Count();
                LinesCache = new string[LinesNumber];
                LinesCache = File.ReadAllLines(TempDir + "\\Alligator-Updater.alg");
                foreach (string ElementA in LinesCache)
                {
                    //Wersjia toolkita na serwerze (na przykład v1.1)
                    if (ElementA.Contains("AlligatorVersion = "))
                    {
                        NewVersionLevel = ElementA.Replace("AlligatorVersion = ", null);
                    }
                    //Numer toolkita (na przykład 4)
                    if (ElementA.Contains("AlligatorVersionController = "))
                    {
                        NewVersionController = ElementA.Replace("AlligatorVersionController = ", null);
                    }
                    //Link do najnowszej stabilnej wersji
                    if (ElementA.Contains("StableRelease = "))
                    {
                        NewVersionSLink = ElementA.Replace("StableRelease = ", null);
                    }
                    //Numer kontrolny najnowszej wersji beta
                    if (ElementA.Contains("BetaVersionController = "))
                    {
                        BetaVerController = ElementA.Replace("BetaVersionController = ", null);
                        IsBetaAvailble = true;
                    }

                    //Link do najnowszej bety
                    if (ElementA.Contains("BetaRelease = "))
                    {
                        NewVersionBLink = ElementA.Replace("BetaRelease = ", null);
                    }

                    //Wersja toolkita w przypadku Bety
                    if (ElementA.Contains("BetaVerLevel = "))
                    {
                        BetaVerLevel = ElementA.Replace("BetaVerLevel = ", null);
                    }
                }
                //Zmiana numeru wersji z pliku "aktualizator" na integer.
                int NewestVersion;
                NewestVersion = Convert.ToInt32(NewVersionController);
                NewestVersion = int.Parse(NewVersionController);

                //Zmiana numeru bety z aktualizatora na integer.
                if (IsBetaAvailble == true)
                {
                    NewestBetaVersion = Convert.ToInt32(BetaVerController);
                    NewestBetaVersion = int.Parse(BetaVerController);
                }

                //Określanie czy jest dostępna jakaś beta - Beta jest zawsze nowsza od wydania oficjalnego.
                if (NewestBetaVersion != 0)
                {
                    if (IsBetaAllowed == true)
                    {
                        if (BetaVersion < NewestBetaVersion)
                        {
                            DialogResult dialogResult = MessageBox.Show("Beta " + BetaVerController + " of Alligator Toolkit " + BetaVerLevel + " is availble! Do You want to download it?", "Beta update is availble!", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                SaveFileDialog SaveNewToolkitVer = new SaveFileDialog();
                                SaveNewToolkitVer.Filter = "Alligator Toolkit Executable|*.exe";
                                SaveNewToolkitVer.Title = "Save new version of Alligator Toolkit";
                                SaveNewToolkitVer.ShowDialog();
                                if (SaveNewToolkitVer.FileName != "")
                                {
                                    UpdateExePath = SaveNewToolkitVer.FileName;
                                    using (StreamWriter sw = File.CreateText(HydraDownloader + "\\metadata.hya"))
                                    {
                                        sw.WriteLine("Manifest = Alligator Toolkit");
                                        sw.WriteLine("Dest = " + UpdateExePath);
                                        sw.WriteLine("URL = " + NewVersionBLink);
                                        sw.WriteLine("FileSize = " + "0");
                                        sw.WriteLine("TextLabel = " + "Downloading new Alligator Toolkit beta!");
                                        sw.WriteLine("DownloadID = " + "BetaToolkit");
                                    }
                                    RunProgram.RunExe(HydraEXE, false, true);
                                    Application.Exit();
                                }
                            }

                            if (dialogResult == DialogResult.No)
                            {
                                InvoAC("You didn't accept beta update.");
                            }
                        }
                    }
                }

                //Określanie czy jest dostępna nowa wersja oficjalnego wydania toolkita.
                if (ControlVersionNumber < NewestVersion)
                {
                    DialogResult dialogResult = MessageBox.Show("An official update to Alligator Toolkit " + NewVersionLevel + " is availble! Do You want to download it??", "Official Alligator Toolkit update is availble!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SaveFileDialog SaveNewToolkitVer = new SaveFileDialog();
                        SaveNewToolkitVer.Filter = "Alligator Toolkit Executable|*.exe";
                        SaveNewToolkitVer.Title = "Save new version of Alligator Toolkit";
                        SaveNewToolkitVer.ShowDialog();
                        if (SaveNewToolkitVer.FileName != "")
                        {
                            UpdateExePath = SaveNewToolkitVer.FileName;
                            using (StreamWriter sw = File.CreateText(HydraDownloader + "\\metadata.hya"))
                            {
                                sw.WriteLine("Manifest = Alligator Toolkit");
                                sw.WriteLine("Dest = " + UpdateExePath);
                                sw.WriteLine("URL = " + NewVersionSLink);
                                sw.WriteLine("FileSize = " + "0");
                                sw.WriteLine("TextLabel = " + "Downloading new Alligator Toolkit!");
                                sw.WriteLine("DownloadID = " + "OfficialToolkit");
                            }
                            RunProgram.RunExe(HydraEXE, false, true);
                            Application.Exit();
                        }
                    }

                    if (dialogResult == DialogResult.No)
                    {
                        InvoAC("You didn't accept official update.");
                    }
                }
            }
        }

        private void RebootBtn_Click(object sender, EventArgs e)
        {
            if (IsItConnected == true)
            {
                if (IsItUSB == true)
                {
                    ADB_Reboot(true);
                }
                else
                {
                    MultiKulti = new Thread(() => ADB_Reboot(false));
                    MultiKulti.Start();
                }
            }
            else
                InvoAC("To reboot your phone, You must firstly connect it to Alligator Toolkit. :P");
        }

        private void RebootRckBtn_Click(object sender, EventArgs e)
        {
            if (IsItConnected == true)
            {
                if (IsItUSB == true)
                {
                    ADB_RebootRecovery();
                }
                else
                {
                    MessageBox.Show("Sorry, but now rebooting doesn't work for wifi connection. :(", "Function is unavailble!");
                }
            }
            else
                InvoAC("To reboot your phone, You must firstly connect it to Alligator Toolkit. :P");
        }

        private void DownAdd(string WhatToAdd)
        {

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////XXXXXX////////////DDDDDDDDD////////////BBBBBBBBBB///////////////////////////
        //////////////////////X//////X///////////D////////D///////////B/////////B//////////////////////////
        //////////////////////XXXXXXXX///////////D////////D///////////B/////////B//////////////////////////
        //////////////////////X//////X///////////D////////D///////////BBBBBBBBBB///////////////////////////
        //////////////////////X//////X///////////D////////D///////////B//////////B/////////////////////////
        //////////////////////X//////X///////////D///////D////////////B//////////B/////////////////////////
        //////////////////////X//////X///////////DDDDDDDD/////////////BBBBBBBBBBB//////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void ADB_Reboot(bool OverUSB)
        {
            if (OverUSB == true)
            {
                RunProgram.RunParaExe(adb, "reboot", true, false);
                InvoAC("Rebooted!");
            }
            else
            {
                RunProgram.RunParaExe(adb, "reboot", false, false);
                Thread.Sleep(3000);
                InvoAC("Rebooted!");
                Thread.ResetAbort();
            }
        }

        private void ADB_RebootRecovery()
        {
            RunProgram.RunParaExe(adb, "reboot recovery", false, false);
            InvoAC("Rebooted!");
        }

        private void ShellSU(string polecenie_powłoki)
        {
            string KompletnaKomenda = "shell " + q + "su -c '" + polecenie_powłoki + "'" + q;
            RunProgram.RunParaExe(adb, KompletnaKomenda, true, false);
        }

        private void Push_Alligator(string WhatToPush, string NameAfterPushing, bool ToAlligatorDir)
        {
            if (ToAlligatorDir == true)
                RunProgram.RunParaExe(adb, "push " + q + WhatToPush + q + " /sdcard/Alligator/" + NameAfterPushing, true, false);

            if (ToAlligatorDir == false)
                RunProgram.RunParaExe(adb, "push " + q + WhatToPush + q + " " + NameAfterPushing, true, false);
        }

        private void ADB_WaitForExit()
        {
            RunProgram.RunParaExe(AlligatorForm.adb, "wait-for-device", true, false);
        }

        private void BootDwnBtnD802_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = File.CreateText(HydraDownloader + "\\metadata.hya"))
            {
                sw.WriteLine("Manifest = Alligator Toolkit");
                sw.WriteLine("Dest = " + FilesD802 + "\\D802Pack.zip");
                sw.WriteLine("URL = " + "https://onedrive.live.com/download?resid=BDCABFA14D6BDAC8!3108&authkey=!AC0VLbOOqcazZHo&ithint=file%2czip");
                sw.WriteLine("FileSize = " + "0");
                sw.WriteLine("TextLabel = " + "Downloading bootloaders for D802...");
                sw.WriteLine("DownloadID = " + "D802BootloadersPack");
            }
            MakeProgress(2);
            InvoAC("Executing Hydra...");
            RunProgram.RunExe(HydraEXE, true, true);

            //Sprawdzanie czy plik został poprawnie pobrany
            if (File.Exists(HydraDownloader + "\\CompletedD802BootloadersPack.hya"))
            {
                MakeProgress(6);
                InvoAC("Content has been downloaded properly!");
                InvoAC("I'm preparing downloaded content...");

                //Przygotowanie bootloaderów
                ZipFile.ExtractToDirectory(FilesD802 + "\\D802Pack.zip", FilesD802);
                Directory.CreateDirectory(FilesD802 + "\\JellyBean");
                Directory.CreateDirectory(FilesD802 + "\\KitKat");
                Directory.CreateDirectory(FilesD802 + "\\Lollipop bump");
                Directory.CreateDirectory(FilesD802 + "\\Lollipop Loki");
                Directory.CreateDirectory(FilesD802 + "\\Lollipop stock");
                MakeProgress(7);
                ZipFile.ExtractToDirectory(FilesD802 + "\\AlligatorD802JBBL.zip", FilesD802 + "\\JellyBean");
                ZipFile.ExtractToDirectory(FilesD802 + "\\AlligatorD802KKBL.zip", FilesD802 + "\\KitKat");
                ZipFile.ExtractToDirectory(FilesD802 + "\\AlligatorD802LPBumpBL.zip", FilesD802 + "\\Lollipop bump");
                ZipFile.ExtractToDirectory(FilesD802 + "\\AlligatorD802LPLokiBL.zip", FilesD802 + "\\Lollipop Loki");
                ZipFile.ExtractToDirectory(FilesD802 + "\\AlligatorD802LPStockBL.zip", FilesD802 + "\\Lollipop stock");
                MakeProgress(8);
                InvoAC("Removing temporary files...");
                File.Delete(FilesD802 + "\\D802Pack.zip");
                Directory.Delete(FilesD802 + "\\JellyBean\\META-INF", true);
                Directory.Delete(FilesD802 + "\\KitKat\\META-INF", true);
                Directory.Delete(FilesD802 + "\\Lollipop bump\\META-INF", true);
                Directory.Delete(FilesD802 + "\\Lollipop Loki\\META-INF", true);
                Directory.Delete(FilesD802 + "\\Lollipop stock\\META-INF", true);
                MakeProgress(9);
                using (StreamWriter sw = File.AppendText(DownloadedInfo))
                {
                    sw.WriteLine("D802-Bootloaders");
                }
                InvoAC("Bootloaders are ready to use now! :)");
                DownloadedBootloaders();
                MakeProgress(10);
                MessageBox.Show("Bootloaders has been downloaded successfully! :)", "Success!");
                Thread.Sleep(2000);
                MakeProgress(0);
            }
            else
            {
                MakeProgress(0);
                MessageBox.Show("File hasn't been downloaded! :'(", "Something's gone wrong!");
            }
        }

        private void BumpRckImgBtn_Click(object sender, EventArgs e)
        {
            string RCKpath;
            InvoAC("Bumping image file...");
            RCKpath = BumperPath + "\\rck.img";
            System.IO.File.Copy(OwnRckPath, RCKpath, true);
            RunProgram.RunParaExe(BumpingBinary, q + RCKpath + q, true, true);
            InvoAC("Image file successfully bumped!");
            SaveFileDialog SaveBumpedRCK = new SaveFileDialog();
            SaveBumpedRCK.Filter = "Recovery Image|*.img";
            SaveBumpedRCK.Title = "Save bumped recovery image";
            SaveBumpedRCK.ShowDialog();
            if (SaveBumpedRCK.FileName != "")
            {
                BumpedRckWritePath = SaveBumpedRCK.FileName;
                File.Copy(BumperPath + "\\rck_bumped.img", BumpedRckWritePath, true);
                InvoAC("I saved Your bumped image!");
            }
        }

        private void InstallMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InstallMethod.SelectedIndex == 0)
            {
                InstallMethodInfoBox.Text = "Flashing bootloader while Android is running (without rebooting)";
            }

            if (InstallMethod.SelectedIndex == 1)
            {
                InstallMethodInfoBox.Text = "Installing bootloader via sideload mode in custom recovery (TWRP is highly recomended)";
            }
        }

        private void ChoosenBootloaderStartBtn_Click(object sender, EventArgs e)
        {
            if (IsItConnected == false)
                if (InstallMethod.SelectedIndex == 0)
                {
                    MessageBox.Show("Please connect Your device first!", "ERROR!");
                    InvoAC("Error! Device isn't connected!");
                    return;
                }
            {
                DisableToolkit();
                //Określanie który bootloader został wybrany
                if (RadioOriginalLP.Checked == true)
                {
                    BLDir = BLDirOriginalLollipop;
                    InvoAC("Preparing Lollipop Stock bootloader...");
                }
                else
                {
                    if (RadioBumpLollipop.Checked == true)
                    {
                        BLDir = BLDirBumpLollipop;
                        InvoAC("Preparing Lollipop Bump bootloader...");
                    }
                    else
                    {
                        if (RadioLokiLollipop.Checked == true)
                        {
                            BLDir = BLDirLokiLollipop;
                            InvoAC("Preparing Lollipop Loki bootloader...");
                        }
                        else
                        {
                            if (RadioKitKat.Checked == true)
                            {
                                BLDir = BLDirKitKat;
                                InvoAC("Preparing KitKat bootloader...");
                            }
                            else
                            {
                                if (RadioJellyBean.Checked == true)
                                {
                                    BLDir = BLDirJellyBean;
                                    InvoAC("Preparing JellyBean bootloader...");
                                }
                                else
                                {
                                    EnableToolkit();
                                    InvoAC("You're n00b!");
                                    MessageBox.Show("Please choose bootloader to install!", "Error!");
                                    return;
                                }
                            }
                        }
                    }
                }

                //Tworzenie ścieżek do części pierwszych wybranego bootloadera
                aboot = BLDir + "\\aboot.bin";
                dbi = BLDir + "\\dbi.bin";
                laf = BLDir + "\\laf.bin";
                persist = BLDir + "\\persist.bin";
                rpm = BLDir + "\\rpm.bin";
                sbl1 = BLDir + "\\sbl1.bin";
                tz = BLDir + "\\tz.bin";

                //Sprawdzanie czy została wybrana poprawna metoda instalacji bootloadera...
                if (InstallMethod.SelectedIndex != 0)
                {
                    if (InstallMethod.SelectedIndex != 1)
                    {
                        EnableToolkit();
                        InvoAC("Don't make me cry... if You want to install bootloader, choose first install method...");
                        MessageBox.Show("Please choose install method!", "Error!");
                        return;
                    }
                }

                MakeProgress(1);

                //Cicha instalacja bootloadera
                if (InstallMethod.SelectedIndex == 0)
                {
                    InvoAC("Install method: quiet");

                    MultiKulti = new Thread(() => Flash_Bootloader_ADB("quite"));
                    MultiKulti.Start();
                }

                //via Sideload
                if (InstallMethod.SelectedIndex == 1)
                {
                    MakeProgress(2);
                    InvoAC("Install method: via sideload!");
                    InvoAC("Looking for devices in sideload mode...");
                    RunProgram.RunParaExe(adb, "devices", true, false);

                    //Jeżeli program poprawnie wykryje telefon w trybie sideload to...
                    if (RunProgram.HangOutput.Contains("sideload"))
                    {
                        MakeProgress(4);
                        InvoAC("I've found device in sideload mode!");
                        InvoAC("Preparing choosen bootloader to installation...");
                        MakeProgress(5);
                        if (RadioOriginalLP.Checked == true)
                        {
                            SideloadZIP = FilesD802 + "\\AlligatorD802LPStockBL.zip";
                        }
                        else
                        {
                            if (RadioBumpLollipop.Checked == true)
                            {
                                SideloadZIP = FilesD802 + "\\AlligatorD802LPBumpBL.zip";
                            }
                            else
                            {
                                if (RadioLokiLollipop.Checked == true)
                                {
                                    SideloadZIP = FilesD802 + "\\AlligatorD802LPLokiBL.zip";
                                }
                                else
                                {
                                    if (RadioKitKat.Checked == true)
                                    {
                                        SideloadZIP = FilesD802 + "\\AlligatorD802KKBL.zip";
                                    }
                                    else
                                    {
                                        if (RadioJellyBean.Checked == true)
                                        {
                                            SideloadZIP = FilesD802 + "\\AlligatorD802JBBL.zip";
                                        }
                                    }
                                }
                            }
                        }
                        MakeProgress(6);
                        MakeProgress(7);
                        InvoAC("Flashing bootloader...");
                        RunProgram.RunParaExe(adb, "sideload " + q + SideloadZIP + q, true, false);
                        MakeProgress(9);
                        if (RunProgram.HangOutput.Contains("Total xfer"))
                        {
                            EnableToolkit();
                            InvoAC("Bootloader have been installed successfully!");
                            MakeProgress(10);
                        }
                        else
                        {
                            EnableToolkit();
                            InvoAC("ERROR! Somethink's gone wrong while sending bootloader...");
                        }
                        Thread.Sleep(1000);
                        MakeProgress(0);
                    }

                    //Jeżeli nie widzi telefonu w trybie sideload
                    else
                    {
                        EnableToolkit();
                        InvoAC("ERROR! I can't find any device in sideload mode!");
                        InvoAC("Make sure that You've installed required drivers, and try once again!");
                        MakeProgress(0);
                        return;
                    }
                }
            }
        }

        private void Flash_Bootloader_ADB(string Metoda)
        {
            //Przygotowanie całej operacji...
            MakeProgress(2);
            InvoAC("Pushing files...");
            Push_Alligator(aboot, "aboot", true);
            InvoAC("+ aboot image");
            Push_Alligator(dbi, "dbi", true);
            InvoAC("+ dbi image");
            Push_Alligator(laf, "laf", true);
            InvoAC("+ laf image");
            Push_Alligator(persist, "persist", true);
            InvoAC("+ persist image");
            Push_Alligator(rpm, "rpm", true);
            InvoAC("+ rpm image");
            MakeProgress(3);
            Push_Alligator(sbl1, "sbl1", true);
            InvoAC("+ sbl1 image");
            Push_Alligator(tz, "tz", true);
            InvoAC("+ tz image");
            InvoAC("Files are pushed now into internal memory...");
            MakeProgress(4);
            InvoAC("I'm flashing bootloaders...");
            InvoAC("DO NOT EXIT FROM ALLIGATOR TOOLKIT NOW!");
            MakeProgress(5);

            //Przejdźmy do sedna!
            ShellSU("dd if=/mnt/shell/emulated/0/Alligator/aboot of=/dev/block/platform/msm_sdcc.1/by-name/aboot");
            ShellSU("dd if=/mnt/shell/emulated/0/Alligator/dbi of=/dev/block/platform/msm_sdcc.1/by-name/dbi");
            ShellSU("dd if=/mnt/shell/emulated/0/Alligator/laf of=/dev/block/platform/msm_sdcc.1/by-name/laf");
            ShellSU("dd if=/mnt/shell/emulated/0/Alligator/persist of=/dev/block/platform/msm_sdcc.1/by-name/persist");
            ShellSU("dd if=/mnt/shell/emulated/0/Alligator/rpm of=/dev/block/platform/msm_sdcc.1/by-name/rpm");
            ShellSU("dd if=/mnt/shell/emulated/0/Alligator/sbl1 of=/dev/block/platform/msm_sdcc.1/by-name/sbl1");
            ShellSU("dd if=/mnt/shell/emulated/0/Alligator/tz of=/dev/block/platform/msm_sdcc.1/by-name/tz");
            MakeProgress(10);
            InvoAC("Operation complete! :)");
            RunProgram.RunParaExe(adb, "shell rm -r /sdcard/Alligator/", true, false);
            Thread.Sleep(2000);
            MakeProgress(0);
            EnableToolkit();
        }

        private void DisableToolkit()
        {
            if (IsToolkitDisabled == false)
            {
                //Wypełnianie zmiennych logicznych
                if (ChooseApkBtn.Enabled == true)
                    BoolChooseApkBtn = true;
                else
                    BoolChooseApkBtn = false;

                if (InstallApkBtn.Enabled == true)
                    BoolInstallApkBtn = true;
                else
                    BoolInstallApkBtn = false;

                if (Reboot2DMBtn.Enabled == true)
                    BoolReboot2DMBtn = true;
                else
                    BoolReboot2DMBtn = false;

                if (LeaveDMBtn.Enabled == true)
                    BoolLeaveDMBtn = true;
                else
                    BoolLeaveDMBtn = false;

                if (DevicesBtn.Enabled == true)
                    BoolDevicesBtn = true;
                else
                    BoolDevicesBtn = false;

                if (CvWiFi.Enabled == true)
                    BoolCvWiFi = true;
                else
                    BoolCvWiFi = false;

                if (KillServerBtn.Enabled == true)
                    BoolKillServerBtn = true;
                else
                    BoolKillServerBtn = false;

                if (RebootBtn.Enabled == true)
                    BoolRebootBtn = true;
                else
                    BoolRebootBtn = false;

                if (RebootRckBtn.Enabled == true)
                    BoolRebootRckBtn = true;
                else
                    BoolRebootRckBtn = false;

                if (DriversInstall.Enabled == true)
                    BoolDriversInstall = true;
                else
                    BoolDriversInstall = false;

                if (FlashRckBtn.Enabled == true)
                    BoolFlashRckBtn = true;
                else
                    BoolFlashRckBtn = false;

                if (FlashOwnRckBtn.Enabled == true)
                    BoolFlashOwnRckBtn = true;
                else
                    BoolFlashOwnRckBtn = false;

                if (BumpRckImgBtn.Enabled == true)
                    BoolBumpRckImgBtn = true;
                else
                    BoolBumpRckImgBtn = false;

                if (ChoosenBootloaderStartBtn.Enabled == true)
                    BoolChoosenBootloaderStartBtn = true;
                else
                    BoolChoosenBootloaderStartBtn = false;

                if (BootDwnBtnD802.Enabled == true)
                    BoolBootDwnBtnD802 = true;
                else
                    BoolBootDwnBtnD802 = false;

                if (RootStartBtn.Enabled == true)
                    BoolRootStartBtn = true;
                else
                    BoolRootStartBtn = false;

                if (SideloadZipFlashBtn.Enabled == true)
                    BoolSideloadZipFlashBtn = true;
                else
                    BoolSideloadZipFlashBtn = false;

                if (SideloadZipPathBtn.Enabled == true)
                    BoolSideloadZipPathBtn = true;
                else
                    BoolSideloadZipPathBtn = false;


                //Wyłączanie wszystkich przycisków
                RootStartBtn.Enabled = false;
                LeaveDMBtn.Enabled = false;
                Reboot2DMBtn.Enabled = false;
                SideloadZipPathBtn.Enabled = false;
                SideloadZipFlashBtn.Enabled = false;
                DevicesBtn.Enabled = false;
                CvWiFi.Enabled = false;
                KillServerBtn.Enabled = false;
                RebootBtn.Enabled = false;
                RebootRckBtn.Enabled = false;
                DriversInstall.Enabled = false;
                FlashRckBtn.Enabled = false;
                FlashOwnRckBtn.Enabled = false;
                BumpRckImgBtn.Enabled = false;
                ChoosenBootloaderStartBtn.Enabled = false;
                BootDwnBtnD802.Enabled = false;

                IsToolkitDisabled = true;
            }
        }

        private void EnableToolkit()
        {
            if (IsToolkitDisabled == true)
            {

                //Włączanie przycisków które były aktywne przed ich dezaktywacją
                if (BoolDevicesBtn == true)
                    DevicesBtn.Invoke((MethodInvoker)(() => DevicesBtn.Enabled = true));

                if (BoolChooseApkBtn == true)
                    ChooseApkBtn.Invoke((MethodInvoker)(() => ChooseApkBtn.Enabled = true));

                if (BoolInstallApkBtn == true)
                    InstallApkBtn.Invoke((MethodInvoker)(() => InstallApkBtn.Enabled = true));

                if (BoolReboot2DMBtn == true)
                    Reboot2DMBtn.Invoke((MethodInvoker)(() => Reboot2DMBtn.Enabled = true));

                if (BoolLeaveDMBtn == true)
                    LeaveDMBtn.Invoke((MethodInvoker)(() => LeaveDMBtn.Enabled = true));

                if (BoolCvWiFi == true)
                    CvWiFi.Invoke((MethodInvoker)(() => CvWiFi.Enabled = true));

                if (BoolKillServerBtn == true)
                    KillServerBtn.Invoke((MethodInvoker)(() => KillServerBtn.Enabled = true));

                if (BoolRebootBtn == true)
                    RebootBtn.Invoke((MethodInvoker)(() => RebootBtn.Enabled = true));

                if (BoolRebootRckBtn == true)
                    RebootRckBtn.Invoke((MethodInvoker)(() => RebootRckBtn.Enabled = true));

                if (BoolDriversInstall == true)
                    DriversInstall.Invoke((MethodInvoker)(() => DriversInstall.Enabled = true));

                if (BoolFlashRckBtn == true)
                    FlashRckBtn.Invoke((MethodInvoker)(() => FlashRckBtn.Enabled = true));

                if (BoolFlashOwnRckBtn == true)
                    FlashOwnRckBtn.Invoke((MethodInvoker)(() => FlashOwnRckBtn.Enabled = true));

                if (BoolBumpRckImgBtn == true)
                    BumpRckImgBtn.Invoke((MethodInvoker)(() => BumpRckImgBtn.Enabled = true));

                if (BoolChoosenBootloaderStartBtn == true)
                    ChoosenBootloaderStartBtn.Invoke((MethodInvoker)(() => ChoosenBootloaderStartBtn.Enabled = true));

                if (BoolBootDwnBtnD802 == true)
                    BootDwnBtnD802.Invoke((MethodInvoker)(() => BootDwnBtnD802.Enabled = true));

                if (BoolRootStartBtn == true)
                    RootStartBtn.Invoke((MethodInvoker)(() => RootStartBtn.Enabled = true));

                if (BoolSideloadZipPathBtn == true)
                    SideloadZipPathBtn.Invoke((MethodInvoker)(() => SideloadZipPathBtn.Enabled = true));

                if (BoolSideloadZipFlashBtn == true)
                    SideloadZipFlashBtn.Invoke((MethodInvoker)(() => SideloadZipFlashBtn.Enabled = true));

                IsToolkitDisabled = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SoundPlayer JoseChant = new SoundPlayer(Properties.Resources.JoseSong);
            JoseChant.Play();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4HEKW66J9MC5Y");
        }

        private void ManualAndroidRooting_CheckedChanged(object sender, EventArgs e)
        {
            if (ManualAndroidRooting.Checked == true)
            {
                RootingVersionsBox.Visible = true;

                if (RadioRootJB.Checked == true)
                    RootSDKLabel.Text = "Jelly Bean";

                if (RadioRootKK.Checked == true)
                    RootSDKLabel.Text = "KitKat";

                if (RadioRootLP.Checked == true)
                    RootSDKLabel.Text = "Lollipop";
            }

            else
            {
                RootingVersionsBox.Visible = false;
                RootSDKLabel.Text = RootStringSDK;
            }
        }

        private void RadioRootJB_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioRootJB.Checked == true)
                RootSDKLabel.Text = "Jelly Bean";

        }

        private void RadioRootKK_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioRootKK.Checked == true)
                RootSDKLabel.Text = "KitKat";
        }

        private void RadioRootLP_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioRootLP.Checked == true)
                RootSDKLabel.Text = "Lollipop";
        }

        private void RootStartBtn_Click(object sender, EventArgs e)
        {
            if (IsItConnected == false)
            {
                InvoAC("First connect Your phone!");
                return;
            }
            DisableToolkit();
            bool Cache;
            Cache = false;
            string NextMethod;
            NextMethod = "Kupa";

            if (RootSDKLabel.Text == "Jelly Bean")
            {
                NextMethod = "JB";
                InvoAC("I'm preparing myself to start rooting Android Jelly Bean!");
                Cache = true;
                MakeProgress(1);
            }

            if (RootSDKLabel.Text == "KitKat")
            {
                NextMethod = "KK";
                InvoAC("I'm preparing myself to start rooting Android KitKat!");
                Cache = true;
                MakeProgress(1);
            }

            if (RootSDKLabel.Text == "Lollipop")
            {
                NextMethod = "LP";
                InvoAC("I'm preparing myself to start rooting Android Lollipop");
                Cache = true;
                MakeProgress(1);
            }

            if (Cache == false)
            {
                MessageBox.Show("ERROR!", "Something gone wrong...");
                InvoAC("Error! Wrong rooting parameter!");
                return;
            }

            MultiKulti = new Thread(() => Rooting_ENGINE(NextMethod));
            MultiKulti.Start();
        }

        private void IfOfAlligator(string FileInAlligatorDir, string OutputWithFilename, bool WithRootPerms)
        {
            if (WithRootPerms == false)
                RunProgram.RunParaExe(adb, "shell dd if=/mnt/shell/emulated/0/Alligator/" + FileInAlligatorDir + " of=" + OutputWithFilename, true, false);

            if (WithRootPerms == true)
                ShellSU("dd if=/mnt/shell/emulated/0/Alligator/" + FileInAlligatorDir + " of=" + OutputWithFilename);
        }

        private void Rooting_ENGINE(string Version)
        {
            //Jeżeli NextMethod nie zostało zmienione na prawidłową wartość
            if (Version == "Kupa")
            {
                MessageBox.Show("CRITICAL ERROR!", "ERROR!");
                MakeProgress(0);
                return;
            }

            //Jeżeli rootujemy Androida Jelly Bean
            if (Version == "JB")
            {
                InvoAC("Pushing needed files into phone internal memory...");
                Push_Alligator(G2Security, "g2_security", false);
                MakeProgress(2);
                InvoAC("");
                MessageBox.Show("Now unplug Your phone from PC, go to developer settings, disable USB Debuging and re-enable it. When You do that, re-plug phone to PC. When phone'll be ready, click the button below.", "What to do now?");

                MakeProgress(3);
                InvoAC("Waiting for Your device...");
                RunProgram.RunParaExe(adb, "wait-for-device", true, false);
                MakeProgress(4);

                InvoAC("Phone is connected once again!");
                MakeProgress(5);
                RunProgram.RunParaExe(adb, "shell " + q + "mount -o remount,rw /system" + q, true, false);
                MakeProgress(6);
                RunProgram.RunParaExe(adb, "push " + q + SUFile + q + " /system/xbin/su", true, false);
                MakeProgress(7);
                RunProgram.RunParaExe(adb, "shell " + q + "chown 0.0 /system/xbin/su;chmod 06755 /system/xbin/su;sync;mount -o remount,ro /system" + q, true, false);
                MakeProgress(8);
                InvoAC("Installing SuperSU...");
                RunProgram.RunParaExe(adb, "install " + q + SuperuserAPK + q, true, false);
                MakeProgress(9);
                InvoAC("Operation completed! Now reboot Your phone, then connect it once again to Toolkit, to check if phone had been rooted successfull. ;)");
            }

            //Jeżeli rootujemy Androida KitKat
            if (IsItUSB == false)
            {
                InvoAC("To root Android KitKat please connect phone with Alligator Toolkit via USB!");
                EnableToolkit();
                MakeProgress(0);
                return;
            }
            if (Version == "KK")
            {
                MakeProgress(3);
                InvoAC("Rebooting phone into stock recovery...");
                RunProgram.RunParaExe(adb, "reboot recovery", true, false);
                MakeProgress(4);
                MessageBox.Show("Now via volume and power buttons choose " + q + "Apply update from ADB" + q + ". Then I'll be able to root Your phone. If You did it, click button below to continue!", "What to do now?");
                MakeProgress(5);
                InvoAC("Sideloading needed files to phone...");
                RunProgram.RunParaExe(adb, "sideload " + q + datrootPath + q, true, false);
                MakeProgress(6);
                InvoAC("If on the display You see big label " + q + "DAT ROOT" + q + ", everything's gone ok! If not... something gone wrong. :-(");
                MakeProgress(7);
                InvoAC("When recovery script will've been finished, choose reboot Your phone manually.");
                InvoAC("I'm waitnig for Your rebooted phone...");
                RunProgram.RunParaExe(adb, "wait-for-device", true, false);
                MakeProgress(8);
                MessageBox.Show("Now please enable option Settings -> Security -> UnKnown Sources in Your phone. When option will be enabled, click " + q + "OK" + q + " Button", "What's to do next?");
                InvoAC("Installing SuperSU...");
                MakeProgress(9);
                RunProgram.RunParaExe(adb, "install " + q + SuperuserAPK + q, true, false);
                InvoAC("SuperSU has been installed! :)");
            }

            //Jeżeli rootujemy Lollipopa
            if (Version == "LP")
            {
                MakeProgress(3);
                InvoAC("I've just executed LG One Click Root by avicohh!");
                Process.Start("http://forum.xda-developers.com/lg-g3/general/guide-root-lg-firmwares-kitkat-lollipop-t3056951");
                RunProgram.RunExe(OneClickExe, true, true);
                InvoAC("All credits to avicohh, for best rooting engine for Lollipop. ;)");
                MakeProgress(9);
            }

            //Sprawdzanie czy urządzenie zostało poprawnie zrootowane
            InvoAC("Checking if phone has been rooted successfull...");
            AndroidPhoneInformations.PermsLevelInfo();
            if (AndroidPhoneInformations.PermissionsState == true)
            {
                InvoAC("Your phone is rooted! :)");
                RootLabel.Invoke((MethodInvoker)(() => RootLabel.Text += "now ROOTED! :)"));
            }
            else
                InvoAC("Something gone wrong :/");
            MakeProgress(10);
            Thread.Sleep(2000);
            MakeProgress(0);
            EnableToolkit();
        }

        private string GetLocationCOM()
        {
            RunProgram.RunCMD("reg query HKLM\\hardware\\devicemap\\SERIALCOMM", true, false);
            string Port;
            int StringLines;
            Port = RunProgram.HangOutput;
            string PortCOM;
            PortCOM = "Kupa";

            RubbBox.Text = Port;
            StringLines = RubbBox.Lines.Length;
            for (int x = 0; x < StringLines; x++)
            {

                if (RubbBox.Lines[x].Contains("LGANDNETDIAG1") == true)
                {
                    PortCOM = RubbBox.Lines[x].Replace("LGANDNETDIAG1", null);
                    PortCOM = PortCOM.Replace("Device", null);
                    PortCOM = PortCOM.Replace("REG_SZ", null);
                    PortCOM = PortCOM.Replace(" ", null);
                    PortCOM = PortCOM.Replace(@"\", null);
                    break;
                }
            }
            if (PortCOM != "Kupa")
            {
                return PortCOM;
            }
            else
            {
                InvoAC("ERROR! I couldn't find Download Mode port of your device!");
                return "Error!";
            }
        }

        private void Reboot2DMBtn_Click(object sender, EventArgs e)
        {
            DisableToolkit();
            MultiKulti = new Thread(() => DownloadMode("enter"));
            MakeProgress(1);
            MultiKulti.Start();
            EnableToolkit();
        }

        private void DownloadMode(string TypeOfReboot)
        {

            //Sprawdzanie czy przypadkiem nie istnieje już scrypt Download Mode
            if (File.Exists(TempDir + "\\DMScript.bat") == true)
                File.Delete(TempDir + "\\DMScript.bat");

            //Sprawdzanie czy aby na pewno telefon jest podłączony do komputera
            if (GetLocationCOM() == "Error!")
            {
                MakeProgress(0);
                EnableToolkit();
                return;
            }

            //Jeżeli widzimy urządzenie generujemy skrypt resetujący
            else
            {
                InvoAC("I've found device at " + GetLocationCOM() + "!");
                MakeProgress(3);
                string ToBatFile = "cd " + q + AppData + q + "\r\nSC.exe " + BackSlash + BackSlash + '.' + BackSlash + GetLocationCOM() + " < " + TypeOfReboot + "Download";
                MakeProgress(5);
                if (Create.BatFile(ToBatFile, TempDir, "DMScript.bat", false) == true)
                    InvoAC("Preparing to reboot...");

                else
                {
                    InvoAC("Occoured an error while creating script file!");
                    MakeProgress(0);
                    EnableToolkit();
                    return;
                }
                MakeProgress(7);

                //Teraz czas wykonać skrypt resetujący
                RunProgram.RunExe(TempDir + "\\DMScript.bat", true, false);
                MakeProgress(10);
                InvoAC("Device has been rebooted!");
                Thread.Sleep(2000);
                MakeProgress(0);
            }
        }

        private void LeaveDMBtn_Click(object sender, EventArgs e)
        {
            DisableToolkit();
            MultiKulti = new Thread(() => DownloadMode("leave"));
            MakeProgress(1);
            MultiKulti.Start();
            EnableToolkit();
        }

        private void BetaSwitchBtn_Click(object sender, EventArgs e)
        {
            if (IsBetaAllowed == true)
            {
                File.Delete(BetaFile);
            }
            else
            {
                File.WriteAllBytes(BetaFile, Properties.Resources.Beta);
            }

            if (File.Exists(BetaFile) == true)
            {
                BetaSwitchBtn.Text = "Disable beta updates";
                IsBetaAllowed = true;
            }
            else
            {
                BetaSwitchBtn.Text = "Enable beta updates";
                IsBetaAllowed = false;
            }
        }

        private void ChooseApkBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ChooseAPKDialog = new OpenFileDialog();
            ChooseAPKDialog.Filter = "Android Packages|*.apk";
            ChooseAPKDialog.Title = "Select an APK File";
            if (ChooseAPKDialog.ShowDialog() == DialogResult.OK)
            {
                //Pobieranie nazwy pliku *.img oraz ścieżki do niego
                OwnAPKPath = ChooseAPKDialog.FileName;
                OwnAPKFile = ChooseAPKDialog.SafeFileName;

                //Przenoszenie pliku do tempa w AppData
                System.IO.File.Copy(OwnAPKPath, TempDir + "\\own.apk", true);
                OwnApkPathDisplay.Text = OwnAPKPath;

                //Odblokowanie przycisku flashowania i instalacji.
                InstallApkBtn.Enabled = true;
            }
        }

        private void InstallApkBtn_Click(object sender, EventArgs e)
        {
            DisableToolkit();
            MakeProgress(2);
            MultiKulti = new Thread(ApkInstaller);
            MultiKulti.Start();
        }

        private void ApkInstaller()
        {
            InvoAC("I'm installing choosen Android Package...");
            MakeProgress(5);
            RunProgram.RunParaExe(adb, "install " + q + OwnAPKPath + q, true, false);
            InvoAC("Packae has been installed! :)");
            MakeProgress(10);
            Thread.Sleep(2000);
            MakeProgress(0);
            EnableToolkit();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void GetPortBtn_Click(object sender, EventArgs e)
        {
            string ComCom;
            ComCom = GetLocationCOM();
            COMLabel.Text = ComCom;
            InvoAC("I've found Your phone at " + ComCom);
        }

        private void SideloadZipPathBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ChooseSideloadZip = new OpenFileDialog();
            ChooseSideloadZip.Filter = "Recovery packages|*.zip";
            ChooseSideloadZip.Title = "Select a recovery zip package";
            if (ChooseSideloadZip.ShowDialog() == DialogResult.OK)
            {
                //Pobieranie nazwy pliku *.zip oraz ścieżki do niego
                SideloadZipPath = ChooseSideloadZip.FileName;
                SideloadZipName = ChooseSideloadZip.SafeFileName;

                //Przenoszenie pliku do tempa w AppData
                System.IO.File.Copy(SideloadZipPath, TempedSideloadZipPath, true);
                SideloadZipPathBox.Text = SideloadZipPath;

                //Odblokowanie przycisku flashowania.
                SideloadZipFlashBtn.Enabled = true;
            }
        }

        private void SideloadZipFlashBtn_Click(object sender, EventArgs e)
        {
            DisableToolkit();
            MultiKulti = new Thread(Sideload_ENGINE);
            MultiKulti.Start();
        }
        
        private void Sideload_ENGINE()
        {
            InvoAC("Sending flashable zip file...");
            MakeProgress(10);
            RunProgram.RunParaExe(adb, "sideload " + q + TempedSideloadZipPath + q, true, true);
            InvoAC("Finished!");
            MakeProgress(0);
            EnableToolkit();
        }
    }

        public class RunProgram
        {
            public static string HangOutput;
            public static void RunExe(string path, bool wfe, bool visable)
            {
                System.Diagnostics.Process RunExecutable = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                if (visable == false)
                {
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                }
                else
                {
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                }
                startInfo.FileName = path;
                RunExecutable.StartInfo = startInfo;
                RunExecutable.Start();
                if (wfe == true)
                {
                    RunExecutable.WaitForExit();
                }
            }

            public static void RunParaExe(string path, string command, bool wfe, bool visible)
            {
                Process RunArgExe = new Process();
                HangOutput = String.Empty;
                ProcessStartInfo ArgExe = new ProcessStartInfo(path, command);
                ArgExe.UseShellExecute = false;
                ArgExe.RedirectStandardOutput = true;
                if (visible == false)
                {
                    ArgExe.CreateNoWindow = true;
                    ArgExe.WindowStyle = ProcessWindowStyle.Hidden;
                }
                RunArgExe.StartInfo = ArgExe;
                RunArgExe.Start();

                HangOutput = RunArgExe.StandardOutput.ReadToEnd();

                RunArgExe.WaitForExit();
                AlligatorForm.Hangometr = HangOutput;
            }

            public static void RunCMD(string command, bool wfe, bool visible)
            {
                Process RunArgExe = new Process();
                HangOutput = String.Empty;
                ProcessStartInfo ArgExe = new ProcessStartInfo("cmd.exe", "/C " + command);
                ArgExe.UseShellExecute = false;
                ArgExe.RedirectStandardOutput = true;
                if (visible == false)
                {
                    ArgExe.CreateNoWindow = true;
                    ArgExe.WindowStyle = ProcessWindowStyle.Hidden;
                }
                RunArgExe.StartInfo = ArgExe;
                RunArgExe.Start();

                HangOutput = RunArgExe.StandardOutput.ReadToEnd();

                RunArgExe.WaitForExit();
                AlligatorForm.Hangometr = HangOutput;
            }
        }

        public class AndroidVersionSDK
        {
            public static string CodeName;
            public static void CheckVer(int SDK)
            {
                switch (SDK)
                {
                    case 8:
                        CodeName = "Froyo";
                        AlligatorForm.RootStringSDK = "NOT SUPPORTED!";
                        break;
                    case 10:
                        CodeName = "Gingerbread";
                        AlligatorForm.RootStringSDK = "NOT SUPPORTED!";
                        break;
                    case 15:
                        CodeName = "Ice Cream Sandwich";
                        AlligatorForm.RootStringSDK = "NOT SUPPORTED!";
                        break;
                    case 16:
                        CodeName = "Jelly Bean 4.1";
                        AlligatorForm.RootStringSDK = "NOT SUPPORTED!";
                        break;
                    case 17:
                        CodeName = "Jelly Bean 4.2";
                        AlligatorForm.RootStringSDK = "Jelly Bean";
                        break;
                    case 18:
                        CodeName = "Jelly Bean 4.3";
                        AlligatorForm.RootStringSDK = "NOT SUPPORTED!";
                        break;
                    case 19:
                        CodeName = "KitKat";
                        AlligatorForm.RootStringSDK = "KitKat";
                        break;
                    case 21:
                        CodeName = "Lollipop 5.0";
                        AlligatorForm.RootStringSDK = "Lollipop";
                        break;
                    case 22:
                        CodeName = "Lollipop 5.1";
                        AlligatorForm.RootStringSDK = "NOT SUPPORTED!";
                        break;
                    case 23:
                        CodeName = "Marshmallow";
                        AlligatorForm.RootStringSDK = "NOT SUPPORTED!";
                        break;

                    default:
                        CodeName = "Can't verify";
                        AlligatorForm.RootStringSDK = "NOT SUPPORTED!";
                        break;
                }
            }
        }

        public class AndroidPhoneInformations
        {
            public int ModelTicker;
            public int SDKTicker;
            public static string RoModel;
            public static string RoSDK;
            public static string PermissionsLevel;
            public static string LabelRoModel;
            public static string LabelRoSDK;
            public static string LabelRoPerms;
            public static bool SpecialVariable;
            public static bool ModelState;
            public static bool SDKState;
            public static bool PermissionsState;

            public static void ModelInfo()
            {
                RunProgram.HangOutput = String.Empty;
                RunProgram.RunParaExe(AlligatorForm.adb, "shell getprop ro.product.model", true, false);
                LabelRoPerms = RunProgram.HangOutput;
                if (RunProgram.HangOutput == "")
                {
                    RoModel = "I'm not able to confirm model of your phone... :( ";
                    ModelState = false;
                }
                else
                {
                    ModelState = true;
                    LabelRoModel = RunProgram.HangOutput;
                    RunProgram.HangOutput = RunProgram.HangOutput.Replace("\r\n", null);
                    RoModel = "Phone model: " + RunProgram.HangOutput;
                }
            }

            public static void SDKInfo()
            {
                RunProgram.RunParaExe(AlligatorForm.adb, "shell getprop ro.build.version.sdk", true, false);
                int AssistSDK;
                bool Assisstant = int.TryParse(RunProgram.HangOutput, out AssistSDK);
                if (Assisstant == false)
                {
                    RoSDK = "I've met unexpected error while I was checking your Android version!";
                    SDKState = false;
                }
                else
                {
                    SDKState = true;
                    AssistSDK = int.Parse(RunProgram.HangOutput);
                    AndroidVersionSDK.CheckVer(AssistSDK);
                    LabelRoSDK = AndroidVersionSDK.CodeName;
                    RoSDK = "Your Android version is " + AndroidVersionSDK.CodeName;
                }
            }

            public static void PermsLevelInfo()
            {
                if (SDKState == true)
                {
                    string q = "\"";
                    string ShCmnd;
                    ShCmnd = "shell " + q + "su -c 'getprop ro.build.version.sdk'" + q;
                    RunProgram.RunParaExe(AlligatorForm.adb, ShCmnd, true, false);

                    ///////////////////////////////

                    int AssistSDK;
                    bool Assisstant = int.TryParse(RunProgram.HangOutput, out AssistSDK);
                    if (Assisstant == true)
                    {
                        PermissionsState = true;
                        PermissionsLevel = "ROOTED";
                    }
                    else
                    {
                        PermissionsState = false;
                        PermissionsLevel = "Non-ROOTED";
                    }

                    //////////////////////////////				
                }
                else
                {
                    PermissionsState = false;
                    PermissionsLevel = "UnKnown state!";
                }
            }
        }

        public class Create
        {
            public static bool BatFile(string lines, string Destination, string FileName, bool Pause)
            {
                //Generowanie pliku tekstowego
                using (StreamWriter sw = File.CreateText(Destination + "\\" + FileName))
                {
                    sw.WriteLine("@echo off");
                    sw.WriteLine(lines);
                    if (Pause == true)
                    {
                        sw.WriteLine("pause");
                    }
                    sw.WriteLine("exit");
                }

                //Sprawdzanie czy plik został utworzony poprawnie
                if (File.Exists(Destination + "\\" + FileName) == true)
                    return true;
                else
                    return false;
            }
        }

        public class InternetConnection
        {
            public static bool PingHttp(string www)
            {
                try
                {
                    System.Net.NetworkInformation.Ping myPing = new System.Net.NetworkInformation.Ping();
                    String host = www;
                    byte[] buffer = new byte[32];
                    int timeout = 1000;
                    System.Net.NetworkInformation.PingOptions pingOptions = new System.Net.NetworkInformation.PingOptions();
                    System.Net.NetworkInformation.PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                    return (reply.Status == System.Net.NetworkInformation.IPStatus.Success);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }

