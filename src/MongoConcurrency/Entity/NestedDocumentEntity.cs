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

using System.ComponentModel;
using System.Collections.Generic;
using SearchAThing.MongoDB;

namespace SearchAThing.Patterns.MongoDBWpf.Ents
{

    public class NestedDocumentEntity : INotifyPropertyChanged, IMongoEntityTrackChanges, ISupportInitialize
    {

        #region IMongoEntityTrackChanges
        MongoEntityTrackChanges _TrackChanges;
        public MongoEntityTrackChanges TrackChanges { get { return _TrackChanges; } }
        #endregion

        #region ISupportInitialize
        public void BeginInit()
        {
        }

        public void EndInit()
        {
            _TrackChanges = new MongoEntityTrackChanges();
        }
        #endregion

        #region INotifyPropertyChanged [pce]       
        public event PropertyChangedEventHandler PropertyChanged;
        protected void SendPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region C [pc]
        string _C;
        public string C
        {
            get
            {
                return _C;
            }
            set
            {
                if (_C != value)
                {
                    _C = value;
                    TrackChanges?.ChangedProperties.Add("C"); // use of ? operator ( until endinit is null )
                    SendPropertyChanged("C");
                }
            }
        }
        #endregion

        #region D [pc]
        string _D;
        public string D
        {
            get
            {
                return _D;
            }
            set
            {
                if (_D != value)
                {
                    _D = value;
                    TrackChanges?.ChangedProperties.Add("D"); // use of ? operator ( until endinit is null )
                    SendPropertyChanged("D");
                }
            }
        }
        #endregion


    }

}
