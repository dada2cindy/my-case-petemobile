using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Container;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.SystemApplications;
using WuDada.Core.SystemApplications.Service;
using System.Globalization;
using WuDada.Core.Generic.Mail;
using WuDada.Core.Post.DTO;
using WuDada.Core.Post.DTOConverter;
using WuDada.Core.Generic.Util;
using WuDada.Provider.ResourceHandle;
using WuDada.Provider.ResourceHandle.Service;
using WuDada.Provider.ResourceHandle.Domain;
using System.IO;
using WuDada.Core.Member.Domain;
using WuDada.Core.Member.Service;
using WuDada.Core.Member;
using WuDada.Core.Accounting.Service;
using WuDada.Core.Accounting.Domain;

namespace GalaxyClinic.Test.Service
{
    [TestFixture]
    public class InitWebDataService
    {
        private AuthFactory m_AuthFactory { get; set; }
        private PostFactory m_PostFactory { get; set; }
        private SystemFactory m_SystemFactory { get; set; }
        private StorageFactory m_StorageFactory { get; set; }
        private IAuthService m_AuthService { get; set; }
        private IPostService m_PostService { get; set; }
        private ITemplateService m_TemplateService { get; set; }
        private ISystemService m_SystemService { get; set; }
        private IMessageService m_MessageService { get; set; }
        private IStorageFileService m_StorageFileService { get; set; }
        private MemberFactory m_MemberFactory { get; set; }
        private IMemberService m_MemberService { get; set; }
        private AccountingFactory m_AccountingFactory { get; set; }
        private IAccountingService m_AccountingService { get; set; }        

        [TestFixtureSetUp]
        public void TestCaseInit()
        {
            m_AuthFactory = new AuthFactory();
            m_PostFactory = new PostFactory();
            m_SystemFactory = new SystemFactory();
            m_StorageFactory = new StorageFactory();
            m_MemberFactory = new MemberFactory();
            m_AccountingFactory = new AccountingFactory();
            m_AuthService = m_AuthFactory.GetAuthService();
            m_PostService = m_PostFactory.GetPostService();
            m_TemplateService = m_SystemFactory.GetTemplateService();
            m_SystemService = m_SystemFactory.GetSystemService();
            m_MessageService = m_PostFactory.GetMessageService();
            m_StorageFileService = m_StorageFactory.GetStorageFileService();
            m_MemberService = m_MemberFactory.GetMemberService();
            m_AccountingService = m_AccountingFactory.GetAccountingService();
        }

        [Test]
        public void InitWebData()
        {
            InitMenu();
            InitLoginRoleAndUser();
            InitNode();
            InitSystemParam();
        }

        [Test]
        public void Test_PostSearch()
        {
            string key = "ggg+odu";
            string[] keys = key.Split('+');
            string[] columns = new string[] { "Title", "TitleENG", "HtmlContent", "HtmlContentENG", "HtmlContent2", "HtmlContent2ENG" };

            StringBuilder sbHql = new StringBuilder();
            //sbHql.Append("WHERE ParentPost.PostId = 4 ");
            sbHql.Append(string.Format(@"WHERE Flag = {0} 
AND ShowDate <= '{1}' AND CloseDate >= '{1}'
AND ParentPost.ParentPost.ParentPost.PostId = {2} 
AND {3} 
ORDER BY SortNo "
                , 1, DateTime.Today.ToShortDateString(), 4
                , ConvertUtil.ConvetSqlLikeStr(keys, columns, "OR")));
            //, ConvertUtil.ConvetSqlLikeStr(keys, columns, "OR")
            //"Title like '%odu%' "
            IList<PostVO> postList = m_PostService.SearchPostByWhere(sbHql.ToString());
            Console.Write("postList count =" + postList.Count);
        }

        [Test]
        public void Test_UserMenuFuncContainer()
        {
            //清除快取
            UserMenuFuncContainer.GetInstance().ReloadAllMenu();

            UserMenuFuncContainer.GetInstance().GetUser("admin");
            UserMenuFuncContainer.GetInstance().GetUser("admin");
        }

        [Test]
        public void Test_InitContactorMail()
        {
            string classify = "聯絡我們收件者";
            ItemParamVO contactor = new ItemParamVO(classify, "吳信達", "dada2cindy@hotmail.com", false);
            m_SystemService.CreateItemParam(contactor);

            ItemParamVO contactor2 = new ItemParamVO(classify, "王小小", "dada@pro2e.com.tw", false);
            m_SystemService.CreateItemParam(contactor2);

            IList<ItemParamVO> contactorList = m_SystemService.GetAllItemParamByNoDel(classify);
            Console.WriteLine("contactorList count =" + contactorList.Count);
        }

