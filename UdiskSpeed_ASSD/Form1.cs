using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;

namespace UdiskSpeed_ASSD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private BackgroundWorker backgroundWorker1 ;
        private readonly HiPerfTimer TestTimer = new HiPerfTimer();
        string _workPath = "H:";
        string _workFolder = "\\UspeedTest";
        public string TestfilePath = "";
        private byte[] rnd_array = new byte[1024*1024*16]; //16M
        private static double _t1;
        private static double _t2;
        public long Testsize = 1;//0x400L;//0x400L;//0x0A; //10M 0x400L 1024M
        private uint _fileFlags = 0x20000000;// FILE_FLAG_NO_BUFFERING;
        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1 = new BackgroundWorker();                      //新建BackgroundWorker
            backgroundWorker1.WorkerReportsProgress = true;                  //允许报告进度
            backgroundWorker1.WorkerSupportsCancellation = false;             //允许取消线程
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;                       //设置主要工作逻辑
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;     //进度变化的相关处理
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;  //线程完成时的处理
        }
        private void ReadBtn_Click(object sender, EventArgs e)
        {

        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            runTest();
        }
        private void runTest()
        {
            progressBar1.Value = 0;
            TimerLB.Text = "";
            _workPath = pathTB.Text;
            Testsize = long.Parse(fileSizeTB.Text);
            _t1 = 0;
            _t2 = 0;
            seq_write_label.Text = "";
            seq_read_label.Text = "";
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            TestTimer.Start();
            Console.WriteLine("background begin work...");
            TestfilePath = _workPath + _workFolder + @"\test.bin";
            //TestfilePath = this.ld.DeviceID + this._workFolder + @"\test.bin";
            Console.WriteLine("TestfilePath:" + TestfilePath);
            Directory.CreateDirectory(_workPath + _workFolder);
            Utils.DecompressFolder(_workPath + _workFolder);
            HiPerfTimer timer = new HiPerfTimer();
            BackgroundWorker worker = sender as BackgroundWorker;
            generate_random_array(this.rnd_array);
            worker.ReportProgress(10);

            Console.WriteLine("test write...");
            Thread.Sleep(0x3e8);
            timer.Start();
            write_file_seq(this.TestfilePath, worker);
            timer.Stop();
            _t1 = (16 * Testsize) / timer.Duration;
            Console.WriteLine("Write:" + _t1);
            Thread.Sleep(0x3e8);
            worker.ReportProgress(50);

            Console.WriteLine("test read...");
            timer.Start();
            read_file_seq(this.TestfilePath, worker);
            timer.Stop();
            _t2 = (16 * Testsize) / timer.Duration;
            Console.WriteLine("Read:" + _t2);
            Thread.Sleep(0x3e8);
            worker.ReportProgress(100);
            TestTimer.Stop();
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            UpdateValues();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateValues();
            try
            {
                new FileInfo(this.TestfilePath).Delete();
                
                Directory.Delete(this._workPath + this._workFolder, true);
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            Console.WriteLine("Timer:" + TestTimer.Duration.ToString());
            TimerLB.Text = string.Format("Timer:{0}s", TestTimer.Duration.ToString());
        }
        public void generate_random_array(byte[] rndarray)
        {
            Random random = new Random();
            for (int i = 0; i < rndarray.Length; i++)
            {
                rndarray[i] = (byte)random.Next(0xff);
            }
        }
        private void write_file_seq(string path, BackgroundWorker worker)
        {
            new FileInfo(path).Delete();
            SafeFileHandle handle = WinApiFunctions.CreateFile(path, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero, FileMode.CreateNew, this._fileFlags, IntPtr.Zero);
            if (handle.IsInvalid)
            {
                throw new IOException("Could not open file stream.", new Win32Exception());
            }
            //FileStream 缓冲区大小1024*1024*16 16M
            FileStream stream = new FileStream(handle, FileAccess.ReadWrite, 1024*1024*16, false);
            for (long i = 0L; i < Testsize; i += 1L)
            {
                stream.Write(this.rnd_array, 0, rnd_array.Length);
            }
            stream.Flush();
            stream.Close();
        }
        private void read_file_seq(string path, BackgroundWorker worker)
        {
            byte[] buffer = new byte[1024*1024*16]; //16M
            SafeFileHandle handle = WinApiFunctions.CreateFile(path, FileAccess.Read, FileShare.None, IntPtr.Zero, FileMode.Open, this._fileFlags, IntPtr.Zero);
            if (handle.IsInvalid)
            {
                throw new IOException("Could not open file stream.", new Win32Exception());
            }
            //缓冲区大小64M
            FileStream stream = new FileStream(handle, FileAccess.Read, 1024*1024*64, false);
            for (long i = 0L; i < Testsize; i += 1L)
            {
                stream.Read(buffer, 0, buffer.Length);
            }
            stream.Close();
        }
        private void UpdateValues()
        {
            seq_write_label.Text = string.Format("Write:{0:0.00} MB/s", _t1);
            seq_read_label.Text = string.Format("Read:{0:0.00} MB/s", _t2);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.backgroundWorker1.IsBusy)
            {
                this.backgroundWorker1.CancelAsync();
                e.Cancel = true;
            }
        }

       
    }

    public class HiPerfTimer
    {
        private readonly ulong _freq;
        private ulong _startTime = 0L;
        private ulong _stopTime = 0L;

        public HiPerfTimer()
        {
            if (!QueryPerformanceFrequency(out this._freq))
            {
                throw new Win32Exception();
            }
        }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out ulong lpPerformanceCount);
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out ulong lpFrequency);
        public void Start()
        {
            Thread.Sleep(0);
            QueryPerformanceCounter(out this._startTime);
        }

        public void Stop()
        {
            QueryPerformanceCounter(out this._stopTime);
        }

        public double Duration
        {
            get
            {
                double num1 = (this._stopTime - this._startTime) / ((double)this._freq);
                if (num1 < 0.0)
                {
                    throw new Exception("mist");
                }
                return num1;
            }
        }

        public ulong Frequency
        {
            get
            {
                return this._freq;
            }
        }
    }
    internal class Utils
    {
        public static uint DecompressFolder(string path)
        {
            using (ManagementObject obj2 = new ManagementObject("Win32_Directory.Name=\"" + path.Replace(@"\", @"\\") + "\""))
            {
                return (uint)obj2.InvokeMethod("UnCompress", null, null).Properties["ReturnValue"].Value;
            }
        }
    }
}
