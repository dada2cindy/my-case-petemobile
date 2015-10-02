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
        public void Test_CreateTarget()
        {
            IList<TargetVO> targetList = new List<TargetVO>();

            double targetAmount = 20000;

            IList<LoginUserVO> userList = m_AuthService.GetAllLoginUserList();
            foreach (LoginUserVO user in userList)
            {
                TargetVO targetVO = new TargetVO();
                targetVO.Id = string.Format("{0}{1}", "201507", user.FullNameInChinese);
                targetVO.Name = user.FullNameInChinese;
                targetVO.Amount = targetAmount;
                targetAmount += 5000;
                targetList.Add(targetVO);
            }

            m_AccountingService.UpdateTargetList(targetList);
        }

        private void InitMenu()
        {
            MenuFuncVO parentMenu51 = CreateParentMenu("客戶/庫存管理", 1000);
            CreateSubMenu("客戶列表", parentMenu51, 1001, "admin/UC05/0511.aspx");
            CreateSubMenu("庫存列表", parentMenu51, 1002, "admin/UC05/0512.aspx");
            CreateSubMenu("庫存類別", parentMenu51, 1003, "admin/UC04/0411.aspx");
            CreateSubMenu("庫存品名", parentMenu51, 1004, "admin/UC04/0421.aspx");
            CreateSubMenu("業績報表", parentMenu51, 1005, "admin/UC07/0711.aspx");

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

            //MenuFuncVO parentMenu7 = CreateParentMenu("網站設定與廣告管理", 8000);
            //CreateSubMenu("首頁廣告", parentMenu7, 8001, "admin/UC07/0701.aspx");
            //CreateSubMenu("網站左方廣告", parentMenu7, 8002, "admin/UC07/0702.aspx");
            //CreateSubMenu("首頁Facebook設定", parentMenu7, 8003, "admin/UC07/0711.aspx");

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

            LoginRoleVO loginRoleVO2 = new LoginRoleVO("店員");
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
            loginUserVO2.FullNameInChinese = "店員";
            loginUserVO2.FullNameInEnglish = "Administrator";
            loginUserVO2.LoginRoleList = new List<LoginRoleVO>();
            loginUserVO2.LoginRoleList.Add(loginRoleVO2);
            loginUserVO2.CreateDate = DateTime.Now;
            m_AuthService.CreateLoginUser(loginUserVO2);
        }

        private void InitNode()
        {
            NodeVO rootNodeVO = CreateNode("Root", null, 0);
            NodeVO nodeVO1 = CreateNode("庫存", rootNodeVO, 1);
            CreatePost("庫存1", nodeVO1, 1);

            NodeVO nodeVO4 = CreateNode("庫存類別", rootNodeVO, 2);
            NodeVO nodeVO41 = CreateNode("手機殼", nodeVO4, 1);
            NodeVO nodeVO42 = CreateNode("保護貼", nodeVO4, 2);

            NodeVO nodeVO5 = CreateNode("庫存品名", rootNodeVO, 3);
            NodeVO nodeVO51 = CreateNode("I6 玻璃包護貼-普通", nodeVO5, 1);
            NodeVO nodeVO52 = CreateNode("I6 玻璃包護貼-高級", nodeVO5, 2);

            NodeVO nodeVO6 = CreateNode("店家", rootNodeVO, 4);
            NodeVO nodeVO61 = CreateNode("台北承德", nodeVO6, 1);
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