        [Test]
        public void Test_SendMessageMail()
        {
            //建立一篇訊息
            MessageVO messageVO = new MessageVO();
            messageVO.Content = "意見";
            messageVO.CreateName = "張大保";
            messageVO.EMail = "dada2338@yahoo.com.tw";
            messageVO.Fax = "23223333";
            messageVO.Phone = "22234563";
            messageVO.Mobile = "0912333444";
            messageVO.CreatedDate = DateTime.Now;
            messageVO.CreateIP = "127.0.0.1";

            messageVO = m_MessageService.CreateMessage(messageVO);

            string classify = "聯絡我們收件者";
            IList<ItemParamVO> contactorList = m_SystemService.GetAllItemParamByNoDel(classify);

            if (contactorList != null && contactorList.Count > 0)
            {
                SystemParamVO mailVO = m_SystemService.GetSystemParamByRoot();
                MailService mailService = new MailService(mailVO.MailSmtp, int.Parse(mailVO.MailPort), mailVO.EnableSSL, mailVO.Account, mailVO.Password);

                StringBuilder sbMailList = new StringBuilder();
                foreach (ItemParamVO contactor in contactorList)
                {
                    sbMailList.Append(string.Format("{0};", contactor.Value));
                }

                string mailTitle = string.Format("收到一封由【{0}】從產基會網站提出的意見信。", messageVO.CreateName);
                string mailContent = GenMailContent(messageVO);

                mailService.SendMail(mailVO.SendEmail, sbMailList.ToString(), mailTitle, mailContent);
            }
        }

        private string GenMailContent(MessageVO messageVO)
        {
            StringBuilder sbContent = new StringBuilder();

            sbContent.Append(string.Format("時　　間：{0}<br />", messageVO.CreatedDate.Value.ToString()));
            sbContent.Append(string.Format("姓　　名：{0}<br />", messageVO.CreateName));
            sbContent.Append(string.Format("電　　話：{0}<br />", messageVO.Phone));
            sbContent.Append(string.Format("手　　機：{0}<br />", messageVO.Mobile));
            sbContent.Append(string.Format("傳　　真：{0}<br />", messageVO.Fax));
            sbContent.Append(string.Format("電子信箱：{0}<br />", messageVO.EMail));
            sbContent.Append(string.Format("意　　見：{0}<br />", messageVO.Content.Replace("\n", "<br />")));

            return sbContent.ToString();
        }

        [Test]
        public void TestDay()
        {
            TaiwanLunisolarCalendar tlc = new TaiwanLunisolarCalendar();

            int year = tlc.GetYear(DateTime.Now);
            int month = tlc.GetMonth(DateTime.Now);
            int day = tlc.GetDayOfMonth(DateTime.Now);

            string aa = string.Format("{0}/{1}/{2}", year, month, day);
            Console.WriteLine("day = " + aa);

            DateTime date2 = tlc.ToDateTime(year, month, day, 0, 0, 0, 0);
            Console.WriteLine("day2 = " + date2.ToShortDateString());
        }

        [Test]
        public void TestPostVOConverter()
        {
            IList<PostVO> postList = m_PostService.GetPostListByParentPostId(22);

            IList<ReportPostVO> reportList = PostVOConverter.ToDataTransferObjects(postList, 2);
            Console.WriteLine("reportList = " + reportList.Count);

            //IList<ReportPostVO> reportList2 = PostVOConverter.ToDataTransferObjects(postList, 3);
            //Console.WriteLine("reportList2 = " + reportList2.Count);

            //IList<ReportPostVO> reportList3 = PostVOConverter.ToDataTransferObjects(postList, 6);
            //Console.WriteLine("reportList3 = " + reportList3.Count);
        }

        [Test]
        public void TestCurrentTemplate()
        {
            TemplateVO templateVO = m_TemplateService.GetCurrentTemplate(7);
            Console.WriteLine("TestCurrentTemplate = " + templateVO.Name);
        }

        [Test]
        public void TestGetPostListByNodeId()
        {
            int nodeId = 22;

            //搜尋條件
            DateTime? startDate = DateTime.Today;
            string sortField = "ShowDate";
            bool sortDesc = true;
            int pageSize = 2;

            IList<PostVO> postList = m_PostService.GetPostListByNodeId(nodeId, true, startDate, 0, pageSize, sortField, sortDesc);

            int count = postList.Count;
            Console.WriteLine("postList count = " + count);
        }

        [Test]
        public void TestGetStorageDirectory()
        {
            DirectoryInfo di = m_StorageFileService.GetStorageDirectory(FolderType.UPLOAD_FOLDER, true);
            Console.WriteLine("di = " + di.FullName);            
        }

