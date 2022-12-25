using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using System.Text;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class PdfRepository : IPdf
    {
        readonly DatabaseContext _dbContext = new();

        public PdfRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public string GetHTMLString(int id)
        {
            try
            {
                DietModel? dietModel = _dbContext.Diets!.Find(id);
                ClientModel? clientModel = _dbContext.Client!.Find(dietModel.clientId);
                if (dietModel != null && clientModel != null)
                {
                    List<DietDayModel> dietDayList = _dbContext.DietDays!.Where(x => x.dietId.Equals(id)).ToList();

                    var sb = new StringBuilder();
                    sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <h1 style='margin-top:12.0pt;margin-right:0in;margin-bottom:0in;margin-left:0in;line-height:107%;font-size:21px;font-family:""Calibri Light"",
sans-serif;color:#2F5496;font-weight:normal;text-align:center;'>Fitim App Diyet Raporu</h1>
                                ");
                    sb.AppendFormat(@"<p style='margin-top:0in;margin-right:0in;margin-bottom:8.0pt;margin-left:0in;line-height:107%;font-size:15px;font-family:""Calibri"",
sans-serif;'><span style=""color:#4472C4;font-style:italic;"">{0}, {1}</span></p>", clientModel.clientName, clientModel.clientAge);
                    sb.AppendFormat(@"<p style='margin-top:0in;margin-right:0in;margin-bottom:8.0pt;margin-left:0in;line-height:107%;font-size:15px;font-family:""Calibri"",
sans-serif;'><span style=""color:#4472C4;font-style:italic;"">Diyet Baslangic Tarihi: &ldquo;{0}&rdquo;</span></p>", dietModel.dietStartDate);
                    sb.Append(@"<p style='margin-top:0in;margin-right:0in;margin-bottom:8.0pt;margin-left:0in;line-height:107%;font-size:15px;font-family:""Calibri"",
sans-serif;'><span style=""color:#4472C4;font-style:italic;"">Basladigi Kilo / Guncel Kilo: 89 / 84</span></p>");
                    sb.Append(@"<ul style=""list-style-type: disc;"">");
                    foreach (var dietDays in dietDayList)
                    {
                        sb.AppendFormat(@"<li>{0}<ol style=\""list-style-type: circle;\"">", dietDays.dietTime);
                        List<DietMenuModel> dietDayMenuList = _dbContext.DietDayMenus!.Where(x => x.dietDayId.Equals(dietDays.dietDayId)).ToList();
                        foreach (var dietMenu in dietDayMenuList)
                        {
                            sb.AppendFormat(@"<li>{0}<ul style=""list-style-type: square;"">
                    <li>{1}</li>
                </ul>
            </li>", dietMenu.dietMenuTitle, dietMenu.dietMenuDetail);
                        }
                        sb.Append(@"</ol>
    </li>
");
                    }

                    sb.Append(@"
</ul>
                            </body>
                        </html>");
                    return sb.ToString();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }


    }
}
