using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using flair.Models;

namespace flair.Services
{    
    public class AtomicUserService : IAtomicUserService
    {
        public AtomicUser GetUserInformation(int userId)
        {
            try
            {
                var user = new AtomicUser(userId);
                var rawUserPage = GetRawAtomicUserData(user.ProfileUrl);

                var enc = new ASCIIEncoding();
                var userPage = new HtmlDocument();
                userPage.Load(new StreamReader(new MemoryStream(enc.GetBytes(rawUserPage))));
                var root = userPage.DocumentNode;

                // Extract user information from the page.
                //<div class='pp-name'>
                //    <table cellpadding='0' cellspacing='0' width='100%'>
                //    <tr>
                //        <td width='1%'><img src='http://forums.atomicmpc.com.au/uploads/av-40858.jpg' border='0' width='78' height='78' alt='' /></td>
                //        <td width='98%' style='padding-left:10px'>
                //            <h3 style='font-size:20px'>Jeruselem</h3>
                //            <strong>Atomican</strong>
                //            <p></p>
                //        </td>

                //    </tr>
                //    </table>
                //</div>                                                
                var userDetailsNode = root.SelectSingleNode("//div[@class='pp-name']");
                if (userDetailsNode != null)
                {
                    // Don't need to have an avatar.
                    var imgNode = userDetailsNode.Descendants("img").FirstOrDefault();
                    if (imgNode != null) user.ImageUrl = imgNode.GetAttributeValue("src", "");

                    var nameNode = userDetailsNode.Descendants("h3").FirstOrDefault();
                    if (nameNode != null) user.Name = nameNode.InnerText;

                    var specialRankNode = userDetailsNode.Descendants("strong").FirstOrDefault();
                    if (specialRankNode != null) user.SpecialRank = specialRankNode.InnerText;
                }

                //<!-- Personal Info -->
                //<div class='subtitle'>Personal Info</div>
                //<div class='row1' style='padding:6px; padding-left:10px'>Jeruselem</div>                        
                //<div class='row1' style='padding:6px; padding-left:10px'>Champion</div>
                var info = root.SelectNodes("//comment()");
                foreach (HtmlNode node in info)
                {
                    if (node.InnerHtml == "<!-- Personal Info -->")
                    {
                        user.Rank =
                            node.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                    }
                }

                return user;
            }
            catch { }

            return null;
        }

        public string GetRawAtomicUserData(string uri)
        {
            string userIdResponse = string.Empty;
            HttpWebResponse webResponse;

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";

            try
            {
                webResponse = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                throw new Exception("No response received from the payclick service: " + ex.Message, ex);
            }

            using (var responseReader = new StreamReader(webResponse.GetResponseStream()))
            {
                userIdResponse = responseReader.ReadToEnd();
            }

            if (webResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Unable to access the information required");
            }

            return userIdResponse;
        }
    }
}