        [Test]
        public void TestCreateStorageFile()
        {
            string sourceFilePath =@"D:\我的case\小布\鋁門窗\SVN上傳\trunk\doc\規格說明書附件.docx";
            FileInfo fileInfo = new FileInfo(sourceFilePath);
            DirectoryInfo dir = m_StorageFileService.GetStorageDirectory(FolderType.UPLOAD_FOLDER, true);
            string newFileName = Guid.NewGuid().ToString() + fileInfo.Extension;
            string destFileName = Path.Combine(dir.FullName, newFileName);
            File.Copy(sourceFilePath, destFileName);

            StorageFileVO storageFileVO = new StorageFileVO();
            storageFileVO.SourceUri = fileInfo.FullName;
            storageFileVO.CurrentPath = destFileName;
            storageFileVO.FileName = fileInfo.Name;
            storageFileVO.DisplayName = fileInfo.Name;
            storageFileVO.FileSize = fileInfo.Length;
            storageFileVO.SourceType = StorageFileVO.StorageSourceType.Post;
            storageFileVO.SourceId = 1;
            storageFileVO.CreatedBy = "admin";
            storageFileVO.UpdatedBy = "admin";
            storageFileVO.CreatedDate = DateTime.Now;
            storageFileVO.UpdatedDate = DateTime.Now;
            m_StorageFileService.CreateStorageFile(storageFileVO);
        }

        [Test]
        public void Test_Member()
        {
            IDictionary<string, string> conditions = new Dictionary<string, string>();

            int memberCount = m_MemberService.GetMemberCount(conditions);
            IList<MemberVO> memberList = m_MemberService.GetMemberList(conditions);
            Assert.AreEqual(memberCount, memberList.Count);
        }

        [Test]
        public void Test_IsAdmin()
        {
            LoginUserVO user = m_AuthService.GetLoginUserById("petechen");
            Assert.IsTrue(m_AuthService.IsAdmin(user));
        }

        [Test]
        public void Test_GetSalesStatistics()
        {
            IList<SalesStatisticsVO> list = m_AccountingService.GetSalesStatistics("201507", "興安總店");
            Assert.IsNotNull(list);
        }

        [Test]
        public void Test_GetSalesStatisticsByLoginUser()
        {
            IList<SalesStatisticsVO> list = m_AccountingService.GetSalesStatisticsByLoginUser("201608");
            Assert.IsNotNull(list);
        }

        [Test]
        public void Test_UpdateCash()
        {
            m_AccountingService.UpdateCash();
            CashStatisticsVO cashStatisticsVO = m_AccountingService.GetCashStatisticsVO(DateTime.Today);
            Assert.IsNotNull(cashStatisticsVO);
        }

        [Test]
        public void Test_UpdateCashByPeriod()
        {
            DateTime dateFrom = DateTime.Parse("2016/01/01");
            DateTime dateTo = DateTime.Today.AddDays(-1);
            m_AccountingService.UpdateCashByPeriod(dateFrom, dateTo);
            CashStatisticsVO cashStatisticsVO = m_AccountingService.GetCashStatisticsVO(DateTime.Today);
            Assert.IsNotNull(cashStatisticsVO);
        }

        [Test]
        public void Test_GetCashStatisticsVO()
        {            
            CashStatisticsVO cashStatisticsVO = m_AccountingService.GetCashStatisticsVO(DateTime.Parse("2016/02/02"));
            Assert.IsNotNull(cashStatisticsVO);
        }

        [Test]
        public void Test_CreateTarget()
        {
            IList<TargetVO> targetList = new List<TargetVO>();

            double targetAmount = 20000;

            IList<LoginUserVO> userList = m_AuthService.GetAllLoginUserList();
            foreach (LoginUserVO user in userList)
            {
                TargetVO targetVO = new TargetVO();
                targetVO.Id = string.Format("{0}{1}", "201601", user.FullNameInChinese);
                targetVO.Name = user.FullNameInChinese;
                targetVO.Amount = targetAmount;
                targetAmount += 5000;
                targetList.Add(targetVO);
            }

            m_AccountingService.UpdateTargetList(targetList);
        }

