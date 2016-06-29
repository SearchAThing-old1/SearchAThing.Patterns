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
using System.Windows;
using System.Linq;
using Repository.Mongo;
using SearchAThing;

namespace SearchAThing.Patterns.MongoDBWpf
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        const string ConnectionString = @"mongodb://localhost:27017/searchathing_mongoconcurrency";

        #region Entity1 [dp]
        public static readonly DependencyProperty Entity1Property =
        DependencyProperty.Register("Entity1", typeof(DocumentEntity), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        public DocumentEntity Entity1
        {
            get
            {
                return (DocumentEntity)GetValue(Entity1Property);
            }
            set
            {
                SetValue(Entity1Property, value);
            }
        }
        #endregion

        #region Entity2 [dp]
        public static readonly DependencyProperty Entity2Property =
        DependencyProperty.Register("Entity2", typeof(DocumentEntity), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        public DocumentEntity Entity2
        {
            get
            {
                return (DocumentEntity)GetValue(Entity2Property);
            }
            set
            {
                SetValue(Entity2Property, value);
            }
        }
        #endregion

        #region EntityDBCurrent [dp]
        public static readonly DependencyProperty EntityDBCurrentProperty =
        DependencyProperty.Register("EntityDBCurrent", typeof(DocumentEntity), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        public DocumentEntity EntityDBCurrent
        {
            get
            {
                return (DocumentEntity)GetValue(EntityDBCurrentProperty);
            }
            set
            {
                SetValue(EntityDBCurrentProperty, value);
            }
        }
        #endregion

        void LoadEntityDBCurrent()
        {
            var repo = new Repository<DocumentEntity>(ConnectionString);
            EntityDBCurrent = repo.FindAll().First();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            {
                var repo = new Repository<DocumentEntity>(ConnectionString);
                repo.FindAll().ToList().Foreach(w => repo.Delete(w));
                var doc = new DocumentEntity() { A = "a", B = "b" };
                doc.Nested.C = "c"; doc.Nested.D = "d";
                {
                    var obc = new ObservableCollection<NestedDocumentEntity>();
                    obc.Add(new NestedDocumentEntity() { C = "cc", D = "dd" });
                    doc.Children = obc;
                }
                repo.Insert(doc);
            }

            {
                var repo = new Repository<DocumentEntity>(ConnectionString);
                Entity1 = repo.FindAll().First();
            }

            {
                var repo = new Repository<DocumentEntity>(ConnectionString);
                Entity2 = repo.FindAll().First();
            }

            LoadEntityDBCurrent();
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Entity1.A = "a1";
            Entity1.Nested.C = "c1";
            Entity1.Children.First().C = "cc1";
            System.Console.WriteLine($"Entity1.Children.First().ChangedProperties.Count={Entity1.Children.First().ChangedProperties.Count}");

            Entity2.B = "b2";
            Entity2.Nested.D = "d2";
            Entity2.Children.First().D = "dd2";
            System.Console.WriteLine($"Entity2.Children.First().ChangedProperties.Count={Entity2.Children.First().ChangedProperties.Count}");
        }

        private void Save1_Click(object sender, RoutedEventArgs e)
        {
            var repo = new Repository<DocumentEntity>(ConnectionString);

            var id = Entity1.Id;

            var updates = Entity1.Changes(repo.Updater).ToArray();
            System.Console.WriteLine($"Save1 : updates {updates.Length}");

            repo.Update(Entity1, updates);

            LoadEntityDBCurrent();
        }

        private void Save2_Click(object sender, RoutedEventArgs e)
        {
            var repo = new Repository<DocumentEntity>(ConnectionString);
            var id = Entity2.Id;

            var updates = Entity2.Changes(repo.Updater).ToArray();
            System.Console.WriteLine($"Save2 : updates {updates.Length}");

            repo.Update(Entity2, updates);

            LoadEntityDBCurrent();
        }

    }

}
