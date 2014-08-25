using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Telerik.OpenAccess;
//using Telerik.OpenAccess.Metadata;

namespace CollaborativeLearning.WebUI.OpenAccess
{
    //public partial class CollaborativeLearningWebUIOpenAccessContext : OpenAccessContext
    //{
    //    static MetadataContainer metadataContainer = new CollaborativeLearningWebUIOpenAccessMetadataSource().GetModel();
    //    static BackendConfiguration backendConfiguration = new BackendConfiguration()
    //    {
    //        Backend = "mssql"
    //    };

    //    private const string DbConnection = "connectionID";

    //    public CollaborativeLearningWebUIOpenAccessContext()
    //        : base(DbConnection, backendConfiguration, metadataContainer)
    //    {

    //    }

    //    public IQueryable<Product> Products
    //    {
    //        get
    //        {
    //            return this.GetAll<Product>();
    //        }
    //    }

    //    public void UpdateSchema()
    //    {
    //        var handler = this.GetSchemaHandler();
    //        string script = null;
    //        try
    //        {
    //            script = handler.CreateUpdateDDLScript(null);
    //        }
    //        catch
    //        {
    //            bool throwException = false;
    //            try
    //            {
    //                handler.CreateDatabase();
    //                script = handler.CreateDDLScript();
    //            }
    //            catch
    //            {
    //                throwException = true;
    //            }
    //            if (throwException)
    //                throw;
    //        }

    //        if (string.IsNullOrEmpty(script) == false)
    //        {
    //            handler.ExecuteDDLScript(script);
    //        }
    //    }
    //}
}
