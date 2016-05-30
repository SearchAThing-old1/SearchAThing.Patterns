#region SearchAThing.Patterns, Copyright(C) 2016 Lorenzo Delana, License under MIT
/*
* The MIT License(MIT)
* Copyright(c) 2016 Lorenzo Delana, https://searchathing.com
*
* Permission is hereby granted, free of charge, to any person obtaining a
* copy of this software and associated documentation files (the "Software"),
* to deal in the Software without restriction, including without limitation
* the rights to use, copy, modify, merge, publish, distribute, sublicense,
* and/or sell copies of the Software, and to permit persons to whom the
* Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
* FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
* DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StatusBarTaskDispatcher
{

    public partial class MainWindow : Window
    {
        int job = 0;

        Global global { get { return Global.Instance; } }

        public MainWindow()
        {
            InitializeComponent();

            statusTblk.SetBinding(TextBlock.TextProperty, new Binding("Status") { Source = global.StatusManager });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var j = ++job;

            log.Text += $"job{j} started\r\n";            
            var idStatus = global.StatusManager.NewStatus($"doing job {j}");
            await DoSomeJob(j);
            global.StatusManager.ReleaseStatus(idStatus);

            log.Text += $"job{j} finished\r\n";
            if (global.StatusManager.Status == "Ready.") MessageBox.Show("Ready");            
        }

        async Task DoSomeJob(int job)
        {
            // not necessary to run a new Task inside
            // the method will run in a task cause its Task signature        

            //await Task.Run(async () =>
            //{
                for (int i = 0; i < 100; ++i)
                {
                    global.StatusManager.Status = $"job{job} working {i}";
                    await Task.Delay(50);
                }
            //});
        }

    }

}