        private void InitMenu()
        {
            MenuFuncVO parentMenu41 = CreateParentMenu("網站管理", 1000);
            CreateSubMenu("手機價格列表", parentMenu41, 1001, "admin/UC04/0432.aspx");
            CreateSubMenu("配件價格列表", parentMenu41, 1002, "admin/UC04/0433.aspx");
            CreateSubMenu("門號折扣列表", parentMenu41, 1003, "admin/UC04/0442.aspx");
            CreateSubMenu("門市據點列表", parentMenu41, 1004, "admin/UC05/0521.aspx");
            CreateSubMenu("品牌管理", parentMenu41, 1005, "admin/UC04/0431.aspx");
            CreateSubMenu("電信公司管理", parentMenu41, 1006, "admin/UC04/0441.aspx");

            MenuFuncVO parentMenu7 = CreateParentMenu("網站設定與廣告管理", 8000);
            CreateSubMenu("首頁廣告", parentMenu7, 8001, "admin/UC07/0701.aspx");
            CreateSubMenu("網站右方廣告", parentMenu7, 8002, "admin/UC07/0702.aspx");
            //CreateSubMenu("首頁Facebook設定", parentMenu7, 8003, "admin/UC07/0711.aspx");

            //MenuFuncVO parentMenu42 = CreateParentMenu("服務項目管理", 2000);
            //CreateSubMenu("服務項目分類", parentMenu42, 2001, "admin/UC04/0421.aspx");
            //CreateSubMenu("服務項目列表", parentMenu42, 2002, "admin/UC04/0422.aspx");

            //MenuFuncVO parentMenu41 = CreateParentMenu("美麗見證管理", 3000);
            //CreateSubMenu("美麗見證分類", parentMenu41, 3001, "admin/UC04/0411.aspx");
            //CreateSubMenu("美麗見證列表", parentMenu41, 3002, "admin/UC04/0412.aspx");

            //MenuFuncVO parentMenu53 = CreateParentMenu("關於臻美管理", 4000);
            //CreateSubMenu("關於臻美列表", parentMenu53, 4001, "admin/UC05/0531.aspx");

            //MenuFuncVO parentMenu52 = CreateParentMenu("醫療團隊管理", 6000);
            //CreateSubMenu("醫師陣容列表", parentMenu52, 6001, "admin/UC05/0521.aspx");
            //CreateSubMenu("儀器設備列表", parentMenu52, 6002, "admin/UC05/0522.aspx");             
           

            //MenuFuncVO parentMenu3 = CreateParentMenu("活動資訊管理", 5000);
            ////CreateSubMenu("相簿分類", parentMenu3, 3001, "admin/UC03/0302.aspx");
            //MenuFuncVO subMenuFuncVO3_1 = CreateSubMenu("活動列表", parentMenu3, 5002, "admin/UC03/0301.aspx");
            //m_AuthService.AddOtherPath(subMenuFuncVO3_1, "admin/UC03/0301_1.aspx");//相簿圖片管理            

            //MenuFuncVO parentMenu6 = CreateParentMenu("聯絡臻美/收件者信箱管理", 7000);
            //CreateSubMenu("線上諮詢紀錄", parentMenu6, 7001, "admin/UC06/0601.aspx");
            //CreateSubMenu("收件者信箱設定", parentMenu6, 7002, "admin/UC06/0602.aspx");
            //CreateSubMenu("人才招募列表", parentMenu6, 7003, "admin/UC01/0111.aspx");            

            MenuFuncVO parentMenu30 = CreateParentMenu("個人設定", 99998);
            CreateSubMenu("登入密碼變更", parentMenu30, 1, "admin/UC30/3001Personal.aspx");

            MenuFuncVO parentMenu14 = CreateParentMenu("權限管理", 99999);
            CreateSubMenu("帳號管理", parentMenu14, 1, "admin/UC14/UserAdd.aspx");
            CreateSubMenu("群組管理", parentMenu14, 2, "admin/UC14/RoleAdd.aspx");
            CreateSubMenu("帳號群組設定", parentMenu14, 3, "admin/UC14/UserRoleSet.aspx");
            CreateSubMenu("群組權限設定", parentMenu14, 4, "admin/UC14/RoleFuncSet.aspx");
            CreateSubMenu("使用紀錄", parentMenu14, 5, "admin/UC14/QueryLog.aspx");
        }

        private void InitLoginRoleAndUser()
        {
            //建立後台角色
            LoginRoleVO loginRoleVO = new LoginRoleVO("系統管理員");
            loginRoleVO.MenuFuncList = m_AuthService.GetNotTopMenuFunc(); //角色功能權限
            m_AuthService.CreateLoginRole(loginRoleVO);

            LoginRoleVO loginRoleVO2 = new LoginRoleVO("行銷人員");
            loginRoleVO2.MenuFuncList = m_AuthService.GetNotTopMenuFunc().Where(m => !8.Equals(m.ParentMenu.MenuFuncId)).ToList(); //角色功能權限
            m_AuthService.CreateLoginRole(loginRoleVO2);

            LoginUserVO loginUserVO = new LoginUserVO();
            loginUserVO.UserId = "admin";
            loginUserVO.Password = "1234";
            loginUserVO.FullNameInChinese = "系統管理者";
            loginUserVO.FullNameInEnglish = "Administrator";
            loginUserVO.LoginRoleList = new List<LoginRoleVO>();
            loginUserVO.LoginRoleList.Add(loginRoleVO);
            loginUserVO.CreateDate = DateTime.Now;
            m_AuthService.CreateLoginUser(loginUserVO);

            LoginUserVO loginUserVO2 = new LoginUserVO();
            loginUserVO2.UserId = "test";
            loginUserVO2.Password = "1234";
            loginUserVO2.FullNameInChinese = "行銷人員";
            loginUserVO2.FullNameInEnglish = "Administrator";
            loginUserVO2.LoginRoleList = new List<LoginRoleVO>();
            loginUserVO2.LoginRoleList.Add(loginRoleVO2);
            loginUserVO2.CreateDate = DateTime.Now;
            m_AuthService.CreateLoginUser(loginUserVO2);
        }

