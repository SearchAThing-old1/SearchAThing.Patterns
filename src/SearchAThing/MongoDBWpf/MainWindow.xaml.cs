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

using MongoDB.Driver;
using SearchAThing.Patterns.MongoDBWpf.Ents;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using SearchAThing.MongoDB;

namespace SearchAThing.Patterns.MongoDBWpf
{

    public partial class MainWindow : Window
    {
        
        ObservableCollection<Contact> obc;
        MongoContext ctx;
        ITypedMongoRepository<Contact> contacts;

        //Repository<Contact> contacts;

        public MainWindow()
        {
            InitializeComponent();

            dg.RowEditEnding += Dg_RowEditEnding;
        }

        private void Dg_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            var item = (Contact)e.Row.Item;
            ctx.Save(); // save only changed fields
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ctx = new MongoContext("mongodb://localhost:27017/searchathing_patterns_mongodbwpf");
            contacts = ctx.GetRepository<Contact>();
            obc = new ObservableCollection<Contact>(contacts.Collection.AsQueryable());
            dg.ItemsSource = obc;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var newContact = ctx.New<Contact>();
            newContact.Name = "newName";
            newContact.Phone = "newPhone";            
            obc.Add(newContact);
            ctx.Save(); // save inserted contact
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            var curIdx = dg.SelectedIndex;

            var sel = dg.SelectedItems.Cast<Contact>().ToList();
            foreach (var x in sel)
            {
                obc.Remove(x);
                x.Delete();
            }
            ctx.Save(); // delete seleced contacts

            if (curIdx < dg.Items.Count) dg.SelectedIndex = curIdx;
            else if (dg.Items.Count > 0) dg.SelectedIndex = dg.Items.Count - 1;
        }

    }

}
