using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using flair.Models;
using flair.Services;

namespace flair.App_Start
{
    public class BadgeHandler : IHttpHandler
    {
        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            // 5095 = robzy
            // 9541 = kikz       

            var userIdString = context.Request.Url.PathAndQuery;
            userIdString = userIdString.Substring(1, userIdString.Length - 5);

            if (!Regex.IsMatch(userIdString, "([0-9]{1,6})"))
            {
                return;
            }

            var userId = int.Parse(userIdString);   
            if (userId == 0)
            {
                return;
            }

            var service = new AtomicUserService();
            var user = service.GetUserInformation(userId);

            if (user == null)
            {
                return;
            }

            context.Response.ContentType = "image/png";

            var badgeFile = context.Server.MapPath("/images/" + userId + ".png");
            if (!System.IO.File.Exists(badgeFile))
            {
                var userModel = AtomicUserViewModel.Create(user);
                userModel.SpecialRankColor = user.IsHeroic
                                                 ? "DodgerBlue"
                                                 : user.IsMod ? "Red" : user.IsGod ? "Purple" : "White";

                Bitmap badge = new Bitmap(300, 80);

                var originalImagePath = context.Server.MapPath("/images/original/" + userModel.UserId + ".png");
                if (!System.IO.File.Exists(originalImagePath))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(userModel.ImageUrl, originalImagePath);
                    }
                }

                var avatarImage = new Bitmap(originalImagePath);
                avatarImage = Resize(avatarImage, 70);

                using (var g = Graphics.FromImage((Image)badge))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    g.FillRectangle(Brushes.Black, 0, 0, badge.Width, badge.Height);
                    g.DrawString(userModel.Name, new Font("Tahoma", 13), Brushes.White, new RectangleF(90, 5, 200, 20));
                    if (!string.IsNullOrEmpty(userModel.SpecialRank))
                    {
                        g.DrawString(userModel.SpecialRank, new Font("Tahoma", 11),
                                     new SolidBrush(Color.FromName(userModel.SpecialRankColor)),
                                     new RectangleF(90, 30, 200, 20));

                        g.DrawString(userModel.Rank, new Font("Tahoma", 11), Brushes.White,
                                     new RectangleF(90, 50, 200, 20));
                    }
                    else
                    {
                        g.DrawString(userModel.Rank, new Font("Tahoma", 11), Brushes.White,
                                     new RectangleF(90, 30, 200, 20));
                    }
                    g.Flush();
                }

                CopyRegionIntoImage(avatarImage, ref badge);
                badge.Save(context.Server.MapPath("/images/" + userModel.UserId + ".png"));                

                badge.Save(context.Response.OutputStream, ImageFormat.Png);
            }
            
            var cachedBadge = new Bitmap(badgeFile);                            
            cachedBadge.Save(context.Response.OutputStream, ImageFormat.Png);

            context.Response.Flush();
        }

        private static Bitmap Resize(Bitmap avatar, int height)
        {
            Bitmap b = new Bitmap(height, height);
            try
            {
                using (Graphics g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(avatar, 0, 0, height, height);
                }
            }
            catch { }
            return b;
        }

        private static void CopyRegionIntoImage(Bitmap srcBitmap, ref Bitmap destBitmap)
        {
            var srcRegion = new Rectangle(new Point(0, 0), new Size(new Point(srcBitmap.Width, srcBitmap.Height)));
            var destRegion = new Rectangle(new Point(5, 5), new Size(new Point(srcBitmap.Width, srcBitmap.Height)));

            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }
    }
}