        private void InitNode()
        {
            NodeVO rootNodeVO = CreateNode("Root", null, 0);
            NodeVO nodeVO1 = CreateNode("網站", rootNodeVO, 1);
            NodeVO nodeVO111 = CreateNode("手機", rootNodeVO, 2);
            NodeVO nodeVO112 = CreateNode("配件", rootNodeVO, 2);
            NodeVO nodeVO113 = CreateNode("門號折扣", rootNodeVO, 2);
            NodeVO nodeVO114 = CreateNode("首頁廣告", rootNodeVO, 3);
            NodeVO nodeVO115 = CreateNode("網站右方廣告", rootNodeVO, 3);
            
            
            NodeVO nodeVO14 = CreateNode("門市據點", rootNodeVO, 2);
            CreatePost("台北酒泉", nodeVO14, 1); //參考門市據點
            CreatePost("台北延平", nodeVO14, 2);
            CreatePost("台北淡水", nodeVO14, 3);

            NodeVO nodeVO4 = CreateNode("品牌", rootNodeVO, 2);
            NodeVO nodeVO41 = CreateNode("Apple", nodeVO4, 1, NodeVO.UnitType.Pic, "logo_apple.png");
            NodeVO nodeVO42 = CreateNode("Asus", nodeVO4, 2, NodeVO.UnitType.Pic, "logo_asus.png");
            NodeVO nodeVO43 = CreateNode("HTC", nodeVO4, 3, NodeVO.UnitType.Pic, "logo_htc.png");
            NodeVO nodeVO44 = CreateNode("LG", nodeVO4, 4, NodeVO.UnitType.Pic, "logo_lg.png");
            NodeVO nodeVO45 = CreateNode("Samsung", nodeVO4, 5, NodeVO.UnitType.Pic, "logo_samsung.png");
            NodeVO nodeVO46 = CreateNode("Sony", nodeVO4, 6, NodeVO.UnitType.Pic, "logo_Sony.png");
            NodeVO nodeVO47 = CreateNode("Infocus", nodeVO4, 7, NodeVO.UnitType.Pic, "logo_infocus.png");
            NodeVO nodeVO48 = CreateNode("Huawei", nodeVO4, 8, NodeVO.UnitType.Pic, "logo_wuawei.png");
            NodeVO nodeVO492 = CreateNode("mi 小米", nodeVO4, 9, NodeVO.UnitType.Pic, "logo_mi.png");
            NodeVO nodeVO491 = CreateNode("Oppo", nodeVO4, 10, NodeVO.UnitType.Pic, "logo_Oppo.png");
            NodeVO nodeVO49 = CreateNode("Sharp", nodeVO4, 11, NodeVO.UnitType.Pic, "logo_sharp.png");

            NodeVO nodeVO5 = CreateNode("電信公司", rootNodeVO, 3);

            NodeVO nodeVO51 = CreateNode("中華電信", nodeVO5, 1, NodeVO.UnitType.Pic, "logo_hinet.png");
            CreatePost("大玩家299(36)", nodeVO113, 1, 1, nodeVO51.Name, "3100", "1600", "2G", "網內15分/網外15分/市話10分");
            CreatePost("大玩家699(36)", nodeVO113, 2, 1, nodeVO51.Name, "7800", "6000", "20G 前6月吃到飽", "網內45分/網外40分/市話15分");
            CreatePost("大4G-359(30)", nodeVO113, 3, 1, nodeVO51.Name, "4200", "3000", "1.5G", "網內前1分免費/網內15分/網外15分/市話10分/熱線一門");
            CreatePost("大4G-469(30)", nodeVO113, 4, 1, nodeVO51.Name, "5800", "4600", "3G 首年吃到飽", "網內前3分免費/網內30分/網外30分/市話10分/熱線一門");
            CreatePost("大4G-579(30)", nodeVO113, 5, 1, nodeVO51.Name, "7700", "6200", "5G 首年吃到飽", "網內前5分免費/網內45分/網外30分/市話20分/熱線二門");
            CreatePost("大4G-799(30)", nodeVO113, 6, 1, nodeVO51.Name, "9800", "8400", "8G 首年吃到飽", "網內前7分免費/網內60分/網外40分/市話35分/熱線三門");
            CreatePost("大4G-999(30)", nodeVO113, 7, 1, nodeVO51.Name, "12900", "10800", "15G 首年吃到飽", "網內前10分免費/網內100分/網外/市話各50分/熱線三門");
            CreatePost("大4G-1199(30)", nodeVO113, 8, 1, nodeVO51.Name, "14700", "12500", "25G 首年吃到飽", "網內免費/網外65分/市話65分");
            CreatePost("大4G-1399(30)", nodeVO113, 9, 1, nodeVO51.Name, "18200", "16000", "吃到飽", "網內免費/網外100分/市話100分");
            CreatePost("大4G-1799(30)", nodeVO113, 10, 1, nodeVO51.Name, "21900", "20000", "吃到飽", "網內免費/網外200分/市話200分");

            NodeVO nodeVO52 = CreateNode("遠傳", nodeVO5, 2, NodeVO.UnitType.Pic, "logo_fet.png");
            CreatePost("398(月租半價)+499(30)", nodeVO113, 1, 0, nodeVO52.Name, "7800", "3600", "吃到飽", "網內前5分免費/網外送43分");
            CreatePost("599月租半價299(30)", nodeVO113, 2, 1, nodeVO52.Name, "4700", "1000", "1G", "網內前5分免費/網外送30分");
            CreatePost("599(30)學生", nodeVO113, 3, 1, nodeVO52.Name, "7900", "5000", "5G 前6月吃到飽", "網內前5分免費/網外送30分");
            CreatePost("799(30)學生", nodeVO113, 4, 1, nodeVO52.Name, "10500", "5000", "7G 首年吃到飽", "網內前10分免費/網外送40分");
            CreatePost("399(30)", nodeVO113, 5, 1, nodeVO52.Name, "5800", "2400", "2G", "網內前3分免費/網外送20分");
            CreatePost("599(30)", nodeVO113, 6, 1, nodeVO52.Name, "8000", "5000", "6G 首年吃到飽", "網內前5分免費/網外送30分");
            CreatePost("799(30)", nodeVO113, 7, 1, nodeVO52.Name, "11000", "6000", "9G 首年吃到飽", "網內前10分免費/網外送40分");
            CreatePost("999(30)", nodeVO113, 8, 1, nodeVO52.Name, "13000", "8000", "16G 首年吃到飽", "網內前10分免費/網外送50分");
            CreatePost("1199(30)", nodeVO113, 9, 1, nodeVO52.Name, "15200", "10000", "26G 首年吃到飽", "網內免費/網外送70分");
            CreatePost("1399(30)", nodeVO113, 10, 1, nodeVO52.Name, "19000", "12000", "吃到飽", "網內免費/網外送100分");

            NodeVO nodeVO53 = CreateNode("台哥大", nodeVO5, 3, NodeVO.UnitType.Pic, "logo_twm.png");
            CreatePost("401月租半價200(30)", nodeVO113, 1, 0, nodeVO53.Name, "3500", "0", "依量計價", "網內200分/網外200元");
            CreatePost("401(月租半價)+489(30)", nodeVO113, 2, 0, nodeVO53.Name, "8500", "3560", "吃到飽", "網內200分/網外200元");
            CreatePost("699(36)", nodeVO113, 3, 0, nodeVO53.Name, "10000", "1800", "吃到飽", "");
            CreatePost("599月租半價299(30)", nodeVO113, 4, 1, nodeVO53.Name, "4500", "1000", "1G", "網內前5分免費/網外送30分");
            CreatePost("399(30)", nodeVO113, 5, 1, nodeVO53.Name, "6000", "1000", "3G 前3月吃到飽", "網內前3分免費/網外送20分");
            CreatePost("599(30)", nodeVO113, 6, 1, nodeVO53.Name, "8000", "4193", "6G 首年吃到飽", "網內前5分免費/網外送30分");
            CreatePost("799(30)", nodeVO113, 7, 1, nodeVO53.Name, "11000", "5593", "9G 首年吃到飽", "網內前10分免費/網外送40分");
            CreatePost("999(30)", nodeVO113, 8, 1, nodeVO53.Name, "12000", "7000", "16G 前15月吃到飽", "網內前10分免費/網外送50分");
            CreatePost("1199(30)", nodeVO113, 9, 1, nodeVO53.Name, "15000", "11990", "26G 前15月吃到飽", "網內免費/網外送70分");
            CreatePost("1399(30)", nodeVO113, 10, 1, nodeVO53.Name, "20000", "13990", "吃到飽", "網內免費/網外送100分");

            NodeVO nodeVO54 = CreateNode("台灣之星", nodeVO5, 4, NodeVO.UnitType.Pic, "logo_tstartel.png");
            CreatePost("288(12)", nodeVO113, 1, 1, nodeVO54.Name, "500", "600", "1G", "網內免費/網外送20分");
            CreatePost("288(30)", nodeVO113, 2, 1, nodeVO54.Name, "3000", "1800", "1G", "網內免費/網外送20分");
            CreatePost("488(30)", nodeVO113, 3, 1, nodeVO54.Name, "5200", "3200", "2G", "網內免費/網外送30分");
            CreatePost("688(30)", nodeVO113, 4, 1, nodeVO54.Name, "9000", "6000", "吃到飽", "網內免費/網外送45分");
            CreatePost("399(30)", nodeVO113, 5, 1, nodeVO54.Name, "4500", "2700", "1G", "網內前5分免費/網外送20分");
            CreatePost("599(12)", nodeVO113, 6, 1, nodeVO54.Name, "1000", "600", "吃到飽", "網內免費/網外送30分");
            CreatePost("599(30)", nodeVO113, 7, 1, nodeVO54.Name, "8000", "6000", "5G", "網內免費/網外送30分");
            CreatePost("599(30)U25", nodeVO113, 8, 1, nodeVO54.Name, "8000", "6000", "吃到飽", "網內免費/網外送30分");
            CreatePost("699(30)U25", nodeVO113, 9, 1, nodeVO54.Name, "8500", "6000", "吃到飽", "網內免費/網外送30分");
            CreatePost("799(30)", nodeVO113, 10, 1, nodeVO54.Name, "11000", "7000", "吃到飽", "網內免費/網外送45分");
            CreatePost("899(30)", nodeVO113, 11, 1, nodeVO54.Name, "12000", "7000", "吃到飽", "網內免費/網外送45分");
            CreatePost("999(30)", nodeVO113, 12, 1, nodeVO54.Name, "15000", "9000", "吃到飽", "網內免費/網外送80分");
            CreatePost("1099(30)", nodeVO113, 13, 1, nodeVO54.Name, "15000", "10000", "吃到飽", "網內免費/網外送80分");
            CreatePost("1199(30)", nodeVO113, 14, 1, nodeVO54.Name, "17000", "14000", "吃到飽", "網內免費/網外送100分");
            CreatePost("1299(30)", nodeVO113, 15, 1, nodeVO54.Name, "17000", "14000", "吃到飽", "網內免費/網外送100分");
            CreatePost("1399(30)", nodeVO113, 16, 1, nodeVO54.Name, "21000", "16000", "吃到飽", "網內免費/網外送120分");
            CreatePost("1499(30)", nodeVO113, 17, 1, nodeVO54.Name, "21000", "16000", "吃到飽", "網內免費/網外送120分");

            NodeVO nodeVO55 = CreateNode("亞太電信", nodeVO5, 5, NodeVO.UnitType.Pic, "logo_aptg.png");
            CreatePost("399(30)", nodeVO113, 1, 1, nodeVO55.Name, "7000", "0", "1.5G", "不分網內外前3分免費");
            CreatePost("699(30)", nodeVO113, 2, 1, nodeVO55.Name, "10000", "0", "3G", "不分網內外前4分免費");
            CreatePost("999(30)", nodeVO113, 3, 1, nodeVO55.Name, "14000", "7400", "5G", "不分網內外前5分免費");
            CreatePost("1299(30)", nodeVO113, 4, 1, nodeVO55.Name, "17500", "12900", "吃到飽", "不分網內外前6分免費");
            CreatePost("498(30)", nodeVO113, 5, 1, nodeVO55.Name, "7500", "0", "5G", "網內免費/網外+市話送40分");
            CreatePost("698(30)", nodeVO113, 6, 1, nodeVO55.Name, "10800", "0", "7G", "網內免費/網外+市話送50分");
            CreatePost("750(30)", nodeVO113, 7, 1, nodeVO55.Name, "9500", "5700", "吃到飽", "網內免費/網外+市話送58分");
            CreatePost("898(30)", nodeVO113, 8, 1, nodeVO55.Name, "13500", "7400", "吃到飽", "網內免費/網外+市話送80分");
            CreatePost("1398(30)", nodeVO113, 9, 1, nodeVO55.Name, "20000", "12900", "吃到飽", "網內免費/網外+市話送120分");
        }

