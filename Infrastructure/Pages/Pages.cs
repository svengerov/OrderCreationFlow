using Infra.Pages.OrderFlowPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Pages
{
    public class Pages
    {
        private static LoginPage _loginPage;
        public static LoginPage loginPage => _loginPage ?? (_loginPage = new LoginPage());
        //public static   AdminPage getAdminPage()
        //{
        //    if( _adminPage == null )
        //    {
        //        _adminPage = new AdminPage();
        //    }
        //    return _adminPage;
        //}

        private static PaymentsManagementPage _payMgtPage;
        public static PaymentsManagementPage payMgtPage => _payMgtPage ?? (_payMgtPage = new PaymentsManagementPage());

        private static MerchantSetPage _merchantSetPage;
    
       
        



        //3 different pages depends on the name of merchant

        private static AnnaScholzSite _AnnaSitePage;
        public static AnnaScholzSite AnnaSitePage => _AnnaSitePage ?? (_AnnaSitePage = new AnnaScholzSite());
       
        public static MerchantSetPage merchantSetPage => _merchantSetPage ?? (_merchantSetPage = new MerchantSetPage());

        

    }

    
}
