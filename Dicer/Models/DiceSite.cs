using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dicer
{
    public abstract class DiceSite : IAutomable
    {

        #region Fields
        protected const string ReferName = "adolfowp95";
        protected bool isAuthenticated = false;
        protected WebProxy Prox;
        string currency = "Btc";
        public string[] Currencies = { "btc" };

        public decimal maxRoll { get; set; }
        public bool AutoWithdraw { get; set; }
        public bool AutoInvest { get; set; }
        public bool ChangeSeed { get; set; }
        public bool AutoLogin { get; set; }
        public decimal edge = 1;
        public string Name { get; protected set; }
        public decimal chance = 0;
        public decimal amount = 0;
        public decimal balance { get; protected set; }
        public int bets = 0;
        public decimal profit = 0;
        public decimal wagered = 0;
        public int wins = 0;
        public int losses = 0;
        public decimal siteprofit = 0;
        public bool High = false;
        public bool Tip { get; set; }
        public bool TipUsingName { get; set; }
        public bool GettingSeed { get; set; }
        public bool ForceUpdateStats = false;
        public bool AutoUpdate = true;
        #endregion

        #region Properties
        public string SiteTitle { get; set; }
        public string SiteIcon { get; set; }

        string _completeSiteUrl = string.Empty;
        public string SiteUrl
        {
            get { return _completeSiteUrl; }
            set { _completeSiteUrl = value; }
        }

        string _completeBetUrl = string.Empty;
        public string BetUrl
        {
            get { return _completeBetUrl; }
            set { _completeBetUrl = value; }
        }

		private bool _NonceBased = true;
		public bool NonceBased
		{
			get { return _NonceBased; }
			set { _NonceBased = value; }
		}

        public string Currency
        {
            get { return currency; }
            set { currency = value; CurrencyChanged(); }
        }
        #endregion

        #region Methods
        #region Protected
        protected void finishedlogin(bool Success)
        {
            if (FinishedLogin != null)
                FinishedLogin(Success);
        }

        protected virtual void CurrencyChanged() { }

		protected void FinishedBet(Bet newBet)
		{
			MessagingCenter.Send<DiceSite, decimal>(this, "updateBalance", balance);
			MessagingCenter.Send<DiceSite, long>(this, "updateBets", bets);
			MessagingCenter.Send<DiceSite, decimal>(this, "updateWagered", wagered);
			MessagingCenter.Send<DiceSite, decimal>(this, "updateProfit", profit);
			MessagingCenter.Send<DiceSite, long>(this, "updateWins", wins);
			MessagingCenter.Send<DiceSite, long>(this, "updateLosses", losses);
			MessagingCenter.Send<DiceSite, long>(this, "updateWins", wins);
			MessagingCenter.Send<DiceSite, long>(this, "updateLosses", losses);

            MessagingCenter.Send<DiceSite, Bet>(this, "addBet", newBet);

			//Parent.AddBet(newBet);
			//Parent.GetBetResult(balance, newBet);

		}
		protected abstract Task<bool> internalPlaceBet(bool High, decimal amount, decimal chance);
        #endregion

        #region Publics
        public abstract Task<bool> Login(string User, string Password, string twofa);
        public async Task<bool> PlaceBet(bool High, decimal amount, decimal chance)
		{
            var param = string.Format("Betting: {0:0.00000000} at {1:0.00000000} {2}", amount, chance, High ? "High" : "Low");
            MessagingCenter.Send<DiceSite, string>(this, "updateStatus", param);
			return await internalPlaceBet(High, amount, chance);
		}
		public abstract void ResetSeed();
		public abstract void SetClientSeed(string Seed);

		public virtual decimal GetLucky(string server, string client, int nonce)
		{
			HMACSHA512 betgenerator = new HMACSHA512();

			int charstouse = 5;
			List<byte> serverb = new List<byte>();

			for (int i = 0; i < server.Length; i++)
			{
				serverb.Add(Convert.ToByte(server[i]));
			}

			betgenerator.Key = serverb.ToArray();

			List<byte> buffer = new List<byte>();
			string msg = /*nonce.ToString() + ":" + */client + ":" + nonce.ToString();
			foreach (char c in msg)
			{
				buffer.Add(Convert.ToByte(c));
			}

			byte[] hash = betgenerator.ComputeHash(buffer.ToArray());

			StringBuilder hex = new StringBuilder(hash.Length * 2);
			foreach (byte b in hash)
				hex.AppendFormat("{0:x2}", b);


			for (int i = 0; i < hex.Length; i += charstouse)
			{

				string s = hex.ToString().Substring(i, charstouse);

				decimal lucky = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
				if (lucky < 1000000)
					return lucky / 10000;
			}
			return 0;
		}

		public static decimal sGetLucky(string server, string client, int nonce)
		{
			HMACSHA512 betgenerator = new HMACSHA512();

			int charstouse = 5;
			List<byte> serverb = new List<byte>();

			for (int i = 0; i < server.Length; i++)
			{
				serverb.Add(Convert.ToByte(server[i]));
			}

			betgenerator.Key = serverb.ToArray();

			List<byte> buffer = new List<byte>();
			string msg = /*nonce.ToString() + ":" + */client + ":" + nonce.ToString();
			foreach (char c in msg)
			{
				buffer.Add(Convert.ToByte(c));
			}

			byte[] hash = betgenerator.ComputeHash(buffer.ToArray());

			StringBuilder hex = new StringBuilder(hash.Length * 2);
			foreach (byte b in hash)
				hex.AppendFormat("{0:x2}", b);


			for (int i = 0; i < hex.Length; i += charstouse)
			{

				string s = hex.ToString().Substring(i, charstouse);

				decimal lucky = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
				if (lucky < 1000000)
					return lucky / 10000;
			}
			return 0;
		}

        public virtual Task<int> AutomateAsync(IAutomationRunner Runner)
        {
            return Runner.Run(this);
        }
        #endregion
        #endregion

        #region Events
        public delegate void dFinishedLogin(bool LoggedIn);
		public event dFinishedLogin FinishedLogin;

        #endregion

    }

	public class PlaceBetObj
	{
		public PlaceBetObj(bool High, decimal Amount, decimal Chance)
		{
			this.High = High;
			this.Amount = Amount;
			this.Chance = Chance;
		}
		public bool High { get; set; }
		public decimal Amount { get; set; }
		public decimal Chance { get; set; }
	}
}
