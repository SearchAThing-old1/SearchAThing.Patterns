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
using SearchAThing.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MongoScopedChildren
{
    class Program
    {

        static void Main(string[] args)
        {

            var repo = new Repository<BaseDoc>("mongodb://localhost:27017/searchathing_patterns_mongoscopedchildren");

            {
                repo.Delete((x) => true);
            }

            {
                var A = new BaseDoc();
                A.Child = new ChildClass(A);
                repo.Insert(A);
            }

            {
                var A = repo.FindAll().First();
                A.Child.DoSomeJob();
            }

        }

    }

    /// <summary>
    /// Base document is the collection document, it need to:
    /// - implement ISupportInitialize to bind child to itself in the EndInit()    
    /// </summary>
    public class BaseDoc : Entity, ISupportInitialize
    {

        #region ISupportInitialize
        public void BeginInit()
        {
        }

        public void EndInit()
        {
            foreach (var x in ScopedChildrenManager.GetScopedChildrenList()) x.SetScope(this);
            ScopedChildrenManager.ClearScopedChildrenList();
        }
        #endregion

        #region Child [pgs]
        public ChildClass Child { get; set; }
        #endregion

        public List<int> SomeBaseInfo()
        {
            return new List<int>() { 1, 2, 3 };
        }

    }

    /// <summary>
    /// Any type used in the document subtree that want to access the base document, need to:
    /// - implement IScopeChild ( for the SetScope(...) method )
    /// - implement ISupportInitialize to register itself on the BeginInit()
    /// - have a private default constructor ( for deserialization )
    /// - have a public constructor that save the base doc reference for the run-time operations.
    /// </summary>
    public class ChildClass : IScopedChild, ISupportInitialize
    {

        /// <summary>
        /// Default constructor ( for deserialization )
        /// </summary>
        private ChildClass()
        {
        }

        /// <summary>
        /// Constructor for run-time object allocation.
        /// </summary>        
        public ChildClass(BaseDoc __BaseDoc)
        {
            _BaseDoc = __BaseDoc;
        }

        #region ISupportInitialize
        public void BeginInit()
        {
            ScopedChildrenManager.RegisterScopedChild(this);
        }

        public void EndInit()
        {
        }
        #endregion

        #region BaseDoc [pfg]
        BaseDoc _BaseDoc;
        public BaseDoc BaseDoc
        {
            get
            {
                return _BaseDoc;
            }
        }
        #endregion

        #region IScopeChild
        /// <summary>
        /// In this child SetScope will called from the ISupportInitialize root at EndInit().
        /// </summary>        
        public void SetScope(object root)
        {
            _BaseDoc = root as BaseDoc;
        }
        #endregion

        /// <summary>
        /// This method demonstrate the child can access base document.
        /// </summary>
        public void DoSomeJob()
        {
            foreach (var x in BaseDoc.SomeBaseInfo()) Console.WriteLine(x);
        }

    }

}
