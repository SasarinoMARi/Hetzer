using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Tweetinvi;
using Tweetinvi.Models;

namespace Hetzer
{
    class Login
    {
        private static Login instance;
        public static Login Instance
        {
            get
            {
                if (instance == null)
                    instance = new Login();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        const string ct = "grAbONJVZSiXQGcmrM0dnUjRx";
        const string cs = "25MOLg2wdVDZdd12s01ufaQdusYZj9thdGC8JDpRuGMx31iKQP";
        static string savedir, path;
        private IAuthenticationContext authenticationContext;

        Login()
        {
            initUri();
        }

        private void initUri()
        {
            savedir = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Assembly.GetEntryAssembly().GetName().Name
            );
            path = System.IO.Path.Combine(savedir, "key");
            if (!Directory.Exists(savedir)) Directory.CreateDirectory(savedir);
        }

        public string startAuthFlow()
        {
            var appCredentials = new TwitterCredentials(ct, cs);
            authenticationContext = AuthFlow.InitAuthentication(appCredentials);
            return (authenticationContext.AuthorizationURL);

        }

        public ITwitterCredentials confirmAuthFlow(string pinCode)
        {
            var userCredentials = AuthFlow.CreateCredentialsFromVerifierCode(pinCode, authenticationContext);
            File.WriteAllText(path, new JavaScriptSerializer().Serialize(HetzerCredential.fromTwitterCredentials(userCredentials)));
            return (userCredentials);
        }

        public ITwitterCredentials getCredentials()
        {
            HetzerCredential credentials = null;
            try
            {
                var json = File.ReadAllText(path);
                credentials = new JavaScriptSerializer().Deserialize<HetzerCredential>(json);
            }
            catch
            {

            }
            return (credentials != null) ? credentials.toTwitterCredentials() : null;
        }


        public class HetzerCredential
        {
            public HetzerCredential() { }

            public string token { get; set; }
            public string secret { get; set; }

            public static HetzerCredential fromTwitterCredentials(ITwitterCredentials c)
            {
                var o = new HetzerCredential();
                o.token = c.AccessToken;
                o.secret = c.AccessTokenSecret;
                return o;
            }

            public ITwitterCredentials toTwitterCredentials()
            {
                var c = new TwitterCredentials();
                c.ConsumerKey = ct;
                c.ConsumerSecret = cs;
                c.AccessToken = token;
                c.AccessTokenSecret = secret;
                return c;
            }
        }
    }


}
