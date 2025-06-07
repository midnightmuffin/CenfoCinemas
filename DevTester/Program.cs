using DataAccess.DAO;
using System.Net.NetworkInformation;
using System.Xml.Linq;

public class Program {
    public static void Main(string[] args)
    {
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "CRE_USER_PR";

        sqlOperation.AddStringParameter("P_UserCode", "fzuniga");
        sqlOperation.AddStringParameter("P_Name", "Fabiola");
        sqlOperation.AddStringParameter("P_Email", "fzunigav@ucenfotec.ac.cr");
        sqlOperation.AddStringParameter("P_Password", "Fabiola123!");
        sqlOperation.AddDateTimeParam("P_BirthDate", DateTime.Now);
        sqlOperation.AddStringParameter("P_Status", "AC");

        var sqlDao = SqlDao.GetInstance();

        sqlDao.ExecuteProcedure(sqlOperation);
    }
}
