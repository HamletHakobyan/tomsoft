using System;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace SOFlairNotifier.Model
{
    public class SOFlair
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public Uri ProfileUrl { get; set; }
        public int Reputation { get; set; }
        public Uri GravatarUrl { get; set; }
        public int GoldBadges { get; set; }
        public int SilverBadges { get; set; }
        public int BronzeBadges { get; set; }


        private static readonly string _flairUrlTemplate = "http://stackoverflow.com/users/flair/{0}.json";

        public static SOFlair GetFlair(int userId)
        {
            string actualUrl = string.Format(_flairUrlTemplate, userId);
            var serializer = new DataContractJsonSerializer(typeof(RawFlairData));
            var request = WebRequest.Create(actualUrl);
            using (var response = request.GetResponse())
            {
                var data = (RawFlairData)serializer.ReadObject(response.GetResponseStream());
                return ParseFlair(data);
            }
        }

        private static SOFlair ParseFlair(RawFlairData data)
        {
            int reputation = int.Parse(data.Reputation, NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
            string gravatarUrl = ParseGravatarUrl(data.GravatarHtml);
            int gold, silver, bronze;
            ParseBadges(data.BadgeHtml, out gold, out silver, out bronze);
            return new SOFlair
            {
                Id = data.Id,
                DisplayName = WebUtility.HtmlDecode(data.DisplayName),
                ProfileUrl = new Uri(WebUtility.HtmlDecode(data.ProfileUrl)),
                Reputation = reputation,
                GravatarUrl = string.IsNullOrEmpty(gravatarUrl) ? null : new Uri(gravatarUrl),
                GoldBadges = gold,
                SilverBadges = silver,
                BronzeBadges = bronze
            };
        }

        private readonly static Regex _gravatarUrlRegex = new Regex(@"src=""(?<url>[^""]+)""", RegexOptions.Compiled);
        private readonly static Regex _badgesRegex = new Regex(@"<span class=""badgecount"">(?<badgecount>[0-9]+)</span>", RegexOptions.Compiled);

        private static void ParseBadges(string badgeHtml, out int gold, out int silver, out int bronze)
        {
            gold = 0;
            silver = 0;
            bronze = 0;

            var matches = _badgesRegex.Matches(badgeHtml);
            if (matches.Count == 3)
            {
                gold = int.Parse(matches[0].Groups["badgecount"].Value);
                silver = int.Parse(matches[1].Groups["badgecount"].Value);
                bronze = int.Parse(matches[2].Groups["badgecount"].Value);
            }
        }

        private static string ParseGravatarUrl(string gravatarHtml)
        {
            var m = _gravatarUrlRegex.Match(gravatarHtml);
            if (m.Success)
            {
                string rawUrl = m.Groups["url"].Value;
                return WebUtility.HtmlDecode(rawUrl);
            }
            return null;
        }

        [DataContract]
        private class RawFlairData
        {
            [DataMember(Name = "id")]
            public int Id { get; set; }

            [DataMember(Name = "displayName")]
            public string DisplayName { get; set; }

            [DataMember(Name = "profileUrl")]
            public string ProfileUrl { get; set; }

            [DataMember(Name = "reputation")]
            public string Reputation { get; set; }

            [DataMember(Name = "gravatarHtml")]
            public string GravatarHtml { get; set; }

            [DataMember(Name = "badgeHtml")]
            public string BadgeHtml { get; set; }
        }
    }
}