        private PostVO CreatePost(string title, NodeVO nodeVO, int sort)
        {
            PostVO postVO = new PostVO();
            postVO.Node = nodeVO;
            postVO.Title = title;
            postVO.SortNo = sort;
            postVO.Flag = 1;
            postVO.CreatedBy = "admin";
            postVO.UpdatedBy = "admin";
            postVO.CreatedDate = DateTime.Now;
            postVO.UpdatedDate = DateTime.Now;
            postVO.ShowDate = DateTime.Today;

            return m_PostService.CreatePost(postVO);
        }

        private PostVO CreatePost(string title, NodeVO nodeVO, int sort, int type, string warrantySuppliers, string customField1, string customField2, string summary, string content)
        {
            PostVO postVO = new PostVO();
            postVO.Node = nodeVO;
            postVO.Title = title;
            postVO.SortNo = sort;
            postVO.Type = type;
            postVO.WarrantySuppliers = warrantySuppliers;
            postVO.CustomField1 = customField1;
            postVO.CustomField2 = customField2;
            postVO.Summary = summary;
            postVO.HtmlContent = content;
            postVO.Flag = 1;
            postVO.CreatedBy = "admin";
            postVO.UpdatedBy = "admin";
            postVO.CreatedDate = DateTime.Now;
            postVO.UpdatedDate = DateTime.Now;
            postVO.ShowDate = DateTime.Today;

            return m_PostService.CreatePost(postVO);
        }

