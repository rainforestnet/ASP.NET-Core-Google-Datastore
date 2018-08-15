using Google.Cloud.Datastore.V1;

namespace WmNosql.Proxy.Core
{
    public class BaseProxy
    {
        protected readonly DatastoreDb _db;
        protected KeyFactory _keyFactory;
        protected const string PROJECT_ID = "wm-nosql";
        protected string _environment; //i.e. Development or Production
        protected Key _envKey;

        protected const string ENVIRONMENT = "ENV";

        public BaseProxy(string tenantId, string environment)
        {
            _environment = environment;
            _db = DatastoreDb.Create(PROJECT_ID, tenantId);
            _envKey = _db.CreateKeyFactory(ENVIRONMENT).CreateKey(_environment);
        }
    }
}
