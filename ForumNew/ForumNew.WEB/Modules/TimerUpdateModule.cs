//using ForumNew.DAL.EF;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Web;

//namespace ForumNew.WEB.Modules
//{
//    public class TimerUpdateModule : IHttpModule
//    {
//        static Timer timer;
//        int period = 600000;

//        public void Init(HttpApplication context)
//        {
//            timer = new Timer(new TimerCallback(Update), null, 0, period);
//        }

//        private async void Update(object obj)
//        {
//            using (ApplicationDbContext db = new ApplicationDbContext("UserConnection"))
//            {
//                foreach (var item in db.Users.Where(i => i.Online == true))
//                {
//                    TimeSpan time = DateTime.Now - item.TimeLogin;
//                    if (time.TotalSeconds > 60)
//                    {
//                        item.Online = false;
//                    }
//                }
//                await db.SaveChangesAsync();
//            }
//        }

//        public void Dispose()
//        {
//        }
//    }

//}