using System;
using System.Threading;
using System.Windows.Forms;
using SoyalWorkTimeWebManager.Models.LacoationContexts;

namespace SoyalEventServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EventWorker.RunWorkerAsync();
            BeregovoEventWorker.RunWorkerAsync();
            BeganyEventWorker.RunWorkerAsync();
            label1.Text = "Server Running...";
        }

        private void EventWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    EventServer.GetOneEvent<HOHContext>();

                }
                catch (SoyalEventLogException ex)
                {
                    Invoke(new MethodInvoker(() => logList.Items.Add("BEREG [EVENT] > " + ex.EvetMessage)));
                    Invoke(new MethodInvoker(() => logList.SelectedIndex = logList.Items.Count-1));

                }
                catch (Exception exx)
                {
                    Invoke(new MethodInvoker(() => logList.Items.Add("BEREG [ERROR] > " + exx.Message)));
                    Invoke(new MethodInvoker(() => logList.SelectedIndex = logList.Items.Count - 1));
                    Invoke(new MethodInvoker(() => Informer.SendMail("BIS - Event Server hiba", "BEREG [ERROR] > " + exx.Message)));
                }
                finally
                {
                    Thread.Sleep(int.Parse(textMilisec.Text));
                }
            }
        }

        private void BeregovoEventWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    EventServer.GetOneEvent<BeregovoContext>();
                }
                catch (SoyalEventLogException ex)
                {
                    Invoke(new MethodInvoker(() => logList.Items.Add("BEREGOVO [EVENT] > " + ex.EvetMessage)));
                    Invoke(new MethodInvoker(() => logList.SelectedIndex = logList.Items.Count - 1));

                }
                catch (Exception exx)
                {
                    Invoke(new MethodInvoker(() => logList.Items.Add("BEREGOVO [ERROR] > " + exx.Message)));
                    Invoke(new MethodInvoker(() => logList.SelectedIndex = logList.Items.Count - 1));
                    Invoke(new MethodInvoker(() => Informer.SendMail("BIS - Event Server hiba", "BEREGOVO [ERROR] > " + exx.Message)));
                }
                finally
                {
                    Thread.Sleep(int.Parse(textMilisec.Text));
                }
            }

        }

        private void BeganyEventWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    EventServer.GetOneEvent<BeganyContext>();
                }
                catch (SoyalEventLogException ex)
                {
                    Invoke(new MethodInvoker(() => logList.Items.Add("BEGANY [EVENT] > " + ex.EvetMessage)));
                    Invoke(new MethodInvoker(() => logList.SelectedIndex = logList.Items.Count - 1));

                }
                catch (Exception exx)
                {
                    Invoke(new MethodInvoker(() => logList.Items.Add("BEGANY [ERROR] > " + exx.Message)));
                    Invoke(new MethodInvoker(() => logList.SelectedIndex = logList.Items.Count - 1));
                    Invoke(new MethodInvoker(() => Informer.SendMail("BIS - Event Server hiba", "BEGANY [ERROR] > " + exx.Message)));
                }
                finally
                {
                    Thread.Sleep(int.Parse(textMilisec.Text));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EventWorker.Dispose();
            BeregovoEventWorker.Dispose();
            BeganyEventWorker.Dispose();
        }
    }
}