        private PostVO CreatePost(string title, NodeVO nodeVO, int sort, string linkUrl, string picFileName)
        {
            PostVO postVO = new PostVO();
            postVO.Node = nodeVO;
            postVO.Title = title;
            postVO.SortNo = sort;
            postVO.Flag = 1;
            postVO.CreatedBy = "admin";
            postVO.UpdatedBy = "admin";
            postVO.CreatedDate = DateTime.Now;
            postVO.UpdatedDate = DateTime.Now;
            postVO.ShowDate = DateTime.Today;
            postVO.LinkUrl = linkUrl;
            postVO.PicFileName = picFileName;

            return m_PostService.CreatePost(postVO);
        }

        private NodeVO CreateNode(string name, NodeVO parentNode, int sort)
        {
            NodeVO rootNodeVO = new NodeVO();
            rootNodeVO.Name = name;
            rootNodeVO.ParentNode = parentNode;
            rootNodeVO.SortNo = sort;
            rootNodeVO.Flag = 1;
            rootNodeVO.CreatedBy = "admin";
            rootNodeVO.UpdatedBy = "admin";
            rootNodeVO.CreatedDate = DateTime.Now;
            rootNodeVO.UpdatedDate = DateTime.Now;

            return m_PostService.CreateNode(rootNodeVO);
        }

