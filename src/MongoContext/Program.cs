using MongoDB.Driver;
using SearchAThing.MongoDB;
using System.ComponentModel;
using System.Linq;

namespace SearchAThing.Patterns.MongoContextSample
{

    class Program
    {

        static void Main(string[] args)
        {


            {
                var ctx = new MongoContext("mongodb://localhost:27017/searchathing_mongocontextsample");
                var repo = ctx.GetRepository<SampleA>();

                repo.Collection.DeleteMany(Builders<SampleA>.Filter.Empty);                         
            }

            {
                var ctx = new MongoContext("mongodb://localhost:27017/searchathing_mongocontextsample");

                var q = ctx.New<SampleA>();
                q.TestProperty = "xxx";
                q.SampleB.Data = "data";

                ctx.Save();
            }

            {
                var ctx = new MongoContext("mongodb://localhost:27017/searchathing_mongocontextsample");

                var q = ctx.Find<SampleA>(x => true).First();                
                q.SampleB.Data = "data12";

                ctx.Save(); // TestProperty preserved - only modified fields are saved
            }
            
        }

    }

    public class SampleA : MongoEntity
    {

        #region TestProperty [pc]
        string _TestProperty;
        public string TestProperty
        {
            get
            {
                return _TestProperty;
            }
            set
            {
                if (_TestProperty != value)
                {
                    _TestProperty = value;
                    SendPropertyChanged("TestProperty");
                }
            }
        }
        #endregion

        #region SampleB [pc]
        SampleB _SampleB;
        public SampleB SampleB
        {
            get
            {
                if (_SampleB == null) _SampleB = new SampleB();
                return _SampleB;
            }
            set
            {
                if (_SampleB != value)
                {
                    _SampleB = value;
                    SendPropertyChanged("SampleB");
                }
            }
        }
        #endregion

    }

    public class SampleB : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged [pce]       
        public event PropertyChangedEventHandler PropertyChanged;
        protected void SendPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Data [pc]
        string _Data;
        public string Data
        {
            get
            {
                return _Data;
            }
            set
            {
                if (_Data != value)
                {
                    _Data = value;
                    SendPropertyChanged("Data");
                }
            }
        }
        #endregion


    }

}
