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

using Repository.Mongo;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System;
using System.Globalization;
using System.Linq;

namespace SearchAThing.Patterns.MongoDBWpf.Ents
{

    public class DocumentEntity : Entity, INotifyPropertyChanged, IMongoEntityTrackChanges, ISupportInitialize
    {

        #region IMongoEntityTrackChanges
        HashSet<string> _ChangedProperties;
        public HashSet<string> ChangedProperties { get { return _ChangedProperties; } }
        #endregion

        #region ISupportInitialize
        public void BeginInit()
        {
        }

        public void EndInit()
        {
            _ChangedProperties = new HashSet<string>();
        }
        #endregion

        #region INotifyPropertyChanged [pce]       
        public event PropertyChangedEventHandler PropertyChanged;
        protected void SendPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region A [pc]
        string _A;
        public string A
        {
            get
            {
                return _A;
            }
            set
            {
                if (_A != value)
                {
                    _A = value;
                    ChangedProperties?.Add("A"); // use of ? operator ( until endinit is null )
                    SendPropertyChanged("A");
                }
            }
        }
        #endregion

        #region B [pc]
        string _B;
        public string B
        {
            get
            {
                return _B;
            }
            set
            {
                if (_B != value)
                {
                    _B = value;
                    ChangedProperties?.Add("B"); // use of ? operator ( until endinit is null )
                    SendPropertyChanged("B");
                }
            }
        }
        #endregion

        #region Nested [pc]
        NestedDocumentEntity _Nested;
        public NestedDocumentEntity Nested
        {
            get
            {
                if (_Nested == null) _Nested = new NestedDocumentEntity();
                return _Nested;
            }
            set
            {
                if (_Nested != value)
                {
                    _Nested = value;
                    SendPropertyChanged("Nested");
                }
            }
        }
        #endregion

        #region Children [pc]
        ObservableCollection<NestedDocumentEntity> _Children;
        public ObservableCollection<NestedDocumentEntity> Children
        {
            get
            {
                return _Children;
            }
            set
            {
                if (_Children != value)
                {
                    _Children = value;
                    SendPropertyChanged("Children");
                }
            }
        }
        #endregion

    }

}
