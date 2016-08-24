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
using SearchAThing;
using MongoDB.Bson.Serialization;
using SearchAThing.MongoDB;

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

        MongoContext ctx;
        MongoContext ctx1;
        MongoContext ctx2;

        void LoadEntityDBCurrent()
        {
            var _CTX = new MongoContext(ConnectionString);
            EntityDBCurrent = _CTX.GetRepository<DocumentEntity>().Collection.AsQueryable().Attach(_CTX).First();
            var q = EntityDBCurrent.Nested;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ctx = new MongoContext(ConnectionString);
            ctx1 = new MongoContext(ConnectionString);
            ctx2 = new MongoContext(ConnectionString);
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            {
                {
                    var coll = ctx.GetRepository<DocumentEntity>().Collection;
                    coll.AsQueryable().Foreach(w => ctx.Delete(w));
                    ctx.Save();
                }

                {
                    var coll = ctx.GetRepository<NestedDocumentEntity>().Collection;
                    coll.AsQueryable().Foreach(w => ctx.Delete(w));
                    ctx.Save();
                }

                var doc = ctx.New<DocumentEntity>();
                doc.A = "a";
                doc.B = "b";
                doc.Nested.C = "c"; doc.Nested.D = "d";
                {
                    var obc = doc.Children;

                    obc.Clear();
                    obc.Add(ctx.New(new NestedDocumentEntity("xx", "yy")));
                    obc.Add(ctx.New(new NestedDocumentEntity("cc", "dd")));
                    obc.Add(ctx.New(new NestedDocumentEntity("ee", "ff")));
                }
                ctx.Save(); // save changes created document
            }

            {
                Entity1 = ctx1.GetRepository<DocumentEntity>().Collection.AsQueryable().Attach(ctx1).First();
            }

            {
                Entity2 = ctx2.GetRepository<DocumentEntity>().Collection.AsQueryable().Attach(ctx2).First();
            }

            LoadEntityDBCurrent();
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Entity1.A = "a1";
            Entity1.Nested.C = "c1";
            Entity1.Children.Skip(1).First().C = "cc1";
            {
                // add item1
                var newItem1 = ctx1.New(new NestedDocumentEntity("ee1", "ff1"));
                Entity1.Children.Add(newItem1); // add to OBC                
            }

            Entity2.B = "b2";
            Entity2.Nested.D = "d2";
            Entity2.Children.Skip(1).First().D = "dd2";
            {
                // del item2
                var oldItem2 = Entity2.Children.Skip(2).First();
                Entity2.Children.Remove(oldItem2); // remove from OBC
                oldItem2.Delete(); // db
            }
        }

        private void Save1_Click(object sender, RoutedEventArgs e)
        {
            ctx1.Save();

            LoadEntityDBCurrent();
        }

        private void Save2_Click(object sender, RoutedEventArgs e)
        {
            ctx2.Save();

            LoadEntityDBCurrent();
        }

    }

}
