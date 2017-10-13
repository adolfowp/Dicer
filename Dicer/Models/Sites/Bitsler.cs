using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Dicer
{
    public sealed class Bitsler : DiceSite
    {
        #region Fields
        HttpClientHandler ClientHandlr;
        HttpClient Client;

		bool IsBitsler = false;
		string accesstoken = "";
		DateTime LastSeedReset = new DateTime();
        DateTime lastupdate = new DateTime();

        string username = "";
        #endregion

        public Bitsler()
        {
            SiteTitle = "Bitsler";
            SiteIcon = "bitsler.png";
            SiteUrl = "https://www.bitsler.com/?ref=" + ReferName;
        }

		void GetBalanceThread()
		{
			while (IsBitsler)
			{
				if ((DateTime.Now - lastupdate).TotalSeconds > 60 || ForceUpdateStats)
				{
					lastupdate = DateTime.Now;
					try
					{
						List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
						pairs.Add(new KeyValuePair<string, string>("access_token", accesstoken));
						FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);
						string sEmitResponse = Client.PostAsync("getuserstats", Content).Result.Content.ReadAsStringAsync().Result;
                        bsStatsBase bsstatsbase = JsonConvert.DeserializeObject<bsStatsBase>(sEmitResponse.Replace("\"return\":", "\"_return\":"));
						if (bsstatsbase != null)
							if (bsstatsbase._return != null)
								if (bsstatsbase._return.success == "true")
								{
									switch (Currency.ToLower())
									{
										case "btc":
											balance = bsstatsbase._return.btc_balance;
											profit = bsstatsbase._return.btc_profit;
											wagered = bsstatsbase._return.btc_wagered; break;
										case "ltc":
											balance = bsstatsbase._return.ltc_balance;
											profit = bsstatsbase._return.ltc_profit;
											wagered = bsstatsbase._return.ltc_wagered; break;
										case "doge":
											balance = bsstatsbase._return.doge_balance;
											profit = bsstatsbase._return.doge_profit;
											wagered = bsstatsbase._return.doge_wagered; break;
										case "eth":
											balance = bsstatsbase._return.eth_balance;
											profit = bsstatsbase._return.eth_profit;
											wagered = bsstatsbase._return.eth_wagered; break;
										case "burst":
											balance = bsstatsbase._return.burst_balance;
											profit = bsstatsbase._return.burst_profit;
											wagered = bsstatsbase._return.burst_wagered; break;
									}
									bets = int.Parse(bsstatsbase._return.bets);
									wins = int.Parse(bsstatsbase._return.wins);
									losses = int.Parse(bsstatsbase._return.losses);

                                    MessagingCenter.Send(this, "updateBalance", balance);
                                    MessagingCenter.Send(this, "updateBets", bets);
                                    MessagingCenter.Send(this, "updateLosses", losses);
                                    MessagingCenter.Send(this, "updateProfit", profit);
                                    MessagingCenter.Send(this, "updateWagered", wagered);
                                    MessagingCenter.Send(this, "updateWins", wins);

								}
								else
								{
									if (bsstatsbase._return.value != null)
									{

                                        MessagingCenter.Send(this, "updateStatus", bsstatsbase._return.value);

									}
								}
					}
					catch { }
				}
				Thread.Sleep(1000);
			}
		}

        protected override void CurrencyChanged()
        {
            base.CurrencyChanged();
            lastupdate = DateTime.Now;
            if (accesstoken != "" && IsBitsler)
            {
                try
                {
                    List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
                    pairs.Add(new KeyValuePair<string, string>("access_token", accesstoken));
                    FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);
                    string sEmitResponse = Client.PostAsync("getuserstats", Content).Result.Content.ReadAsStringAsync().Result;
                    bsStatsBase bsstatsbase = json.JsonDeserialize<bsStatsBase>(sEmitResponse.Replace("\"return\":", "\"_return\":"));
                    if (bsstatsbase != null)
                        if (bsstatsbase._return != null)
                            if (bsstatsbase._return.success == "true")
                            {
                                switch (Currency.ToLower())
                                {
                                    case "btc":
                                        balance = bsstatsbase._return.btc_balance;
                                        profit = bsstatsbase._return.btc_profit;
                                        wagered = bsstatsbase._return.btc_wagered; break;
                                    case "ltc":
                                        balance = bsstatsbase._return.ltc_balance;
                                        profit = bsstatsbase._return.ltc_profit;
                                        wagered = bsstatsbase._return.ltc_wagered; break;
                                    case "doge":
                                        balance = bsstatsbase._return.doge_balance;
                                        profit = bsstatsbase._return.doge_profit;
                                        wagered = bsstatsbase._return.doge_wagered; break;
                                    case "eth":
                                        balance = bsstatsbase._return.eth_balance;
                                        profit = bsstatsbase._return.eth_profit;
                                        wagered = bsstatsbase._return.eth_wagered; break;
                                    case "burst":
                                        balance = bsstatsbase._return.burst_balance;
                                        profit = bsstatsbase._return.burst_profit;
                                        wagered = bsstatsbase._return.burst_wagered; break;
                                }
                                bets = int.Parse(bsstatsbase._return.bets);
                                wins = int.Parse(bsstatsbase._return.wins);
                                losses = int.Parse(bsstatsbase._return.losses);

                                MessagingCenter.Send(this, "updateBalance", balance);
                                MessagingCenter.Send(this, "updateBets", bets);
                                MessagingCenter.Send(this, "updateLosses", losses);
                                MessagingCenter.Send(this, "updateProfit", profit);
                                MessagingCenter.Send(this, "updateWagered", wagered);
                                MessagingCenter.Send(this, "updateWins", wins);
                            }
                            else
                            {
                                if (bsstatsbase._return.value != null)
                                {
                                    MessagingCenter.Send(this, "updateStatus", bsstatsbase._return.value);
                                }
                            }
                }
                catch { }
            }
        }

        public override async Task<bool> Login(string User, string Password, string twofa)
        {
            bool result = false;

            ClientHandlr = new HttpClientHandler { 
                UseCookies = true, 
                AutomaticDecompression = DecompressionMethods.Deflate | 
                                         DecompressionMethods.GZip, 
                Proxy = this.Prox, UseProxy = Prox != null };

            Client = new HttpClient(ClientHandlr) 
            { BaseAddress = new Uri("https://www.bitsler.com/api/") };

			Client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
			Client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));

            try
            {
				List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
				pairs.Add(new KeyValuePair<string, string>("username", User));
				pairs.Add(new KeyValuePair<string, string>("password", Password));
				pairs.Add(new KeyValuePair<string, string>("api_key", "0b2edbfe44e98df79665e52896c22987445683e78"));
				//if (!string.IsNullOrWhiteSpace(twofa))
				{
					pairs.Add(new KeyValuePair<string, string>("twofactor", twofa));
				}

				FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);
                string sEmitResponse = await (await Client.PostAsync("login", Content)).Content.ReadAsStringAsync();

				//getuserstats 
				bsloginbase bsbase = JsonConvert.DeserializeObject<bsloginbase>(sEmitResponse.Replace("\"return\":", "\"_return\":"));

                if (bsbase != null)
                {
                    if (bsbase._return != null)
                    {
                        if (bsbase._return.success == "true")
                        {
                            accesstoken = bsbase._return.access_token;
                            IsBitsler = true;
                            lastupdate = DateTime.Now;

                            pairs = new List<KeyValuePair<string, string>>();
                            pairs.Add(new KeyValuePair<string, string>("access_token", accesstoken));
                            Content = new FormUrlEncodedContent(pairs);
                            sEmitResponse = await (await Client.PostAsync("getuserstats", Content)).Content.ReadAsStringAsync();
                            bsStatsBase bsstatsbase = JsonConvert.DeserializeObject<bsStatsBase>(sEmitResponse.Replace("\"return\":", "\"_return\":"));
                            if (bsstatsbase != null)
                                if (bsstatsbase._return != null)
                                    if (bsstatsbase._return.success == "true")
                                    {
                                        switch (Currency.ToLower())
                                        {
                                            case "btc":
                                                balance = bsstatsbase._return.btc_balance;
                                                profit = bsstatsbase._return.btc_profit;
                                                wagered = bsstatsbase._return.btc_wagered; break;
                                            case "ltc":
                                                balance = bsstatsbase._return.ltc_balance;
                                                profit = bsstatsbase._return.ltc_profit;
                                                wagered = bsstatsbase._return.ltc_wagered; break;
                                            case "doge":
                                                balance = bsstatsbase._return.doge_balance;
                                                profit = bsstatsbase._return.doge_profit;
                                                wagered = bsstatsbase._return.doge_wagered; break;
                                            case "eth":
                                                balance = bsstatsbase._return.eth_balance;
                                                profit = bsstatsbase._return.eth_profit;
                                                wagered = bsstatsbase._return.eth_wagered; break;
                                            case "burst":
                                                balance = bsstatsbase._return.burst_balance;
                                                profit = bsstatsbase._return.burst_profit;
                                                wagered = bsstatsbase._return.burst_wagered; break;
                                        }
                                        bets = int.Parse(bsstatsbase._return.bets);
                                        wins = int.Parse(bsstatsbase._return.wins);
                                        losses = int.Parse(bsstatsbase._return.losses);

                                        MessagingCenter.Send(this, "updateBalance", balance);
                                        MessagingCenter.Send(this, "updateBets", bets);
                                        MessagingCenter.Send(this, "updateLosses", losses);
                                        MessagingCenter.Send(this, "updateProfit", profit);
                                        MessagingCenter.Send(this, "updateWagered", wagered);
                                        MessagingCenter.Send(this, "updateWins", wins);

                                        this.username = User;
                                        result = true;
                                    }
                                    else
                                    {
                                        if (bsstatsbase._return.value != null)
                                        {
                                            MessagingCenter.Send(this, "updateStatus", bsstatsbase._return.value);
                                        }
                                    }


                            IsBitsler = true;
                            var t = new Thread(GetBalanceThread);
                            t.Start();
                            finishedlogin(true);
                            return result;
                        }
                        else
                        {
                            if (bsbase._return.value != null)
                                MessagingCenter.Send(this, "updateStatus", bsbase._return.value);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result = false; 
            }

            return result;
        }

        protected override Task internalPlaceBet(bool High, decimal amount, decimal chance)
        {
            throw new NotImplementedException();
        }

        public override void ResetSeed()
        {
            throw new NotImplementedException();
        }

        public override void SetClientSeed(string Seed)
        {
            throw new NotImplementedException();
        }
    }

	public class bsLogin
	{
		public string success { get; set; }
		public string value { get; set; }
		public string access_token { get; set; }
	}
	public class bsloginbase
	{
		public bsLogin _return { get; set; }
	}
	//"{\"return\":{\"success\":\"true\",\"balance\":1.0e-5,\"wagered\":0,\"profit\":0,\"bets\":\"0\",\"wins\":\"0\",\"losses\":\"0\"}}"
	public class bsStats
	{
		public string success { get; set; }
		public string value { get; set; }
		public decimal btc_balance { get; set; }
		public decimal btc_wagered { get; set; }
		public decimal btc_profit { get; set; }
		public string bets { get; set; }
		public decimal ltc_balance { get; set; }
		public decimal ltc_wagered { get; set; }
		public decimal ltc_profit { get; set; }

		public decimal doge_balance { get; set; }
		public decimal doge_wagered { get; set; }
		public decimal doge_profit { get; set; }
		public decimal eth_balance { get; set; }
		public decimal eth_wagered { get; set; }
		public decimal eth_profit { get; set; }
		public decimal burst_balance { get; set; }
		public decimal burst_wagered { get; set; }
		public decimal burst_profit { get; set; }
		public string wins { get; set; }
		public string losses { get; set; }
	}
	public class bsStatsBase
	{
		public bsStats _return { get; set; }
	}
	public class bsBetBase
	{
		public bsBet _return { get; set; }
	}
	public class bsBet
	{
		public string success { get; set; }
		public string value { get; set; }
		public string username { get; set; }
		public string id { get; set; }
		public string type { get; set; }
		public string devise { get; set; }
		public long ts { get; set; }
		public string time { get; set; }
		public string amount { get; set; }
		public decimal roll_number { get; set; }
		public string condition { get; set; }
		public string game { get; set; }
		public decimal payout { get; set; }
		public string winning_chance { get; set; }
		public string amount_return { get; set; }
		public string new_balance { get; set; }
		public string _event { get; set; }
		public string server_seed { get; set; }
		public string client_seed { get; set; }
		public long nonce { get; set; }

		public Bet ToBet()
		{
			Bet tmp = new Bet
			{
				Amount = decimal.Parse(amount, System.Globalization.NumberFormatInfo.InvariantInfo),
				date = json.ToDateTime2(ts.ToString()),
				Id = id,
				Profit = decimal.Parse(amount_return, System.Globalization.NumberFormatInfo.InvariantInfo),
				Roll = (decimal)roll_number,
				high = condition == ">",
				Chance = decimal.Parse(winning_chance, System.Globalization.NumberFormatInfo.InvariantInfo),
				nonce = nonce,
				serverhash = server_seed,
				clientseed = client_seed
			};
			return tmp;
		}
	}
	public class bsResetSeedBase
	{
		public bsResetSeed _return { get; set; }
	}
	public class bsResetSeed
	{
		public string seed_server_hashed { get; set; }
		public string seed_server { get; set; }
		public string seed_client { get; set; }
		public string nonce { get; set; }
		public string seed_server_revealed { get; set; }
		public bsResetSeed last_seeds_revealed { get; set; }
	}
}
