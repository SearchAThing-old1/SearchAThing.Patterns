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

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace UITask
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pick a filename");

            var v = await CompositeFunction();

            Console.WriteLine($"filename [{v}]");
        }

        async Task<string> CompositeFunction()
        {
            await PureLogic();

            return await GUI();
        }

        async Task<string> GUI()
        {
            string content = "";

            await Dispatcher.InvokeAsync(() => // do not use any async () => lambda here (argument here is an Action)
            {
                // do not use any await in this body
                var ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true) content = ofd.FileName;
            });

            // if need to execute other gui methods in form of async Task
            // await them outside the Dispatcher.InvokeAsync()
            content += await GUI2();

            return content;
        }

        async Task<string> GUI2()
        {
            var c = "";
            await Dispatcher.InvokeAsync(() =>
            {
                c = (string)btn.Content;
            });
            return c;
        }

        async Task<bool> PureLogic()
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine($"middle-fn delay begin");
            await Task.Delay(1000);
            Console.WriteLine($"middle-fn delay end : {sw.Elapsed}");

            return true;
        }

        //-------------------------

        async Task IntensiveJob()
        {
            await Task.Run(() =>
            {
                var begin = DateTime.Now;

                while ((DateTime.Now - begin).TotalSeconds <= 10)
                {
                    // note that here I use Dispatcher.Invoke because the lambda method () => of the Task
                    // is not async, but if it was then you need to use Dispatcher.InvokeAsync(() => { });
                    Dispatcher.Invoke(() => btnDoJob.Content = DateTime.Now.Millisecond.ToString());
                }
            });
        }

        private async void btnDoJob_Click(object sender, RoutedEventArgs e)
        {
            await IntensiveJob();

            MessageBox.Show("Finished");
        }

    }

}