        private NodeVO CreateNode(string name, NodeVO parentNode, int sort, NodeVO.UnitType unitType, string filePath)
        {
            NodeVO rootNodeVO = new NodeVO();
            rootNodeVO.Name = name;
            rootNodeVO.ParentNode = parentNode;
            rootNodeVO.SortNo = sort;
            rootNodeVO.Flag = 1;
            rootNodeVO.CreatedBy = "admin";
            rootNodeVO.UpdatedBy = "admin";
            rootNodeVO.CreatedDate = DateTime.Now;
            rootNodeVO.UpdatedDate = DateTime.Now;
            rootNodeVO.UType = unitType;
            rootNodeVO.PicFileName = filePath;

            return m_PostService.CreateNode(rootNodeVO);
        }

        private MenuFuncVO CreateParentMenu(string menuName, int sort)
        {
            MenuFuncVO menuFuncVO = new MenuFuncVO(menuName, null);
            menuFuncVO.ListOrder = sort;

            return m_AuthService.CreateMenuFunc(menuFuncVO);
        }

        private MenuFuncVO CreateSubMenu(string menuName, MenuFuncVO parentMenu, int sort, string path)
        {
            MenuFuncVO menuFuncVO = new MenuFuncVO(menuName, parentMenu);
            menuFuncVO.ListOrder = sort;
            menuFuncVO.MainPath = path;

            return m_AuthService.CreateMenuFunc(menuFuncVO);
        }

        private void InitSystemParam()
        {
            //系統設定
            SystemParamVO systemParamVO = new SystemParamVO();
            //systemParamVO.MailSmtp = "smtp.gmail.com";
            //systemParamVO.Account = "test@pro2e.com.tw";
            //systemParamVO.SendEmail = "test@pro2e.com.tw";
            //systemParamVO.MailPort = "587";
            //systemParamVO.EnableSSL = true;
            //systemParamVO.Password = "28005786";

            //systemParamVO.MailSmtp = "60.248.85.123";
            //systemParamVO.Account = "SmtpUser";
            //systemParamVO.SendEmail = "test@test.com";
            //systemParamVO.MailPort = "25";
            //systemParamVO.EnableSSL = false;
            //systemParamVO.Password = "abc+1234";

            //systemParamVO.PageTitle = "彼得杜拉克社會企業";
            //systemParamVO.PageKeyWord = "彼得杜拉克社會企業";
            //systemParamVO.PageDescription = "彼得杜拉克社會企業";            

            systemParamVO.FilePassword = "";
            m_SystemService.CreateSystemParam(systemParamVO);
        }
    }
}
