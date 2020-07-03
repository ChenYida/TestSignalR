using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private HubConnection _connection;

        public Form1()
        {
            InitializeComponent();

            _connection = new HubConnectionBuilder()
                .WithUrl("https://cyd.azurewebsites.net/")
                .WithAutomaticReconnect()
                .Build();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            _connection.On<string>("Chat", OnSend);
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _connection.InvokeAsync("Chat", textBox1.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            messagesList.Items.Clear();
        }

        private void OnSend(string name)
        {
            Log(Color.Black, name);
        }

        private void Log(Color color, string message)
        {
            Action callback = () =>
            {
                messagesList.Items.Add(new LogMessage(color, message));
            };

            Invoke(callback);
        }

        private class LogMessage
        {
            public Color MessageColor { get; }

            public string Content { get; }

            public LogMessage(Color messageColor, string content)
            {
                MessageColor = messageColor;
                Content = content;
            }
        }

        private void messagesList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var message = (LogMessage)messagesList.Items[e.Index];
            e.Graphics.DrawString(
                message.Content,
                messagesList.Font,
                new SolidBrush(message.MessageColor),
                e.Bounds);
        }
    }
}